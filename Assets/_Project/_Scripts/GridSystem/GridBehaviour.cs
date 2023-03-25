using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.GridCell;
using SnekTech.MineSweeperRogue.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using SnekTech.UI;
using UnityEngine;
using Grid = SnekTech.MineSweeperRogue.GridSystem.Grid;

namespace SnekTech.GridSystem
{
    public class GridBehaviour : MonoBehaviour, IHumbleGrid
    {
        [SerializeField]
        private CellBehavior cellBehaviorPrefab;

        [SerializeField]
        private InputEventChannel inputEventChannel;

        [SerializeField]
        private GridEventChannel gridEventChannel;

        [SerializeField]
        private Player player;

        [SerializeField]
        private UIStateManager uiStateManager;

        [SerializeField]
        private Camera mainCamera;

        private int _cellLayer;
        private Grid _grid;

        public IGrid Grid => _grid;
        public GridEventChannel EventChannel => gridEventChannel;

        private void Awake()
        {
            _cellLayer = 1 << LayerMask.NameToLayer("Cell");
            _grid = new Grid(this);
        }

        private void OnEnable()
        {
            inputEventChannel.PrimaryPerformed += HandlePrimary;
            inputEventChannel.DoublePrimaryPerformed += HandleDoublePrimary;
            inputEventChannel.SecondaryPerformed += HandleSecondary;
        }

        private void OnDisable()
        {
            inputEventChannel.PrimaryPerformed -= HandlePrimary;
            inputEventChannel.DoublePrimaryPerformed -= HandleDoublePrimary;
            inputEventChannel.SecondaryPerformed -= HandleSecondary;
        }

        #region input event handlers

        private void HandlePrimary(Vector2 mousePosition) => ProcessPrimaryAsync(mousePosition).Forget();

        private void HandleDoublePrimary(Vector2 mousePosition) =>
            ProcessDoublePrimaryAsync(mousePosition).Forget();

        private void HandleSecondary(Vector2 mousePosition) => ProcessSecondaryAsync(mousePosition).Forget();

        #endregion

        private async UniTaskVoid ProcessPrimaryAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            var canClickCell = cell is {IsCovered: true};
            if (!canClickCell)
                return;

            player.UseClickAbilities();

            await player.Weapon.Primary(cell);

            HandleRecursiveRevealCellComplete(cell);
        }

        private async UniTaskVoid ProcessDoublePrimaryAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            if (cell == null)
                return;

            await Grid.RevealAroundAsync(cell.Index);

            HandleRecursiveRevealCellComplete(cell);
        }

        private async UniTaskVoid ProcessSecondaryAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            if (cell == null)
                return;

            await player.Weapon.Secondary(cell);
        }

        private ICell GetMouseHoveringCell(Vector2 mousePosition)
        {
            if (uiStateManager.isBlockingRaycast)
            {
                return null;
            }

            var ray = mainCamera.ScreenPointToRay(mousePosition);
            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _cellLayer);

            return hit.collider != null ? hit.collider.GetComponent<IHumbleCell>().Cell : null;
        }

        private void HandleRecursiveRevealCellComplete(ICell originalCell)
        {
            gridEventChannel.InvokeOnRecursiveRevealComplete(originalCell);
            CheckIfAllCleared();
        }

        private void CheckIfAllCleared()
        {
            if (Grid.IsCleared)
            {
                gridEventChannel.InvokeOnGridCleared(_grid);
            }
        }

        public List<IHumbleCell> InstantiateHumbleCells(int count)
        {
            transform.DestroyAllChildren();

            var humbleCells = new List<IHumbleCell>();
            for (var i = 0; i < count; i++)
            {
                var cell = Instantiate(cellBehaviorPrefab, transform);
                humbleCells.Add(cell);
            }

            return humbleCells;
        }

        public UniTask OnInitComplete()
        {
            var localX = -Grid.Size.ColumnCount / 2f;
            var localY = -Grid.Size.RowCount / 2f;
            transform.localPosition = new Vector3(localX, localY, 0);
            gridEventChannel.InvokeOnGridInitComplete(Grid);
            return UniTask.CompletedTask;
        }
    }
}