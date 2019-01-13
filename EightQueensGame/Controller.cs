using System;
using System.Collections.Generic;
using System.Text;

namespace EightQueensGame
{
    // 単純化のためにComputerを黒、人を白に固定。
    // 自分の手番のときに置き場所が無いほうが負けというゲーム。
    // 石を置くことができる場所は、８クィーンパズルと同じ規則に従う。
    public class Controller
    {
        private Board _board = new Board();
        Computer com = new Computer();
        Human man = new Human();

        public Action<Board> Progress { get; set; }

        public IPlayer Start(Type first)
        {
            IPlayer player;
            if (first == typeof(Computer))
                player = com;
            else
                player = man;
            IPlayer winner = null;
            Progress(_board);
            while (true)
            {
                var index = player.GetNextHand(_board);
                if (index < 0)
                {
                    winner = NextPlayer(player);
                    break;
                }
                _board.PutPiece(index, player.MyPiece);
                Progress(_board);
                player = NextPlayer(player);
            }
            return winner;
        }


        private IPlayer NextPlayer(IPlayer current)
        {
            if (current is Computer)
                return man;
            return com;
        }

        // locに石が置かれた時の処理
        private void PlayMan(int index)
        {
            if (_board.CanPut(index))
            {
                _board[index] = Piece.White;
                PlayComputer();
            }
        }

        // Computerが手番の時の処理
        // BackgroundWorkerを使い、バックグラウンドで思考
        private void PlayComputer()
        {
            var index = com.GetNextHand(_board);
        }
    }
}
