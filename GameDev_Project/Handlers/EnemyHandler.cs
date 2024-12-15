using GameDev_Project.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Handlers
{
    public class EnemyHandler
    {
        public static List<Character> Enemies = new List<Character>();

        public static void AddEnemy(Character enemy)
        {
            Enemies.Add(enemy);
        }
        public static void RemoveEnemy(Character enemy)
        {
            Enemies.Remove(enemy);
        }
        public static bool IsEmpty()
        {
            if (Enemies.Count != 0)
                return false;
            else
                return true;
        }
    }
}
