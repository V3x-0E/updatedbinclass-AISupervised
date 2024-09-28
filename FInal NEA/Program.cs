using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInal_NEA
{
    internal class Program
    {
        static void Main(string[] args)
        {
           bool nets; 
           Naivebayes NB= new Naivebayes(); // instanciates naive bayes class
           word[] words = NB.generateProbabilities();  // generates probabilities of new data 
           word[] load = NB.loadfromcsv(); // loads previous system data 
           word[] words1 = NB.average(load, words); // averages matches and adds new data to word array 
           // NB.write(words1); // writes new weights to a csv (still needs fixing)
           graphofconvo start= new graphofconvo(); // instantiates graphofconvo class
           nets = start.Convo(true, 0 , words1); // gets boolean of day 
            if (nets == false) // selection 
            {
                Console.WriteLine("I'm sorry you didn't have a good day my friend");
            }
            else
            {
                Console.WriteLine("I'm so happy you had a good day my friend");
            
            }
            Console.WriteLine("Thank you so much for talking with me today friend, I hope tomorrow is better then today!!");
            Console.WriteLine("Have a blessed rest of your day.");
            Console.ReadLine(); 
        }



    }
}

 


