﻿using System;

namespace SnekTech.GridCell
{
    public interface IFlag
    {
        event Action LiftCompleted, PutDownCompleted;
        void Lift();
        void PutDown();
        void Hide();
    }
}