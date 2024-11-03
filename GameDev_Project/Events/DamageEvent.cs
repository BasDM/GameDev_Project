using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Events
{
    public class DamageEvent : CollideWithEvent
    {
        public int Damage { get; set; }

        public DamageEvent(int damage)
        {
            Damage = damage;
        }
    }
}
