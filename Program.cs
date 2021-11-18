using System;
using System.IO;
using System.Runtime.InteropServices;
using MemoryGame.Cards;
using MemoryGame.Files;

namespace MemoryGame
{
  internal class Program
  {
    public static Random rdm = new Random();
    
    
    public static void Main(string[] args)
    {
      SetFullScreen();
      
      Menu.Menu.DisplayCenterMenu(2);
      
      /* FilesManager.InitFilesList();
      Console.WriteLine(FilesManager.FilesNumber);
      var files = new FilesManager();
      var statusFilesSelected = files.GenerateFilesSelected(2);
      var cardList = new CardManager();
      cardList.GenerateCardList(files.FilesListSelected);
      cardList.DrawCardList(); */
      
      
      
      Console.ReadKey();
    }


    #region Windows & Scroll Bar

    /// <summary>
    /// Library allowing to get user information, as Length/height of the screen.
    /// </summary>
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();
    private static readonly IntPtr ThisConsole = GetConsoleWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    /// <summary>
    /// Full screen function
    /// </summary>
    private static void SetFullScreen()
    {
      // Allows you to set the size of the window
      Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight); 
      // Allows you to remove the scroll bar
      Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
      // Allows to put the window in full screen
      ShowWindow(ThisConsole, 3);
    }

    #endregion
    
    #region Windows Size

    public static int WindowWidth
    {
      get => WindowWidth;
      set
      {
        WindowWidth = Console.WindowWidth;
      }
    }
    public static int WindowHeight
    {
      get => WindowHeight;
      set
      {
        WindowHeight = Console.WindowHeight;
      }
    }
    

    #endregion
    
  }
}