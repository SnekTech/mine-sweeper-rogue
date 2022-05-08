using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech
{
    public class GameManager : MonoBehaviour
    {
        public GridBehaviour gridBehaviour;

        private void OnEnable()
        {
            gridBehaviour.BombRevealed += OnBombRevealed;
            gridBehaviour.Cleared += OnGridCleared;
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
