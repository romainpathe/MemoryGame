using System;
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
        
        
        public Status GenerateFilesSelected(int numberOfCard, int type = 0)
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
                    var tempFilesListSelected = new List<string>();
                    for (var i = 0; i < numberOfCard; i++)
                    {
                        var filesSelectedNumber = Global.rdm.Next(0,tempFilesList.Count);
                        tempFilesListSelected.Add(tempFilesList[filesSelectedNumber]);
                        tempFilesListSelected.Add(tempFilesList[filesSelectedNumber]);
                        tempFilesList.RemoveAt(filesSelectedNumber);
                    }
                    
                    FilesListSelected = new List<string>();
                    while (tempFilesListSelected.Count > 0)
                    {
                        var tempFilesListSelectedNumber = Global.rdm.Next(0, tempFilesListSelected.Count-1);
                        FilesListSelected.Add(tempFilesListSelected[tempFilesListSelectedNumber]);
                        tempFilesListSelected.RemoveAt(tempFilesListSelectedNumber);
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