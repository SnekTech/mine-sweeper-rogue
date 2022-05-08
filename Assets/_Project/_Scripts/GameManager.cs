using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech
{
    public class GameManager : MonoBehaviour
    {
        public GridEventManager gridEventManager;

        private void OnEnable()
        {
            gridEventManager.BombRevealed += OnBombRevealed;
            gridEventManager.GridCleared += OnGridCleared;
        }

        private void OnDisable()
        {
            gridEventManager.BombRevealed -= OnBombRevealed;
            gridEventManager.GridCleared -= OnGridCleared;
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
