using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Console_Intro_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Number Guesser 9000! Please write a lower value for the random number generator!");
            BigInteger lowerNum = BigInteger.Parse(Console.ReadLine());

            string filePath = "I:\\rbk\\Elevmappar\\2023\\PROG1222320\\Jonatan Wåger M TE3 22 UNY\\FileName\\BigIntegerNumber.txt";
            string fileContent = File.ReadAllText(filePath);
            if (BigInteger.TryParse(fileContent, out BigInteger highNum))
            {
                Console.WriteLine($"Imported BigInteger: {highNum}");
            }
            else
            {
                Console.WriteLine("Invalid format in the file.");
                Environment.Exit(1);
            }
            //Console.WriteLine("And a higher value please!");
            //BigInteger highNum = BigInteger.Parse(Console.ReadLine());

            BigInteger guessNum = RandomBigInteger(lowerNum, highNum);
            
            Console.WriteLine("System generated a number between " + lowerNum + " and " + highNum);
            Thread.Sleep(2000);
            Console.WriteLine("Write 1 to guess yourself or 2 to have an algoritm find it for you!");

            if (highNum > int.MaxValue)
            {
                Console.WriteLine("The given range excedes the capabilities of sef guessing. The number will be guessed by algoritm! Please stand by...");
                Thread.Sleep(2000);
                AlgGuess(guessNum, lowerNum, highNum);
            }
            else
            {
                int algQuestion = Int32.Parse(Console.ReadLine());
                if (algQuestion == 1)
                {
                    Guessing(guessNum);
                }
                else if (algQuestion == 2)
                {
                    AlgGuess(guessNum, lowerNum, highNum);
                }
                else
                {
                    Environment.Exit(1);
                }
            }
            
        }
        static BigInteger RandomBigInteger(BigInteger minValue, BigInteger maxValue)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            while (true)
            {
                int byteCount = (maxValue - minValue).ToByteArray().Length;

                byte[] randomBytes = new byte[byteCount];
                rand.NextBytes(randomBytes);

                BigInteger guessNum = new BigInteger(randomBytes);
                if (guessNum > 0)
                {
                    return guessNum % (maxValue - minValue) + minValue;
                }
            }
        }

        static void Guessing(BigInteger guessNum)
        {
            int numOfGuesses = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                Console.WriteLine("Guess the number:");
                int guess = Int32.Parse(Console.ReadLine());
                if (guess < guessNum)
                {
                    Console.WriteLine("You guessed too low");
                    numOfGuesses++;
                }
                else if (guess > guessNum)
                {
                    Console.WriteLine("You guessed too high");
                    numOfGuesses++;
                }
                else if (guess == guessNum)
                {
                    sw.Stop();
                    numOfGuesses++;
                    break;
                }
                else {
                    Environment.Exit(1);
                }
            }
            TimeSpan time = sw.Elapsed;
            double elapsedTime = time.TotalSeconds;
            Victory(elapsedTime, numOfGuesses);
        }

        static void AlgGuess(BigInteger guessNum, BigInteger lowerNum, BigInteger highNum)
        {
            List<BigInteger> guessedNumbers = new List<BigInteger> ();
            BigInteger numOfGuesses = 0;
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            while (true)
            {
                BigInteger guess = RandomAlgoritmGuess(lowerNum, highNum);
                while (true)
                {
                    if (!guessedNumbers.Contains(guess))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(guess + " has already been guessed, retrying!");
                    }
                    guess = RandomAlgoritmGuess(lowerNum, highNum);
                }
                guessedNumbers.Add(guess);
                Console.WriteLine(guess);
                //Thread.Sleep(10);
                if (guess < guessNum)
                {
                    lowerNum = guess;
                    numOfGuesses ++;
                    Console.WriteLine("Too low");
                }
                else if (guess > guessNum)
                {
                    highNum = guess;
                    numOfGuesses ++;
                    Console.WriteLine("Too high");
                }
                else if (guess == guessNum)
                {
                    sw1.Stop();
                    numOfGuesses++;
                    Console.WriteLine("Just right! Stay put!");
                    Thread.Sleep(5000);
                    break;
                }
                else
                {
                    Environment.Exit(2);
                }
                //List<BigInteger> filteredList = FilterNumbersInRange
                
            }
            TimeSpan time = sw1.Elapsed;
            double elapsedTime = time.TotalMilliseconds;
            Console.WriteLine("The number was: " + guessNum);
            Victory(elapsedTime, numOfGuesses);
        }
        static BigInteger RandomAlgoritmGuess(BigInteger minValue, BigInteger maxValue)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            while (true)
            {
                int byteCount = (maxValue - minValue).ToByteArray().Length;

                byte[] randomBytes = new byte[byteCount];
                rand.NextBytes(randomBytes);

                BigInteger guess = new BigInteger(randomBytes);

                if (guess > 0)
                {
                    return guess % (maxValue - minValue) + minValue;
                }
            }
        }   
            
            static void Victory(double elapsedTime, BigInteger numOfGuesses)
        {
            Thread.Sleep(2000);
            Console.WriteLine("You won, congrats! It took: " + Math.Round(elapsedTime) + " milliseconds, and " + numOfGuesses + " attempts!");
        }
    }
}
