using NUnit.Framework;
using SnekTech.Player;

namespace _Project.Tests.EditMode
{
    public class HealthArmourTest
    {
        [Test]
        public void health_should_remain_the_same_when_armour_is_enough()
        {
            const int originalHealth = 1;
            const int originalArmour = 3;
            const int damage = 2;
            var healthArmour = new HealthArmour(originalHealth, originalArmour);

            healthArmour.TakeDamage(damage);

            Assert.That(healthArmour.Health, Is.EqualTo(originalHealth));
        }

        [Test]
        public void armour_should_be_zero_after_ran_out()
        {
            const int originalHealth = 1;
            const int originalArmour = 2;
            const int damage = 3;
            var healthArmour = new HealthArmour(originalHealth, originalArmour);

            healthArmour.TakeDamage(damage);

            Assert.That(healthArmour.Armour, Is.EqualTo(0));
        }

        [Test]
        public void should_consume_health_after_armour_ran_out()
        {
            const int originalHealth = 1;
            const int originalArmour = 2;
            const int damage = 3;
            var healthArmour = new HealthArmour(originalHealth, originalArmour);

            Assert.That(originalArmour, Is.LessThan(damage));
            healthArmour.TakeDamage(damage);

            Assert.That(healthArmour.Health, Is.LessThan(originalHealth));
        }
 
        [Test]
        public void health_should_be_zero_after_ran_out()
        {
            const int originalHealth = 1;
            const int originalArmour = 2;
            const int damage = 4;
            var healthArmour = new HealthArmour(originalHealth, originalArmour);

            Assert.That(damage, Is.GreaterThan(originalHealth + originalArmour));
            healthArmour.TakeDamage(damage);

            Assert.That(healthArmour.Health, Is.EqualTo(0));
        }
    }
}