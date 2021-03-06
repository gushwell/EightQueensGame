﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EightQueensGame
{
    public interface IPlayer
    {
        int GetNextHand(Board board);

        Piece MyPiece { get; }
    }
}
