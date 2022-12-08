using System;
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
        private int armour;

        public int Health => health;
        public int Armour => armour;
        public static HealthArmour Default => new HealthArmour(GameConstants.InitialHealth,GameConstants.InitialArmour);

        public HealthArmour(int health, int armour)
        {
            this.health = health;
            this.armour = armour;
        }

        public void ResetWith(int newHealth, int newArmour)
        {
            // todo: necessary param validation
            health = newHealth;
            armour = newArmour;
        }

        public void ResetWith(HealthArmour other)
        {
            ResetWith(other.health, other.Armour);
        }

        public void TakeDamage(int damage)
        {
            if (damage < Armour)
            {
                armour -= damage;
                return;
            }
            
            damage -= Armour;
            armour = 0;
            ArmourRanOut?.Invoke();

            if (damage < Health)
            {
                health -= damage;
                return;
            }

            health = 0;
            HealthRanOut?.Invoke();
        }

        public void AddHealth(int amount)
        {
            int newHealth = Health + amount;
            if (newHealth <= 0)
            {
                HealthRanOut?.Invoke();
            }

            health = Mathf.Max(0, newHealth);
        }

        public void AddArmour(int amount)
        {
            int newArmour = Armour + amount;
            if (newArmour <= 0)
            {
                ArmourRanOut?.Invoke();
            }

            armour = Mathf.Max(0, newArmour);
        }
    }
}