using System;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    public interface IPlayerEffect
    {
        public UniTask Take(IPlayer target);
    }
}
