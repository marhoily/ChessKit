using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.N;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public static class GetLegalMoves
    {
        static List<Move> InnternalGetLegalMoves(this Position position)
        {
            // BUG: Actually creates boards, but only returns moves!
            var res = new List<Move>(50);
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
        static List<Move> InnternalGetLegalMoves(this Position position, int moveFrom)
        {
            var sideOnMove = position.Core.ActiveColor;
            var piece = (Piece)position.Core.Squares[moveFrom];
            if (piece == Piece.EmptyCell) return new List<Move>();
            if (piece.Color() != sideOnMove) return new List<Move>();
            var res = new List<Move>(28);
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
                m => position.ValidateLegal(m))
                .ToList();
        }
        public static List<LegalMove> GetAllLegalMoves(this Position position)
        {
            var makeMove = position.InnternalGetLegalMoves();
            return makeMove.Select(
                m => position.ValidateLegal(m))
                .ToList();
        }
    }
}
