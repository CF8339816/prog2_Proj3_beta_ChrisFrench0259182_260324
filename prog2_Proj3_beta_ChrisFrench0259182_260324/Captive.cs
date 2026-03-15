using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Captive : Collectable
    {

       
        public static bool _newPrisoner = true;
        public static Random _prisonerSpawn = new Random();
        public static int _prisonerCount=6;
        public static int _prisoner_x_pos;
        public static int _prisoner_y_pos;
        public static (int, int) _prisoner_min_max_x = (8, 46);
        public static (int, int) _prisoner_min_max_y = (8, 21);
        public static int _freed = 0;
       
        public static List<(int x, int y)> _prisonerLocations = new List<(int, int)>();


        public Captive(string Name, int x, int y, int count, char symbol, int output, ConsoleColor color, (int, int) min_max_x, (int, int) min_max_y) : base(Name, x, y, count: 8, symbol: 'S', output: _freed, ConsoleColor.White, min_max_x, min_max_y)
        {
            Name = "hostage";
            _prisonerCount = count;

            _prisoner_x_pos = x;

            _prisoner_y_pos = y;

            _prisoner_min_max_x = min_max_x;///

            _prisoner_min_max_y = min_max_y;///
        }




        public static void DrawPrisoner()
        {
            if (_newPrisoner)
            {

                //int minX = 8, maxX = 46, minY = 8, maxY = 21;  
                int capSpawnX = -1, capSpawnY = -1;

                _prisonerLocations.Clear(); // Clear old positions

                for (int i = 0; i < _prisonerCount; i++)
                {
                    bool clearPrisonerSpawn = false;



                    while (!clearPrisonerSpawn)
                    {

                        capSpawnX = _prisonerSpawn.Next(_prisoner_min_max_x.Item1, _prisoner_min_max_x.Item2 + 1);///
                        capSpawnY = _prisonerSpawn.Next(_prisoner_min_max_y.Item1, _prisoner_min_max_y.Item2 + 1);///
                        char targetTile = Program.map._mapsCurrent[_prisoner_x_pos][_prisoner_y_pos];///
                        char[] forbiddenTiles = { '#', 'w', '%', 'S', '$', '&', 'O', 'H', '@', '!', '*' };
                        bool isForbidden = Array.Exists(forbiddenTiles, t => t == targetTile);

                        //Console.SetCursorPosition(60, 1);
                        //Console.WriteLine($"map tile referenced {targetTile}");
                        //Console.ReadKey(true);

                        if (Program.map.CanMoveTo(capSpawnX, capSpawnY) && !isForbidden)///
                        {
                            if (capSpawnX != Program.player._x || capSpawnY != Program.player._y)///
                            {
                                clearPrisonerSpawn = true;
                            }

                        }
                    }

                    _prisonerLocations.Add((capSpawnX, capSpawnY));///

                    Console.SetCursorPosition(capSpawnX, capSpawnY);///
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("S");
                    Console.ResetColor();


                }
                _newPrisoner = false;
                Console.ResetColor();
            }
        }


        public static void CheckCapCollection()
        {
            // Check if player is standing on any prisoner's location
            for (int i = 0; i < _prisonerLocations.Count; i++)
            {
              
                if (Program.player._x == _prisonerLocations[i].x && Program.player._y == _prisonerLocations[i].y)
                {
                    _prisonerLocations.RemoveAt(i);
                    _freed++;

                    Player.plXP += 10;
                    Buffs.IncreaseATK(0);
                    Buffs.IncreaseMaxHealth(0);
                    Treasure._gold += 4;
                    Console.SetCursorPosition(Program.player._x, Program.player._y);
                    Console.Write(" ");
                    HUD.Moses();
                    break;
        
                }
            }
        }
    }
}
