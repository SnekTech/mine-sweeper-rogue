using System;

namespace SnekTech.Player
{
    public class HealthArmour
    {
        public event Action ArmourRanOut;
        public event Action HealthRanOut;
        
        public int Health { get; private set; }
        public int Armour { get; private set; }
        public static HealthArmour Default => new HealthArmour(10, 10);

        public HealthArmour(int health, int armour)
        {
            Health = health;
            Armour = armour;
        }

        public void TakeDamage(int damage)
        {
            if (damage < Armour)
            {
                Armour -= damage;
                return;
            }
            
            damage -= Armour;
            Armour = 0;
            ArmourRanOut?.Invoke();

            if (damage < Health)
            {
                Health -= damage;
                return;
            }

            Health = 0;
            HealthRanOut?.Invoke();
        }

        public void AddHealth(int amount)
        {
            Health += amount;
        }

        public void AddArmour(int amount)
        {
            Armour += amount;
        }
    }
}