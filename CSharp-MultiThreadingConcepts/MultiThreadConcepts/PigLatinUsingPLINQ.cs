using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PigLatinUsingPLINQ
    {
        #region PLINQ Setup
        public static void PrintPigLatinSentence(string statement)
        {            
            var words = statement.Split()
                .AsParallel() //Converting LINQ to PLINQ
                .AsOrdered()  //Forcing Ordered resultset whcih is not the default behaviour of PLINQ
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism) //Forcing Parallelism
                .Select(word => new string(PerformAction(word).ToArray()));

            Console.WriteLine("PLINQ Result: {0}", string.Join(" ", words));
        }
        #endregion

        #region Perform Pig latin 
        public static string PerformAction(string word)
        {
            //Performing word pig latin 
            StringBuilder sb = new StringBuilder();
            sb.Append(word.ToLower().Substring(1, word.Length - 1));
            sb.Append(word.ToLower().Substring(0, 1));
            sb.Append("y");

            return sb.ToString();
        }
        #endregion
    }
}