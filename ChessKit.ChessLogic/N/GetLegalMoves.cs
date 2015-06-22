using System.Collections.Generic;
using System.Linq;

namespace ChessKit.ChessLogic.N
{
    public static class GetLegalMoves
    {
        public static List<LegalMove> GetLegalMovesFromSquare(this Position position, int coordinate)
        {
            var board = position.ToBoard();
            var makeMove = board
                .GetLegalMoves(coordinate);
            return makeMove.Select(
                m => board.MakeMove(m).ToLegalMove(board))
                .ToList();
        }
        public static List<LegalMove> GetAllLegalMoves(this Position position)
        {
            var board = position.ToBoard();
            var makeMove = board
                .GetLegalMoves();
            return makeMove.Select(
                m => board.MakeMove(m).ToLegalMove(board))
                .ToList();
        }
    }
}
