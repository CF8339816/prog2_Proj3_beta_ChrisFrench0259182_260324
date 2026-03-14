using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Treasure : Collectable
    {
        public static int _gold = 0;
        public static bool _goldTreasure = true;
        public static Random _goldPileSpawn = new Random();
        public static (int, int) _plPosition = (Program.player._x, Program.player._y);
        public static (int, int) goldLoc = (treasure_x_pos, treasure_y_pos);
        public static int treasure_x_pos;
        public static int treasure_y_pos;
        static int treasure_min_max_x;
        static int treasure_min_max_y;
        public static Random _lootRando = new Random();
        public static int loot;

        public Treasure(string Name, int x, int y, int count, char symbol, int output, ConsoleColor color (int, int) min_max_x, (int, int) min_max_y) : base(Name,  x,  y, count: 4 , symbol: '$', output: _gold, ConsoleColor.Yellow,  min_max_x, min_max_y)
        {

            






        }
        public static void DrawGold()
        {
            if (_goldTreasure)
            {
                bool clearGoldSpawn = false;
                while (!clearGoldSpawn)
                {
                    loot = _lootRando.Next(15, 35);

                    treasure_x_pos = _goldPileSpawn.Next(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 1);
                    treasure_y_pos = _goldPileSpawn.Next(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 1);
                    char targetTile = Program.map._mapsCurrent[treasure_y_pos][treasure_x_pos];
                    char[] forbiddenTiles = { '#', 'w', '%', 'S', '$', '&', 'O', 'H', '@', '!', '*' };
                    bool isForbidden = Array.Exists(forbiddenTiles, t => t == targetTile);

                    if (Program.map.CanMoveTo(treasure_x_pos, treasure_y_pos) && !isForbidden)
                    {
                        if (treasure_x_pos != Program.player._x || treasure_y_pos != Program.player._y) //checks for player
                        {
                            clearGoldSpawn = true;
                        }
                    }
                }
                goldLoc = (treasure_x_pos, treasure_y_pos);
                Console.SetCursorPosition(treasure_x_pos, treasure_y_pos);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("$");
                Console.ResetColor();
                _goldTreasure = false;
            }
                //_gold += loot;
                HUD.Looter();
                Console.ResetColor();
        }
        public static void CheckTreasureCollection()
        {
            if (Program.player._x == treasure_x_pos && Program.player._y == treasure_y_pos)// Check if player is on the treasure tile
            {
                _gold += loot;

                
                HUD.Looter();// Trigger HUD update

                
                treasure_x_pos = -1;
                treasure_y_pos = -1;

                
            }
        }
    }
}

