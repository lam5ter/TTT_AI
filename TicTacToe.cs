//TicTacToe with an auto-finishing AI
//To use the AI, #define AI at the beginning of this file
#define AI
using System;

namespace BoardGame
{
    class Executable
    {
        static void Main(string[] args)
        {
            TicTacToe t = new TicTacToe();
#if AI
            TTT_AI ai = new TTT_AI();
#endif
            char victor = t.IsWinner(t);

            t.DisplayBoard();
            t.DisplayPosition();

            for (;;)
            {
                ConsoleKeyInfo k = Console.ReadKey();
                Console.Clear();
                if (k.Key == ConsoleKey.R)
                {
                    t.Reset();
#if AI
                    ai.ResetAI();
#endif
                } else if (k.Key == ConsoleKey.Q || k.Key == ConsoleKey.Escape)
                    System.Environment.Exit(0);
                else if (victor == '\0')
                {
                    switch (k.Key)
                    {
                        case ConsoleKey.W: t.MoveUp(); break;
                        case ConsoleKey.S: t.MoveDown(); break;
                        case ConsoleKey.A: t.MoveLeft(); break;
                        case ConsoleKey.D: t.MoveRight(); break;
                        case ConsoleKey.Enter: if (t.InsertPlayer()) t.IncCounter();
#if AI
                            ai.MoveInput(t);
                            ai.WinningMove(t);
#endif
                            break;
                    }
                }
                t.DisplayBoard();
                t.DisplayPosition();
                if ((victor = t.IsWinner(t)) != '\0')
                    Console.WriteLine("Winner is {0}.", victor);
                else if (t.IsFull(t)) Console.WriteLine("Draw.");
            }
        }
    }
}
