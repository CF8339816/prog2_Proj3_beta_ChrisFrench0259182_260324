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
        //public static (int, int) goldLoc = (treasure_x_pos, treasure_y_pos);
        public static int treasure_x_pos;
        public static int treasure_y_pos;
        public static (int, int) treasure_min_max_x = (8, 46);///
        public static (int, int) treasure_min_max_y = (8, 21);///
        public static Random _lootRando = new Random();
        public static int loot;
        public static int _gold;
        public static int _gpCount = 6;
        public static List<(int x, int y)> activeGoldPiles = new List<(int x, int y)>();///

        public Treasure(string Name, int x, int y, int count, char symbol,  ConsoleColor color, (int, int) min_max_x, (int, int) min_max_y) : base(Name, x, y, count: 6, symbol: '$', ConsoleColor.Yellow, min_max_x, min_max_y)
        {
            Name = "gold";
           

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
                   
                    int tSpawnX = -1, tSpawnY = -1;
                    bool clearGoldSpawn = false;
                    while (!clearGoldSpawn)
                    {
                        loot = _lootRando.Next(15, 35);

                        tSpawnX = _goldPileSpawn.Next(treasure_min_max_x.Item1, treasure_min_max_x.Item2 + 1);
                        tSpawnY = _goldPileSpawn.Next(treasure_min_max_y.Item1, treasure_min_max_y.Item2 + 1);
                      
                        if (!Program.IsTileOccupied(tSpawnX, tSpawnY))
                        {
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
                    _goldTreasure = false;
                }
            }
            Console.ResetColor();
        }
        public static void CheckTreasureCollection()
        {
                for (int i = activeGoldPiles.Count - 1; i >= 0; i--)
                {
                    if (Program.player._x == activeGoldPiles[i].x && Program.player._y == activeGoldPiles[i].y)
                    {
                        loot = _lootRando.Next(15, 35);
                        _gold += loot;
                        HUD.Looter();

                        activeGoldPiles.RemoveAt(i);// remove picked up loot pile
                    }
                }
          
        } 
    }
}

    


