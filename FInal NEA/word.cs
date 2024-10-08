using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInal_NEA
{
    internal class word
    {
        public double Pprobability = 0;
        public double Nprobability = 0;
        public string name = "";
        public double wordprobability = 0;

        public void title(string temp) // passes in the unique word and names the object that 
        {
            name = temp;
        }
        public void determinePprob(double numerator, double divisor) // determines Positive P of word
        {
            Pprobability = numerator / divisor;
        }
        public void determineNprob(double numerator, double divisor) // determines Negative P of word
        {
            Nprobability = numerator / divisor;
        }
        public void determineWprob(double numerator, double divisor) // determines general P of word
        {
            wordprobability = numerator / divisor;      
        }
        public double getNprob()
        {
            return Nprobability;
        }//all output their respective probabilities
        public double getPprob()
        { return Pprobability; }
        public double getWprob()
        { return wordprobability; }

    }
}
