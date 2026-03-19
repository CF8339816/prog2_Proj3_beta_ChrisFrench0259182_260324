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
    public static void MoveTowards(EnemyRiders enemyRiders)
    {
        Console.SetCursorPosition(enemyRiders._x, enemyRiders._y);
        char oldTile = Program.map._mapsCurrent[enemyRiders._y][enemyRiders._x];
        Program.WriteTileWithColor(oldTile);

        int nextX = enemyRiders._x;
        int nextY = enemyRiders._y;

        if (enemyRiders._x < Program.player._x) nextX++;
        else if (enemyRiders._x > Program.player._x) nextX--;

        if (enemyRiders._y < Program.player._y) nextY++;
        else if (enemyRiders._y > Program.player._y) nextY--;

        bool isPathBlockedByEnemy = false;
        foreach (EnemyRiders other in Program.enemyRiderList)
        {
            if (other != enemyRiders && nextX == other._x && nextY == other._y)
            {
                isPathBlockedByEnemy = true;
                break;
            }
        }

        char targetTile = Program.map._mapsCurrent[nextY][nextX];

        if (!isPathBlockedByEnemy && Program.map.CanMoveTo(nextX, nextY) && targetTile != '%' && targetTile != '^' && targetTile != 'w' && targetTile != 'M' && (nextX != Program.player._x || nextY != Program.player._y))
        {
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
