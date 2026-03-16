using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class HUD
    {

        public static void alias()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("What is your character's name");
            Console.ForegroundColor = ConsoleColor.Blue;
            Program.Name = Console.ReadLine();
            while (true)
            {
                if (Program.Name.Length <= Program.MaxNameLLength) break;

                Console.WriteLine($"Error: Input is too long! please limit to 15 characters({Program.Name.Length}/{Program.MaxNameLLength})");
             alias();
            }
           
        Console.ResetColor();
        }

        public static void combat()
        {
            foreach (var enmy in Program.enemies)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(60, 14);
                Console.WriteLine($" {enmy._name} takes {Program.player._attack} points of combat damage");
                Console.SetCursorPosition(60, 15);
                Console.WriteLine($" {enmy._name} has {enmy._health} health...");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(60, 17);
                Console.WriteLine($" {Program.player._name} takes {enmy._attack} points of combat damage");
                Console.SetCursorPosition(60, 18);
                Console.WriteLine($" {Program.player._name} has {Program.player._health} health...");
            }
        }

        public static void Instructions()
        {
            Console.SetCursorPosition(0, 27);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any Key to start... Use W,A,S,D  or arrow keys to move around the map...Press 'Q' to exit...\nFight enemies '&' by manouvering to them or try to avoid them... Lava '%' will damage you ");
            Console.ResetColor();
        }
        public static void plStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(60, 1);
            Console.WriteLine($" Name:{Program.player._name} Health:{Program.player._health}/{Program.plaMaxHP} XP: {Player.plXP} Level:{Player.plLevel}");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine($"Gold:{Treasure._gold} Captives Freed:{Captive._freed}");
            Console.ResetColor();
        }
        public static void plDied()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(60, 23);// outputs player death and end of game prompts to exit
            Console.WriteLine($" {Program.player._name} has {Program.player._health} health, {Program.player._name} has died with {Treasure._gold} golds on them");
            Console.ReadKey(true);
            Console.ResetColor();
        }

        public static void plWin()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(60, 22);// outputs player death and end of game prompts to exit
            Console.WriteLine($" {Program.player._name} has reached the goal with {Program.player._health} health, ");
            Console.SetCursorPosition(60, 23);
            Console.WriteLine($"{Program.player._name} is safe with {Treasure._gold} golds and freed {Captive._freed} captives.");
            Console.ReadKey(true);
            Console.ResetColor();
        }


        public static void Farewell()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(60, 24);
            Console.WriteLine(" please come back soon");
            Console.ReadKey(true);
            Console.SetCursorPosition(60, 25);
            Console.WriteLine(" please press any key to exit");
            Console.ReadKey(true);
            Console.WriteLine("\n\n\n\n\n\n");
            Console.ResetColor();
            
        }

        public static void Looter()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(60, 5);
            Console.WriteLine($" {Program.player._name} loots {Treasure.loot} amounts of golds! ");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine($"{Program.player._name} now has {Treasure._gold} gold...woooo!");

            Console.ResetColor();
        }

        public static void Moses()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.SetCursorPosition(60, 4);
            Console.WriteLine($"{Program.player._name} has freed a captive... Good Job!");

            Console.ResetColor();
        }

        public static void AnakinMustafar()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(60, 8);
            Console.WriteLine($" {Program.player._name} takes 30 points of lava damage");
            Console.SetCursorPosition(60, 9);
            Console.WriteLine($" {Program.player._name} now has {Program.player._health} HP");
            Console.ResetColor();
        }




    }
}
