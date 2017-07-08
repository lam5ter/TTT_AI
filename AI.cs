using System;

class MoveCount
{
    private const int size = 3;
    public char player;
    public int[] columns = new int[size];
    public int[] rows = new int[size];
    public int[] diagonals = new int[size-1];

    public MoveCount(char c)
    {
        player = c;
    }

}

namespace BoardGame
{
    class TTT_AI
    {
        private const int win = 3, size = 3, numDiag = 2;
        public MoveCount x = new MoveCount('X');
        public MoveCount o = new MoveCount('O');

        public bool WinningMove(TicTacToe t)
        {
            MoveCount n = x;
            if (n.player != t.CurrentPlayer()) return false;
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
                }
                if (n.rows[i] == (win - 1))
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
                    if (t.ValInBoard(j, (size - 1 - j)) == '\0')
                    {
                        t.Insert(n.player, j, (size - 1 - j));
                        return true;
                    }
            return false;
        }

        public void MoveInput(TicTacToe t)
        {
            MoveCount n;
            //The move from the previous player
            if (t.CurrentPlayer() == 'O') n = x;
            else n = o;

            n.rows[t.ValX()]++;
            n.columns[t.ValY()]++;
            if (t.ValX() == t.ValY()) n.diagonals[0]++;
            if (t.ValX() == (t.BoardSize() - 1 - t.ValY()))
                n.diagonals[1]++;
            /*
            Console.WriteLine("{0} {1} {2}", n.rows[0], n.rows[1], n.rows[2]);
            Console.WriteLine("{0} {1} {2}", n.columns[0], n.columns[1], n.columns[2]);
            Console.WriteLine("{0} {1}", n.diagonals[0], n.diagonals[1]);
            */
        }

        public void ResetAI()
        {
            for (int i = 0; i < size; i++)
            {
                x.columns[i] = x.rows[i] = o.columns[i] = o.rows[i] = 0;
                if (i < numDiag) x.diagonals[i] = o.diagonals[i] = 0;
            }
        }
    }
}
