using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using MemoryGame.Cards;
using MemoryGame.Files;
using MemoryGame.Menus;

namespace MemoryGame
{
  internal class Program
  {
    public static List<MenuItem> start = Menu.GeneretedStartMenu();
    public static void Main(string[] args)
    {
      SetFullScreen();
      Console.Title = "Memory de Romain Pathé";
      Console.CursorVisible = false;
      
      Menu.DisplayMenu(start);


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
      //Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
      // Allows to put the window in full screen
      ShowWindow(ThisConsole, 3);
      WindowHeight = Console.WindowHeight;
      WindowWidth = Console.WindowWidth;

    }

    #endregion
    
    #region Windows Size

    public static int WindowWidth { get; private set; }
    public static int WindowHeight { get; private set; }
    

    #endregion
    
  }
}