using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.C;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [Serializable]
    public class Life
    {
        public event Action ArmourRanOut;
        public event Action HealthRanOut;

        [SerializeField]
        private int health;

        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private int armour;

        public int Health => health;
        public int MaxHealth => maxHealth;
        public int Armour => armour;
        public static Life Default => new Life(GameConstants.InitialHealth,GameConstants.InitialArmour);

        private List<IHealthArmourDisplay> _displays = new List<IHealthArmourDisplay>();

        public Life(int health, int maxHealth, int armour)
        {
            if (maxHealth < health)
            {
                throw new ArgumentException($"maxHealth[{maxHealth}] < health[{health}], invalid");
            }
            this.health = health;
            this.maxHealth = maxHealth;
            this.armour = armour;
        }

        public Life(int health, int armour): this(health, health, armour)
        {
        }

        public void ResetWith(int newHealth, int newMaxHealth, int newArmour)
        {
            if (newHealth < 0 || newHealth > newMaxHealth || newArmour < 0)
            {
                throw new ArgumentException();
            }
            
            health = newHealth;
            maxHealth = newMaxHealth;
            armour = newArmour;
            
            _displays.Clear();
        }

        public void ResetWith(Life other)
        {
            ResetWith(other.health, other.MaxHealth, other.Armour);
        }

        /// <summary>
        /// damage on armour, won't put remaining damage on health
        /// </summary>
        /// <param name="damage">must >= 0</param>
        public async UniTask TakeDamageOnArmour(int damage)
        {
            if (Armour <= 0 || damage < 0)
            {
                // prevent from unnecessary multiple async operation
                return;
            }
            bool isArmourRanOut = damage >= armour;
            armour = Mathf.Max(0, Armour - damage);
            await PerformAllArmourDamageAsync(damage);
            UpdateAllDisplays();
            if (isArmourRanOut)
            {
                ArmourRanOut?.Invoke();
            }
        }

        /// <summary>
        /// take damage on health, ignore the armour, like poison
        /// </summary>
        /// <param name="damage">must >= 0</param>
        public async UniTask TakeDamageOnHealth(int damage)
        {
            if (Health <= 0 || damage < 0)
            {
                // prevent from unnecessary multiple async operation
                return;
            }
            bool isHealthRanOut = damage >= Health;
            health = Mathf.Max(0, health - damage);
            await PerformAllHealthDamageAsync(damage);
            UpdateAllDisplays();
            if (isHealthRanOut)
            {
                HealthRanOut?.Invoke();
            }
        }

        /// <summary>
        /// take damage on armour first, consume health after armour ran out
        /// </summary>
        /// <param name="damage"></param>
        public async UniTask TakeDamage(int damage)
        {
            if (health <= 0)
            {
                // to damage a dead object is pointless
                return;
            }

            if (Armour > 0)
            {
                int damageAfterBreakArmour = damage - Armour;
                await TakeDamageOnArmour(damage);
                
                damage = damageAfterBreakArmour;
            }
            
            if (damage == 0)
            {
                // after break the armour, an armour break effect has been performed,
                // so no need to perform zero-valued damage for health
                return;
            }

            await TakeDamageOnHealth(damage);
        }

        /// <summary>
        /// adjust the max health, roughly maxHealth += <see cref="amount"/>
        /// </summary>
        /// <param name="amount">how much to increase, can be negative</param>
        public void AdjustMaxHealth(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount > 0)
            {
                maxHealth += amount;
                AddHealth(amount).Forget();
            }
            else
            {
                // bug: unintended behavior when adjust negative max-health
                int newMaxHealth = Mathf.Max(0, maxHealth + amount);
                int newHealth = Mathf.Min(health, newMaxHealth);
                int damageOnHealth = health - newHealth;
                maxHealth = newMaxHealth;
                health = newHealth;
                TakeDamageOnHealth(damageOnHealth).Forget();
            }
        }

        /// <summary>
        /// heal the target
        /// </summary>
        /// <param name="increment">how much to heal, must be >= 0</param>
        public async UniTask AddHealth(int increment)
        {
            if (increment < 0)
            {
                return;
            }
            
            int newHealth = Mathf.Min(Health + increment, MaxHealth);
            health = newHealth;
            await PerformAllAddHealthAsync(increment);
            UpdateAllDisplays();
        }

        /// <summary>
        /// add armour
        /// </summary>
        /// <param name="increment">must >= 0</param>
        public async UniTask AddArmour(int increment)
        {
            if (increment < 0)
            {
                return;
            }
            armour += increment;
            await PerformAllAddArmourAsync(increment);
            UpdateAllDisplays();
        }

        public void AddDisplay(IHealthArmourDisplay display)
        {
            display.UpdateContent(Health, MaxHealth, Armour);
            _displays.Add(display);
        }

        private void UpdateAllDisplays()
        {
            foreach (var display in _displays)
            {
                display.UpdateContent(Health, MaxHealth, Armour);
            }
        }

        private UniTask PerformAllHealthDamageAsync(int damage)
        {
            return UniTask.WhenAll(_displays.Select(display => display.PerformHealthDamageAsync(damage)));
        }

        private UniTask PerformAllArmourDamageAsync(int damage)
        {
            return UniTask.WhenAll(_displays.Select(display => display.PerformArmourDamageAsync(damage)));
        }

        private UniTask PerformAllAddHealthAsync(int healthAddAmount)
        {
            return UniTask.WhenAll(_displays.Select(display => display.PerformAddHealthAsync(healthAddAmount)));
        }

        private UniTask PerformAllAddArmourAsync(int armourIncrement)
        {
            return UniTask.WhenAll(_displays.Select(display => display.PerformAddArmourAsync(armourIncrement)));
        }
    }
}