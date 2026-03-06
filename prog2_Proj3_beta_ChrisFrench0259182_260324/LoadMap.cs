using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class LoadMap
    {
        private string _filepath1 = "map1.txt";
        private string _filepath2 = "map2.txt";
        private string _filepath3 = "map3.txt";
        private string _filepath4 = "map4.txt";
        public string[] _maps1;
        public string[] _maps2;
        public string[] _maps3;
        public string[] _maps4;
        public string[] _mapsCurrent;





        _mapsCurrent= _maps1;


        public void MapLoader()
        {
            try
            {// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
                _maps1 = File.ReadAllLines(_filepath1);
                Console.Clear(); // Ensure screen is fresh

                foreach (string line in _maps1)
                {
                    foreach (char mapTile in line)
                    {
                        SetTileColor(mapTile);
                        Console.Write(mapTile);
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file '{_filepath1}' was not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            try
            {// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
                _maps2 = File.ReadAllLines(_filepath2);
                Console.Clear(); // Ensure screen is fresh

                foreach (string line in _maps2)
                {
                    foreach (char mapTile in line)
                    {
                        SetTileColor(mapTile);
                        Console.Write(mapTile);
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file '{_filepath2}' was not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            try
            {// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
                _maps3 = File.ReadAllLines(_filepath3);
                Console.Clear(); // Ensure screen is fresh

                foreach (string line in _maps3)
                {
                    foreach (char mapTile in line)
                    {
                        SetTileColor(mapTile);
                        Console.Write(mapTile);
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file '{_filepath1}' was not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            try
            {// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
                _maps4 = File.ReadAllLines(_filepath4);
                Console.Clear(); // Ensure screen is fresh

                foreach (string line in _maps4)
                {
                    foreach (char mapTile in line)
                    {
                        SetTileColor(mapTile);
                        Console.Write(mapTile);
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file '{_filepath4}' was not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        public void MapChanger()
        {

        }

        public void DrawTileAt(int x, int y)// redrawsorigional  map when tile isvacated by  player or enemy.
        {
            if (y < 0 || y >= _mapsCurrent.Length || x < 0 || x >= _mapsCurrent[y].Length) return;

            char tile = _mapsCurrent[y][x];
            Console.SetCursorPosition(x, y);
            SetTileColor(tile);
            Console.Write(tile);
            Console.ResetColor();
        }

        private void SetTileColor(char tile)// sets the tile colors for the map
        {
            switch (tile)
            {
                case '#': Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case '%': Console.ForegroundColor = ConsoleColor.Red; break;
                case 'G': Console.ForegroundColor = ConsoleColor.Cyan; break;
                case '@': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case '*': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case '|': Console.ForegroundColor = ConsoleColor.Yellow; break;
                case '-': Console.ForegroundColor = ConsoleColor.Yellow; break;
                case '+': Console.ForegroundColor = ConsoleColor.Yellow; break;
                case '~': Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 'R': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'e': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 't': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'u': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'r': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'n': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'o': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'N': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'V': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'W': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'H': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'E': Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 'w': Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case '^': Console.ForegroundColor = ConsoleColor.Green; break;
                case ',': Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 'M': Console.ForegroundColor = ConsoleColor.Gray; break;
                case '.': Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case '{': Console.ForegroundColor = ConsoleColor.Gray; break;
                case '}': Console.ForegroundColor = ConsoleColor.Gray; break;
                case '[': Console.ForegroundColor = ConsoleColor.Gray; break;
                case ']': Console.ForegroundColor = ConsoleColor.Gray; break;
                case 'X': Console.ForegroundColor = ConsoleColor.White; break;

                default: Console.ForegroundColor = ConsoleColor.Gray; break;
            }
        }

        public bool CanMoveTo(int tarMapX, int tarMapY)
        {
            // Safety check if map failed to load
            if (_mapsCurrent == null) return false;

            // Boundary Check
            if (tarMapY >= 0 && tarMapY < _mapsCurrent.Length && tarMapX >= 0 && tarMapX < _mapsCurrent[tarMapY].Length)
            {
                char tarTile = _mapsCurrent[tarMapY][tarMapX];

                switch (tarTile)
                {
                    case '#':
                        break;
                    case '|':
                        break;
                    case '-':
                        break;
                    case '+':
                        break;
                    case '{':
                        break;
                    case '}':
                        break;
                    case '[':
                        break;
                    case ']':
                        break;
                    case 'M':
                        break;

                    //return false; // Walls and borders  player  and enemy are blocked
                    case '^':
                        return false; // Walls and borders  player  and enemy are blocked

                    default:
                        return true; // Spaces and Lava are walkable
                }
            }
            return false;
        }






    }
}