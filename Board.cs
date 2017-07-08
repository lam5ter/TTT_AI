//Generic board for any 2D boardgame
using System;

namespace BoardGame
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

    class Board : Cursor
    {
        protected const int size = 3;
        char[,] board = new char[size, size];


        public void DisplayBoard()
        {
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
                    }
                    else if (v == ValX() && h == ValY() && h == 0)
                    {
                        Console.Write(" ^");
                    }
                    else if (h == 0)
                    {
                        Console.Write(" -");
                    }
                    else
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

        public char ValInBoard(int v, int h) { return board[v, h]; }
    }
}
