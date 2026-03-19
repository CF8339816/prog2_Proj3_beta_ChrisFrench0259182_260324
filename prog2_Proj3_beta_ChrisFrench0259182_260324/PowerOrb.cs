using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class PowerOrb : Collectable
    {
        public static bool _powerOrb = true;
        public static Random _powerOrbSpawn = new Random();
        public static (int, int) _plPosition = (Program.player._x, Program.player._y);
      //  public static char orbSymbol = '\u2699'; will not diaplay . 
        public static int powerOrb_x_pos;
        public static int powerOrb_y_pos;
        public static (int, int) powerOrb_min_max_x = (8, 46);///
        public static (int, int) powerOrb_min_max_y = (8, 21);///

        public static int _poCount = 1;


        public PowerOrb(string Name, int x, int y, int count, char symbol, ConsoleColor color, (int, int) min_max_x, (int, int) min_max_y) : base(Name, x, y, count: 1, symbol: '0' /*orbSymbol*/, ConsoleColor.Cyan, min_max_x, min_max_y)
        {
            Name = "Power Orb";
            powerOrb_x_pos = x;
            powerOrb_y_pos = y;
            count = 1;
        }

        public static void DrawPowerOrb()
        {
            int currentMap = Program.map._currentMapIndex;

            if (!Program.MapOrbRegistry.ContainsKey(currentMap))// onlly spawns new list if map never visited otherwise holds locations of uncolllected treasures
            {
                List<(int x, int y)> PowerOrb = new List<(int x, int y)>();
                for (int i = 0; i < _poCount; i++)
                {
                    int poSpawnX, poSpawnY;
                    bool valid = false;
                    while (!valid)
                    {
                        poSpawnX = _powerOrbSpawn.Next(powerOrb_min_max_x.Item1, powerOrb_min_max_x.Item2 + 1);
                        poSpawnY = _powerOrbSpawn.Next(powerOrb_min_max_y.Item1, powerOrb_min_max_y.Item2 + 1);

                        if (!Program.IsTileOccupied(poSpawnX, poSpawnY))
                        {
                            PowerOrb.Add((poSpawnX, poSpawnY));
                            valid = true;
                        }
                    }
                }
                Program.MapOrbRegistry[currentMap] = PowerOrb;
            }

            foreach (var pOrb in Program.MapOrbRegistry[currentMap])//Drawing  from the dictionary list for the current map
            {
                Console.SetCursorPosition(pOrb.x, pOrb.y);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write('0');
            }
            Console.ResetColor();
        }
        public static void CheckOrbCollection()
        {
            int currentMap = Program.map._currentMapIndex;
            if (!Program.MapOrbRegistry.ContainsKey(currentMap)) return;// checks the correct map dictionary for orb location

            var orbs = Program.MapOrbRegistry[currentMap];

            for (int i = orbs.Count - 1; i >= 0; i--)
            {

                if (Program.player._x == orbs[i].x && Program.player._y == orbs[i].y)// checks for player on the Orb
                {

                    foreach (var enmy in Program.enemiesMap1)
                    {
                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("░"); // plays  some nice crunch 
                        Console.Beep(800, 50);
                        Thread.Sleep(650); // delay  for effect
                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Program.WriteTileWithColor(Program.map._mapsCurrent[enmy._y][enmy._x]); //Resets  map tile
                        Thread.Sleep(20); // resets threadsleep
                    }

                    Program.enemiesMap1.Clear(); //Clears the enemy list 
                    Buffs.IncreaseXP(150);  //awards a base xp
                    HUD.Kaboom();

                    orbs.RemoveAt(i);

                }


                //    if (!Program.MapTreasureRegistry.ContainsKey(currentMap)) return;

                //    var orbs = Program.MapTreasureRegistry[currentMap];

                //    for (int i = orbs.Count - 1; i >= 0; i--)
                //    {
                //        if (Program.player._x == orbs[i].x && Program.player._y == orbs[i].y)
                //        {

                //            foreach (var enmy in Program.enemiesMap1)
                //            {

                //                Console.SetCursorPosition(enmy._x, enmy._y);
                //                //WriteTileWithColor(map._mapsCurrent[enmy._y][enmy._x]);
                //                Console.ForegroundColor = ConsoleColor.Cyan;
                //                Console.Write("░");
                //                Console.ReadKey(true);
                //                Console.ForegroundColor = ConsoleColor.Black;
                //                Console.Write(" ");
                //            }


                //            Console.Beep(400, 200);

                //            Program.enemiesMap1.Clear();


                //            Player.plXP += 150;
                //             HUD.Kaboom();

                //            orbs.RemoveAt(i);// Remove collected orb
                //        }
                //    }
                //}
            }
        }
    }
}