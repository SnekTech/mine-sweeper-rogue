﻿using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    public static class Utils
    {
        public static Vector3 GetMouseWorldPosition(Vector2 screenPosition, Camera camera)
        {
            return camera.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 GetMouseWorldPosition()
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Camera mainCamera = Camera.main;
            return GetMouseWorldPosition(screenPosition, mainCamera);
        }
    }
}
