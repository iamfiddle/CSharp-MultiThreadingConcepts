using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PigLatinUsingTask
    {
        #region Map
        public static string[] Map(string sentence)
        {
            return sentence.Split();
        }
        #endregion

        #region Process
        public static string[] Process(string[] words)
        {
            //Pick splitted task and call PerformAction over it
            for (int i = 0; i < words.Length; i++)
            {
                int index = i;
                Task.Factory.StartNew(() => words[index] = PerformAction(words[index]), TaskCreationOptions.AttachedToParent);
            }
            return words;
        }
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

        #region Reducer
        public static string Reduce(string[] words)
        {
            //Collect or gather and return final result
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                sb.Append(word);
                sb.Append(" ");
            }
            return sb.ToString();
        }
        #endregion

        #region Call Task and Print PigLatin Sentence
        public static void PrintPigLatinSentence(string statement)
        {
            var task = Task<string[]>.Factory.StartNew(() => Map(statement))
                .ContinueWith<string[]>(t => Process(t.Result))
                .ContinueWith<string>(t => Reduce(t.Result));

            Console.WriteLine("TPL Result: {0}", task.Result);
        }
        #endregion
    }
}