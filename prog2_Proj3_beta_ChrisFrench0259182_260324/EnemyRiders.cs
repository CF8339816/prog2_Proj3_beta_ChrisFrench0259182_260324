using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    internal class EnemyRiders : Character
    {
        public EnemyRiders(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack, symbol, hp, color)
        {
        }


        //public static void MoveTowards(Program.player._x, Program.player._y)
        public static void MoveTowards(EnemyRiders enemyRider)
        {
           
            int nextX = enemyRider._x;
            int nextY = enemyRider._y;

            if (enemyRider._x < Program.player._x) nextX++;
            else if (enemyRider._x > Program.player._x) nextX--;

            if (enemyRider._y < Program.player._y) nextY++;
            else if (enemyRider._y > Program.player._y) nextY--;

            bool isPathBlockedByEnemy = false;

            foreach (EnemyRiders rideOther in Program.enemyRiderList)
            {
                if (rideOther != enemyRider && nextX == rideOther._x && nextY == rideOther._y)
                {
                    isPathBlockedByEnemy = true;
                    break;
                }
            }

            char targetTile = Program.map._mapsCurrent[nextY][nextX];

            if (!isPathBlockedByEnemy && !Program.IsTileOccupied(nextX, nextY) && targetTile != '%' && targetTile != '^' && targetTile != 'w' && targetTile != 'M' && (nextX != Program.player._x || nextY != Program.player._y))
            {
                Console.SetCursorPosition(enemyRider._x, enemyRider._y);
                char oldTile = Program.map._mapsCurrent[enemyRider._y][enemyRider._x];
                Program.WriteTileWithColor(oldTile);

                enemyRider._x = nextX;
                enemyRider._y = nextY;

                Console.SetCursorPosition(enemyRider._x, enemyRider._y);
                Console.ForegroundColor = enemyRider._color;
                Console.Write(enemyRider._symbol);
                Console.ResetColor();
            }
        }


    }
}
