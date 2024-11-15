using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Characters
{
    public abstract class Enemy : Character
    {
        public Vector2 EnemyPosition { get; set; } = new Vector2(300, 300);

        public void AI(Hero hero)
        {
            EnemyPosition += hero.Position;
        }
    }
}
