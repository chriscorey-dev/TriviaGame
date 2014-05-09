using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TriviaGame
{
    class Program
    {
        //Public variables
        static string input = "";
        static int correct = 0;
        static int incorrect = 0;
        static List<string> highscores = new List<string>() { };
        static bool GMTrivia = false;
        static bool GMVideoGames = false;
        static bool GMMovies = false;
        static bool TFilms = false;
        static bool TLyrics = false;
        static bool TGeo = false;
        static bool TCaps = false;
        static bool TRandom = false;

        static void Main(string[] args)
        {
            Welcome();
            Menu();
            GameOver();
            Environment.Exit(0);
        }

        //Welcome Function
        static void Welcome()
        {
            Print("Welcome to the game!");
            Print(@"You will be guessing trivia questions until you get either 10 
correct or 10 incorrect.");
            PrintWrite("At any time, you can type ");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintWrite("'Help'");
            Console.ResetColor();
            Print(" to view this text");
            PrintWrite("At any time, you can type ");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintWrite("'Highscores'");
            Console.ResetColor();
            Print(" to view highscores");
            PrintWrite("At any time, you can type ");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintWrite("'Menu'");
            Console.ResetColor();
            Print(" to to select the type of trivia to play.");
            Print("Good luck!");
            End();
            Console.Clear();
        }

        //Menu Function
        static void Menu()
        {
            Print("What type of trivia do you want to play?");
            Print("1. Trivia");
            Print("2. Video Games");
            Print("3. Movies");
            input = Console.ReadLine().ToLower();

            if (input == "help")
            {
                Console.WriteLine();
                Welcome();
            }
            else if (input == "highscores" || input == "scores")
            {
                HighScores();
            }
            else if (input == "menu")
            {
                Console.Clear();
                Menu();
            }
            else if (input == "1" || input == "trivia")
            {
                GMTrivia = true;
                Console.Clear();
                Print("What section do you choose in Trivia?");
                Print("1. 80's Films");
                Print("2. Lyrics");
                Print("3. Geography");
                Print("4. Capitals of...");
                Print("5. Random");

                input = Console.ReadLine().ToLower();
                if (input == "1" || input == "80's films")
                {
                    TFilms = true;
                }
                else if (input == "2" || input =="lyrics")
                {
                    TLyrics = true;
                }
                else if (input == "3" || input == "geography")
                {
                    TGeo = true;
                }
                else if (input == "4" || input == "capitals of...")
                {
                    TCaps = true;
                }
                else  if (input == "5" || input == "random")
                {
                    TRandom = true;
                }
                Console.Clear();
                input = "";
            }
            else if (input == "2" || input == "video games")
            {
                Console.Clear();
                GMVideoGames = true;
            }
            else if (input == "3" || input == "movies")
            {
                Console.Clear();
                GMMovies = true;
            }
            GetTriviaList();
        }

        //GetTriviaList Fuction
        static List<Trivia> GetTriviaList()
        {
            //Get Contents from the file.  Remove the special char "\r".  Split on each line.  Convert to a list.
            List<string> contents = new List<string>() {};

            if (GMTrivia)
            {
                GMTrivia = false;
                List<string> contentsTemp = File.ReadAllText("trivia.txt").Replace("\r", "").Split('\n').ToList();

                if (TFilms)
                {
                    contents = contentsTemp.Where(x => x.Contains("80's Films:")).ToList();
                    TFilms = false;
                }
                else if (TLyrics)
                {
                    contents = contentsTemp.Where(x => x.Contains("Lyrics:")).ToList();
                    TLyrics = false;
                }
                else if (TGeo)
                {
                    contents = contentsTemp.Where(x => x.Contains("Geography:")).ToList();
                    TGeo = false;
                }
                else if (TCaps)
                {
                    contents = contentsTemp.Where(x => x.Contains("What is the capital of")).ToList();
                    TCaps = false;
                }
                else if (TRandom)
                {
                    contents = File.ReadAllText("trivia.txt").Replace("\r", "").Split('\n').ToList();
                    TRandom = false;
                }
                else
                {
                    Console.WriteLine();
                    Print("Please pick a different type of trivia.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Menu();
                }
            }
            else if (GMVideoGames)
            {
                contents = File.ReadAllText("VideoGames.txt").Replace("\r", "").Split('\n').ToList();
                GMVideoGames = false;
            }
            else if (GMMovies)
            {
                contents = File.ReadAllText("Movies.txt").Replace("\r", "").Split('\n').ToList();
                GMMovies = false;
            }
            else
            {
                Console.WriteLine();
                Print("Please pick a different type of trivia.");
                Thread.Sleep(2000);
                Console.Clear();
                Menu();
            }

            //Each item in list "contents" is now one line of the Trivia.txt document.
            while (correct < 10 && incorrect < 10)
            {
                var randomTemp = new Random();
                var random = randomTemp.Next(0, contents.Count());
                var question = contents[random].Split('*');
                Print(question[0]);
                input = Console.ReadLine().ToLower();
                Console.WriteLine();

                if (input == "help")
                {
                    Console.WriteLine();
                    Welcome();
                }
                else if (input == "highscores" || input == "scores")
                {
                    HighScores();
                }
                else if (input == "menu")
                {
                    Console.Clear();
                    Menu();
                }
                else if (input == question[1] || input == "yay")
                {
                    Print("Answer: " + question[1]);
                    Console.WriteLine();
                    Print("Great! That answer is correct.");
                    correct++;
                    Print("Correct: " + correct);
                    Print("Incorrect: " + incorrect);
                    End();
                    Console.Clear();
                }
                else if (input != question[1])
                {
                    Print("Answer: " + question[1]);
                    Console.WriteLine();
                    Print("Oh no, That answer is incorrect.");
                    incorrect++;
                    Print("Correct: " + correct);
                    Print("Incorrect: " + incorrect);
                    End();
                    Console.Clear();
                }
            }
            
            return new List<Trivia>();
        }

        //HighScores Function
        static void HighScores()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();
            highscores = File.ReadAllText("HighScores.txt").Replace("\r", "").Split('\n').ToList();
            foreach (var item in highscores)
            {
                Print(item);
                File.WriteAllText("HighScores.txt", "Winner");
            }
            End();
            Console.Clear();
        }

        //Game Over
        static void GameOver()
        {
            if (correct == 10)
            {
                Print("Congratulations, you won!");
                Print("Correct: " + correct);
                Print("Incorrect: " + incorrect);
                End();
            }
            else if (incorrect == 10)
            {
                Print("Oh no, you lost!");
                Print("Correct: " + correct);
                Print("Incorrect: " + incorrect);
                End();
            }
        }

        //Print Function
        static void Print(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i]);
                Thread.Sleep(1);
            }
            Console.WriteLine();
        }

        //PrintWrite Function
        static void PrintWrite(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i]);
                Thread.Sleep(1);
            }
        }

        //End Function
        static void End()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Print("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
