using System;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell.Cover;
using UnityEngine;

namespace Tests.EditMode.Builder
{
    public class FakeCover : ICover
    {
        public Animator Animator { get; }
        public SpriteRenderer SpriteRenderer { get; }
        public bool IsActive { get; set; } = true;

        public bool RevealResult = true;
        public bool PutCoverResult = true;
        
        public UniTask<bool> RevealAsync() => UniTask.FromResult(RevealResult);

        public UniTask<bool> PutCoverAsync() => UniTask.FromResult(PutCoverResult);
    }
}