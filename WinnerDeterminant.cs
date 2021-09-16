using System;

namespace ThirdTask
{
    public class WinnerDeterminant
    {
        // Winner determinant algorithm
        private static int GetWinner(int countOfMoves, int computerMove, int userMove)
        {
            var number = (computerMove - userMove + countOfMoves) % countOfMoves;
            if (number == 0) return 0;
            return (int) (((double) countOfMoves / 2 - number) / Math.Abs((double) countOfMoves / 2 - number));
        }

        // Get winner with custom answers
        public static string GetWinnerString(int countOfMoves, int computerMove, int userMove, string[] answers)
        {
            return answers[GetWinner(countOfMoves, computerMove, userMove) + 1];
        }

        // Get winner with default answers
        public static string GetWinnerString(int countOfMoves, int computerMove, int userMove)
        {
            return GetWinnerString(countOfMoves, computerMove, userMove, new[] {"You lose", "Draw", "You win!"});
        }
    }
}