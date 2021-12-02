using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MemoryGame.GamesOne
{
    public class ResultOne
    {
        private static IEnumerable<string> FilesList;
        
        public static void InitFilesList()
        {
            var files = Directory.EnumerateFiles("ResultOne");
            FilesList = files.ToList();
        }
        
    }
}