using GameDev_Project.Interfaces;
using System;

namespace GameDev_Project.Events
{
    public class DamageEvent : ICollideWithEvent
    {
        public int Damage { get; set; }

        public DamageEvent(int damage)
        {
            Damage = damage;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
