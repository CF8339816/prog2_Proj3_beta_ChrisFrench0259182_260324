using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{

    public class MyEvents
    {
        public static bool _ambushTriggered = false;

        public static void AmbushMapCheck()
        {

            if (Program.map._currentMapIndex == 3 && !_ambushTriggered) //sets this to run on map 3 only  and only if not alreacdy active
            {
                if ((Program.map._mapsCurrent[Program.player._y][Program.player._x] == '`'))// defines trigger location for event to begin
                {
                    _ambushTriggered = true;

                    Program.enemyRiderList.Clear();
                    Program.enemyRiderList.Add(new EnemyRiders("Slasher", 44, 5, 10, 'k', 25, ConsoleColor.Red));
                    Program.enemyRiderList.Add(new EnemyRiders("Crasher", 3, 12, 8, 'k', 20, ConsoleColor.Red));
                    Program.enemyRiderList.Add(new EnemyRiders("Harrier", 13, 3, 12, 'k', 30, ConsoleColor.Red));
                    Program.enemyBoss.Add(new EnemyBoss("Pack Alpha Nasty", 39, 17, 25, 'K', 800, ConsoleColor.DarkRed));

                    //Console.SetCursorPosition(60, 0);
                    //Console.WriteLine("here comes a new challenger");
                    Console.ReadKey(true);
                    Console.Beep(); // Audio cue for the ambush

                }
            }
            foreach (var enmy in Program.enemyRiderList)
            {
                if (enmy._health > 0) // Only draw if alive
                {
                    Console.SetCursorPosition(enmy._x, enmy._y);
                    Console.ForegroundColor = enmy._color;
                    Console.Write(enmy._symbol);
                }
            }
            Program.DrawBoss();

            UpdateRiders();
            UpdateBoss();

        }

        public static void UpdateRiders()
        {
            // Only move riders if the ambush has started
            if (_ambushTriggered)
            {
                foreach (var enmy in Program.enemyRiderList)
                {
                    if (enmy._health > 0) //verifies enemy alive before move
                    {
                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Console.ForegroundColor = enmy._color;
                        Console.Write(enmy._symbol);

                        EnemyRiders.MoveTowards(enmy); //  move towards rather than randopm 
                    }

                }
            }
        }

        public static void UpdateBoss()
        {
            // Only move riders if the ambush has started
            if (_ambushTriggered)
            {
                int bossIndex = Program.map._currentMapIndex;

                if (Program.enemyBoss.Count > bossIndex)
                {
                    var boss = Program.enemyBoss[bossIndex];

                    if (boss._health > 0) //verifies enemy alive before move
                    {
                        Console.SetCursorPosition(boss._x, boss._y);
                        Console.ForegroundColor = boss._color;
                        Console.Write(boss._symbol);

                        EnemyBoss.MoveEnemy(boss); //  move towards rather than randopm 
                    }




                }
            }
        }
    }
}


