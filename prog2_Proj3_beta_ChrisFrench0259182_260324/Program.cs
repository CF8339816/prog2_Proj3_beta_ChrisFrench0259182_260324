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

        public static Player player = new Player(" ", 3, 3, plaAtkUP, '!', 50, ConsoleColor.Blue);
        public static List<Enemy> enemies = new List<Enemy>();

        public static LoadMap map = new LoadMap();

        public static int unicodeValue = 219;
        public static char Q = (char)unicodeValue;

        public static bool isPlaying = true;

        public static int nextX;
        public static int nextY;

      
        public static bool isAlly = false; //sets bool to check for other allies in movement path

       

        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            HUD.alias();
            Console.Clear();
            Console.CursorVisible = false;

            Console.CursorVisible = false;
            map.DrawMap();
            HUD.Instructions();

            enemies.Add(new Enemy("Gobbo", 50, 4, 10, '&', 25, ConsoleColor.Green));
            enemies.Add(new Enemy("Slobbo", 20, 23, 8, '&', 20, ConsoleColor.Green));
            enemies.Add(new Enemy("Orcus", 15, 12, 12, 'O', 30, ConsoleColor.DarkGreen));
            enemies.Add(new Enemy("Boss Hobbo", 49, 19, 15, 'H', 40, ConsoleColor.DarkYellow));
            enemies.Add(new Enemy("testo", 4, 10, 0, '\u00DB', 1, ConsoleColor.Cyan));



            while (isPlaying)
            {
                player._name = Name;
                player._attack = plaAtkUP;

                // MovePlayer();

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

              // MyEvents.MyEvents();

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
                    Enemy.MoveEnemy(enemies[i]);
                }
               
                
                DrawEntities();

                CollectSpawner.DrawCollectables();


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


