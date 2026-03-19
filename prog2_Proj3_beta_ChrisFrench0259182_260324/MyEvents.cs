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
                    Program.enemyRiderList.Add(new EnemyRiders("PackAlphaNasty", 39, 17, 15, 'K', 40, ConsoleColor.DarkRed));

                    //Console.SetCursorPosition(60, 0);
                    //Console.WriteLine("here comes a new challenger");
                    Console.ReadKey(true);
                    Console.Beep(); // Audio cue for the ambush

                }
            }
            foreach (var enmyRide in Program.enemyRiderList)
            {
                if (enmyRide._health > 0) // Only draw if alive
                {
                    Console.SetCursorPosition(enmyRide._x, enmyRide._y);
                    Console.ForegroundColor = enmyRide._color;
                    Console.Write(enmyRide._symbol);
                }
            }
            UpdateRiders();
        }

        public static void UpdateRiders()
        {
            // Only move riders if the ambush has started
            if (_ambushTriggered)
            {
                foreach (var enmyRide in Program.enemyRiderList)
                {
                    if (enmyRide._health > 0) //verifies enemy alive before move
                    {
                       Console.SetCursorPosition(enmyRide._x, enmyRide._y);
                        Console.ForegroundColor = enmyRide._color;
                        Console.Write(enmyRide._symbol);
                        
                        EnemyRiders.MoveTowards(enmyRide); //  move towards rather than randopm 
                    }
                    
                }
            }
        }
    }
}


