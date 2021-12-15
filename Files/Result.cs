using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MemoryGame.Management;
using MemoryGame.Menus;

namespace MemoryGame.Files
{
    public class Result
    {
        private static Dictionary<string, string> resultGO; 

        #region GameOne

        private static void InitResultGameOne()
        {
            resultGO = new Dictionary<string,string>();
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

        public static void EditResultGameOne(string key, string value)
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
                if (keyValuePair.Key == key)
                {
                    key1 = keyValuePair.Key;
                    value1 = keyValuePair.Value;
                    t = true;
                }
                if (keyValuePair.Key == key)
                {
                    key2 = keyValuePair.Key;
                    value2 = keyValuePair.Value;
                    infotime = true;
                }
                if (keyValuePair.Key == key)
                {
                    bestkey = keyValuePair.Key;
                    bestvalue = keyValuePair.Value;
                    besttime = true;
                }
            }

            if (!t)
            {
                myresultGo.Add(key,value+" ("+name+")");
            }
            else
            {
                if (Convert.ToInt32(value1.Split(' ')[0]) > Convert.ToInt32(value))
                {
                    myresultGo.Remove(key);
                    myresultGo.Add(key, value + " (" + name + ")");
                }
            }

            if (!infotime)
            {
                myresultGo.Add("Meilleur temps",time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
            }
            else
            {
                infotime = true;
                var newtime = new Time();
                var timelist = value2.Split(':');
                timelist[2] = timelist[2].Substring(0,timelist[2].IndexOf('('));
                newtime.secondes = Convert.ToInt32(timelist[2]);
                newtime.minutes = Convert.ToInt32(timelist[1]);;
                newtime.heures = Convert.ToInt32(timelist[0]);;
                    
                if (newtime.heures <= time.heures)
                {
                    if (newtime.heures < time.heures)
                    {
                        myresultGo.Remove(key2);
                        myresultGo.Add("Meilleur temps", time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                    }
                    else
                    {
                        if (newtime.minutes < time.minutes)
                        {
                            myresultGo.Remove(key2);
                            myresultGo.Add("Meilleur temps", time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                        }else
                        {
                            if (newtime.secondes < time.secondes)
                            {
                                myresultGo.Remove(key2);
                                myresultGo.Add("Meilleur temps", time.heures+":"+time.minutes+":"+time.secondes+" ("+name+")");
                            }
                        }
                    }
                }
            }

            if (!besttime)
            {
                // TODO: Mettre la bonne variable (voir a l'intérieur de fichier de résultat)
                myresultGo.Add("Meilleur Ration Temps/Coups",bestvalue+" ("+name+")");
                
            }
            
            

            StreamWriter sw = new StreamWriter("Result/GameOne.txt");
            foreach (var keyValuePair in myresultGo)
            {
                sw.WriteLine(keyValuePair.Key+"-"+keyValuePair.Value);
            }
                
            sw.Close();
            
        }

        
        public static void DisplayResult()
        {
            InitResultGameOne();
            Console.Clear();
            var nbItem = resultGO.Count;
            var noValue = false;
            if (nbItem == 0)
            {
                nbItem = 1;
                noValue = true;
            }
            DisplayCenterResult(nbItem);
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
                        Console.Write(keyValuePair.Key + " : "+keyValuePair.Value);
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
        
        
        
        
        
        
        
        
        private static void DisplayCenterResult(int nbItem)
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
                // TODO: Check si sur pc portable j'ai toujours autant de hauteurs sur les étoiles
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

    }
}