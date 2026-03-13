using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Treasure
    {

        public static int _gold = 0;
        public static bool _goldTreasure = true;
        public static Random _goldPileSpawn = new Random();
        public static (int, int) _plPosition = (Program.player._x, Program.player._y);
        public static (int, int) goldLoc = (treasure_x_pos, treasure_y_pos);
        public static int treasure_x_pos;
        public static int treasure_y_pos;
        public static (int, int) treasure_min_max_x = (9, 45);
        public static (int, int) treasure_min_max_y = (7, 20);
        public static Random _lootRando = new Random();
        public static int loot;


        public static void DrawGold()
        {
            if (_goldTreasure)
            {
               
               
                    bool clearGoldSpawn = false;
                    while (!clearGoldSpawn)
                    {
                        loot = _lootRando.Next(15, 35);

                        treasure_x_pos = _goldPileSpawn.Next(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 4);
                        treasure_y_pos = _goldPileSpawn.Next(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 4);
                        char targetTile = Program.map._mapsCurrent[Program.nextY][Program.nextX];

                        if (Program.map.CanMoveTo(treasure_x_pos, treasure_y_pos) && targetTile != '%' && targetTile != 'w' && targetTile != '#' && targetTile != 'S')
                        {

                            if (treasure_x_pos != Program.player._x || treasure_y_pos != Program.player._y) //checks for player
                            {
                            _gold += loot;

                            HUD.Looter();
                            clearGoldSpawn = true;
                            DrawGold();
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
                Console.ResetColor();
            }
       
    }
}

