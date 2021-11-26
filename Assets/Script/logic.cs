using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
public class logic : MonoBehaviour
{
    static List<List<(int s, string t)>> board = new List<List<(int s, string t)>>();

    public abstract class Chessman
    {
        internal (int x, int z) Posi { get; set; }
        internal bool Side { get; set; }

        public Chessman((int, int) posi, bool side)
        {
            Posi = posi;
            Side = side;
        }
        public abstract List<(int, int)> GetPossibleMove();

        public (int, int) MakeMove(int xP, int zP)
        {
            (int, int) r = PMakeMove(Posi.x, Posi.z, xP, zP);
            Posi = (xP, zP);
            return r;
        }

    }
    public class Zu: Chessman
    {
        public Zu((int, int) posi, bool side) : base(posi, side)
        {
            Posi = posi;
            Side = side;
        }
        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            if (Side)
            {
                bool forward = Posi.z + 1 <= 9 && board[Posi.x][Posi.z + 1].s != 1;
                if (forward)
                {
                    moves.Add((Posi.x, Posi.z + 1));
                }
            }
            else
            {
                bool forward = Posi.z - 1 >= 0 && board[Posi.x][Posi.z - 1].s != 2;
                if (forward)
                {
                    moves.Add((Posi.x, Posi.z - 1));
                }
            }

            if ((Posi.z >= 5 && Side) ||(Posi.z <= 4 && !Side))
            {
                int ally;
                if (Side)
                {
                    ally = 1;
                }
                else
                {
                    ally = 2;
                }
                bool left = Posi.x - 1 >= 0 && board[Posi.x - 1][Posi.z].s != ally;
                if (left)
                {
                    moves.Add((Posi.x - 1, Posi.z));
                }

                bool right = Posi.x + 1 <= 8 && board[Posi.x + 1][Posi.z].s != ally;
                if (right)
                {
                    moves.Add((Posi.x + 1, Posi.z));
                }
            }

            return moves;
        }
        
    }
    
    public class Ju: Chessman
    {
        public Ju((int, int) posi, bool side) : base(posi, side) 
        {
            Posi = posi;
            Side = side;
        }

        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            int ally = 1;
            if (!Side)
            {
                ally = 2;
            }

            //forward
            int z = Posi.z + 1;
            while (z < 10 && board[Posi.x][z].s != ally)
            {
                moves.Add((Posi.x, z));
                if (board[Posi.x][z].s != ally && board[Posi.x][z].s != 0)
                {
                    break;
                }
                z += 1;
            }
            
            //backward
            z = Posi.z - 1;
            while (z >= 0 && board[Posi.x][z].s != ally)
            {
                moves.Add((Posi.x, z));
                if (board[Posi.x][z].s != ally && board[Posi.x][z].s != 0)
                {
                    break;
                }
                z -= 1;
            }
            
            //left
            int x = Posi.x - 1;
            while (x >= 0 && board[x][Posi.z].s != ally)
            {
                moves.Add((x, Posi.z));
                if (board[x][Posi.z].s != ally && board[x][Posi.z].s != 0)
                {
                    break;
                }
                x -= 1;
            }

            //right
            x = Posi.x + 1;
            while (x < 9 && board[x][Posi.z].s != ally)
            {
                moves.Add((x, Posi.z));
                if (board[x][Posi.z].s != ally && board[x][Posi.z].s != 0)
                {
                    break;
                }
                x += 1;
            }

            return moves;
        }
    }

    public class Pao: Chessman
    {
        public Pao((int, int) posi, bool side) : base(posi, side) 
        {
            Posi = posi;
            Side = side;
        }

        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            int ally = 1;
            int lo = 11;
            if (!Side)
            {
                ally = 2;
            }

            //forward
            int z = Posi.z + 1;
            while (z < 10)
            {
                if (board[Posi.x][z].s == 0)
                {
                    moves.Add((Posi.x, z));
                }
                else
                {
                    lo = z;
                    break;
                }
                z += 1;
            }

            if (lo < 9)
            {
                for (int i = lo + 1; i < 10; i++)
                {
                    if (board[Posi.x][i].s != 0 && board[Posi.x][i].s != ally)
                    {
                        moves.Add((Posi.x, i));
                        break;
                    }
                }
            }

            lo = -1;

            //backward
            z = Posi.z - 1;
            while (z >= 0)
            {
                if (board[Posi.x][z].s == 0)
                {
                    moves.Add((Posi.x, z));
                }
                else
                {
                    lo = z;
                    break;
                }
                z -= 1;
            }
            
            if (lo > 0)
            {
                for (int i = lo - 1; i >= 0; i--)
                {
                    if (board[Posi.x][i].s != 0 && board[Posi.x][i].s != ally)
                    {
                        moves.Add((Posi.x, i));
                        break;
                    }
                }
            }

            lo = -1;
            
            //left
            int x = Posi.x - 1;
            while (x >= 0)
            {
                if (board[x][Posi.z].s == 0)
                {
                    moves.Add((x, Posi.z));
                }
                else
                {
                    lo = x;
                    break;
                }
                x -= 1;
            }
            
            if (lo > 0)
            {
                for (int i = lo - 1; i >= 0; i--)
                {
                    if (board[i][Posi.z].s != 0 && board[i][Posi.z].s != ally)
                    {
                        moves.Add((i, Posi.z));
                        break;
                    }
                }
            }

            lo = 11;

            //right
            x = Posi.x + 1;
            while (x < 9)
            {
                if (board[x][Posi.z].s == 0)
                {
                    moves.Add((x, Posi.z));
                }
                else
                {
                    lo = x;
                    break;
                }
                x += 1;
            }
            if (lo < 8)
            {
                for (int i = lo + 1; i <= 8; i++)
                {
                    if (board[i][Posi.z].s != 0 && board[i][Posi.z].s != ally)
                    {
                        moves.Add((i, Posi.z));
                        break;
                    }
                }
            }

            return moves;
        }
    }
    
    public class Ma: Chessman
    {
        public Ma((int, int) posi, bool side) : base(posi, side)
        {
            Posi = posi;
            Side = side;
        }

        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            int ally = 1;
            if (!Side)
            {
                ally = 2;
            }

            //往上走
            if (Posi.z < 8 && board[Posi.x][Posi.z + 1].s == 0)
            {
                if (Posi.x > 0 && (board[Posi.x - 1][Posi.z + 2].s == 0 || board[Posi.x - 1][Posi.z + 2].s != ally ))
                {
                    moves.Add((Posi.x - 1, Posi.z + 2));
                }
                if (Posi.x < 8 && (board[Posi.x + 1][Posi.z + 2].s == 0 || board[Posi.x + 1][Posi.z + 2].s != ally ))
                {
                    moves.Add((Posi.x + 1, Posi.z + 2));
                }
            }
            //往下走
            if (Posi.z > 1 && board[Posi.x][Posi.z - 1].s == 0)
            {
                if (Posi.x > 0 && (board[Posi.x - 1][Posi.z - 2].s == 0 || board[Posi.x - 1][Posi.z - 2].s != ally ))
                {
                    moves.Add((Posi.x - 1, Posi.z - 2));
                }
                if (Posi.x < 8 && (board[Posi.x + 1][Posi.z - 2].s == 0 || board[Posi.x + 1][Posi.z - 2].s != ally ))
                {
                    moves.Add((Posi.x + 1, Posi.z - 2));
                }
            }
            //往左走
            if (Posi.x > 1 && board[Posi.x - 1][Posi.z].s == 0)
            {
                if (Posi.z < 8 && (board[Posi.x - 2][Posi.z + 1].s == 0 || board[Posi.x - 2][Posi.z + 1].s != ally ))
                {
                    moves.Add((Posi.x - 2, Posi.z + 1));
                }
                if (Posi.z > 0 && (board[Posi.x - 2][Posi.z - 1].s == 0 || board[Posi.x - 2][Posi.z - 1].s != ally ))
                {
                    moves.Add((Posi.x - 2, Posi.z - 1));
                }
            }
            //往右走
            if (Posi.x < 7 && board[Posi.x + 1][Posi.z].s == 0)
            {
                if (Posi.z < 8 && (board[Posi.x + 2][Posi.z + 1].s == 0 || board[Posi.x + 2][Posi.z + 1].s != ally ))
                {
                    moves.Add((Posi.x + 2, Posi.z + 1));
                }
                if (Posi.z > 0 && (board[Posi.x + 2][Posi.z - 1].s == 0 || board[Posi.x + 2][Posi.z - 1].s != ally ))
                {
                    moves.Add((Posi.x + 2, Posi.z - 1));
                }
            }

            return moves;
        }
    }
    
    public class Xiang: Chessman
    {
        public Xiang((int, int) posi, bool side) : base(posi, side)
        {
            Posi = posi;
            Side = side;
        }

        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            List<(int, int)> possMove = new List<(int, int)> {(0, 2), (2, 0), (2, 4), (4, 2), (6, 4), (6, 0), (8, 2)};
            int ally = 1;
            if (!Side)
            {
                ally = 2;
                possMove = new List<(int, int)> {(0, 7), (2, 9), (2, 5), (4, 7), (6, 9), (6, 5), (8, 7)};
            }

            foreach (var (x, z) in possMove)
            {
                if (board[x][z].s != ally && Math.Abs(Posi.x - x) == 2 && Math.Abs(Posi.z - z) == 2)
                {
                    moves.Add((x, z));
                }
            }
            return moves;
        }
    }
    
    public class Shi: Chessman
    {
        public Shi((int, int) posi, bool side) : base(posi, side)
        {
            Posi = posi;
            Side = side;
        }

        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            List<(int, int)> possMove = new List<(int, int)> {(3, 0), (3, 2), (4, 1), (5, 0), (5, 2)};
            int ally = 1;
            if (!Side)
            {
                ally = 2;
                possMove = new List<(int, int)> {(3, 9), (3, 9), (4, 8), (5, 9), (5, 7)};
            }

            foreach (var (x, z) in possMove)
            {
                if (board[x][z].s != ally && Math.Abs(Posi.x - x) == 1 && Math.Abs(Posi.z - z) == 1)
                {
                    moves.Add((x, z));
                }
            }
            return moves;
        }
    }
    
    public class Jiang: Chessman
    {
        public Jiang((int, int) posi, bool side) : base(posi, side)
        {
            Posi = posi;
            Side = side;
        }

        public override List<(int, int)> GetPossibleMove()
        {
            List<(int, int)> moves = new List<(int, int)>();
            List<List<(int s, string t)>> board = GetBoard();
            List<(int, int)> possMove = new List<(int, int)> {(3, 0), (4, 0), (5, 0), (3, 1), (4, 1), (5, 1), (3, 2), (4, 2), (5, 2)};
            int ally = 1;
            int d = 1;
            if (!Side)
            {
                ally = 2;
                d = -1;
                possMove = new List<(int, int)> {(3, 9), (4, 9), (5, 9), (3, 8), (4, 8), (5, 8), (3, 7), (4, 7), (5, 7)};
            }

            foreach (var (x, z) in possMove)
            {
                if (board[x][z].s != ally && 
                    ((Math.Abs(Posi.x - x) == 1 && Math.Abs(Posi.z - z) == 0)||
                     (Math.Abs(Posi.z - z) == 1 && Math.Abs(Posi.x - x) == 0)))
                {
                    moves.Add((x, z));
                }
            }

            int a = 0;
            int c = Posi.z;
            while (a < 9)
            {
                c += d;
                if (board[Posi.x][c].s != 0)
                {
                    if (board[Posi.x][c].t == "Jiang")
                    {
                        moves.Add((Posi.x, c));
                    }
                    break;
                }

                a += 1;
            }
            return moves;
        }
    }

    internal static List<List<(int s, string t)>> GetBoard()
    {
        return board;
    }

    internal List<List<(int s, string t)>> GetBoardInfo()
    {
        List<List<(int s, string t)>> b = board;
        return b;
    }

    internal static (int, int) PMakeMove(int oldX, int oldZ, int x, int z)
    {
        int s = board[oldX][oldZ].s;
        bool killed = false;
        string t = board[oldX][oldZ].t;
        board[oldX][oldZ] = (0, "e");
        if (board[x][z].s != 0)
        {
            killed = true;
        }

        board[x][z] = (s, t);

        if (killed)
        {
            return (x, z);
        }

        return (-1, -1);
    }

    internal void ChangeChessMan(int x, int z, int side, string type)
    {
        board[x][z] = (side, type);
    }

    internal void PrintBoard()
    {
        string bo = "";
        for (int y = 9; y >= 0; y--)
        {
            string b = "";
            for(int x = 0; x <= 8; x++)
            {
                b += board[x][y].s.ToString() + " ";
            }

            bo += b + "\n";
        }
        print(bo);
    }

    internal void CreateDefaultBoard()
    {
        for (int x = 0; x < 9; x++)
        {
            List<(int s, string t)> r = new List<(int s, string t)>();
            for (int y = 0; y < 10; y++)
            {
                if (y == 0 || (x == 1 && y == 2) || (x == 7 && y == 2) || (y == 3 && x % 2 == 0))
                {
                    r.Add((1, "e"));
                }
                else if (y == 9 || (x == 1 && y == 7) || (x == 7 && y == 7) || (y == 6 && x % 2 == 0))
                {
                    r.Add((2, "e"));
                }
                else
                {
                    r.Add((0, "e"));
                }
            }
            board.Add(r);
        }

        board[4][0] = (1, "Jiang");
        board[4][9] = (2, "Jiang");
    }
    
    internal void CreateCustomBoard(Dictionary<(int, int), string> data)
    {
    }

    private void Start()
    {
        CreateDefaultBoard();
    }
}
