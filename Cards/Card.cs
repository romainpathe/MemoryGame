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

        #region Constructor

        public Card(string path, Location location)
        {
            _path = path;
            _location = location;
            _display = true;
        }

        #endregion
        
        #region Accessers

        public bool Display { get; set; }

        #endregion

        #region Picture Management
        
        private void Init()
        {
            _picture = new List<string>();
            var sr = new StreamReader(_path);
            var line = sr.ReadLine();
            while(line != null){
                _picture.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
        }
        
        public void Draw()
        {
            // TODO: Draw no image
            if(_display == false) return;
            if(_picture == null) Init();
            var y = _location.Y;
           foreach (var line in _picture)
           {
               for (var i = _location.X; i < line.Length; i++)
               {
                   Console.SetCursorPosition(i,y);
                   Console.Write(line[i]);
               }
               y++;
           }
        }

        #endregion
        
        

    }
}