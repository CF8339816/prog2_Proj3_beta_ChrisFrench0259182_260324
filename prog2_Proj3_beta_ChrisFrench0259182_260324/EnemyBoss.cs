using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    internal class EnemyBoss : Character
    {
        public EnemyBoss(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack, symbol, hp, color)
        {
        }
        private static Random enRando = new Random();

        public static void MoveEnemy(EnemyBoss enmy)
        {
           
            int nextX = enmy._x;
            int nextY = enmy._y;
            Random _rando = new Random();
            int nextRandX = enmy._x + _rando.Next(-1, 2); //randomises mocve on x
            int nextRandY = enmy._y + _rando.Next(-1, 2); // randomises moves on y
            nextX = nextRandX;
            nextY = nextRandY;
            ///
            if (enmy._x + 2 <= Program.player._x || enmy._x - 2 <= Program.player._x || enmy._y + 2 <= Program.player._y || enmy._y - 2 <= Program.player._y)
            {
                char targetTile = Program.map._mapsCurrent[nextY][nextX];
                if (!Program.IsTileOccupied(nextX, nextY) && targetTile != '*' && targetTile != '!' && targetTile != '#' && targetTile != 'S' && targetTile != '$' && targetTile != 'w' && targetTile != '%' && targetTile != '@')
                {
                    Console.SetCursorPosition(enmy._x, enmy._y);
                    char oldTile = Program.map._mapsCurrent[enmy._y][enmy._x];
                    Program.WriteTileWithColor(oldTile);

                    if (enmy._x < Program.player._x) nextX++;
                    else if (enmy._x > Program.player._x) nextX--;

                    if (enmy._y < Program.player._y) nextY++;
                    else if (enmy._y > Program.player._y) nextY--;

                    bool isPathBlockedByEnemy = false;
                    foreach (EnemyBoss other in Program.enemyBoss)
                    {
                        if (other != enmy && nextX == other._x && nextY == other._y)
                        {
                            isPathBlockedByEnemy = true;
                            break;
                        }
                    }
                    //char targetTile = Program.map._mapsCurrent[nextY][nextX];

                    if (!isPathBlockedByEnemy && Program.map.CanMoveTo(nextX, nextY) && targetTile != '%' && targetTile != 'S' && targetTile != '$' && targetTile != 'w' && targetTile != '#' && (nextX != Program.player._x || nextY != Program.player._y))
                    {
                        enmy._x = nextX;
                        enmy._y = nextY;

                        Console.SetCursorPosition(enmy._x, enmy._y);
                        Console.ForegroundColor = enmy._color;
                        Console.Write(enmy._symbol);
                        Console.ResetColor();
                    }
                }
            }
            ///
            else
            {
                foreach (EnemyBoss other in Program.enemyBoss)
                {
                    if (other != enmy && nextX == other._x && nextY == other._y)
                    {
                        Program.isAlly = true;
                        break;
                    }
                }
                foreach (EnemyBoss other in Program.enemyBoss)
                {
                    if (other != enmy && nextX == other._x && nextY == other._y)
                    {
                        Program.isAlly = true;
                        break;
                    }
                }
                foreach (EnemyBoss other in Program.enemyBoss)
                {
                    if (other != enmy && nextX == other._x && nextY == other._y)
                    {
                        Program.isAlly = true;
                        break;
                    }
                }

                foreach (EnemyBoss other in Program.enemyBoss)
                {
                    if (other != enmy && nextX == other._x && nextY == other._y)
                    {
                        Program.isAlly = true;
                        break;
                    }
                }

                char targetTile = Program.map._mapsCurrent[nextY][nextX];

                if (!Program.IsTileOccupied(nextX, nextY) && targetTile != '*' && targetTile != '!' && targetTile != 'S' && targetTile != '$' && targetTile != '#' && targetTile != 'w' && targetTile != '%' && targetTile != '@')
                {
                    Console.SetCursorPosition(enmy._x, enmy._y);
                    char oldTile = Program.map._mapsCurrent[enmy._y][enmy._x];
                    Program.WriteTileWithColor(oldTile);

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
}
        



  