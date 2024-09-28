using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInal_NEA
{
    internal class Request : word // inherited class so ue of the title method can be used 
    {
        
        public double Nscore;
        public double Pscore;
        // sets both scores 
        public void setnscore(double nscore)
        {
            Nscore = nscore;
        }
        public void setpscore(double pscore)
        {
            Pscore = pscore;
        }
        

    }
}
