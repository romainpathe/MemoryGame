using System;
using System.Threading;

namespace MemoryGame.Management
{
    public struct Time
    {
        public int secondes;
        public int minutes;
        public int heures;
    }
    public class Timer
    {
        private Thread thread;
        public Time time;
        public Timer()
        {
            thread = new Thread(Timers);
            thread.Start();
        }

        public void Timers()
        {
            while (true)
            {
                Thread.Sleep(1000);
                time.secondes++;
                if (time.secondes == 60)
                {
                    if (time.secondes > 60)
                    {
                        time.secondes -= 60;
                    }
                    else
                    {
                        time.secondes = 0;
                    }
                    time.minutes++;
                }
                if (time.minutes >= 60)
                {
                    if (time.minutes > 60)
                    {
                        time.minutes -= 60;
                    }else{
                        time.minutes = 0;
                    }
                    time.heures++;
                }
                if (time.heures >= 1)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0,0);
                    Console.Write("Pour le bien de votre ordinateur et de la planéte, le programme s'est arrété tout seul !");
                    Thread.Sleep(10000);
                    Environment.Exit(0);
                }  
            }
        }
        public void StopTimer()
        {
            thread.Abort();
        }


        public static int ConvertToSecond(Time time)
        {
            var result = 0;

            result += time.secondes;
            result += time.minutes * 60;
            result += time.heures * 60 * 60;

            return result;
        }

        
        
        
    }
}