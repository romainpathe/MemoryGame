using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryGame.Cards
{
    public class Card
    {
        private Location _location;
        private string _path;
        private List<string> _picture;
        private bool _display;
        private bool _firstDisplay;
        private int _heigth;
        private int _width;

        #region Constructor

        public Card(string path, Location location)
        {
            _path = path;
            _location = location;
            _display = true;
            _firstDisplay = true;
            _width = 0;
            _heigth = 0;
            Init();
        }

        #endregion
        
        #region Accessers

        public bool Display { get => _display; set { _display = value; } }
        
        public string path { get => _path;}
        public int width { get; private set; }
        public int heigth { get; private set; }
        
        

        #endregion

        #region Picture Management
        
        private void Init()
        {
            _picture = new List<string>();
            var sr = new StreamReader(_path);
            var line = sr.ReadLine();
            while(line != null)
            {
                if (width < line.Length)
                {
                    width = line.Length;
                }
                _picture.Add(line);
                line = sr.ReadLine();
                heigth++;
            }
            sr.Close();
        }
        
        public void Draw(ConsoleColor colors)
        {
            if (_firstDisplay)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(_location.X,_location.Y);
                Console.Write("┌");
                Console.SetCursorPosition(_location.X,_location.Y+14);
                Console.Write("└");
                Console.SetCursorPosition(_location.X+31,_location.Y);
                Console.Write("┐");
                Console.SetCursorPosition(_location.X+31,_location.Y+14);
                Console.Write("┘");
                for (var i = _location.X+1; i < _location.X+31; i++)
                {
                    Console.SetCursorPosition(i,_location.Y);
                    Console.Write("-");
                    Console.SetCursorPosition(i,_location.Y+14);
                    Console.Write("-");
                }

                for (var i = _location.Y+1; i < _location.Y+14; i++)
                {
                    Console.SetCursorPosition(_location.X,i);
                    Console.Write("|");
                    Console.SetCursorPosition(_location.X+31,i);
                    Console.Write("|");
                }
                _firstDisplay = false;
            }
            Console.ForegroundColor = colors;
            if(_display == false)
            {
                for (var i = _location.X+1; i < _location.X+31; i++)
                {
                    for (int j = _location.Y+1; j < _location.Y+14; j++)
                    {
                        Console.SetCursorPosition(i,j);
                        Console.Write("?");
                    }
                }
                return;
            }
            if(_picture == null) Init();
            var y = _location.Y;
            var spacey = 15 - heigth;
            spacey = spacey / 2 + _location.Y;

            for (var i = _location.X+1; i < _location.X+31; i++)
            {
                for (int j = _location.Y+1; j < _location.Y+14; j++)
                {
                    Console.SetCursorPosition(i,j);
                    Console.Write(" ");
                }
            }
            foreach (var line in _picture)
            {
                var t = width;
                var space = 30 - t;
                space /= 2;
                var start = space+1+_location.X;
                Console.SetCursorPosition(70, 0);
                for (var i = 0; i < line.Length; i++)
                {
                    Console.SetCursorPosition(start,spacey);
                    Console.Write(line[i]);
                    start++;
                }
                spacey++;
            }
           Console.SetCursorPosition(0,0);
        }

        #endregion
        
        

    }
}