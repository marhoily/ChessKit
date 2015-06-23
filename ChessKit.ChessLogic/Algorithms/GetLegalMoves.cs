using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.Algorithms
{
    public static class GetLegalMoves
    {
        static List<GeneratedMove> InnternalGetLegalMoves(this Position position)
        {
            // BUG: Actually creates boards, but only returns moves!
            var res = new List<GeneratedMove>(50);
            var sideOnMove = position.Core.ActiveColor;
            for (var moveFrom = 0; moveFrom < 64; moveFrom++)
            {
                var moveFromSq = moveFrom + (moveFrom & ~7);
                var piece =(Piece) position.Core.Squares[moveFromSq];
                if (piece == 0) continue;
                if (piece.Color() != sideOnMove) continue;
                MoveGeneration.GenerateMoves(position.Core.Squares, 
                    position.Core.WhiteKing, position.Core.BlackKing, 
                    piece, moveFromSq, position.Core.EnPassant, 
                    position.Core.CastlingAvailability, res);
            }
            return res;
        }
        static List<GeneratedMove> InnternalGetLegalMoves(this Position position, int moveFrom)
        {
            var sideOnMove = position.Core.ActiveColor;
            var piece = (Piece)position.Core.Squares[moveFrom];
            if (piece == Piece.EmptyCell) return new List<GeneratedMove>();
            if (piece.Color() != sideOnMove) return new List<GeneratedMove>();
            var res = new List<GeneratedMove>(28);
            MoveGeneration.GenerateMoves(position.Core.Squares,
                position.Core.WhiteKing, position.Core.BlackKing,
                piece, moveFrom, position.Core.EnPassant,
                position.Core.CastlingAvailability, res);
            return res;
        }
        
        public static List<LegalMove> GetLegalMovesFromSquare(this Position position, int coordinate)
        {
            var makeMove = position.InnternalGetLegalMoves(coordinate);
            return makeMove.Select(
                m => position.ValidateLegal(new Move(m.From, m.To)))
                .ToList();
        }
        public static List<LegalMove> GetAllLegalMoves(this Position position)
        {
            var makeMove = position.InnternalGetLegalMoves();
            return makeMove.Select(
                m => position.ValidateLegal(new Move(m.From, m.To)))
                .ToList();
        }
    }
}
