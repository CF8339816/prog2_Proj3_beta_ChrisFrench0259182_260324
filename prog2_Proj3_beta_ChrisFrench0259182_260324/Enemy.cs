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
            Thread.Sleep(75);
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

            if (Program.map.CanMoveTo(nextX, nextY) && targetTile != '%' && (nextX != Program.player._x || nextY != Program.player._y) && targetTile != 'w' && targetTile != '#')

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
    }







}
