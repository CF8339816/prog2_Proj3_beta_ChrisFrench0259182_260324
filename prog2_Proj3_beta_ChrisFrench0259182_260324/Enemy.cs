using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Enemy : Character
    {

        public Enemy(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack, symbol, hp, color)
        {
        }
        private static Random enRando = new Random();



        public static void MoveEnemy(Enemy enmy)
        {
            Thread.Sleep(40);
            int nextX = enmy._x;
            int nextY = enmy._y;
            Random _rando = new Random();
            int nextRandX = enmy._x + _rando.Next(-1, 2); //randomises mocve on x
            int nextRandY = enmy._y + _rando.Next(-1, 2); // randomises moves on y
            nextX = nextRandX;
            nextY = nextRandY;


            foreach (Enemy other in Program.enemies)
            {
                if (other != enmy && nextX == other._x && nextY == other._y)
                {
                    Program.isAlly = true;
                    break;
                }
            }

            char targetTile = Program.map._mapsCurrent[nextY][nextX];

            //if (Program.map.CanMoveTo(nextX, nextY) && targetTile != '%' && (nextX != Program.player._x || nextY != Program.player._y) && targetTile != 'w' && targetTile != '#')
                if (!Program.IsTileOccupied(nextX, nextY))
                           {
                Console.SetCursorPosition(enmy._x, enmy._y);
                Console.Write(" ");

                enmy._x = nextX;
                enmy._y = nextY;
            }
            else
            {
                nextX = 0;
                nextY = 0;
            }
        }

         //public static void MoveTowards(Program.player._x, Program.player._y)
        public static void MoveTowards(Enemy enemyRiders)
        {
          
            Thread.Sleep(20);

            int nextX = enemyRiders._x;
            int nextY = enemyRiders._y;

            
            if (enemyRiders._x < Program.player._x) nextX++;
            else if (enemyRiders._x > Program.player._x) nextX--;

        
            if (enemyRiders._y < Program.player._y) nextY++;
            else if (enemyRiders._y > Program.player._y) nextY--;

          
            bool isPathBlockedByEnemy = false;
            foreach (Enemy other in MyEvents.enemyRiderList) 
            {
                if (other != enemyRiders && nextX == other._x && nextY == other._y)
                {
                    isPathBlockedByEnemy = true;
                    break;
                }
            }

            char targetTile = Program.map._mapsCurrent[nextY][nextX];

            if (!isPathBlockedByEnemy &&
                Program.map.CanMoveTo(nextX, nextY) && targetTile != '%' && targetTile != '^' && targetTile != 'w' && targetTile != 'M' && (nextX != Program.player._x || nextY != Program.player._y))
            {
             
                Console.SetCursorPosition(enemyRiders._x, enemyRiders._y);
                Console.Write(" ");


                enemyRiders._x = nextX;
                enemyRiders._y = nextY;

            
                Console.SetCursorPosition(enemyRiders._x, enemyRiders._y);
                Console.ForegroundColor = enemyRiders._color;
                Console.Write(enemyRiders._symbol);
                Console.ResetColor();
            }
        }






    }







}
