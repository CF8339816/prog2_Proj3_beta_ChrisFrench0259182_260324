using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{

    //public class Program
    class Program
    {
        public static string Name;
        public static int MaxNameLLength = 15;
        public static int plaAtkUP = 15;
        public static int plaMaxHP = 50;
        public static Player player = new Player(" ", 3, 3, plaAtkUP, '!', plaMaxHP, ConsoleColor.Blue);
        public static List<Enemy> enemiesMap1 = new List<Enemy>();
        public static List<Enemy> enemiesMap2 = new List<Enemy>();
        public static List<Enemy> enemiesMap3 = new List<Enemy>();
        public static List<Enemy> enemyRiderList = new List<Enemy>();
        public static LoadMap map = new LoadMap();

        public static bool isPlaying = true;

        public static bool isAlly = false; //sets bool to check for other allies in movement path
        public static bool IsTileOccupied(int x, int y)
        {
            // moved the  tile check here  to see if it would stop the treasure and  captive spawns in the lava
            char targetTile = Program.map._mapsCurrent[y][x];
            char[] forbiddenTiles = { '#', 'w', '%','|', 'M', '-', '+' };//, 'S', '$', '&', 'O', 'H', '@', '!','*'
            if (Array.Exists(forbiddenTiles, t => t == targetTile))
            {     return true;      }
            // Check if player  is there
            if (x == Program.player._x && y == Program.player._y)
            {     return true;       }
            // check for enemmies
            if (Program.enemiesMap1.Any(enmy => enmy._x == x && enmy._y == y))
            {   return true;         }
            // Check for gold spawn
            if (Treasure.activeGoldPiles.Any(g => g.x == x && g.y == y))
            {     return true;        }
            // Check there is already a captive there
            if (Captive._prisonerLocations.Any(p => p.x == x && p.y == y))
            {    return true;     }
            return false;
        }
        public static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            HUD.alias();
            Console.Clear();
            Console.CursorVisible = false;

            Console.CursorVisible = false;
            map.DrawMap();
            MyEvents.AmbushMapCheck();
            //HUD.Instructions();
              
            enemiesMap1.Clear();
            enemiesMap1.Add(new Enemy("Gobbo", 50, 4, 10, '&', 25, ConsoleColor.Green));
            enemiesMap1.Add(new Enemy("Slobbo", 20, 23, 8, '&', 20, ConsoleColor.Green));
            enemiesMap1.Add(new Enemy("Orcus", 15, 13, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemiesMap1.Add(new Enemy("Boss Hobbo", 49, 20, 15, 'H', 40, ConsoleColor.DarkYellow));

            enemiesMap2.Clear();
            enemiesMap2.Add(new Enemy("Gnolie",4, 4, 16, 'g', 25, ConsoleColor.Red));
            enemiesMap2.Add(new Enemy("Gnawlie", 5, 20, 18, 'g', 20, ConsoleColor.Red));
            enemiesMap2.Add(new Enemy("ZugZug", 31, 12, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemiesMap2.Add(new Enemy("Boss Gobstomper", 45, 22, 15, 'G', 40, ConsoleColor.DarkRed));

            enemiesMap3.Clear();
            enemiesMap3.Add(new Enemy("Bammo", 17, 6, 10, 'O', 25, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemy("Slammo", 17, 23, 8, 'O', 20, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemy("Ogrelet", 37, 10, 20, 'Q', 60, ConsoleColor.Yellow));
            enemiesMap3.Add(new Enemy("Boss Drowkus", 48, 23, 25, 'D', 80, ConsoleColor.DarkMagenta));

            //enemyRiderList.Clear();
            //enemyRiderList.Add(new Enemy("Slasher", 44, 5, 10, 'k', 25, ConsoleColor.Red));
            //enemyRiderList.Add(new Enemy("Crasher", 3, 12, 8, 'k', 20, ConsoleColor.Red));
            //enemyRiderList.Add(new Enemy("Harrier", 13, 3, 12, 'k', 30, ConsoleColor.Red));
            //enemyRiderList.Add(new Enemy("PackAlphaNasty", 39, 17, 15, 'K', 40, ConsoleColor.DarkRed));

            while (isPlaying)
            {
                HUD.Instructions();
                player._name =Name;
                player._attack =plaAtkUP;
              
                int plX = 0, plY = 0;
                ConsoleKey input = Console.ReadKey(true).Key;
                // move player with W,A,S,D or optional arrow keys 
                if (input == ConsoleKey.LeftArrow) plX = -1;
                if (input == ConsoleKey.A) plX = -1;
                if (input == ConsoleKey.RightArrow) plX = 1;
                if (input == ConsoleKey.D) plX = 1;
                if (input == ConsoleKey.UpArrow) plY = -1;
                if (input == ConsoleKey.W) plY = -1;
                if (input == ConsoleKey.DownArrow) plY = 1;
                if (input == ConsoleKey.S) plY = 1;

                if (input == ConsoleKey.Q) isPlaying = false; //Quit the 'is playing' loop
                HUD.ClearMessage();
                player.Move(plX, plY);
                Treasure.CheckTreasureCollection();
                Captive.CheckCapCollection();

                /*>>>>>>*/
                var newSpawn = map.MapChanger(player._x, player._y); //references the map changer function

                if (newSpawn.HasValue) //changes maps if triggers are found
                {
                    // sets player position to new spawn point 
                    player._x = newSpawn.Value.x;
                    player._y = newSpawn.Value.y;
                    Treasure._goldTreasure = true;
                    Captive._newPrisoner = true;
                }
                //CollectSpawner.SetupMapAssets();
                if (Program.map._currentMapIndex < 3)
                {
                    //Treasure.activeGoldPiles.Clear();
                    //Captive._prisonerLocations.Clear();
                    Treasure.DrawGold();
                    Captive.DrawPrisoner();
                  
                }
                EnviroHeal.SpringWatterHealling();
                EnviroDmg.LavaDamage();
                if (map._mapsCurrent[player._y][player._x] == 'X')
                {
                    isPlaying = false;
                    continue; //skips past rest
                }
                             
                if (Program.map._currentMapIndex == 0)
                {
                    for (int i = enemiesMap1.Count - 1; i >= 0; i--)
                    {
                        if (enemiesMap1[i]._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(enemiesMap1[i]._x, enemiesMap1[i]._y);
                            WriteTileWithColor(map._mapsCurrent[enemiesMap1[i]._y][enemiesMap1[i]._x]);
                            enemiesMap1.RemoveAt(i);
                        }
                        else
                        { Enemy.MoveEnemy(enemiesMap1[i]); }
                    }
                }
                if (Program.map._currentMapIndex == 1)
                {                   
                    for (int i = enemiesMap2.Count - 1; i >= 0; i--)
                    {
                        if (enemiesMap2[i]._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(enemiesMap2[i]._x, enemiesMap2[i]._y);
                            WriteTileWithColor(map._mapsCurrent[enemiesMap2[i]._y][enemiesMap2[i]._x]);
                            enemiesMap2.RemoveAt(i);
                        }
                        else
                        { Enemy.MoveEnemy(enemiesMap2[i]); }
                    }
                }

                if (Program.map._currentMapIndex == 2)
                {                    
                    for (int i = enemiesMap3.Count - 1; i >= 0; i--)
                    {
                        if (enemiesMap3[i]._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(enemiesMap3[i]._x, enemiesMap3[i]._y);
                            WriteTileWithColor(map._mapsCurrent[enemiesMap3[i]._y][enemiesMap3[i]._x]);
                            enemiesMap3.RemoveAt(i);
                        }
                        else
                        {  Enemy.MoveEnemy(enemiesMap3[i]); }
                    }
                }

                if (Program.map._currentMapIndex == 3)
                {
                    for (int i = enemyRiderList.Count - 1; i >= 0; i--)
                    {
                        if (enemyRiderList[i]._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(enemyRiderList[i]._x, enemyRiderList[i]._y);
                            WriteTileWithColor(map._mapsCurrent[enemyRiderList[i]._y][enemyRiderList[i]._x]);
                            enemyRiderList.RemoveAt(i);
                        }
                        else
                        { Enemy.MoveEnemy(enemyRiderList[i]); }
                    }
                }
                 DrawEntities();
                Thread.Sleep(20);///
                HUD.plStats();
            }

            if ((map._mapsCurrent[player._y][player._x] == 'X') || (player._health == 0))
            {
                if (player._health == 0)
                {  HUD.plDied();  }
                if (map._mapsCurrent[player._y][player._x] == 'X')
                { 
                    isPlaying = false;
                    HUD.plWin();
                }
            }
            HUD.Farewell();
        }
        public static void WriteTileWithColor(char tile) //colours the map tiles and writes them to screen
        {
            if (tile == '%')
            { Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.DarkRed; }
            else if (tile == 'w')
            { Console.ForegroundColor = ConsoleColor.DarkCyan; Console.BackgroundColor = ConsoleColor.Blue; }
            else if (tile == '#')
            { Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.DarkGray; }
            else if (tile == ',')
            { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.BackgroundColor = ConsoleColor.Yellow; }
            else if (tile == '^')
            { Console.ForegroundColor = ConsoleColor.DarkGreen; Console.BackgroundColor = ConsoleColor.Green; }
            else if (tile == '[')
            { Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.Gray; }
            else if (tile == ']')
            { Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.Gray; }
            else if (tile == 'M')
            { Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.Gray; }
            else if (tile == '{')
            { Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.BackgroundColor = ConsoleColor.Magenta; }
            else if (tile == '}')
            { Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.BackgroundColor = ConsoleColor.Magenta; }
            else if (tile == 'X')
            { Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Gray; }
            else if (tile == '.')
            { Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.DarkGray; }
            else if (tile == '`')
            { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.BackgroundColor = ConsoleColor.Yellow; }
            else Console.ForegroundColor = ConsoleColor.White;

            Console.Write(tile);
            Console.ResetColor();
        }
 /*>>>>>>*/ public static void DrawEntities()// draws the player and the enemy symbols/ sprites
        {
            if (Program.map._currentMapIndex == 0)
            {
                foreach (var enmy in enemiesMap1)
                {
                    if (enmy._health > 0) // Only draw if alive
                    {
                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Console.ForegroundColor = enmy._color;
                        Console.Write(enmy._symbol);
                    }
                }
            }

            if (Program.map._currentMapIndex == 1)
            {
                foreach (var enmy in enemiesMap2)
                {
                    if (enmy._health > 0) // Only draw if alive
                    {
                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Console.ForegroundColor = enmy._color;
                        Console.Write(enmy._symbol);
                    }
                }
            }
            if (Program.map._currentMapIndex == 2)
            {
                foreach (var enmy in enemiesMap3)
                {
                    if (enmy._health > 0) // Only draw if alive
                    {
                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Console.ForegroundColor = enmy._color;
                        Console.Write(enmy._symbol);
                    }
                }
            }
            if (Program.map._currentMapIndex == 3)
            {
                MyEvents.AmbushMapCheck();
                //foreach (var enmy in enemyRiderList)
                //{
                //    if (enmy._health > 0) // Only draw if alive
                //    {
                //        Console.SetCursorPosition(enmy._x, enmy._y);
                //        Console.ForegroundColor = enmy._color;
                //        Console.Write(enmy._symbol);
                //    }
                //}
            }
            Console.SetCursorPosition(player._x, player._y);
            Console.ForegroundColor = player._color;
            Console.Write(player._symbol);
            Console.ResetColor();
        }
    }
}


