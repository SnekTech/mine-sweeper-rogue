using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech
{
    public class GameManager : MonoBehaviour
    {
        public GridBehaviour gridBehaviour;

        private IGrid Grid => gridBehaviour;

        private void OnEnable()
        {
            Grid.BombRevealed += OnBombRevealed;
            Grid.Cleared += OnGridCleared;
        }

        private void OnDisable()
        {
            Grid.BombRevealed -= OnBombRevealed;
            Grid.Cleared -= OnGridCleared;
        }

        private void OnGridCleared()
        {
            print("cleared a grid");
        }

        private void OnBombRevealed()
        {
            print("hit bomb!");
        }
    }
}
