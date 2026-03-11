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
        }

        public static void Instructions()
        {
            Console.SetCursorPosition(0, 27);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any Key to start... Use W,A,S,D  or arrow keys to move around the map...Press 'Q' to exit...\nFight enemies '&' by manouvering to them or try to avoid them... Lava '%' will damage you ");

        }
        public static void plStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(60, 2);
            Console.WriteLine($" Name:{Program.player._name} Health:{Program.player._health}/{ XP: {Player.plXP} Gold:{CollectSpawner._gold} Captives Freed:{CollectSpawner._captives}");

        }
        public static void plDied()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(60, 23);// outputs player death and end of game prompts to exit
            Console.WriteLine($" {Program.player._name} has {Program.player._health} health, {Program.player._name} has died with {CollectSpawner._gold} golds on them");
            Console.ReadKey(true);

        }

        public static void plWin()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(60, 22);// outputs player death and end of game prompts to exit
            Console.WriteLine($" {Program.player._name} has reached the goal with {Program.player._health} health, ");
            Console.SetCursorPosition(60, 23);
            Console.WriteLine($"{Program.player._name} is safe with {CollectSpawner._gold} golds and freed {CollectSpawner._captives} captives.");
            Console.ReadKey(true);

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







    }
}
