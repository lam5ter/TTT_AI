using System;

namespace BoardGame
{
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

    class TicTacToe : Board
    {
        Players p = new Players(2);
        private int total = 0;

        public int BoardSize() { return size; }

        public bool InsertPlayer()
        {
            char c = '\0';
            switch (p.player)
            {
                case 0: c = 'X'; break;
                case 1: c = 'O'; break;
            }
            if (Insert(c, ValX(), ValY()) == true)
            {
                p.TogglePlayer();
                return true;
            }
            else return false;
        }

        public char CurrentPlayer()
        {
            if (p.player == 0) return 'X';
            else return 'O';
        }

        public void IncCounter() { total++; }

        public bool IsFull(TicTacToe t)
        {
            return (total >= t.BoardSize() * t.BoardSize()) ? true : false;
        }

        public void Reset()
        {
            total = 0;
            ResetBoard();
        }

        private char HoriWin(TicTacToe t)
        {
            for (int v = 0; v < t.BoardSize(); v++)
            {
                if (t.ValInBoard(v, 0) == t.ValInBoard(v, 1) && t.ValInBoard(v, 1) == t.ValInBoard(v, 2))
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
}
