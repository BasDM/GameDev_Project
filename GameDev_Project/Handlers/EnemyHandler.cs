using GameDev_Project.Characters;
using System.Collections.Generic;

namespace GameDev_Project.Handlers
{
    public class EnemyHandler
    {
        public static List<Character> Enemies = new List<Character>();

        public static void AddEnemy(Character enemy)
        {
            Enemies.Add(enemy);
        }
        public static void RemoveEnemy(List<Character> toRemove)
        {
            foreach (var enemy in toRemove)
            {
                Enemies.Remove(enemy);
            }
        }
        public static bool IsEmpty()
        {
            if (Enemies.Count != 0)
                return false;
            else
            {
                EnemyHandler.Flush();
                return true;
            }
        }
        public static void Flush()
        {
            Enemies.Clear();
        }
    }
}
