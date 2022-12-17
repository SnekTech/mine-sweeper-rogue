using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Constants;
using UnityEngine;

namespace SnekTech.Player
{
    [Serializable]
    public class HealthArmour
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
        public static HealthArmour Default => new HealthArmour(GameConstants.InitialHealth,GameConstants.InitialArmour);

        private List<IHealthArmourDisplay> _displays = new List<IHealthArmourDisplay>();

        public HealthArmour(int health, int maxHealth, int armour)
        {
            if (maxHealth < health)
            {
                throw new ArgumentException($"maxHealth[{maxHealth}] < health[{health}], invalid");
            }
            this.health = health;
            this.maxHealth = maxHealth;
            this.armour = armour;
        }

        public HealthArmour(int health, int armour): this(health, health, armour)
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

        public void ResetWith(HealthArmour other)
        {
            ResetWith(other.health, other.MaxHealth, other.Armour);
        }

        /// <summary>
        /// take damage on health, ignore the armour, like poison
        /// </summary>
        /// <param name="damage"></param>
        public async UniTask TakeDamageOnHealth(int damage)
        {
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
                int damageRemaining = damage - Armour;
                bool isArmourRanOut = damageRemaining >= 0;
                armour = Mathf.Max(0, Armour - damage);
                await PerformAllArmourDamageAsync(damage);
                UpdateAllDisplays();
                if (!isArmourRanOut)
                {
                    return;
                }
                ArmourRanOut?.Invoke();
                
                damage = damageRemaining;
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
            int newHealth = Mathf.Min(Health + increment, MaxHealth);
            health = newHealth;
            await PerformAllAddHealthAsync(increment);
            UpdateAllDisplays();
        }

        public void AddArmour(int amount)
        {
            // todo: perform add armour
            int newArmour = Armour + amount;
            if (newArmour <= 0)
            {
                ArmourRanOut?.Invoke();
            }

            armour = Mathf.Max(0, newArmour);
        }

        public void AddDisplay(IHealthArmourDisplay display)
        {
            display.UpdateContent();
            _displays.Add(display);
        }

        private void UpdateAllDisplays()
        {
            foreach (IHealthArmourDisplay display in _displays)
            {
                display.UpdateContent();
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
    }
}