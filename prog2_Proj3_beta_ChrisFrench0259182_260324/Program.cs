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
        public static int plaAtkUP = 15;
        public static int plaMaxHP = 50;
        public static Player player = new Player(" ", 3, 3, plaAtkUP, '!', plaMaxHP, ConsoleColor.Blue);
        public static List<Enemy> enemies = new List<Enemy>();

        public static LoadMap map = new LoadMap();

        public static bool isPlaying = true;

        //public static int nextX;
        //public static int nextY;

      
        public static bool isAlly = false; //sets bool to check for other allies in movement path
        public static bool IsTileOccupied(int x, int y)
        {

            // moved the  tile check here  to see if it would stop the treasure and  captive spawns in the lava

            char targetTile = Program.map._mapsCurrent[y][x];
            char[] forbiddenTiles = { '#', 'w', '%',  '*' };//'S', '$', '&', 'O', 'H', '@', '!',
            if (Array.Exists(forbiddenTiles, t => t == targetTile))
            {
                return true;
            }

            // Check if player  is there
            if (x == Program.player._x && y == Program.player._y)
            {
                return true;
            }
            // check for enemmies
            if (Program.enemies.Any(enmy => enmy._x == x && enmy._y == y))
            {
                return true;
            }
            // Check for gold spawn
            if (Treasure.activeGoldPiles.Any(g => g.x == x && g.y == y))
            {
                return true;
            }
            // Check there is already a captive there
            if (Captive._prisonerLocations.Any(p => p.x == x && p.y == y))
            {
                return true;
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

            Console.SetCursorPosition(60, 0);
            Console.WriteLine("map drawn");
            Console.ReadKey(true);


            MyEvents.MapCheck();

            Console.SetCursorPosition(60, 0);
            Console.WriteLine("map checked");
            Console.ReadKey(true);

            HUD.Instructions();

            Console.SetCursorPosition(60, 0);
            Console.WriteLine("instructions written");
            Console.ReadKey(true);

            
            Treasure.treasure_min_max_x = (8, 46);
            Treasure.treasure_min_max_y = (8, 21);
            Treasure.DrawGold();
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("gold drawn");
            Console.ReadKey(true);

            Captive._prisoner_min_max_x = (8, 46);
            Captive._prisoner_min_max_y = (8, 21);

            Captive.DrawPrisoner();


                Console.SetCursorPosition(60, 0);
                Console.WriteLine("prisoner drawn");
                Console.ReadKey(true);

           
            enemies.Add(new Enemy("Gobbo", 50, 4, 10, '&', 25, ConsoleColor.Green));
            enemies.Add(new Enemy("Slobbo", 20, 23, 8, '&', 20, ConsoleColor.Green));
            enemies.Add(new Enemy("Orcus", 15, 13, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemies.Add(new Enemy("Boss Hobbo", 49, 20, 15, 'H', 40, ConsoleColor.DarkYellow));
            enemies.Add(new Enemy("testo", 3, 10, 0, '☺', 1, ConsoleColor.DarkGray));

            Console.SetCursorPosition(60, 0);
            Console.WriteLine("enemies added to  list");
            Console.ReadKey(true);


            //GameManager.PlayGame();
            while (isPlaying)
            {
                Console.SetCursorPosition(60, 0);
                Console.WriteLine("Loop running");
                //Console.ReadKey(true);


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

               player.Move(plX, plY);

                Console.SetCursorPosition(60, 0);
                Console.WriteLine("Player Move");
              //  Console.ReadKey(true);
                Treasure.CheckTreasureCollection();
               // Captive.CheckCapCollection();

                if (map._mapsCurrent[player._y][player._x] == 'X')
                {
                    isPlaying = false;
                    continue; //skips past rest
                }
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    if (enemies[i]._health <= 0)
                    {
                        Console.Beep(300, 100);
                        Console.Beep(200, 150);
                        Console.SetCursorPosition(enemies[i]._x, enemies[i]._y);
                        WriteTileWithColor(map._mapsCurrent[enemies[i]._y][enemies[i]._x]);
                        enemies.RemoveAt(i);

                    }
                    else
                    {
                        Enemy.MoveEnemy(enemies[i]);
                    }
                }

                DrawEntities();

                HUD.plStats();

            }


            if ((map._mapsCurrent[player._y][player._x] == 'X') || (player._health == 0))
            {
                if (player._health == 0)
                {
                    HUD.plDied();
                }

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

            else Console.ForegroundColor = ConsoleColor.White;

            Console.Write(tile);
            Console.ResetColor();
        }

      

        public static void DrawEntities()// draws the player and the enemy symbols/ sprites
        {

            foreach (var enmy in enemies)
            {
                if (enmy._health > 0) // Only draw if alive
                {
                    Console.SetCursorPosition(enmy._x, enmy._y);
                    Console.ForegroundColor = enmy._color;
                    Console.Write(enmy._symbol);
                }
            }


            Console.SetCursorPosition(player._x, player._y);
            Console.ForegroundColor = player._color;
            Console.Write(player._symbol);
            Console.ResetColor();
        }







       
    }

}


