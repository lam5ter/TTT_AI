using System;

namespace TicTacToe
{
    class Cursor
    {
        int x, y;

        public void DisplayPosition()
        {
            Console.WriteLine("Cursor Position: [{0}, {1}]", x, y);
        }

        public void MoveXY(int temp1, int temp2)
        {
            x = temp1;
            y = temp2;
        }

        public void IncX() { x++; }
        public void DecX() { x--; }
        public void IncY() { y++; }
        public void DecY() { y--; }
        public int ValX() { return x; }
        public int ValY() { return y; }
    }

    class Board: Cursor
    {
        protected const int size = 3;
        char [,] board = new char [size, size];


        public void DisplayBoard()
        {
            Console.Clear();
            for (int v = 0; v < size; v++)
            {
                for (int h = 0; h < size; h++)
                {
                    if (h == 0)
                    {
                        Console.Write(" {0}", board[v, h]);
                    }
                    else
                    {
                        Console.Write("|{0}", board[v, h]);
                    }
                }
                Console.Write("\n");
                for (int h = 0; h < size; h++)
                {
                    if (v == ValX() && h == ValY() && h != 0)
                    {
                        Console.Write("-^");
                    } else if (v == ValX() && h == ValY() && h == 0)
                    {
                        Console.Write(" ^");
                    } else if (h == 0)
                    {
                        Console.Write(" -");
                    } else
                    {
                        Console.Write("--");
                    }
                }
                Console.Write("\n");
            }
        }

        public bool Insert(Object c, int v, int h)
        {
            if (board[v, h] == '\0')
            {
                board[v, h] = Convert.ToChar(c);
                return true;
            }
            return false;
        }
        public void ResetBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) board[i, j] = '\0';
            }
            ResetCursor();
        }
        public void MoveDown() { if (ValX() < (size - 1)) IncX(); }
        public void MoveUp() { if (ValX() > 0) DecX(); }
        public void MoveRight() { if (ValY() < (size - 1)) IncY(); }
        public void MoveLeft() { if (ValY() > 0) DecY(); }
        public void ResetCursor() { MoveXY(0, 0); }
        public int VPos() { return ValX(); }
        public int HPos() { return ValY(); }

        public char ValInBoard(int v, int h) { return board[h, v]; }
    }

    class Players
    {
        private int numPlayers;
        public int player = 0;

        public Players(int num) { numPlayers = num; }

        public void TogglePlayer()
        {
            player = (player == (numPlayers - 1)) ? 0 : player + 1;
        }

        public void TogglePlayer(int temp) { player = temp; }
    }

    class TicTacToe: Board
    {
        Players p = new Players(2);

        public int BoardSize() { return size; }

        public void InsertPlayer()
        {
            char c = '\0';
            switch (p.player)
            {
                case 0: c = 'X'; break;
                case 1: c = 'O'; break;
            }
            if (Insert(c, ValX(), ValY()) == true) p.TogglePlayer();
        }

        public char CurrentPlayer()
        {
            if (p.player == 0) return 'X';
            else return 'O';
        }
    }

    class TTTWinner
    {
        private char HoriWin(TicTacToe t)
        {
            for (int v = 0; v < t.BoardSize(); v++)
            {
                if (t.ValInBoard(v,0) == t.ValInBoard(v, 1) && t.ValInBoard(v, 1) == t.ValInBoard(v, 2))
                {
                    return t.ValInBoard(v, 0);
                }
            }
            return '\0';
        }

        private char VertWin(TicTacToe t)
        {
            for (int h = 0; h < t.BoardSize(); h++)
            {
                if (t.ValInBoard(0, h) == t.ValInBoard(1, h) && t.ValInBoard(1, h) == t.ValInBoard(2, h))
                {
                    return t.ValInBoard(0, h);
                }
            }
            return '\0';
        }

        private char DiagWin(TicTacToe t)
        {
            char c = t.ValInBoard(0, 0);
            if (c == t.ValInBoard(1, 1) && c == t.ValInBoard(2, 2)) return c;
            else
            {
                c = t.ValInBoard(0, 2);
                if (c == t.ValInBoard(1, 1) && c == t.ValInBoard(2, 0)) return c;
            }

            return '\0';
        }

        public char IsWinner(TicTacToe t)
        {
            char c = HoriWin(t);
            if (c != '\0') return c;
            else if ((c = VertWin(t)) != '\0') return c;
            else if ((c = DiagWin(t)) != '\0') return c;
            else return '\0';
        }
    }

    class MoveCount
    {
        private const int size = 3;
        public char player;
        public int[] columns = new int[size];
        public int[] rows = new int[size];
        public int[] diagonals = new int[size];

        public MoveCount(char c)
        {
            player = c;
        }

    }

    class TTT_AI
    {
        private const int win = 3;
        public MoveCount x = new MoveCount('X');
        public MoveCount o = new MoveCount('O');

        public bool WinningMove(TicTacToe t)
        {
            MoveCount n = x;
            for (int i = 0; i < win; i++)
            {
                if (n.columns[i] == (win - 1))
                {
                    for (int j = 0; j < win; j++)
                        if (t.ValInBoard(j, i) == '\0')
                        {
                            t.Insert(n.player, j, i);
                            return true;
                        }
                } else if (n.rows[i] == (win - 1))
                {
                    for (int j = 0; j < win; j++)
                        if (t.ValInBoard(i, j) == '\0')
                        {
                            t.Insert(n.player, i, j);
                            return true;
                        }
                }
            }
            if (n.diagonals[0] == (win - 1))
            {
                for (int j = 0; j < win; j++)
                    if (t.ValInBoard(j, j) == '\0')
                    {
                        t.Insert(n.player, j, j);
                        return true;
                    }
            }
            else if (n.diagonals[1] == (win - 1))
                for (int j = 0; j < win; j++)
                    if (t.ValInBoard(j, (t.BoardSize() - 1)) == '\0')
                    {
                        t.Insert(n.player, j, (t.BoardSize() - 1));
                        return true;
                    }
            return false;
        }

        public void MoveInput(TicTacToe t)
        {
            MoveCount n;
            if (t.CurrentPlayer() == 'O') n = o;
            else n = x;

            n.rows[t.ValX()]++;
            n.columns[t.ValY()]++;
            if (t.ValX() == t.ValY()) n.diagonals[0]++;
            else if (t.ValX() == (t.BoardSize() - 1 - t.ValY()))
                n.diagonals[1]++;
        }
    }

    class Executable
    {
        static void Main(string[] args)
        {
            TicTacToe t = new TicTacToe();
            TTTWinner w = new TTTWinner();
            TTT_AI ai = new TTT_AI();
            char victor = w.IsWinner(t);

            t.DisplayBoard();
            t.DisplayPosition();

            for (;;)
            {
                ConsoleKeyInfo k = Console.ReadKey();
                if (k.Key == ConsoleKey.R)
                {
                    t.ResetBoard();
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
                        case ConsoleKey.Enter: t.InsertPlayer();
                            ai.MoveInput(t);
                            break;
                    }
                }
                ai.WinningMove(t);
                t.DisplayBoard();
                t.DisplayPosition();
                if ((victor = w.IsWinner(t)) != '\0')
                    Console.WriteLine("Winner is {0}.", victor);
            }
        }
    }
}
