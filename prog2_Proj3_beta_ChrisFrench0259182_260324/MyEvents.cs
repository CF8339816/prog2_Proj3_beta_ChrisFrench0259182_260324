using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{

    public class MyEvents
    {
        public static List<Enemy> enemyRiderList = new List<Enemy>();
        public static bool _ambushTriggered = false;

        public static void MapCheck()
        {
            enemyRiderList.Clear();
            enemyRiderList.Add(new Enemy("Slasher", 50, 4, 10, 'k', 25, ConsoleColor.Red));
            enemyRiderList.Add(new Enemy("Crasher", 20, 23, 8, 'k', 20, ConsoleColor.Red));
            enemyRiderList.Add(new Enemy("Harrier", 15, 12, 12, 'k', 30, ConsoleColor.Red));
            enemyRiderList.Add(new Enemy("PackAlphaNasty", 49, 19, 15, 'K', 40, ConsoleColor.DarkRed));

            // Only run this logic on Map 3 if not already triggered
            if (Program.map._currentMapIndex == 3 && !_ambushTriggered)
            {
                // Define your trigger coordinates (example: x:15, y:10)
                if (Program.player._x <= 15 || Program.player._y <= 10)
                {
                    _ambushTriggered = true;
                    Console.SetCursorPosition(60, 0);
                    Console.WriteLine("here comes a new challenger");
                    Console.ReadKey(true);
                    Console.Beep(); // Audio cue for the ambush
                    foreach (var enmy in enemyRiderList)
                    {
                        if (enmy._health > 0) // Only draw if alive
                        {
                            Console.SetCursorPosition(enmy._x, enmy._y);
                            Console.ForegroundColor = enmy._color;
                            Console.Write(enmy._symbol);
                        }
                    }


                    Console.SetCursorPosition(Program.player._x, Program.player._y);
                    Console.ForegroundColor = Program.player._color;
                    Console.Write(Program.player._symbol);
                    Console.ResetColor();
                }
            }
        }

        public static void UpdateRiders()
        {
            // Only move riders if the ambush has started
            if (_ambushTriggered)
            {
                foreach (var enemy in enemyRiderList)
                {
                    if (enemy._health > 0)
                    {
                        Enemy.MoveEnemy(enemy); // Or your MoveTowards method
                    }
                }
            }
        }
    }
}


