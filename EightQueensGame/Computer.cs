﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightQueensGame
{
    // Computerの思考を担当
    public class Computer : IPlayer
    {
        public Piece MyPiece => Piece.Black;

        private Record _record;

        // Solveで得られた最善手を返す。
        public int GetNextHand(Board board)
        {
            Solve(board);
            if (_record.Children.Count > 0)
            {
                var rec = _record.Children[0];
                return rec.Index;
            }
            else
            {
                // 勝つ手が無い。
                // 最初に見つかった置ける場所に石を置く。（かなり手抜き）
                var places = board.CanPutPlaces();
                if (places.Count() > 0)
                    return places.First();
                return -1;
            }
        }

        // 最後まで探索し、最善手を見つける
        public Piece Solve(Board board)
        {
            _record = new Record();
            _record.Root = _record;
            Piece win;
            // すべての空いている位置に対して試してみる。
            foreach (var index in board.GetVacantIndexes())
            {
                // int loc = this._board.ToLocation(index);
                _record = new Record();
                _record.Root = _record;
                // 現在のボードの状態をコピーし、コピーしたものを利用する
                var workboard = board.Clone();
                int p = index;
                if (workboard.CanPut(p))
                {
                    // 石を置いてみる
                    Record nr = Put(workboard, p, MyPiece, _record);
                    // 相手（白）の番の先読み
                    win = Search(workboard, Opponent(MyPiece), nr);
                    if (win == MyPiece)
                    {
                        // もし、Computerが勝ちならば、処理をやめて戻る。
                        return win;
                    }
                }
            }
            return Opponent(MyPiece);
        }

        // 再帰処理をし、探索する。
        // 相手がどんな手を打っても勝てる位置を探す。
        public Piece Search(Board board, Piece piece, Record rec)
        {
            // すべての置ける位置に対して処理をする
            foreach (var place in board.CanPutPlaces())
            {
                Board temp = board.Clone();
                // 石を置く。
                Record nr = Put(temp, place, piece, rec);
                // 相手の番（先読み）
                Piece win = Search(temp, Opponent(piece), nr);
                if (win == piece)
                {
                    // pieceが勝ったら戻る 
                    //（pieceの相手がどんな手を打っても pieceの勝ち)
                    // 今までの手を捨て去って、新しい手をrecに加える
                    rec.Clear();
                    rec.Add(nr);
                    return piece;
                }
                // この位置に置くと相手が勝ってしまうので、次の位置を調べる。
                // ちなみにこのゲームには引き分けはない。
            }
            // どこにも打つ場所が無い。
            // あるいは、どこに置いても勝てない。
            // つまり相手の勝ち
            return Opponent(piece);
        }

        // pieceをplaceの位置に置き、置いた石とその位置を記録する
        private Record Put(Board board, int place, Piece piece, Record rec)
        {
            // board[place] = piece;
            board.PutPiece(place, piece);
            Record nr = new Record(place);
            rec.Add(nr);
            return nr;
        }

        // 次の手番の相手の石を返す
        private Piece Opponent(Piece piece)
        {
            return piece == Piece.Black ? Piece.White : Piece.Black;
        }

        // デバッグ用
        public string Print()
        {
            _record.Root.result = "";
            _record.Print(0);
            return _record.Root.result;
        }
    }
}
