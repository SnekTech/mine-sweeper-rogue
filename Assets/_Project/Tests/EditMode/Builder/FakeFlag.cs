using System;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell.Flag;
using UnityEngine;

namespace Tests.EditMode.Builder
{
    public class FakeFlag : IFlag
    {
        public bool IsActive { get; set; }
        public Animator Animator { get; }
        public SpriteRenderer SpriteRenderer { get; }
        public UniTask<bool> LiftAsync() => UniTask.FromResult<bool>(true);

        public UniTask<bool> PutDownAsync() => UniTask.FromResult<bool>(true);
    }
}