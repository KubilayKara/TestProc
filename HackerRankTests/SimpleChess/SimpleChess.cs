using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HackerRankTests
{

    static public class SimpleChess
    {
        public static void Play()
        {
            int gameCount = int.Parse(Console.ReadLine());
            Game[] gameList = new Game[gameCount];
            for (int gameIndex = 0; gameIndex < gameCount; gameIndex++)
            {
                //The first line contains three space-separated integers denoting
                //the respective values of  (the number of White pieces),  
                //(the number of Black pieces),
                //and  (the maximum number of moves we want to know if White can win in).
                string[] line = Console.ReadLine().Split(' ');
                int whitePicesCount = int.Parse(line[0]);
                int blackPicesCount = int.Parse(line[1]);
                int turnCount = int.Parse(line[2]);


                Game game = new Game(Player.White, turnCount);
                //get white pieces
                for (int i = 0; i < whitePicesCount; i++)
                {
                    string[] input = Console.ReadLine().Split(' ');
                    game.PutPiace(Player.White, input[0], input[1], int.Parse(input[2]));
                }

                //get black pieces
                for (int i = 0; i < blackPicesCount; i++)
                {
                    string[] input = Console.ReadLine().Split(' ');
                    game.PutPiace(Player.Black, input[0], input[1], int.Parse(input[2]));
                }
                gameList[gameIndex] = game;

            }
            foreach (var game in gameList)
            {
                if (game.CanCaptureQueen())
                    Console.WriteLine("YES");
                else
                    Console.WriteLine("NO");
            }

            Console.ReadLine();

        }


        public static void AddIfPosible(List<KeyValuePair<int, int>> list, int boardSize, int x, int y)
        {
            if (x >= 0 && y >= 0 && x < boardSize && y < boardSize)
                list.Add(new KeyValuePair<int, int>(x, y));
        }

        public static int GetHorizontalIndex(string c)
        {
            switch (c)
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
                case "D":
                    return 3;
                default:
                    throw new Exception("Böyle char olmaz...");
            }
        }
    }

    public class Game
    {
        public Player CurretPlayer { get; private set; }
        public int TurnCount { get; private set; }

        public List<ChessPiece> Pieces { get; private set; }
        private const int boardSize = 4;

        public bool CanCaptureQueen()
        {
            this.PrintBoard();
            if (this.TurnCount <= 0)
                return false;

            foreach (var currentPiece in this.Pieces)
            {
                var options = currentPiece.GetAvaliableMovements(boardSize);
                var avaliableOptions = options.Where(pair =>
                    {
                        ChessPiece targetPiece = this.Pieces.FirstOrDefault(p => p.X == pair.Key && p.Y == pair.Value);
                        return targetPiece == null || targetPiece.Player != currentPiece.Player; //we can move if the target piece belongs to the oponent
                    }).ToList();
                if (avaliableOptions.Count == 0)
                    continue;

                foreach (var option in avaliableOptions)
                {//option 
                    bool optionResult = RunOption(currentPiece, option);
                    if (CurretPlayer == Player.White && optionResult) //if white, at least one option can be true true
                    {
                        Console.WriteLine("result: true");
                        return true;
                    }
                    if (CurretPlayer == Player.Black && optionResult) //if black, every option should return true
                    {
                        Console.WriteLine("result: false");
                        return false;
                    }
                }//option



            }

            Console.WriteLine("result: false");
            return false;


        }
        private bool RunOption(ChessPiece currentPiece, KeyValuePair<int, int> option)
        {
            Player nextPlayer = (this.CurretPlayer == Player.White) ? Player.Black : Player.White;
            Game nextGame = new Game(nextPlayer, TurnCount - 1);
            bool optionResult = false;
            foreach (var pieceToCopy in this.Pieces)
            {
                var clone = pieceToCopy.Clone();
                if (pieceToCopy == currentPiece)
                {
                    clone.X = option.Key;
                    clone.Y = option.Value;
                }
                else if (pieceToCopy.X == option.Key && pieceToCopy.Y == option.Value)//removed piece
                {
                    if (pieceToCopy is Queen)
                    {
                        return pieceToCopy.Player == Player.Black;

                    }
                }

                nextGame.PutPiace(clone);
            }
            return nextGame.CanCaptureQueen();
        }


        public Game(Player currentPlayer, int turnCount)
        {
            this.CurretPlayer = currentPlayer;
            this.TurnCount = turnCount;
            this.Pieces = new List<ChessPiece>();

        }
        public void PutPiace(Player player, string pieceType, string horizontal, int vertical)
        {
            int x = SimpleChess.GetHorizontalIndex(horizontal);
            int y = vertical - 1;
            this.PutPiace(CreatePiece(player, pieceType, x, y));

        }

        public void PutPiace(ChessPiece piece)
        {
            this.Pieces.Add(piece);

        }

        // (where  is Queen,  is Knight,  is Bishop, and  is Rook)
        private static ChessPiece CreatePiece(Player player, string pieceType, int x, int y)
        {

            switch (pieceType)
            {
                case "Q":
                    return new Queen(player, x, y);
                case "N":
                    return new Knight(player, x, y);
                case "B":
                    return new Bishop(player, x, y);
                case "R":
                    return new Rook(player, x, y);
                default:
                    throw new Exception("Böyle type olmaz...");
            }
        }

        void PrintBoard()
        {
            string[,] board = new string[boardSize, boardSize];

            foreach (var p in Pieces)
            {
                board[p.X, p.Y] = p.ToString();
            }

            Console.Write("\n");

            for (int y = boardSize - 1; y >= 0; y--)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Console.Write(string.Format("{0}\t", (string.IsNullOrEmpty(board[x, y])) ? " _______ " : board[x, y]));
                }
                Console.Write("\n");
            }
        }
    }

    public abstract class ChessPiece
    {
        public Player Player { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ChessPiece(Player player, int x, int y)
        {
            this.Player = player;
            this.X = x;
            this.Y = y;
        }

        public abstract List<KeyValuePair<int, int>> GetAvaliableMovements(int boardSize);
        public abstract ChessPiece Clone();

        public override string ToString()
        {
            return string.Format("{0}-{1}[{2},{3}]", this.Player.ToString().Substring(0, 1), this.GetType().Name.Substring(0, 1), this.X, this.Y); ;
        }
    }

    public class Queen : ChessPiece
    {
        public Queen(Player player, int x, int y) : base(player, x, y)
        { }



        public override List<KeyValuePair<int, int>> GetAvaliableMovements(int boardSize)
        {
            var result = new Bishop(this.Player, this.X, this.Y).GetAvaliableMovements(boardSize);
            result.AddRange(new Rook(this.Player, this.X, this.Y).GetAvaliableMovements(boardSize));
            return result;
        }
        public override ChessPiece Clone()
        {
            return new Queen(this.Player, this.X, this.Y);
        }
    }

    public class Knight : ChessPiece
    {
        public Knight(Player player, int x, int y) : base(player, x, y)
        { }

        public override List<KeyValuePair<int, int>> GetAvaliableMovements(int boardSize)
        {
            var result = new List<KeyValuePair<int, int>>();
            SimpleChess.AddIfPosible(result, boardSize, this.X + 2, this.Y + 1);
            SimpleChess.AddIfPosible(result, boardSize, this.X + 2, this.Y - 1);
            SimpleChess.AddIfPosible(result, boardSize, this.X - 2, this.Y + 1);
            SimpleChess.AddIfPosible(result, boardSize, this.X - 2, this.Y - 1);

            SimpleChess.AddIfPosible(result, boardSize, this.X + 1, this.Y + 2);
            SimpleChess.AddIfPosible(result, boardSize, this.X + 1, this.Y - 2);
            SimpleChess.AddIfPosible(result, boardSize, this.X - 1, this.Y + 2);
            SimpleChess.AddIfPosible(result, boardSize, this.X - 1, this.Y - 2);
            return result;

        }
        public override ChessPiece Clone()
        {
            return new Knight(this.Player, this.X, this.Y);
        }
    }

    public class Bishop : ChessPiece
    {
        public Bishop(Player player, int x, int y) : base(player, x, y)
        { }
        public override List<KeyValuePair<int, int>> GetAvaliableMovements(int boardSize)
        {
            var result = new List<KeyValuePair<int, int>>();
            for (int i = 1; i < boardSize; i++)
            {
                //sol-üst çapraz
                SimpleChess.AddIfPosible(result, boardSize, this.X + i, this.Y + i);

                //sol-alt çapraz
                SimpleChess.AddIfPosible(result, boardSize, this.X + i, this.Y - i);

                //sağ-üst çapraz
                SimpleChess.AddIfPosible(result, boardSize, this.X - i, this.Y + i);

                //sol-alt çapraz
                SimpleChess.AddIfPosible(result, boardSize, this.X - i, this.Y - i);
            }
            return result;
        }
        public override ChessPiece Clone()
        {
            return new Bishop(this.Player, this.X, this.Y);
        }
    }

    public class Rook : ChessPiece
    {
        public Rook(Player player, int x, int y) : base(player, x, y)
        { }
        public override List<KeyValuePair<int, int>> GetAvaliableMovements(int boardSize)
        {
            var result = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < boardSize; i++)
            {
                SimpleChess.AddIfPosible(result, boardSize, i, this.Y);
                SimpleChess.AddIfPosible(result, boardSize, this.X, i);
            }
            return result;
        }
        public override ChessPiece Clone()
        {
            return new Rook(this.Player, this.X, this.Y);
        }
    }



    public enum Player
    {
        White, Black
    }
}
