using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Player : Character
    {
        public static int plMaxHP = 50;
        public Player(string Name, int x, int y, int attack, char symbol, int hp, ConsoleColor color) : base(Name, x, y, attack: 20, symbol: '!', hp: 50, color: ConsoleColor.Blue)
        {


        }




        public void Move(int X,int Y)
        {
            int nextX =_x + X;
            int nextY = _y + Y;


            bool hitEnemy = false;
            foreach (var enmy in Program.enemies)
            {
                if (nextX == enmy._x && nextY == enmy._y)
                {

                    Console.Beep(800, 50);
                    enmy._health -=_attack;
                   _health -= enmy._attack;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(60, 14);
                    Console.WriteLine($" {enmy._name} takes {_attack} points of combat damage");
                    Console.SetCursorPosition(60, 15);
                    Console.WriteLine($" {enmy._name} has {enmy._health} health...");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(60, 17);
                    Console.WriteLine($" {_name} takes {enmy._attack} points of combat damage");
                    Console.SetCursorPosition(60, 18);
                    Console.WriteLine($" {_name} has {_health} health...");

                    if (_health <= 0 || enmy._health <= 0)
                    {
                        if (_health <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            _health = 0;
                            Console.SetCursorPosition(60, 20);
                            Console.WriteLine($" {_name} has {_health} health, {_name} has died");
                            Program.isPlaying = false;
                        }
                        if (enmy._health <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            enmy._health = 0;
                            Console.SetCursorPosition(60, 21);
                            Console.WriteLine($" {enmy._name} has {enmy._health} health, {enmy._name} has died");
                            Program.isPlaying = true;
                        }
                    }
                }
            }
            if (!hitEnemy && Program.map.CanMoveTo(nextX, nextY))
            {
                Console.SetCursorPosition(_x, _y);
                char oldTile = Program.map._mapsCurrent[_y][_x];
                Program.WriteTileWithColor(oldTile);

                _x = nextX;
                _y = nextY;

                if ((_x, _y) == (Program.treasure_x_pos, Program.treasure_y_pos))// applies lootable gold 
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Program.gold += Program.loot;
                    Console.SetCursorPosition(60, 5);
                    Console.WriteLine($" {_name} loots 15 amounts of golds! ");
                    Console.SetCursorPosition(60, 6);
                    Console.WriteLine($"{_name} now has {Program.gold} gold...woooo!");
                    Program.goldTreasure = true;
                    //DrawGold();
                    CollectSpawner.DrawGold();
                }


                if (Program.prisonerLocations.Contains((_x, _y)))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;


                    Program.captives += 1;


                    Program.prisonerLocations.Remove((_x, _y));

                    Console.SetCursorPosition(60, 4);
                    Console.WriteLine($"{_name} has freed a captive... Good Job!");

                    CollectSpawner.DrawPrisoner();
                }




                if (Program.map._mapsCurrent[_y][_x] == 'w')// applies spring water healing
                {
                    _health += 20;
                    if (_health > plMaxHP)
                    {
                        _health = plMaxHP;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(60, 11);
                    Console.WriteLine($" {_name} Finds cool refreshing sparkling mineral");
                    Console.SetCursorPosition(60, 12);
                    Console.WriteLine($" water and is healed for 20 pts {_name} now has {_health} HP");
                }

                if (Program.map._mapsCurrent[_y][_x] == '%')// applies lava damage 
                {
                    _health = _health - 30;

                    if (_health < 0)
                    {
                        _health = 0;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(60, 8);
                    Console.WriteLine($" {_name} takes 30 points of lava damage");
                    Console.SetCursorPosition(60, 9);
                    Console.WriteLine($" {_name} now has {_health} HP");
                    if (_health == 0)
                    {
                        Program.isPlaying = false;
                    }
                }
                ////>>>>>>>>>>>>>>>>
                //if ((Program.map._mapsCurrent[_y][_x] == '@') || (Program.map._mapsCurrent[_y][_x] == '*'))//portal forward and backward throught the maps  
                //{
                //    var spawnPoint LoadMap.MapChanger(x, y);// should be looking for the reverse symbol so that you spawn at the entrance if going through the exit and the exit if you leave throught the entrance
                //}



            }


        }


    }



      


}




