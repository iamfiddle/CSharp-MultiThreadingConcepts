using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class ProducerConsumerQueue
    {
        #region Variable declaration
        //Consumers Thread list
        public static List<Thread> consumers = new List<Thread>();
        //Queue of Actions
        public static Queue<Action> tasks = new Queue<Action>();
        //Task Locker
        public static Object taskLocker = new object();
        //AutoResetEvent, works as turnstile
        public static EventWaitHandle newTask = new AutoResetEvent(false);
        //ConsoleLocker, to lock console for forground color as console is not thread safe
        public static Object consoleLocker = new Object();
        #endregion

        #region Producer and consumer settings
        public static int result = 0;
        public static void EnqueTask(Action task)
        {
            lock (taskLocker)
            {
                tasks.Enqueue(task);
            }
            newTask.Set();
        }
        public static void Work(ConsoleColor color)
        {
            while (true)
            {
                Action task = null;

                lock (taskLocker)
                {
                    if (tasks.Count > 0)
                    {
                        task = tasks.Dequeue();
                    }
                }
                if (task != null)
                {
                    lock (consoleLocker)
                    {
                        Console.ForegroundColor = color;
                    }
                    task();
                }
                else
                    newTask.WaitOne();
            }
        }

        public static void Producer()
        {
            Console.WriteLine("Producer Consumer Queue Scenario");
            //Setting up 3 consumers
            consumers.Add(new Thread(() => { Work(ConsoleColor.Red); }));
            consumers.Add(new Thread(() => { Work(ConsoleColor.Green); }));
            consumers.Add(new Thread(() => { Work(ConsoleColor.Blue); }));

            consumers.ForEach((t) => { t.Start(); });

            while (true)
            {
                int i = result + 1;
                result++;
                //Producer creating new task
                EnqueTask(() =>
                {
                    Console.Write(i);
                });

                Thread.Sleep(100);
            }
        }
        #endregion
    }
}