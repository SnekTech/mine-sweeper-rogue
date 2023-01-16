using System;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell.Cover;
using UnityEngine;

namespace Tests.EditMode.Builder
{
    public class FakeCover : ILogicCover
    {
        public UniTask<bool> RevealAsync() => UniTask.FromResult(true);

        public UniTask<bool> PutCoverAsync() => UniTask.FromResult(true);
    }
}