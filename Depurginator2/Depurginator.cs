using System;
using System.Collections.Generic;
using System.IO;

namespace Depurginator
{
    class Depurginator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Usage: Reads a file called numbers_in.txt from the same directory \n" +
                "the program is being executed from. The list should contain \n" +
                "the numbers you want depurginated seperated by a newline. Output \n" +
                "will be in format <number_in>,<operations_order>,<derpurginated_number>. \n\n" +
                "Operations_order is defined as \n" +
                "1: -49  \n" +
                "2: -49  \n" +
                "3: *.2  \n" +
                "4: -35  \n" +
                "5: -38  \n" +
                "6: /6   \n" +
                "7: /13  \n\n" +
                "Output file will be in execution directory and named depurginated_numbers.txt \n" +
                "and reverse_depurginated_numbers.txt \n");

            if (!File.Exists("numbers_in.txt"))
            {               
                Console.WriteLine("\nNo numbers_in.txt file found in current directory.\n\n" +
                    "Press Enter key to exit.\n");
                int input = Console.Read();
            }

            Console.WriteLine("Depurginating numbers in numbers_in.txt...\n");

            double result = 0;
            double revResult = 0;
            Permutator permutator = new Permutator();
            int[] arr1 = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            List<double> numberList = new List<double>();
            List<double> permutedList = new List<double>();
            List<double> resultList = new List<double>();
            List<double> reverseResultList = new List<double>();

            if (!ReadFile())
                return;

            permutator.Permutate(arr1, 0, 6, permutedList);

            foreach (double d in numberList)
            {
                int p = 0;
                result = d;
                revResult = d;

                foreach (var number in permutedList)
                {
                    p++;
                    string num = number.ToString();

                    foreach (char c in num)
                    {
                        switch (c)
                        {
                            case '1':
                                result = result - 49;
                                revResult = revResult + 49;
                                break;
                            case '2':
                                result = result - 49;
                                revResult = revResult - 49;
                                break;
                            case '3':
                                result = result * .2;
                                revResult = revResult * .5;
                                break;
                            case '4':
                                result = result - 35;
                                revResult = revResult + 35;
                                break;
                            case '5':
                                result = result - 38;
                                revResult = revResult + 38;
                                break;
                            case '6':
                                result = result / 6;
                                revResult = revResult * 6;
                                break;
                            case '7':
                                result = result / 13;
                                revResult = revResult * 13;
                                break;
                        }
                    }

                    if (p % 7 == 0)
                    {
                        double key = 0;
                        string skey = "";

                        for (int i = p - 7; i < p; i++)
                        {
                            skey += permutedList[i].ToString();
                        }

                        //if (!resultList.Exists(e => e.Equals(result)))
                        //{
                        key = double.Parse(skey);
                        resultList.Add(key);
                        resultList.Add(result);
                        reverseResultList.Add(key);
                        reverseResultList.Add(revResult);
                        //}
                        result = d;
                        revResult = d;
                    }
                }
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\depurginated_numbers.txt"))
                {
                    int x = 1;
                    int y = 1;
                    string outString = "";
                    string oNumber = numberList[0].ToString();

                    foreach (double line in resultList)
                    {
                        if (x % 5040 == 0 && y <= numberList.Count)
                        {
                            oNumber = numberList[y - 1].ToString();
                            y++;
                        }

                        if (x % 2 == 0)
                        {
                            outString += line.ToString();
                            outputFile.WriteLine(outString);
                        }
                        else
                        {
                            outString = oNumber + "," + line.ToString() + ",";
                        }
                        x++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to write to depurginated_numbers.txt");
                Console.WriteLine("Press Enter key to exit.");
                int input2 = Console.Read();
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\reverse_depurginated_numbers.txt"))
                {
                    int x = 1;
                    int y = 1;
                    string outString = "";
                    string oNumber = numberList[0].ToString();

                    foreach (double line in reverseResultList)
                    {
                        if (x % 5040 == 0 && y <= numberList.Count)
                        {
                            oNumber = numberList[y - 1].ToString();
                            y++;
                        }

                        if (x % 2 == 0)
                        {
                            outString += line.ToString();
                            outputFile.WriteLine(outString);
                        }
                        else
                        {
                            outString = oNumber + "," + line.ToString() + ",";
                        }
                        x++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to write to reverse_depurginated_numbers.txt");
                Console.WriteLine("Press Enter key to exit.");
                int input2 = Console.Read();
            }

            bool ReadFile()
            {
                bool isGood = true;
                string dir = System.IO.Directory.GetCurrentDirectory();
                try
                {
                    using (StreamReader reader = new StreamReader(dir + "\\numbers_in.txt"))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            numberList.Add(double.Parse(line));
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to read numbers_in.txt");
                    Console.WriteLine("Press Enter key to exit.");
                    int input4 = Console.Read();
                    isGood = false;
                }
                return isGood;
            }

            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("Numbers depurginated to depurginated_numbers.txt \nand reverse_depurginated_numbers.txt \n");
            Console.WriteLine("Press Enter key to exit.");
            int input5 = Console.Read();
        }
    }

    class Permutator
    {
        public void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public void Permutate(int[] list, int k, int m, List<double> numList)
        {
            int i;

            if (k == m)
            {
                for (i = 0; i <= m; i++)
                {
                    numList.Add(list[i]);
                }
            }
            else
                for (i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    Permutate(list, k + 1, m, numList);
                    Swap(ref list[k], ref list[i]);
                }
        }
    }
}
