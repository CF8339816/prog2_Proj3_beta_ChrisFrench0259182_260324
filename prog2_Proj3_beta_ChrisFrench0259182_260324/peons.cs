using System;
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
        public static int _peonCount=9;
        public static int _peon_x_pos;
        public static int _peon_y_pos;
        public static (int, int) _peon_min_max_x = (8, 46);
        public static (int, int) _peon_min_max_y = (8, 21);
        public static int _defeated;

        //public static List<(int x, int y)> _prisonerLocations = new List<(int, int)>();

        public Peons(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack, symbol, hp, color)
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

            if (!Program.MapCaptiveRegistry.ContainsKey(currentMap))// onlly spawns new list if map never visited otherwise holds locations of uncolllected captives
            {
                List<(int x, int y)> Peon = new List<(int x, int y)>();
                for (int i = 0; i < _peonCount; i++)
                {
                    int capSpawnX, capSpawnY;
                    bool valid = false;
                    while (!valid)
                    {
                        capSpawnX = _peonSpawn.Next(_peon_min_max_x.Item1, _peon_min_max_x.Item2 + 1);///
                        capSpawnY = _peonSpawn.Next(_peon_min_max_y.Item1, _peon_min_max_y.Item2 + 1);///

                        if (!Program.IsTileOccupied(capSpawnX, capSpawnY))
                        {
                            Peon.Add((capSpawnX, capSpawnY));
                            valid = true;
                        }
                    }
                }
                Program.MapPeonRegistry[currentMap] = Peon;
            }
                        
            foreach (var slaves in Program.MapCaptiveRegistry[currentMap])// checks the dictionary to draw from fro the currerent map
            {
                Console.SetCursorPosition(slaves.x, slaves.y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("S");
            }
            Console.ResetColor();
        }
        public static void CheckCapCollection()
        {
            int currentMap = Program.map._currentMapIndex;
            if (!Program.MapCaptiveRegistry.ContainsKey(currentMap)) return;

            var peon = Program.MapCaptiveRegistry[currentMap];

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
    }
}
