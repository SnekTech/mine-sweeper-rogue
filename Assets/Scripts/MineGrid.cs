using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    public class MineGrid : MonoBehaviour
    {
        [SerializeField]
        private Cell cellPrefab;

        [SerializeField]
        private Vector2Int size = new Vector2Int(10, 10);

        private List<Cell> _cells = new List<Cell>();

        // Start is called before the first frame update
        void Start()
        {
            InitCells();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void InitCells()
        {
            _cells.Clear();
        
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    Cell cell = Instantiate(cellPrefab, transform);
                    cell.transform.localPosition = new Vector3(x, y, 0);
                    _cells.Add(cell);
                }
            }
        }
    
        private void OnUncover()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Debug.Log($"{mousePos.x} {mousePos.y}");
        }
    }
}
