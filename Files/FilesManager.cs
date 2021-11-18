using System.Collections.Generic;
using System.IO;
using System.Linq;
using MemoryGame.Management;

namespace MemoryGame.Files
{
    public class FilesManager
    {
        #region Accessers

        public static int FilesNumber { get; private set; }

        private static List<string> FilesList { get; set; }
            
        public List<string> FilesListSelected { get; set; }

        public static void InitFilesList()
        {
            var files = Directory.EnumerateFiles("Card");
            FilesList = new List<string>();
            foreach (var file in files)
            {
                FilesList.Add(file);
            }
            FilesNumber = FilesList.Count;
        }

        #endregion
        
        
        public Status GenerateFilesSelected(int numberOfCard)
        {
            var status = new Status();
            if (FilesList == null)
            {
                InitFilesList();
            }
            else
            {
                const int  min = 1;
                if (numberOfCard >= min-1 && numberOfCard <= FilesNumber)
                {
                    var tempFilesList = FilesList.ToList();
                    FilesListSelected = new List<string>();
                    for (var i = 0; i < numberOfCard; i++)
                    { 
                        var filesSelectedNumber = Program.Rdm.Next(0,tempFilesList.Count-1);
                        FilesListSelected.Add(tempFilesList[filesSelectedNumber]);
                        tempFilesList.RemoveAt(filesSelectedNumber);
                    }
                }
                else
                {
                
                    status.Message = "Le nombre de paires demandé n'est pas compris entre " + min + " et " + FilesNumber;
                    status.IsError = true;
                }
            }
            
            return status;
            
        }
        
        
        
        
        
        
        
    }
}