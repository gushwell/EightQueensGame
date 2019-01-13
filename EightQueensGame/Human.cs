using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightQueensGame
{
    class Human : IPlayer
    {
        public Piece MyPiece => Piece.White;

        public int GetNextHand(Board board)
        {
            if (board.CanPutPlaces().Count() == 0)
            {
                return -1;
            }
            while (true)
            {
                var line = Console.ReadLine();
                if (line.Length != 2)
                    continue;
                var x = line[0] - '0';
                var y = line[1] - '0';
                if (1 <= x && x <= 8 && 1 <= y && y <= 8)
                {
                    var index = board.ToIndex(x, y);
                    if (board.CanPut(index))
                    {
                        return index;
                    }
                }
            }
        }
    }
}
