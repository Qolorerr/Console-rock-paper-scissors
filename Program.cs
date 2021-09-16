using System;
using System.Collections.Generic;
using ConsoleTables;

namespace ThirdTask
{
    internal class Program
    {
        private static void CheckArgs(string[] args)
        {
            // Checking number of arguments
            if (args.Length < 3)
                throw new ArgumentException("Too few arguments");
            else if (args.Length % 2 == 0)
                throw new ArgumentException("Even number of arguments");
            else if (new HashSet<string>(args).Count != args.Length)
                // Checking for difference of arguments
                throw new ArgumentException("There are the same arguments");
        }


        private static int GetRandomMove(int countOfMoves)
        {
            return (int) (BitConverter.ToUInt32(SecureKeyGenerator.GetRandomByteSequence(4), 0) % (uint) countOfMoves);
        }


        private static void ShowAvailableMoves(string[] moves, int countOfMoves)
        {
            Console.WriteLine("Available moves:");
            for (var i = 0; i < countOfMoves; i++) Console.WriteLine(i + 1 + " - " + moves[i]);
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }


        private static void ShowHelpMessage(string[] moves, int countOfMoves)
        {
            // First row
            var table = new ConsoleTable("PC \\ User");
            for (var i = 0; i < countOfMoves; i++) table.AddColumn(new[] {moves[i]});

            // Other rows
            for (var i = 0; i < countOfMoves; i++)
            {
                var newRow = new string[countOfMoves + 1];
                newRow[0] = moves[i];
                for (var j = 0; j < countOfMoves; j++)
                    newRow[j + 1] =
                        WinnerDeterminant.GetWinnerString(countOfMoves, i, j, new[] {"LOSE", "DRAW", "WIN"});

                table.AddRow(newRow);
            }

            table.Write(Format.Alternative);
        }


        private static int GetUserMove(string[] moves, int countOfMoves)
        {
            while (true)
            {
                // Showing available moves
                ShowAvailableMoves(moves, countOfMoves);

                // Parsing user input
                Console.Write("Enter your move: ");
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var userMove))
                    if (userMove >= 0 && userMove <= countOfMoves)
                        return userMove;

                // Showing help table
                if (userInput == "?") ShowHelpMessage(moves, countOfMoves);
            }
        }


        public static void Main(string[] moves)
        {
            // Checking arguments
            try
            {
                CheckArgs(moves);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("You need to specify an odd number of more than 2 different arguments");
                Console.WriteLine("For example: Rock Paper Scissors");
                Environment.Exit(0);
            }

            // PC turn and making HMAC
            var countOfMoves = moves.Length;
            var hmac = new Hmac();
            var computerMove = GetRandomMove(countOfMoves);
            Console.WriteLine("HMAC: " + hmac.Encrypt(moves[computerMove]));

            // User turn
            var userMove = GetUserMove(moves, countOfMoves);
            if (userMove == 0)
            {
                Console.WriteLine("Bye!");
                return;
            }

            userMove--;

            // Writing results
            Console.WriteLine("Your move: " + moves[userMove]);
            Console.WriteLine("Computer move: " + moves[computerMove]);
            Console.WriteLine(WinnerDeterminant.GetWinnerString(countOfMoves, computerMove, userMove));
            Console.WriteLine("HMAC key: " + hmac.GetHexSecureKey());
        }
    }
}