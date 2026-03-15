using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Treasure : Collectable
    {
        public static bool _goldTreasure = true;
        public static Random _goldPileSpawn = new Random();
        public static (int, int) _plPosition = (Program.player._x, Program.player._y);
        public static (int, int) goldLoc = (treasure_x_pos, treasure_y_pos);
        public static int treasure_x_pos;
        public static int treasure_y_pos;
        public static (int, int) treasure_min_max_x = (8, 46);///
        public static (int, int) treasure_min_max_y = (8, 21);///
        public static Random _lootRando = new Random();
        public static int loot;
        public static int _gold;
        public static int _gpCount = 6;
        public static List<(int x, int y)> activeGoldPiles = new List<(int x, int y)>();///

        public Treasure(string Name, int x, int y, int count, char symbol, int output, ConsoleColor color, (int, int) min_max_x, (int, int) min_max_y) : base(Name, x, y, count: 6, symbol: '$', output: _gold, ConsoleColor.Yellow, min_max_x, min_max_y)
        {
            Name = "gold";
            //treasure_min_max_x = min_max_x;///
            //treasure_min_max_y = min_max_y;///
            //_gold = output;

            treasure_x_pos = x;
            treasure_y_pos = y;
            count = 6;
        }
        public static void DrawGold()
        {
            if (_goldTreasure)
            {
                for (int i = 0; i < _gpCount; i++)
                {
                    // int minX = 8, maxX = 46, minY = 8, maxY = 21;    ///
                    int tSpawnX = -1, tSpawnY = -1;
                    bool clearGoldSpawn = false;
                    while (!clearGoldSpawn)
                    {
                        loot = _lootRando.Next(15, 35);

                        tSpawnX = _goldPileSpawn.Next(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 1);
                        tSpawnY = _goldPileSpawn.Next(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 1);
                        //treasure_x_pos = _goldPileSpawn.Next(minX, maxX + 1);//(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 1);///
                        //treasure_y_pos = _goldPileSpawn.Next(minY, maxY + 1);//(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 1);///

                        //char targetTile = Program.map._mapsCurrent[treasure_y_pos][treasure_x_pos];
                        //char[] forbiddenTiles = { '#', 'w', '%', 'S', '$', '&', 'O', 'H', '@', '!', '*' };
                        //bool isForbidden = Array.Exists(forbiddenTiles, t => t == targetTile);

                        // if (Program.map.CanMoveTo(treasure_x_pos, treasure_y_pos) && !isForbidden)///
                        if (Program.IsTileOccupied(tSpawnX, tSpawnY))
                        {
                            //if (treasure_x_pos != Program.player._x || treasure_y_pos != Program.player._y) //checks for player///
                            if (tSpawnX != Program.player._x || tSpawnY != Program.player._y) //checks for player
                            {
                                clearGoldSpawn = true;
                            }
                        }
                    }
                    activeGoldPiles.Add((tSpawnX, tSpawnY));// goldLoc = (treasure_x_pos, treasure_y_pos);///
                    Console.SetCursorPosition(tSpawnX, tSpawnY);//(treasure_x_pos, treasure_y_pos);///
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("$");
                    // Console.ResetColor();
                    // _goldTreasure = false;
                }
            }
            //_gold += loot;
            // HUD.Looter();

            Console.ResetColor();
        }
        public static void CheckTreasureCollection()
        {
            if (Program.player._x == treasure_x_pos && Program.player._y == treasure_y_pos)// Check if player is on the treasure tile
            {

                for (int i = activeGoldPiles.Count - 1; i >= 0; i--)
                {
                    if (Program.player._x == activeGoldPiles[i].x && Program.player._y == activeGoldPiles[i].y)
                    {
                        loot = _lootRando.Next(15, 35);
                        _gold += loot;
                        HUD.Looter();


                        activeGoldPiles.RemoveAt(i);// remove picked up loot pile



                        //_gold += loot;

                        //HUD.Looter();// Trigger HUD update

                        //treasure_x_pos = -1;
                        //treasure_y_pos = -1;
                        //DrawGold();
                    }
                }
            }
        } 
    }
}

    


