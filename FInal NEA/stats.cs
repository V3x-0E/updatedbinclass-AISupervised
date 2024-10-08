using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FInal_NEA
{
    internal class Naivebayes
    {

        public word[] generateProbabilities()
        //input new data to generate robabilities 
        {
            List<string> sentences = new List<string>();
            List<string> positivesentences = new List<string>();
            List<string> negativesentences = new List<string>();
            List<string> positivetokens = new List<string>();
            List<string> negativetokens = new List<string>();
            List<int> sentiment = new List<int>();
            Dictionary<string, double> Sysngram = new Dictionary<string, double>();
            { }
            List<string> unique = new List<string>();
            string temp1;
            List<string> token = new List<string>();
            Dictionary<string, double> Nngram = new Dictionary<string, double>();
            { }
            Dictionary<string, double> Pngram = new Dictionary<string, double>();
            { }
            
            double catcher;
            string temp2;
            
            int z = 0;
            string namer;
            string[] corpus = System.IO.File.ReadAllLines(@"insertfilepathhere");
            char[] sepchar = new char[] { ' ', '.', ',', '!', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
            // above is all data structres used in this function except the array of word itself

            for (int j = 1; j < corpus.Length; j++) // takes the corpus and splits it by the comma then splits it again by column 
            {
                string[] column = corpus[j].Split(',');
                sentences.Add(column[0].ToLower());
                sentiment.Add(Convert.ToInt32(column[1]));
                // adds the phrases/ words used and their sentiment 2 lists to be split off further 

            }

            for (int c = 0; c < sentences.Count; c++)
            {
                temp1 = sentences[c];
                foreach (string a in temp1.Split(sepchar, StringSplitOptions.RemoveEmptyEntries))
                {
                    token.Add(a);
                    // creates a list of every individual word used in the corpus 
                }

            }
            //splits up sentences into positive and negative
            for (int c = 0; c < sentiment.Count; c++)
            {
                if (sentiment[c] == -1)
                {
                    
                    negativesentences.Add(sentences[c]);
                }
                else if (sentiment[c] == 1)
                {
                    
                    positivesentences.Add(sentences[c]);
                }

            }


            //tokenizing all words in positive 
            for (int c = 0; c < positivesentences.Count; c++)
            {
                temp2 = positivesentences[c];
                foreach (string a in temp2.Split(sepchar, StringSplitOptions.RemoveEmptyEntries))
                {
                    positivetokens.Add(a);

                }

            }
            //tokenize all negative words 
            for (int c = 0; c < negativesentences.Count; c++)
            {
                temp2 = negativesentences[c];
                foreach (string a in temp2.Split(sepchar, StringSplitOptions.RemoveEmptyEntries))
                {
                    negativetokens.Add(a);

                }

            }
            // Ngrams give a value to every unique word in a dataset based on their frequency, this will be used later to determine the word probaility 
            for (int n = 0; n < token.Count; n++)
            {
                if (Sysngram.ContainsKey(token[n]))
                {
                    Sysngram[token[n]]++;
                }
                else
                {
                    Sysngram.Add(token[n], 1);
                    unique.Add(token[n]);// all unique words in a list for object generation later

                }

            }
            //Negative words Ngram
            for (int n = 0; n < negativetokens.Count; n++)
            {
                if (Nngram.ContainsKey(negativetokens[n]))
                {
                    Nngram[negativetokens[n]]++;
                }
                else
                {
                    Nngram.Add(negativetokens[n], 1);
                }

            }
            //Postivie words Ngram
            for (int n = 0; n < positivetokens.Count; n++)
            {
                if (Pngram.ContainsKey(positivetokens[n]) == true)
                {
                    Pngram[positivetokens[n]]++;
                }
                else
                {
                    Pngram.Add(positivetokens[n], 1);
                }

            }
            // array of objects used for each unique word is vital for program. 
            word[] wordcollection = new word[unique.Count];

            foreach (word a in wordcollection)
            {
                namer = unique[z];
                wordcollection[z] = new word();
                wordcollection[z].title(namer);
                z++;

            }
            // this loop gives all of the words the probabilities by cross checking their ngram values vs net negram count to determine probability fo the word in that given sentiment 
            for (int count = 0; count < wordcollection.Length; count++)
            {

                if (Pngram.ContainsKey(wordcollection[count].name)) // checks if ngram contains a specific word i.e. nice 
                {
                    Pngram.TryGetValue(wordcollection[count].name, out catcher); // gets the number of times that word is in the positive set of data 
                    wordcollection[count].determinePprob(catcher, Pngram.Count);  // assigns Positive probability through the function
                    

                }
                if (Nngram.ContainsKey(wordcollection[count].name)) // checks if ngram contains a specific word i.e. nice
                {
                    Nngram.TryGetValue(wordcollection[count].name, out catcher); // gets th enumber of times the word is in the ngram
                    wordcollection[count].determineNprob(catcher, Nngram.Count);
                    
                }
                // generates the probability of the word existing in the sentence 
                if (Sysngram.ContainsKey(wordcollection[count].name)) // checks if ngram contains a specific word i.e. nice
                {
                    Sysngram.TryGetValue(wordcollection[count].name, out catcher);
                    wordcollection[count].determineWprob(catcher, Sysngram.Count);

                }

               
            }
            // returns the array of objects to the main prgram with all given probabilities 
           // write(wordcollection);
            return wordcollection;
        }
        public double negativescores(string userinput, word[] words)// generates the negative probability given that the words exist for the request I.e. userinput 
        {
            char[] sepchar = new char[] { ' ', '.', ',', '!', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
            List<string> query = new List<string>();
            double Nscore;
            double temp = 1;
            
            foreach (string a in userinput.Split(sepchar, StringSplitOptions.RemoveEmptyEntries))  //tokenizes user input
            {
                query.Add(a);
            }
            for (int i = 0; i < words.Length; i++)
            {
                for (int c = 0; c < query.Count; c++) // refine formula
                    if (words[i].name == query[c])
                    {
                       
                        Nscore = ((words[i].Nprobability * 0.5) / words[i].wordprobability);
                        temp = temp * Nscore;//probability law of multiplying events

                    }

            }
            // returns new probability for P(sentiment | request)
            return temp;
            
        }
        public double positivescores(string userinput, word[] words)// generates the positive probability given that the words exist for the request I.e. userinput
        {
            char[] sepchar = new char[] { ' ', '.', ',', '!', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
            List<string> query = new List<string>();
            double Pscore;
            double temp = 1;
            foreach (string a in userinput.Split(sepchar, StringSplitOptions.RemoveEmptyEntries)) //tokenizes the user input
            {
                query.Add(a);
            }
            for (int i = 0; i < words.Length; i++)
            {
                for (int c = 0; c < query.Count; c++) // refine formula
                    if (words[i].name == query[c])
                    {
                        
                        Pscore = ((words[i].Pprobability * 0.5) / words[i].wordprobability);
                        temp = temp * Pscore; // probability law of multiplying for 2 events. 
                    }

            }
            // returns new probability for P(sentiment | request)
            return temp;
        }
        public word[] loadfromcsv()// loads previous "save file" in system unique format 
        {
            
            string[] c1 = File.ReadAllLines("newdata.csv"); //file for older data made by program
            int y = c1.Length/4;
            word[] newwords = new word[y]; // new word array 
            
            int x = 0;
            int count = 0;
            string temp;
            double temp1; // required variables
            for (int i =0; i < c1.Length - 1; i++) // sets all the variables for the new data into the word 
            {

                if (x == 0) //selection 
                {
                    
                    newwords[count] = new word();   
                    temp = c1[i].ToLower();
                    newwords[count].title(temp);
                    
                   
                }
                if (x == 1)
                {
                    
                    temp1 = Convert.ToDouble(c1[i]);
                    newwords[count].Nprobability = temp1;
                    
                }
                if (x == 2)
                {
                    temp1 = Convert.ToDouble(c1[i]);
                    newwords[count].Pprobability = temp1;     
                }
                if (x == 3)
                {
                    temp1 = Convert.ToDouble(c1[i]);
                    newwords[count].wordprobability = temp1;
                    x = -1;
                    count++;
                }
                x++;
               
                
            }
            
            
            

            return newwords;
        }
        public word[] average(word[] x, word[] y)
        {
            int temp = 0;
            int count = 0;
            int catcher;
            List<string> unknown = new List<string>();
            List<string> matched = new List<string>();
            List<string>matched1 = new List<string>();  
            if (x.Length > y.Length) // finds larger array to create new array to match sizes to ensure all words are added
            {
                temp = x.Length;
                
                for (int i = 0; i < temp; i++)
                {
                    unknown.Add(x[i].name); // adds all elements from array x to a list 
                    for (int n = 0; n < y.Length; n++)
                    {
                        if (unknown.Contains(y[n].name))
                        {
                            unknown.Remove(y[n].name); // removes any matching elements from y and adds them to the matched list
                            matched.Add(y[n].name);
                        }

                    }
                }
              matched1 =  matched.Distinct().ToList(); // makes sure there are no duplicate elements in the list
                for (int i = 0; i < y.Length; i++)
                {
                    unknown.Add(y[i].name); // adds all elements from array y to a list
                    for (int n = 0; n < matched1.Count; n++)
                    {
                        if (matched1.Contains(y[n].name))
                        {
                            unknown.Remove(y[n].name); //removes any matched elements from list 

                        }
                    }


                }
            }
            else if (y.Length > x.Length)
            {
                temp = y.Length;
            
                for (int i = 0; i < temp; i++)
                {
                    unknown.Add(y[i].name);
                    for (int n = 0; n < x.Length; n++)
                    {
                        if (unknown.Contains(x[n].name))
                        {
                            unknown.Remove(x[n].name);
                            matched.Add(x[n].name);
                        }
                    }
                }
                matched1 = matched.Distinct().ToList();
                for (int i = 0; i < x.Length; i++)
                {
                    unknown.Add(x[i].name);
                    for (int n = 0; n < matched1.Count; n++)
                    {
                        if (matched1.Contains(x[n].name))
                        {
                            unknown.Remove(x[n].name);
                        }
                    }


                }
            }
            else
            {
                temp = x.Length; 
            }
            word[] wordaverage = new word[matched.Count + unknown.Count];
            for (int i = 0; i < x.Length; i++)
            {
                for (int n = 0; n < y.Length; n++) // assigns the matched elements in new word arra by using basic average formula 
                {
                    if (x[i].name == y[n].name)
                    {
                        wordaverage[count] = new word();
                        wordaverage[count].name = y[n].name;
                        wordaverage[count].Nprobability = (x[i].Nprobability + y[n].Nprobability) / 2; // sets N prob
                        wordaverage[count].Pprobability = (x[i].Pprobability + y[n].Pprobability) / 2; // sets P prob
                        wordaverage[count].wordprobability = (x[i].wordprobability + y[n].wordprobability) / 2; // probsets 
                        count++;
                        
                    }
                }
            
            }
            
            catcher = count;
            

              for (int n = 0; n < x.Length; n++) // adds any words that arent the same in both arrays to the new array 
              {
                    if (unknown.Contains(x[n].name)) // checks all elements in x for any non matches and adds them straight across
                    {

                        wordaverage[catcher] = new word();
                        wordaverage[catcher].name = x[n].name;
                        wordaverage[catcher].Nprobability = x[n].Nprobability;
                        wordaverage[catcher].Pprobability = x[n].Pprobability;
                        wordaverage[catcher].wordprobability = x[n].wordprobability;
                        catcher++;
                        unknown.Remove(x[n].name); // removes found eleemnt 

                    }           
              }
            for (int n = 0; n < y.Length; n++)
            {
                if (unknown.Contains(y[n].name)) // checks all elements in y for any non matches and adds them straight across
                {
                    wordaverage[catcher] = new word();
                    wordaverage[catcher].name = y[n].name;
                    wordaverage[catcher].Nprobability = y[n].Nprobability;
                    wordaverage[catcher].Pprobability = y[n].Pprobability;
                    wordaverage[catcher].wordprobability = y[n].wordprobability;
                    catcher++;
                    unknown.Remove(y[n].name);// removes found element 

                }

            }


            return wordaverage;
        }
        public void write(word[] x) //writes in system unique format 
        {
            List<string> adder = new List<string>();
            word worda = new word();
            for (int i =0; i < x.Length; i++) // adds all elements in the word array into a list 
            {
                worda = x[i];   
                adder.Add(worda.name);
                adder.Add(worda.Nprobability.ToString());
                adder.Add(worda.Pprobability.ToString());
                adder.Add(worda.wordprobability.ToString());
            }
           

            System.IO.File.WriteAllLines("newdata.csv", adder); // writes everything into a csv 
        
        }
    }
}
