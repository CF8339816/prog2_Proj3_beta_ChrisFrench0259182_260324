using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Captive
    {

        public static int _prisonerCount = 6;
        public static bool _newPrisoner = true;
        public static Random _prisonerSpawn = new Random();
        public static (int, int) _prisonerLoc = (_prisoner_x_pos, _prisoner_y_pos);
        public static int _prisoner_x_pos;
        public static int _prisoner_y_pos;
        public static (int, int) _prisoner_min_max_x = (9, 45);
        public static (int, int) _prisoner_min_max_y = (7, 20);
        public static int _captives = 0;

          public static List<(int, int)> _prisonerLocations = new List<(int, int)>();
        public static void DrawPrisoner()
        {
            if (_newPrisoner)
            {
                _prisonerLocations.Clear(); // Clear old positions

                for (int i = 0; i < 8; i++)
                {
                    bool clearPrisonerSpawn = false;



                    while (!clearPrisonerSpawn)
                    {

                        _prisoner_x_pos = _prisonerSpawn.Next(_prisoner_min_max_x.Item1, _prisoner_min_max_x.Item2 + 1);
                        _prisoner_y_pos = _prisonerSpawn.Next(_prisoner_min_max_y.Item1, _prisoner_min_max_y.Item2 + 1);
                        char targetTile = Program.map._mapsCurrent[Program.nextY][Program.nextX];

                        if (Program.map.CanMoveTo(_prisoner_x_pos, _prisoner_y_pos) && targetTile != '%' && targetTile != 'w' && targetTile != '#' && targetTile != '$')
                        {

                            if (_prisoner_x_pos != Program.player._x || _prisoner_y_pos != Program.player._y) //checks for player
                            {
                                clearPrisonerSpawn = true;
                            }

                        }
                    }

                    _prisonerLoc = (_prisoner_x_pos, _prisoner_x_pos);
                    Console.SetCursorPosition(_prisoner_x_pos, _prisoner_y_pos);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("S");
                    Console.ResetColor();


                }
                _newPrisoner = false;
                Console.ResetColor();
            }
        }


        public static void CheckCollection()
        {
            // Check if player is standing on any prisoner's location
            for (int i = 0; i < _prisonerLocations.Count; i++)
            {
                //>>>>>>>>>>>>           //if (Program.player._x == _prisoner_x_pos && Program.player._y == _prisoner_x_pos)
                //if (Program.player._x != _prisonerLocations[i].x || Program.player._y != _prisonerLocations[i].y)
                //{
                //    continue;
                //}
                if (Captive._prisonerLocations.Contains((Program.player._x, Program.player._y)))



                    _prisonerLocations.RemoveAt(i);

                _captives++;
                Player.plXP += 10;
                Buffs.IncreaseATK(0);
                Buffs.IncreaseMaxHealth(0);
                Console.SetCursorPosition(Program.player._x, Program.player._y);
                Console.Write(" ");

                break;
            }
        }
    }
}
