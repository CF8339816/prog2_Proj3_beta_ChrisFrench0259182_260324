using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Treasure : CollectSpawner

    {
        public static void DrawGold()
        {
            if (_goldTreasure)
            {
                bool clearGoldSpawn = false;
                while (!clearGoldSpawn)
                {
                    treasure_x_pos = _goldPileSpawn.Next(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 4);
                    treasure_y_pos = _goldPileSpawn.Next(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 4);
                    char targetTile = Program.map._mapsCurrent[Program.nextY][Program.nextX];

                    if (Program.map.CanMoveTo(treasure_x_pos, treasure_y_pos) && targetTile != '%' && targetTile != 'w' && targetTile != '#' && targetTile != 'S')
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
            Console.ResetColor();
        }
    }
}

