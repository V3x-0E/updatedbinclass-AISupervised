using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInal_NEA
{
    internal class node
    {

        public string userprompt;
        public bool sentiment;
        public node negative1;
        public node positive1;
        public int temp;
        // all files for use in response with programmer designed responses that the machine picks at random when given condition I.e. sentiment 
        public string[] rpositive = System.IO.File.ReadAllLines(@"insertfilepathhere");
        public string[] rnegative = System.IO.File.ReadAllLines(@"insertfilepathhere");
        // file containing follow up questions
        public string[] questionbank = System.IO.File.ReadAllLines(@"insertfilepathhere"); 
        //states user propmpt 
        public void startp()
        {
            Console.WriteLine(userprompt);
           
        }
        // determines sentiment based on the "scores" which are just probabilities 
        public void determineSentiment(double Pscore, double nscore)
        {
            if (Pscore > nscore) 
            { sentiment = true; }
            else if (Pscore < nscore) 
            { sentiment = false; }

        }
        // determines system response based on sentiment and loads next question 
        public void getuserstring(bool bankpull)
        {
            if (bankpull == true)
            {
              Random n = new Random();
               temp = n.Next(0, 6);
                Console.WriteLine(rpositive[temp]);

            }
            else if (bankpull == false)
            {
                Random n = new Random();
                temp = n.Next(0, 6);
                Console.WriteLine(rnegative[temp]);

            }



        }
        // asks next question 
        public void nextquestion(int count)
        {

            userprompt = questionbank[count];
        }
    }
}
