using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class LoadMap
    {

        private string[] _filePaths = { "map1.txt", "map2.txt", "map3.txt", "map4.txt" };// creates a string of filep[atths to call tto for creatting the string of strings in all maps
        private string[][] _allMaps = new string[4][]; // stores maps in a string of strings so they can be inddexed to move forward and back through them
        public string[] _mapsCurrent; // initiattes ourt usage string for current to draw map from the all maps string
        private int _currentMapIndex = 0; //initiates and sets the default map to display first 

        //private string _filepath1 = "map1.txt";
        //private string _filepath2 = "map2.txt";
        //private string _filepath3 = "map3.txt";
        //private string _filepath4 = "map4.txt";
        //public string[] _maps1;
        //public string[] _maps2;
        //public string[] _maps3;
        //public string[] _maps4;
        //public string[] _mapsCurrent;

        //_mapsCurrent= _maps1;

        public LoadMap()
        {
            MapLoader();
            // Initialize _mapsCurrent to the first map in the array
            if (_allMaps[0] != null)
            {
                _mapsCurrent = _allMaps[0];
            }
        }

        public void MapLoader()
        {
            for (int i = 0; i < _filePaths.Length; i++)
            {
                try
                {
                    _allMaps[i] = File.ReadAllLines(_filePaths[i]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading {_filePaths[i]}: {ex.Message}"); // srror loading messages 
                    
                    _allMaps[i] = new string[] 
                    
                    {
                    "+~~~~~~~~~~~~~~~~ Return to NEVERWHERE ~~~~~~~~~~~~~~~~~+" +
                    "|-------------------------------------------------------|" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                        404                            |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                Your Castle is around                  |" +
                    "|                    another Princess                   |" +
                    "|                          ...                          |" +
                    "|              You had best look elsewhere              |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "|                      A__|./__A                        |" +
                    "|                      ( o Y o )                        |" +
                    "|                      @| ()() |@                       |" +
                    "|                      ;|++++++|;                       |" +
                    "|                        |_@@_|                         |" +
                    "|                                                       |" +
                    "|                                                       |" +
                    "+-------------------------------------------------------+" };// dummy output if map file is broken

                }
            }


            //try
            //{// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
            //    _maps1 = File.ReadAllLines(_filepath1);
            //    Console.Clear(); // Ensure screen is fresh

            //    foreach (string line in _maps1)
            //    {
            //        foreach (char mapTile in line)
            //        {
            //            SetTileColor(mapTile);
            //            Console.Write(mapTile);
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.ResetColor();
            //}
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine($"Error: The file '{_filepath1}' was not found.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
            //try
            //{// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
            //    _maps2 = File.ReadAllLines(_filepath2);
            //    Console.Clear(); // Ensure screen is fresh

            //    foreach (string line in _maps2)
            //    {
            //        foreach (char mapTile in line)
            //        {
            //            SetTileColor(mapTile);
            //            Console.Write(mapTile);
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.ResetColor();
            //}
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine($"Error: The file '{_filepath2}' was not found.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
            //try
            //{// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
            //    _maps3 = File.ReadAllLines(_filepath3);
            //    Console.Clear(); // Ensure screen is fresh

            //    foreach (string line in _maps3)
            //    {
            //        foreach (char mapTile in line)
            //        {
            //            SetTileColor(mapTile);
            //            Console.Write(mapTile);
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.ResetColor();
            //}
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine($"Error: The file '{_filepath1}' was not found.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}

            //try
            //{// recovers map from text file and stores it to an array  hasa check in case of file recovery issue
            //    _maps4 = File.ReadAllLines(_filepath4);
            //    Console.Clear(); // Ensure screen is fresh

            //    foreach (string line in _maps4)
            //    {
            //        foreach (char mapTile in line)
            //        {
            //            SetTileColor(mapTile);
            //            Console.Write(mapTile);
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.ResetColor();
            //}
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine($"Error: The file '{_filepath4}' was not found.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
        }
        public (int x, int y)? MapChanger(int x, int y) //sets the map changer to look for the specified tiles to get the x,y ints
        {
            char tile = _mapsCurrent[y][x];

          
            if (tile == '@' && _currentMapIndex < _allMaps.Length - 1)// checks for forward portall in player movement
            {
                _currentMapIndex++;
                _mapsCurrent = _allMaps[_currentMapIndex];
                DrawMap();
                return FindTile('*'); // Spawn at the backward entrance of next map
            }
            // Step on *: Go Backward
            else if (tile == '*' && _currentMapIndex > 0)
            {
                _currentMapIndex--;
                _mapsCurrent = _allMaps[_currentMapIndex];
                DrawMap();
                return FindTile('@'); // Spawn at the forward exit of previous map
            }

            return null; // No map change
        }

        private (int x, int y) FindTile(char target)
        {
            for (int y = 0; y < _mapsCurrent.Length; y++)
            {
                for (int x = 0; x < _mapsCurrent[y].Length; x++)
                {
                    if (_mapsCurrent[y][x] == target) return (x, y);
                }
            }
            return (3, 3); // Fallback spawn incase find tile breaks
        }

        public void DrawMap()
        {
            Console.Clear();
            for (int y = 0; y < _mapsCurrent.Length; y++)
            {
                foreach (char tile in _mapsCurrent[y])
                {
                    SetTileColor(tile);
                    Console.Write(tile);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        //public void DrawFullMap()
        //{
        //    Console.Clear();
        //    for (int y = 0; y < _mapsCurrent.Length; y++)
        //    {
        //        foreach (char tile in _mapsCurrent[y])
        //        {
        //            SetTileColor(tile);
        //            Console.Write(tile);
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.ResetColor();
        //}
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