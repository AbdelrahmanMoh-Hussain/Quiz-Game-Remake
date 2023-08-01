using System.IO;
using static System.Net.WebRequestMethods;

namespace Quiz_Game_Remake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game.Start();
        }

        
    }

    struct User
    {
        public string Name { get; set; }
        public int Score { get; set; } = 0;
        public User(string name, int score)
        {
            Name = name;
            Score = score;
        }

    }


    public class Game
    {
        public static void Start()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Quiz Game");
                Console.WriteLine("Enter User Name:  ");
                User user = new User();
                user.Name = Console.ReadLine();
                string feild;
                while (true)
                {
                    Console.WriteLine("Choose a Feild: \nPress 'sp' for Sports, Press 't' for Tech, Press 'm' for Movies, Press 'sc' for scince");
                    feild = Console.ReadLine();
                    if (feild != "sp" && feild != "sc" && feild != "m" && feild != "t")
                    {
                        Console.WriteLine("Invaild Answer!!");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (feild == "sp") // Sport Filed
                {
                    var path = "F:\\C#\\Quiz Game Remake\\Quiz Game Remake\\data\\Sport.txt";
                    user.Score = GameQuestion(path);
                }
                else if (feild == "t") // Technology Filed
                {
                    var path = "F:\\C#\\Quiz Game Remake\\Quiz Game Remake\\data\\Tech.txt";
                    user.Score = GameQuestion(path);
                }
                else if (feild == "m") // Movies Filed
                {
                    var path = "F:\\C#\\Quiz Game Remake\\Quiz Game Remake\\data\\Movies.txt";
                    user.Score = GameQuestion(path);
                }
                else if (feild == "sc") // Scince Filed
                {
                    var path = "F:\\C#\\Quiz Game Remake\\Quiz Game Remake\\data\\Science.txt";
                    user.Score = GameQuestion(path);
                }

                ShowUserScore(user);

                Console.WriteLine("Press 'p' to play again, 'x' to quit");
                if (Console.ReadLine() == "x") break;
            }
        }

        private static int GameQuestion(string? path)
        {
            var lines = System.IO.File.ReadAllLines(path);
            var countCorrectAns = 0;
            var award = 250m;
            var questionNum = 1;
            foreach (var line in lines)
            {
                var contents = line.Split(',');
                Console.Write($"Question {questionNum++}: {contents[0]}");
                Console.Write($"Choices: \n{contents[1]}\t{contents[2]}\t{contents[3]}\t{contents[4]}\n");
                Console.Write("Enter your answer: ");
                var ans = Console.ReadLine();

                while (ans.ToLower() != "a" && ans.ToLower() != "b" && ans.ToLower() != "c" && ans.ToLower() != "d") 
                {
                    Console.WriteLine("Please, Enter correct choice A,B,C,D");
                    ans = Console.ReadLine();
                } 

                if (ans.ToLower() == contents[5].ToLower())
                {
                    Console.WriteLine($"Correct answer, you win {award} L.E.!!!");
                    award *= 2;
                    countCorrectAns++;

                    if (countCorrectAns == 5) { Console.WriteLine("Congratulations!! You reached Level 2"); }
                    else if (countCorrectAns == 10) { Console.WriteLine("Congratulations!! You won the Quiz Game"); }
                }
                else
                {
                    Console.WriteLine("Sorry, Wrong Answer");
                    Console.WriteLine("\n******************************");
                    break;
                }
                Console.WriteLine("\n******************************");
            }

            return countCorrectAns;
        }

        private static void ShowUserScore(User user)
        {
            Dictionary<string, int> scoreDictionary = new Dictionary<string, int>();

            var path = "F:\\C#\\Quiz Game Remake\\Quiz Game Remake\\data\\Score.txt";
            var lines = System.IO.File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var contents = line.Split();
                scoreDictionary[contents[1]] = int.Parse(contents[2]);
            }
            scoreDictionary[user.Name] = user.Score;

            var sortedDict = from entry in scoreDictionary orderby entry.Value ascending select entry;

            var newLines = "";
            var rank = 1;
            foreach(var entry in sortedDict)
            {
                newLines += $"{rank++} {entry.Key} {entry.Value}\n";
                if (rank > 10) break;
            }
            System.IO.File.WriteAllText(path, newLines);
        }
    }
}