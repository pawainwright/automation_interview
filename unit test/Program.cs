using System;
using System.Diagnostics;

namespace unit_test
{
    class Program
    {
        private static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        private static bool MatchNames(string s, string t)
        {
            if (s.ToUpper() == t.ToUpper()) { return true; }
            if (s.ToUpper().Contains(t.ToUpper())) { return true; }
            if (t.ToUpper().Contains(s.ToUpper())) { return true; }
            return false;
        }


        static void Main(string[] args)
        {

            //Test Fixture
            string FirstString = "RANDSTAD PUBLIC SERVICES LTD";
            string SecondString = "RANDSTAD PUBLIC SERVICES LIMITED";
            double DistanceScore = 0;
            bool NameMatch = false;

            //If either name is empty, return as no match
            if (FirstString.Length < 1 || SecondString.Length < 1)
            {
                NameMatch = false;
                DistanceScore = 0;
                return;
            }

            //Check whether names match (including exact coefficients)
            NameMatch = MatchNames(FirstString, SecondString);
            //Get Levenshtein distance in number of edits required
            int distance = Compute(FirstString, SecondString);
            //Convert steps to percentage score
            double longest = Math.Max(FirstString.Length, SecondString.Length);
            double pct = (longest - distance) / longest * 100;
            DistanceScore = pct;

            Debug.WriteLine("First company name: " + FirstString);
            Debug.WriteLine("Second company name: " + SecondString);
            Debug.WriteLine("Names Match: " + NameMatch.ToString());
            Debug.WriteLine("Longest string: " + longest.ToString());
            Debug.WriteLine("Lev edit distance: " + distance.ToString());
            Debug.WriteLine("Percentage similarity: " + DistanceScore.ToString());
        }
    }
}





        
        

    

