using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        #region Thread Synchronisation Using AutoResetEvent
        public static int result = 0;
        public static Object locker = new Object();
        public static EventWaitHandle readyForResult = new AutoResetEvent(false);
        public static EventWaitHandle ready = new AutoResetEvent(false);

        public static void Work()
        {
            while (true)
            {
                int i = result;

                // Calling waitone and suspending itself
                readyForResult.WaitOne();
                lock (locker)
                {
                    result = i + 1;
                }
                ready.Set();
            }
        }
        static void MainThread()
        {
            Console.WriteLine("Thread Synchronisation Using AutoResetEvent");
            Thread t = new Thread(Work);
            t.Start();

            for (int i = 0; i < 100; i++)
            {
                // Let in
                readyForResult.Set();
                ready.WaitOne();
                lock (locker)
                {
                    Console.WriteLine(result);
                }

                Thread.Sleep(100);
            }
            Console.ReadLine();
        }
        #endregion

        static void Main(string[] args)
        {
            // Thread Synchronisation Using AutoResetEvent
            // MainThread();

            // Producer Consumer Scenario
            // ProducerConsumerQueue.Producer();

            //Performing Pig Laitin on given Sentence using TPL and PLINQ
            Console.WriteLine("Performing Pig Latin on Sentence using TPL and PLINQ");
            PigLatinUsingTask.PrintPigLatinSentence("This is an Example of PigLatin Sentence using TPL");
            PigLatinUsingPLINQ.PrintPigLatinSentence("This is an Example of PigLatin Sentence using PLINQ");
            
            Console.ReadLine();            
        }
    }
}