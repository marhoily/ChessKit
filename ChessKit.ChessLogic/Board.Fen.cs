using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ChessKit.ChessLogic.Primitives;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        /// <summary>Board with all pieces set into the start position</summary>
        [SuppressMessage("Microsoft.Security",
          "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
          Justification = "Board is immutable")]
        public static readonly Board StartPosition = Fen.ParseFen(
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

    }
}

