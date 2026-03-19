using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Peons : Character
    {

        public static bool _newPeon = true;
        public static Random _peonSpawn = new Random();
        public static int _peonCount = 9;
        public static int _peon_x_pos;
        public static int _peon_y_pos;
        public static (int, int) _peon_min_max_x = (8, 46);
        public static (int, int) _peon_min_max_y = (8, 21);
        public static int _defeated;
       // public static Random _rando = new Random();
 

        public Peons(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base("Peon", x, y, 2, '6', 3, ConsoleColor.Green)
        {
            Name = "hostage";
            _peonCount = 9;
            _peon_x_pos = x;
            _peon_y_pos = y;
            _peon_min_max_x = (8, 46);
            _peon_min_max_y = (8, 21);

        }

        public static void DrawPeon()
        {
            int currentMap = Program.map._currentMapIndex;

            if (!Program.MapPeonRegistry.ContainsKey(currentMap))// onlly spawns new list if map never visited otherwise holds locations of uncolllected captives
            {
                List<(int x, int y)> Peon = new List<(int x, int y)>();
                for (int i = 0; i < _peonCount; i++)
                {
                    int peonSpawnX, peonSpawnY;
                    bool valid = false;
                    while (!valid)
                    {
                        peonSpawnX = _peonSpawn.Next(_peon_min_max_x.Item1, _peon_min_max_x.Item2 + 1);///
                        peonSpawnY = _peonSpawn.Next(_peon_min_max_y.Item1, _peon_min_max_y.Item2 + 1);///

                        if (!Program.IsTileOccupied(peonSpawnX, peonSpawnY))
                        {
                            Peon.Add((peonSpawnX, peonSpawnY));
                            valid = true;
                        }
                    }
                }
                Program.MapPeonRegistry[currentMap] = Peon;
            }

            foreach (var peons in Program.MapPeonRegistry[currentMap])// checks the dictionary to draw from fro the currerent map
            {
                Console.SetCursorPosition(peons.x, peons.y);
              
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('6');
            }
            Console.ResetColor();
        }
        public static void CheckPeonCollection()
        {
            int currentMap = Program.map._currentMapIndex;
            if (!Program.MapPeonRegistry.ContainsKey(currentMap)) return;

            var peon = Program.MapPeonRegistry[currentMap];

            for (int i = peon.Count - 1; i >= 0; i--)
            {
                if (Program.player._x == peon[i].x && Program.player._y == peon[i].y)
                {
                    Program.player._health -= 2;
                    Buffs.IncreaseXP(5);

                    Treasure._gold += 1;
                    HUD.PeonSmite();

                    peon.RemoveAt(i);// remove Captives from map list
                }
            }
        }



        //public static void MoveEnemy(Peons Peon)
        //{
        //    //Thread.Sleep(40);
        //    int nextX = Peon._x;
        //    int nextY = Peon._y;
           
        //    int nextRandX = Peon._x + _rando.Next(-1, 2); //randomises mocve on x
        //    int nextRandY = Peon._y + _rando.Next(-1, 2); // randomises moves on y
        //    nextX = nextRandX;
        //    nextY = nextRandY;
        //    ///
        //    if (Peon._x + 2 <= Program.player._x || Peon._x - 2 <= Program.player._x || Peon._y + 2 <= Program.player._y || Peon._y - 2 <= Program.player._y)
        //    {
        //        char targetTile = Program.map._mapsCurrent[nextY][nextX];
        //        if (!Program.IsTileOccupied(nextX, nextY) && targetTile != '*' && targetTile != '!' && targetTile != '#' && targetTile != 'S' && targetTile != '$' && targetTile != 'w' && targetTile != '%' && targetTile != '@')
        //        {
        //            Console.SetCursorPosition(Peon._x, Peon._y);
        //            char oldTile = Program.map._mapsCurrent[Peon._y][Peon._x];
        //            Program.WriteTileWithColor(oldTile);

        //            if (Peon._x < Program.player._x) nextX++;
        //            else if (Peon._x > Program.player._x) nextX--;

        //            if (Peon._y < Program.player._y) nextY++;
        //            else if (Peon._y > Program.player._y) nextY--;


        //            bool isPathBlockedByPeon = false;
        //            int currentMap = Program.map._currentMapIndex;

        //            if (Program.MapPeonRegistry.ContainsKey(currentMap))
        //            {
        //                foreach (var otherPeon in Program.MapPeonRegistry[currentMap])  // checks the dictionary to ensure peon is not stepping on other peon
        //                {

        //                    if (nextX == otherPeon.x && nextY == otherPeon.y)
        //                    {
        //                        isPathBlockedByPeon = true;
        //                        break;
        //                    }
        //                }
        //            }

        //            bool isPathBlockedByEnemy = false;
        //            foreach (EnemyLeader enmy in Program.enemiesMap1)
        //                foreach (EnemyLeader other in Program.enemiesMap1)
        //                {
        //                    if (other != enmy && nextX == other._x && nextY == other._y)
        //                    {
        //                        isPathBlockedByEnemy = true;
        //                        break;
        //                    }
        //                }
                    
          
        //            if (!isPathBlockedByEnemy && !isPathBlockedByPeon && Program.map.CanMoveTo(nextX, nextY) && targetTile != '%' && targetTile != 'S' && targetTile != '$' && targetTile != 'w' && targetTile != '#' && (nextX != Program.player._x || nextY != Program.player._y))
        //            {
        //                for (int i = 0; i < Program.MapPeonRegistry[currentMap].Count; i++)// ensures the peon's new position is tracked for checking and not its sapwn point
        //                {
        //                    if (Program.MapPeonRegistry[currentMap][i].x == Peon._x &&
        //                        Program.MapPeonRegistry[currentMap][i].y == Peon._y)
        //                    {
        //                        Program.MapPeonRegistry[currentMap][i] = (nextX, nextY);
        //                        break;
        //                    }
        //                }

        //                Peon._x = nextX;
        //                Peon._y = nextY;

        //                Console.SetCursorPosition(Peon._x, Peon._y);
        //                Console.ForegroundColor = Peon._color;
        //                Console.Write(Peon._symbol);
        //                Console.ResetColor();
        //            }
        //        }
        //    }

        //}





    }
}
