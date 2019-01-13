using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightQueensGame
{
    public class Piece
    {
        public static Piece White = new Piece { Value = 'O' };
        public static Piece Black = new Piece { Value = 'X' };
        public static Piece Empty = new Piece { Value = '.' };
        public static Piece Forbid = new Piece { Value = '-' };

        public char Value { get; set; }
    }

    public class Board : BoardBase<Piece>
    {

        // 少しでも速くするためにキャッシュしておく
        private IEnumerable<int> AllIndexes = null;

        public Board()
            : base(8, 8)
        {
            AllIndexes = GetAllIndexes();
            InitDirections();
            foreach (var ix in AllIndexes)
                this[ix] = Piece.Empty;
        }

        // オブジェクト複製用
        public Board(Board board)
            : base(board)
        {
            AllIndexes = GetAllIndexes();
            InitDirections();
        }


        public Board Clone()
        {
            return new Board(this);
        }

        // 石を置く
        public void PutPiece(int index, Piece piece)
        {
            this[index] = piece;
            if (piece == Piece.Forbid || piece == Piece.Empty)
                return;
            // Queenが動けるところにはマークをつける
            foreach (var i in Courses(index))
                if (this[i] == Piece.Empty)
                    this[i] = Piece.Forbid;
        }

        internal IEnumerable<int> GetVacantIndexes()
        {
            return AllIndexes.Where(x => this[x] == Piece.Empty);
        }

        // 置けるか？
        public bool CanPut(int place)
        {
            return this[place] == Piece.Empty;
        }

        // 置ける位置だけを列挙
        public IEnumerable<int> CanPutPlaces()
        {
            return AllIndexes.Where(ix => CanPut(ix));
        }

        // nowを通る 縦横斜めのすべての位置を列挙
        public IEnumerable<int> Courses(int now)
        {
            return Virtical(now)
                .Concat(Horizontal(now))
                .Concat(SlantL(now))
                .Concat(SlantR(now)).Distinct();
        }

        private int Up { get; set; }
        private int Down { get; set; }
        private int Left { get; set; }
        private int Right { get; set; }
        private int UpperLeft { get; set; }
        private int UpperRight { get; set; }
        private int LowerLeft { get; set; }
        private int LowerRight { get; set; }

        private void InitDirections()
        {
            Up = -(this.XSize + 2);
            Down = this.XSize + 2;
            Left = -1;
            Right = +1;
            UpperLeft = Up - 1;
            UpperRight = Up + 1;
            LowerLeft = Down - 1;
            LowerRight = Down + 1;
        }

        // 縦の位置を列挙
        public IEnumerable<int> Virtical(int now)
        {
            var (x, y) = ToLocation(now);
            return this.EnumerateIndexes(x, y, Up)
                       .Skip(1)
                       .Concat(this.EnumerateIndexes(x, y, Down));
        }

        // 横の位置を列挙
        public IEnumerable<int> Horizontal(int now)
        {
            var (x, y) = ToLocation(now);
            return this.EnumerateIndexes(x, y, Left)
                       .Skip(1)
                       .Concat(this.EnumerateIndexes(x, y, Right));
        }

        // 右上がり斜線
        public IEnumerable<int> SlantR(int now)
        {
            var (x, y) = ToLocation(now);
            return this.EnumerateIndexes(x, y, UpperRight)
                       .Skip(1)
                       .Concat(this.EnumerateIndexes(x, y, LowerLeft));
        }

        // 左上がりの斜線
        public IEnumerable<int> SlantL(int now)
        {
            var (x, y) = ToLocation(now);
            return this.EnumerateIndexes(x, y, UpperLeft)
                       .Skip(1)
                       .Concat(this.EnumerateIndexes(x, y, LowerRight));
        }

    }
}
