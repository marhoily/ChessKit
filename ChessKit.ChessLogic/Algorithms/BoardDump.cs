using System;
using System.Text;
using ChessKit.ChessLogic.Primitives;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic.Algorithms
{
    public static class BoardDump
    {
        public static string Dump([NotNull] this Position board)
        {
            if (board == null) throw new ArgumentNullException("board");
            var sb = new StringBuilder(17 * 36);
            sb.AppendLine(" ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗");
            sb.AppendLine("8║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("7║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("6║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("5║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("4║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("3║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("2║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            sb.AppendLine("1║ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 ║");
            sb.AppendLine(" ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝");
            foreach (var position in Coordinates.All)
            {
                var piece = (Piece)board.Core.Squares[position];
                sb[((7 - position.GetY()) * 2 + 1) * 36 + position.GetX() * 4 + 3]
                    = piece.GetSymbol();
            }
            return sb.ToString();
        }
    }
}