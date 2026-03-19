using System;
using System.CodeDom;
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
        public static List<Enemypeon> enemiesMap1 = new List<Enemypeon>();
        public static List<Enemypeon> enemiesMap2 = new List<Enemypeon>();
        public static List<Enemypeon> enemiesMap3 = new List<Enemypeon>();
        public static List<EnemyBoss> enemyBoss = new List<EnemyBoss>();
        public static List<EnemyRiders> enemyRiderList = new List<EnemyRiders>();
        public static LoadMap map = new LoadMap();
        public static Dictionary<int, List<(int x, int y)>> MapTreasureRegistry = new Dictionary<int, List<(int x, int y)>>();// dictionary set up to track treasure per map to prevent respawn when going back to map after leaving 
        public static Dictionary<int, List<(int x, int y)>> MapCaptiveRegistry = new Dictionary<int, List<(int x, int y)>>();// dictionary set up to track Captives per map to prevent respawn when going back to map after leaving 
        public static Dictionary<int, List<(int x, int y)>> MapOrbRegistry = new Dictionary<int, List<(int x, int y)>>();
        public static bool isPlaying = true;

        public static bool isAlly = false; //sets bool to check for other allies in movement path
        public static bool IsTileOccupied(int x, int y)
        {
            // moved the  tile check here  to see if it would stop the treasure and  captive spawns in the lava
            int currentMap = Program.map._currentMapIndex;// checks using info from current map
            char targetTile = Program.map._mapsCurrent[y][x];
            char[] forbiddenTiles = { '#', 'w', '%', '|', 'M', '-', '+' };//, 'S', '$', '&', 'O', 'H', '@', '!','*'
            if (Array.Exists(forbiddenTiles, t => t == targetTile))
            { return true; }
            // Check if player  is there
            if (x == Program.player._x && y == Program.player._y)
            { return true; }
            // check for enemmies
            if (Program.enemiesMap1.Any(enmy => enmy._x == x && enmy._y == y))
            { return true; }
            // Check for gold spawn using current map's dictionary list
            if (Program.MapTreasureRegistry.ContainsKey(currentMap))
            {
                if (Program.MapTreasureRegistry[currentMap].Any(g => g.x == x && g.y == y))/// checks positions from dictionary for current map
                { return true; }
            }
            if (Program.MapOrbRegistry.ContainsKey(currentMap))
            {
                if (Program.MapOrbRegistry[currentMap].Any(g => g.x == x && g.y == y))/// checks positions from dictionary for current map
                { return true; }
            }

            // Check there is already a captive there using current dictionary list for current map
            if (Program.MapCaptiveRegistry.ContainsKey(currentMap))
            {
                if (Program.MapCaptiveRegistry[currentMap].Any(p => p.x == x && p.y == y))
                { return true; }
            }
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

            enemiesMap1.Clear();
            enemiesMap1.Add(new Enemypeon("Gobbo", 50, 4, 10, '&', 25, ConsoleColor.Green));
            enemiesMap1.Add(new Enemypeon("Slobbo", 20, 23, 8, '&', 20, ConsoleColor.Green));
            enemiesMap1.Add(new Enemypeon("Orcus", 15, 13, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemiesMap1.Add(new Enemypeon("Lawbbo", 6, 22, 10, '&', 25, ConsoleColor.Green));
            enemiesMap1.Add(new Enemypeon("Rawbbo", 8, 23, 8, '&', 20, ConsoleColor.Green));
            enemiesMap1.Add(new Enemypeon("Morcus", 10, 2, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemiesMap1.Add(new Enemypeon("Pawbbo", 24, 2, 10, '&', 25, ConsoleColor.Green));
            enemiesMap1.Add(new Enemypeon("Steve", 31, 8, 8, '&', 20, ConsoleColor.Green));


            enemiesMap2.Clear();
            enemiesMap2.Add(new Enemypeon("Gnolie", 4, 4, 16, 'g', 25, ConsoleColor.Red));
            enemiesMap2.Add(new Enemypeon("Gnawlie", 5, 20, 18, 'g', 20, ConsoleColor.Red));
            enemiesMap2.Add(new Enemypeon("ZugZug", 31, 12, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemiesMap2.Add(new Enemypeon("Chawlie", 10, 12, 16, 'g', 25, ConsoleColor.Red));
            enemiesMap2.Add(new Enemypeon("WaAAAlieeEE", 7, 9, 18, 'g', 20, ConsoleColor.Red));
            enemiesMap2.Add(new Enemypeon("Gabbo", 25, 2, 10, '&', 25, ConsoleColor.Green));
            enemiesMap2.Add(new Enemypeon("Slabbo", 51, 3, 8, '&', 20, ConsoleColor.Green));
            enemiesMap2.Add(new Enemypeon("Orcus", 6, 20, 12, 'O', 30, ConsoleColor.DarkGreen));

            enemiesMap3.Clear();
            enemiesMap3.Add(new Enemypeon("Bammo", 17, 6, 10, 'O', 25, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemypeon("Slammo", 17, 23, 8, 'O', 20, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemypeon("Ogrelet", 37, 10, 20, 'Q', 60, ConsoleColor.Yellow));
            enemiesMap3.Add(new Enemypeon("Gobby", 9, 5, 10, '&', 25, ConsoleColor.Green));
            enemiesMap3.Add(new Enemypeon("Slobby", 20, 6, 8, '&', 20, ConsoleColor.Green));
            enemiesMap3.Add(new Enemypeon("Dorcus", 38, 12, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemypeon("Wammo", 28, 23, 10, 'O', 25, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemypeon("Shammo", 21, 22, 8, 'O', 20, ConsoleColor.DarkGreen));
            enemiesMap3.Add(new Enemypeon("Ogrelot", 52, 3, 20, 'Q', 60, ConsoleColor.Yellow));


            enemyBoss.Add(new EnemyBoss("Boss Hobbo", 49, 20, 15, 'H', 40, ConsoleColor.DarkYellow));
            enemyBoss.Add(new EnemyBoss("Boss Gobstomper", 45, 22, 15, 'G', 40, ConsoleColor.DarkRed));
            enemyBoss.Add(new EnemyBoss("Boss Drowkus", 48, 23, 25, 'D', 80, ConsoleColor.DarkMagenta));


            while (isPlaying)
            {
                HUD.Instructions();
                player._name = Name;
                player._attack = plaAtkUP;

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
                PowerOrb.CheckOrbCollection();
                /*>>>>>>*/
                var newSpawn = map.MapChanger(player._x, player._y); //references the map changer function

                if (newSpawn.HasValue) //changes maps if triggers are found
                {
                    // sets player position to new spawn point 
                    player._x = newSpawn.Value.x;
                    player._y = newSpawn.Value.y;
                    //Treasure._goldTreasure = true;
                    //Captive._newPrisoner = true;
                }
                CollectSpawner.SetupMapAssets();

                EnviroHeal.SpringWatterHealling();
                EnviroDmg.LavaDamage();
                if (map._mapsCurrent[player._y][player._x] == 'X')
                {
                    isPlaying = false;
                    continue; //skips past rest
                }

                if (Program.map._currentMapIndex == 0)
                {
                    if (enemyBoss.Count > 0) // checks count to prevent crash
                    {
                        var boss = enemyBoss[0]; // First entry in the boss list

                        if (boss._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(boss._x, boss._y);
                            WriteTileWithColor(map._mapsCurrent[boss._y][boss._x]);
                            enemyBoss.RemoveAt(0); // Remove the first boss
                        }
                        else
                        {
                            EnemyBoss.MoveEnemy(boss);
                        }
                    }

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
                        { Enemypeon.MoveEnemy(enemiesMap1[i]); }
                    }
                }


                if (Program.map._currentMapIndex == 1)
                {

                    if (enemyBoss.Count > 0) // checks count to prevent crash
                    {
                        var boss = enemyBoss[1]; // First entry in the boss list

                        if (boss._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(boss._x, boss._y);
                            WriteTileWithColor(map._mapsCurrent[boss._y][boss._x]);
                            enemyBoss.RemoveAt(1); // Remove the first boss
                        }
                        else
                        {
                            EnemyBoss.MoveEnemy(boss);
                        }
                    }


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
                        { Enemypeon.MoveEnemy(enemiesMap2[i]); }
                    }
                }



                if (Program.map._currentMapIndex == 2)
                {

                    if (enemyBoss.Count > 0) // checks count to prevent crash
                    {
                        var boss = enemyBoss[2]; // First entry in the boss list

                        if (boss._health <= 0)
                        {
                            Console.Beep(300, 100);
                            Console.Beep(200, 150);
                            Console.SetCursorPosition(boss._x, boss._y);
                            WriteTileWithColor(map._mapsCurrent[boss._y][boss._x]);
                            enemyBoss.RemoveAt(2); // Remove the first boss
                        }
                        else
                        {
                            EnemyBoss.MoveEnemy(boss);
                        }
                    }

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
                        { Enemypeon.MoveEnemy(enemiesMap3[i]); }
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
                        { EnemyRiders.MoveTowards(enemyRiderList[i]); }
                    }
                }
                DrawEntities();
                Thread.Sleep(20);///
                HUD.plStats();
            }

            if ((map._mapsCurrent[player._y][player._x] == 'X') || (player._health == 0))
            {
                if (player._health == 0)
                { HUD.plDied(); }
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
        /*>>>>>>*/
        public static void DrawEntities()// draws the player and the enemy symbols/ sprites
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
                if (Program.enemyBoss.Count > 0)
                {
                    var boss = Program.enemyBoss[0];// grabs first boss

                    if (boss._health > 0) // Only draw if alive
                    {
                        Console.SetCursorPosition(boss._x, boss._y);
                        Console.ForegroundColor = boss._color;
                        Console.Write(boss._symbol);
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
                    if (Program.enemyBoss.Count > 0)
                    {
                        var boss = Program.enemyBoss[1];// grabs second boss

                        if (boss._health > 0) // Only draw if alive
                        {
                            Console.SetCursorPosition(boss._x, boss._y);
                            Console.ForegroundColor = boss._color;
                            Console.Write(boss._symbol);
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
                    if (Program.enemyBoss.Count > 0)
                    {
                        var boss = Program.enemyBoss[2];// grabs third boss

                        if (boss._health > 0) // Only draw if alive
                        {
                            Console.SetCursorPosition(boss._x, boss._y);
                            Console.ForegroundColor = boss._color;
                            Console.Write(boss._symbol);
                        }
                    }
                }
                if (Program.map._currentMapIndex == 3)
                {
                    MyEvents.AmbushMapCheck();

                }
                Console.SetCursorPosition(player._x, player._y);
                Console.ForegroundColor = player._color;
                Console.Write(player._symbol);
                Console.ResetColor();
            }
        }
    }
}


