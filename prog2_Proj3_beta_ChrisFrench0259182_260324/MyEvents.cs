using System;
using System.Collections.Generic;
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
            

            // Only run this logic on Map 3 if not already triggered
            if (Program.map._currentMapIndex == 3 && !_ambushTriggered)
            {
                // Define your trigger coordinates (example: x:15, y:10)
                if ((Program.map._mapsCurrent[Program.player._y][Program.player._x] == '`'))
                {
                    _ambushTriggered = true;
                    Console.SetCursorPosition(60, 0);
                    Console.WriteLine("here comes a new challenger");
                    Console.ReadKey(true);
                    Console.Beep(); // Audio cue for the ambush
                    foreach (var enmy in Program.enemyRiderList)
                    {
                        if (enmy._health > 0) // Only draw if alive
                        {
                            Console.SetCursorPosition(enmy._x, enmy._y);
                            Console.ForegroundColor = enmy._color;
                            Console.Write(enmy._symbol);
                        }
                    }


                    Console.SetCursorPosition(Program.player._x, Program.player._y);
                    UpdateRiders();
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
                foreach (var enemy in Program.enemyRiderList)
                {
                    if (enemy._health > 0)
                    {
                        Enemy.MoveTowards(enemy); // Or your MoveTowards method
                    }
                }
            }
        }
    }
}


