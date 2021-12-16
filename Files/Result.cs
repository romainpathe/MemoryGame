using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using MemoryGame.Cards;
using MemoryGame.Management;
using MemoryGame.Menus;
using Timer = MemoryGame.Management.Timer;

namespace MemoryGame.Files
{
    public class Result
    {
        private static Dictionary<string, string> resultGO; 
        private static Dictionary<string, string> resultGameTwo; 

        #region GameOne

        private static void InitResultGameOne(string coup, string time)
        {
            resultGO = new Dictionary<string,string>();
            resultGO.Add("Ton nombre de coup",coup);
            resultGO.Add("Ton temps",time);
            var sr = new StreamReader("Result/GameOne.txt");
            var line = sr.ReadLine();
            while(line != null)
            {
                var tab = line.Split('-');
                if (resultGO.ContainsKey(tab[0]))
                {
                    resultGO.Remove(tab[0]);
                }
                resultGO.Add(tab[0],tab[1]);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public static void EditResultGameOne(int value)
        {
            string name;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.CursorVisible = true;
                Console.WriteLine("Saisir votre prénom, compris en 1 et 10 caractéres");
                name = Console.ReadLine();
            } while (name == null || name.Length > 10 || name.Length <= 0);
            Console.CursorVisible = false;
            var myresultGo = new Dictionary<string,string>();
            var sr = new StreamReader("Result/GameOne.txt");
            var line = sr.ReadLine();
            while(line != null)
            {
                var tab = line.Split('-');
                if (myresultGo.ContainsKey(tab[0]))
                {
                    myresultGo.Remove(tab[0]);
                }
                myresultGo.Add(tab[0],tab[1]);
                line = sr.ReadLine();
            }
            sr.Close();

            var t = false;
            var infotime = false;
            var besttime = false;
            string key1="";
            string key2="";
            string bestkey="";
            string value1="";
            string value2="";
            string bestvalue="";
            var time = Menu.timer.time;
            foreach (var keyValuePair in myresultGo)
            {
                if (keyValuePair.Key == "En moins de coup")
                {
                    key1 = keyValuePair.Key;
                    value1 = keyValuePair.Value;
                    t = true;
                }
                if (keyValuePair.Key == "Meilleur temps")
                {
                    key2 = keyValuePair.Key;
                    value2 = keyValuePair.Value;
                    infotime = true;
                }
                if (keyValuePair.Key == "Meilleur ratio temps/coups")
                {
                    bestkey = keyValuePair.Key;
                    bestvalue = keyValuePair.Value;
                    besttime = true;
                }
            }

            if (!t)
            {
                myresultGo.Add("En moins de coup",value+" ("+name+")");
            }
            else
            {
                if (Convert.ToInt32(value1.Split(' ')[0]) > Convert.ToInt32(value))
                {
                    myresultGo.Remove(key1);
                    myresultGo.Add(key1, value + " (" + name + ")");
                }
            }

            if (!infotime)
            {
                myresultGo.Add("Meilleur temps",time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
            }
            else
            {
                var oldtime = new Time();
                var timelist = value2.Split(':');
                oldtime.minutes = Convert.ToInt32(timelist[1]);;
                oldtime.heures = Convert.ToInt32(timelist[0]);;
                timelist[2] = timelist[2].Split('(')[0];
                oldtime.secondes = Convert.ToInt32(timelist[2]);
                    
                if (time.heures <= oldtime.heures)
                {
                    if (time.heures < oldtime.heures)
                    {
                        myresultGo.Remove(key2);
                        myresultGo.Add(key2, time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                    }
                    else
                    {
                        if (time.minutes < oldtime.minutes)
                        {
                            myresultGo.Remove(key2);
                            myresultGo.Add(key2, time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                        }else
                        {
                            if (time.secondes < oldtime.secondes)
                            {
                                myresultGo.Remove(key2);
                                myresultGo.Add(key2, time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                            }
                        }
                    }
                }
            }

            if (!besttime)
            {
                // TODO: Mettre la bonne variable (voir a l'intérieur de fichier de résultat)

                myresultGo.Add("Meilleur ratio temps/coups",value+" | "+time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                
            }
            else
            {
                var valueList = bestvalue.Split('|');
                int oldNbCoup = Convert.ToInt32(valueList[0]);
                valueList = valueList[1].Split(':');
                Time oldTime = new Time();
                oldTime.heures = Convert.ToInt32(valueList[0]);
                oldTime.minutes = Convert.ToInt32(valueList[1]);
                oldTime.secondes = Convert.ToInt32(valueList[2].Split('(')[0]);

                int newtime = Timer.ConvertToSecond(time);
                int oldalltime = Timer.ConvertToSecond(oldTime);

                if (newtime/value < oldalltime/oldNbCoup)
                {
                    myresultGo.Remove(bestkey);
                    myresultGo.Add(bestkey,value+" | "+time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                }
            }
            StreamWriter sw = new StreamWriter("Result/GameOne.txt");
            foreach (var keyValuePair in myresultGo)
            {
                sw.WriteLine(keyValuePair.Key+"-"+keyValuePair.Value);
            }
            sw.Close();
        }

        
        public static void DisplayResultGame1()
        {
            Time lastTime = Menu.timer.time;
            InitResultGameOne((CardManager.nbMin).ToString(), lastTime.heures+":"+lastTime.minutes+":"+lastTime.minutes);
            Console.Clear();
            var nbItem = resultGO.Count;
            var noValue = false;
            if (nbItem == 0)
            {
                nbItem = 1;
                noValue = true;
            }
            DisplayCenterResultGame1(nbItem);
            while (true)
            {
                var size = Convert.ToInt32(Program.WindowWidth * 0.25);
                var start = (Program.WindowWidth - size) / 2;

                var hauteur = nbItem + 7;
                var startH = (Program.WindowHeight - hauteur) / 2;

                int[] position = { start + 5, startH + 4 };
                Console.ForegroundColor = ConsoleColor.Gray;
                if (noValue)
                {
                    Console.SetCursorPosition(position[0], position[1]);
                    Console.Write("Il n'y a encore aucun résultat !");
                }
                else
                {
                    foreach (var keyValuePair in resultGO)
                    {
                        Console.SetCursorPosition(position[0], position[1]);
                        Console.Write(keyValuePair.Key + ": "+keyValuePair.Value);
                        position[1]++;
                        if (keyValuePair.Key == "Ton temps")
                        {
                            Console.Write(' ');
                            position[1]++;
                        }
                    }
                }

                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Menu.DisplayMenu(Program.start);
                        break;
                    default:
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        continue;
                }

                break;
            }
        }
        
        
        
        
        
        
        
        
        private static void DisplayCenterResultGame1(int nbItem)
        {
            const string title = "MemoryGames";
            const string title1 = "Score Jeu 1";
            const string footer = "Projet créer par Romain Pathé";
            var size = Convert.ToInt32(Program.WindowWidth*0.25);
            var start = (Program.WindowWidth - size) / 2;
            
            var hauteur = nbItem+7;
            var startH = ((Program.WindowHeight - hauteur) / 2);
            
            for (var i = start; i < start+size;i++)
            {
                if (i%2 != 0)
                {
                    Console.SetCursorPosition(i,startH);
                    Console.Write("*");
                    Console.SetCursorPosition(i, startH + hauteur);
                    Console.Write("*");
                }
            }

            if (start % 2 == 0)
            {
                start++;
            }
            for (var j = startH; j <= startH + hauteur; j++)
            {
                var end = start + size -1;
                Console.SetCursorPosition(start, j);
                Console.Write("*");
                Console.SetCursorPosition(end, j);
                Console.Write("*");
            }

            var titleWidth = ((Program.WindowWidth)/2)-title.Length/2;
            if (start%2 != 0)
            {
                titleWidth++;
            }
            Console.SetCursorPosition(titleWidth,startH+1);
            Console.WriteLine(title);
            
            var title1Width = ((Program.WindowWidth)/2)-title1.Length/2;
            if (start%2 != 0)
            {
                title1Width++;
            }
            Console.SetCursorPosition(title1Width,startH+2);
            Console.WriteLine(title1);
                
            var footerWidth = ((Program.WindowWidth)/2)-footer.Length/2;
            if (start%2 != 0)
            {
                footerWidth++;
            }
            Console.SetCursorPosition(footerWidth,startH+hauteur-1);
            Console.WriteLine(footer);
        }
        
        #endregion

        #region GameTwo

        private static void InitResultGameTwo(string coup, string time)
        {
            resultGameTwo = new Dictionary<string,string>();
            resultGameTwo.Add("Ton nombre de coup",coup);
            resultGameTwo.Add("Ton temps",time);
            var sr = new StreamReader("Result/GameTwo.txt");
            var line = sr.ReadLine();
            while(line != null)
            {
                var tab = line.Split('-');
                if (resultGameTwo.ContainsKey(tab[0]))
                {
                    resultGameTwo.Remove(tab[0]);
                }
                resultGameTwo.Add(tab[0],tab[1]);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public static void EditResultGameTwo(int value)
        {
            string name;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.CursorVisible = true;
                Console.WriteLine("Saisir votre prénom, compris en 1 et 10 caractéres");
                name = Console.ReadLine();
            } while (name == null || name.Length > 10 || name.Length <= 0);
            Console.CursorVisible = false;
            var myresultGo = new Dictionary<string,string>();
            var sr = new StreamReader("Result/GameTwo.txt");
            var line = sr.ReadLine();
            while(line != null)
            {
                var tab = line.Split('-');
                if (myresultGo.ContainsKey(tab[0]))
                {
                    myresultGo.Remove(tab[0]);
                }
                myresultGo.Add(tab[0],tab[1]);
                line = sr.ReadLine();
            }
            sr.Close();

            var t = false;
            var infotime = false;
            var besttime = false;
            string key1="";
            string key2="";
            string bestkey="";
            string value1="";
            string value2="";
            string bestvalue="";
            var time = Menu.timer.time;
            foreach (var keyValuePair in myresultGo)
            {
                if (keyValuePair.Key == "En moins de coup")
                {
                    key1 = keyValuePair.Key;
                    value1 = keyValuePair.Value;
                    t = true;
                }
                if (keyValuePair.Key == "Meilleur temps")
                {
                    key2 = keyValuePair.Key;
                    value2 = keyValuePair.Value;
                    infotime = true;
                }
                if (keyValuePair.Key == "Meilleur ratio temps/coups")
                {
                    bestkey = keyValuePair.Key;
                    bestvalue = keyValuePair.Value;
                    besttime = true;
                }
            }

            if (!t)
            {
                myresultGo.Add("En moins de coup",value+" ("+name+")");
            }
            else
            {
                if (Convert.ToInt32(value1.Split(' ')[0]) > Convert.ToInt32(value))
                {
                    myresultGo.Remove(key1);
                    myresultGo.Add(key1, value + " (" + name + ")");
                }
            }

            if (!infotime)
            {
                myresultGo.Add("Meilleur temps",time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
            }
            else
            {
                var oldtime = new Time();
                var timelist = value2.Split(':');
                oldtime.minutes = Convert.ToInt32(timelist[1]);;
                oldtime.heures = Convert.ToInt32(timelist[0]);;
                timelist[2] = timelist[2].Split('(')[0];
                oldtime.secondes = Convert.ToInt32(timelist[2]);
                    
                if (time.heures <= oldtime.heures)
                {
                    if (time.heures < oldtime.heures)
                    {
                        myresultGo.Remove(key2);
                        myresultGo.Add(key2, time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                    }
                    else
                    {
                        if (time.minutes < oldtime.minutes)
                        {
                            myresultGo.Remove(key2);
                            myresultGo.Add(key2, time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                        }else
                        {
                            if (time.secondes < oldtime.secondes)
                            {
                                myresultGo.Remove(key2);
                                myresultGo.Add(key2, time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                            }
                        }
                    }
                }
            }

            if (!besttime)
            {
                // TODO: Mettre la bonne variable (voir a l'intérieur de fichier de résultat)

                myresultGo.Add("Meilleur ratio temps/coups",value+" | "+time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                
            }
            else
            {
                var valueList = bestvalue.Split('|');
                int oldNbCoup = Convert.ToInt32(valueList[0]);
                valueList = valueList[1].Split(':');
                Time oldTime = new Time();
                oldTime.heures = Convert.ToInt32(valueList[0]);
                oldTime.minutes = Convert.ToInt32(valueList[1]);
                oldTime.secondes = Convert.ToInt32(valueList[2].Split('(')[0]);

                int newtime = Timer.ConvertToSecond(time);
                int oldalltime = Timer.ConvertToSecond(oldTime);

                if (newtime/value < oldalltime/oldNbCoup)
                {
                    myresultGo.Remove(bestkey);
                    myresultGo.Add(bestkey,value+" | "+time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                }


            }
            
            

            StreamWriter sw = new StreamWriter("Result/GameTwo.txt");
            foreach (var keyValuePair in myresultGo)
            {
                sw.WriteLine(keyValuePair.Key+"-"+keyValuePair.Value);
            }
                
            sw.Close();
            
        }

        
        public static void DisplayResultGameTwo()
        {
            Time lastTime = Menu.timer.time;
            InitResultGameTwo((CardManager.nbMin).ToString(), lastTime.heures+":"+lastTime.minutes+":"+lastTime.minutes);
            Console.Clear();
            var nbItem = resultGameTwo.Count;
            var noValue = false;
            if (nbItem == 0)
            {
                nbItem = 1;
                noValue = true;
            }
            DisplayCenterResultGame1(nbItem);
            while (true)
            {
                var size = Convert.ToInt32(Program.WindowWidth * 0.25);
                var start = (Program.WindowWidth - size) / 2;

                var hauteur = nbItem + 7;
                var startH = (Program.WindowHeight - hauteur) / 2;

                int[] position = { start + 5, startH + 4 };
                Console.ForegroundColor = ConsoleColor.Gray;
                if (noValue)
                {
                    Console.SetCursorPosition(position[0], position[1]);
                    Console.Write("Il n'y a encore aucun résultat !");
                }
                else
                {
                    foreach (var keyValuePair in resultGameTwo)
                    {
                        Console.SetCursorPosition(position[0], position[1]);
                        Console.Write(keyValuePair.Key + ": "+keyValuePair.Value);
                        position[1]++;
                    }
                }

                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Menu.DisplayMenu(Program.start);
                        break;
                    default:
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        continue;
                }

                break;
            }
        }
        
        #endregion
        
        
        
        
        
    }
}