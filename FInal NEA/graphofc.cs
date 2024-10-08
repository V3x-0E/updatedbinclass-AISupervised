using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInal_NEA
{
    internal class graphofconvo
    {
        public node root;


       
        public bool Convo(bool ongoing, int count, word[] words)
        {
            bool netS;
            int PScore = 1;
            int Nscore = 1;
            Naivebayes NB1= new Naivebayes(); // new naive bayes class for use of the functions involved
            Request[] currentrequest = new Request[5]; //initialised array of the request class for use in program
            node point; // "tracking" node so we knew where we are 
            List<string> positiveresponese = new List<string>();
            List<string> negativeresponese = new List<string>();
            string check;
            while (ongoing == true)
            {
                root = new node();
                for (int i = 0; i < 5; i++)
                {
                    if (count > 2)
                    {
                        ongoing = false;
                    }
                    currentrequest[count] = new Request(); //instanciates request class
                    //start node as count will = 0 at start 
                    if (count == 0)
                    {
                        root.userprompt = "Hey, how was your day";
                        
                        root.startp();
                        string startinput = Console.ReadLine().ToLower();
                        currentrequest[count].title(startinput); // sets name of objecrt as userinput
                        currentrequest[count].setnscore(NB1.negativescores(startinput, words)); // sets n score with result form function
                        currentrequest[count].setpscore(NB1.positivescores(startinput, words)); // sets p score w result from function 
                        root.determineSentiment(currentrequest[count].Pscore, currentrequest[count].Nscore);  // determines sentiment for the node 
                        root.getuserstring(root.sentiment); //responds based on sentiment 
                        if (root.sentiment == true)
                        {
                            PScore++;
                        }
                        else
                        {
                            Nscore++;
                        }


                        Console.WriteLine("have i understood this sentiment correctly (y/n)");
                        check = Console.ReadLine();
                        if (!root.sentiment && check == "y")
                        {
                            negativeresponese.Add(startinput);


                        }
                        else if (root.sentiment && check == "y")
                        {
                            positiveresponese.Add(startinput);
                        }
                        else if (!root.sentiment && check == "n")
                        {
                            positiveresponese.Add(startinput);
                        }
                        else
                        {
                            negativeresponese.Add(startinput);
                        }
                    }
                    else

                    {
                        node current = root;
                        Console.ReadLine();
                        root.nextquestion(count - 1);
                        root.startp();
                        string userinput = Console.ReadLine().ToLower();
                        currentrequest[count].title(userinput);
                        currentrequest[count].setnscore(NB1.negativescores(userinput, words)); //sets negative score for node
                        currentrequest[count].setpscore(NB1.positivescores(userinput, words));//sets positive score for node
                        root.determineSentiment(currentrequest[count].Pscore, currentrequest[count].Nscore);  // determines sentiment of root node 
                        if (root.sentiment == true) // changes nodes based on sentiment. 
                        {
                            current.positive1 = new node();
                            root.getuserstring(root.sentiment); // determines response given the sentiment
                            point = current.positive1;
                            root = point;
                            point = null;
                            PScore++;
                            // completes transference of node to keep track of conversation   
                        }
                        else if (root.sentiment == false)
                        {
                            current.negative1 = new node();
                            root.getuserstring(root.sentiment); // determines response based on sentiment 
                            point = current.negative1;
                            root = point;
                            point = null;
                            Nscore++;
                          // completes transference of nodes to keep track of conversation
                        }


                        Console.WriteLine("have i understood this sentiment correctly (y/n)");
                        check = Console.ReadLine();
                        if (!root.sentiment && check == "y")
                        {
                            negativeresponese.Add(userinput);


                        }
                        else if (root.sentiment && check == "y")
                        {
                            positiveresponese.Add(userinput);
                        }
                        else if (!root.sentiment && check == "n")
                        {
                            positiveresponese.Add(userinput);
                        }
                        else
                        {
                            negativeresponese.Add(userinput);
                        }

                    }
                    //if moving to negative/positive pull node userprompt from random bank of user prompts from given fields (p/n)
                    count++;
                }
            }

            if (PScore > Nscore) // returns how overrall day went 
            {
                netS = true;
            }
            else
            {
                netS = false;
            }

            return netS;
        }




        void writer()
        { 
            File.WriteAllLines("newpositivedata.csv", )
        
        
        
        }
    }

}
