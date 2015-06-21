using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ChessKit.ChessLogic.Enums;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
    public static class San
    {
        /// <summary>
        /// Get the SAN (standard algebraic notation) index for a move - move number and the side to move
        /// without checking the status of the game or if it's a valid move.
        /// </summary>
        /// <returns></returns>
        public static string GetSanIndex([NotNull] this Board board)
        {
            if (board == null) throw new ArgumentNullException("board");
            var sb = new StringBuilder(5);
            sb.Append(board.MoveNumber.ToString(CultureInfo.InvariantCulture));
            if (board.SideOnMove == Color.White)
            {
                sb.Append('.');
            }
            else
            {
                sb.Append("...");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the end of the SAN (standard algebraic notation) for a move - promotion and check/checkmate representation - after the move is made
        /// without checking the status of the game or if it's a valid move.
        /// </summary>
        /// <param name="board">The game</param>
        /// <param name="move">The move</param>
        /// <returns></returns>
        public static string GetSanEnd([NotNull] this Board board, [NotNull] Move move)
        {
            if (board == null) throw new ArgumentNullException("board");
            if (move == null) throw new ArgumentNullException("move");

            var sb = new StringBuilder(3);

            if ((move.Annotations & MoveAnnotations.Promotion) != 0)
                sb.Append('=').Append(move.ProposedPromotion.With(Color.White).GetSymbol());

            if (board.IsCheck) sb.Append('+');
            else if (board.IsMate) sb.Append('#');

            return sb.ToString();
        }

        /// <summary>
        /// Gets the begin of the SAN (standard algebraic notation) for a move - without promotion or check/checkmate representation -  before the move is made
        /// without checking the status of the game or if it's a valid move.
        /// </summary>
        /// <param name="board">The game</param>
        /// <param name="move">The move</param>
        /// <returns></returns>
        public static string GetSanBegin([NotNull] this Board board, Move move)
        {
            if (board == null) throw new ArgumentNullException("board");

            var sb = new StringBuilder(6);

            if ((move.Annotations & (MoveAnnotations.WhiteKingsideCastling | MoveAnnotations.BlackKingsideCastling)) !=
                0)
            {
                sb.Append("O-O");
            }
            else if ((move.Annotations &
                      (MoveAnnotations.WhiteQueensideCastling | MoveAnnotations.BlackQueensideCastling)) != 0)
            {
                sb.Append("O-O-O");
            }
            else
            {
                if ((move.Annotations & MoveAnnotations.Pawn) != 0)
                {
                    if ((move.Annotations & MoveAnnotations.Capture) != 0)
                        sb.Append(move.From.GetFile());
                }
                else
                {
                    sb.Append(board[move.From].GetSymbol());

                    // TODO: move should have Piece prop?
                    var disambiguationList = new List<int>(
                        from m in board.GetLegalMoves()
                        where m.From != move.From
                              && m.To == move.To
                              && board[move.From] == board[m.From]
                        select m.From);

                    if (disambiguationList.Count > 0)
                    {
                        var uniqueFile = true;
                        // ReSharper disable LoopCanBeConvertedToQuery
                        // ReSharper disable ForCanBeConvertedToForeach
                        for (var index = 0; index < disambiguationList.Count; index++)
                        {
                            var m = disambiguationList[index];
                            if (move.From.GetFile() == m.GetFile())
                            {
                                uniqueFile = false;
                                break;
                            }
                        }
                        if (uniqueFile)
                        {
                            sb.Append(move.From.GetFile());
                        }
                        else
                        {
                            var uniqueRank = true;
                            for (var i = 0; i < disambiguationList.Count; i++)
                            {
                                var m = disambiguationList[i];
                                if (move.From.GetRank() == m.GetRank())
                                {
                                    uniqueRank = false;
                                    break;
                                }
                            }
                            // ReSharper restore ForCanBeConvertedToForeach
                            // ReSharper restore LoopCanBeConvertedToQuery
                            if (uniqueRank)
                            {
                                sb.Append(move.From.GetRank().ToString(CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                sb.Append(move.From);
                            }
                        }
                    }
                }

                // if there is a capture, add capture notation
                if ((move.Annotations & MoveAnnotations.Capture) == MoveAnnotations.Capture)
                {
                    sb.Append('x');
                }

                // add destination square
                sb.Append(move.To);
            }

            return sb.ToString();
        }

        public static string GetSan([NotNull] this Board board, Move move)
        {
            if (board == null) throw new ArgumentNullException("board");
            return board.GetSanBegin(move) /*+ board.GetSanEnd(move)*/;
        }

        /// <summary>
        /// Gets a move from its SAN (standard algebraic notation).
        /// Throws ArgumentException if it's not a valid move.
        /// </summary>
        /// <param name="board">The game</param>
        /// <param name="san">The SAN string</param>
        /// <returns></returns>
        public static Move ParseMoveFromSan([NotNull] this Board board, [NotNull] string san)
        {
            if (board == null) throw new ArgumentNullException("board");
            if (san == null) throw new ArgumentNullException("san");

            if (san == "O-O") return Move.Parse(board.SideOnMove == Color.White ? "e1-g1" : "e8-g8");
            if (san == "O-O-O") return Move.Parse(board.SideOnMove == Color.White ? "e1-c1" : "e8-c8");

            var index = san.Length - 1;
            // remove chess and checkmate representation (if any)
            if (index > -1 && (san[index] == '+' || san[index] == '#')) index--;
            if (index < 1) return null;

            // get the promotion (if any)
            var prom = PieceType.Queen;
            if (san[index - 1] == '=')
            {
                prom = san[index].Parse().PieceType();
                index -= 2;
            }

            if (index < 1) return null;
            var to = Coordinate.Parse(san.Substring(index - 1, 2));
            index -= 2;

            // remove capture char (if any)
            if (index > -1 && san[index] == 'x') index--;

            // get the rank of the starting square (if any)
            int? rank = null;
            if (index > -1 && san[index] >= '1' && san[index] <= '8')
            {
                rank = san[index] - '1';
                index--;
            }

            // get the file of the starting square (if any)
            int? file = null;
            if (index > -1 && san[index] >= 'a' && san[index] <= 'h')
            {
                file = san[index] - 'a';
                index--;
            }

            // get piece Type char (if any)
            var pieceChar = PieceType.Pawn;
            if (index > -1)
            {
                pieceChar = san[index].Parse().PieceType();
                index--;
            }
            if (index != -1) throw new FormatException("Illegal characters");
            return GetMove(board, to, file, rank, pieceChar, prom);
        }

        private static Move GetMove(Board board, int to, int? file, int? rank, PieceType pieceChar, PieceType prom)
        {
            var move = default(Move);
            foreach (var m in board.GetLegalMoves())
                if (m.To == to)
                    if (file == null || file == m.From.GetX())
                        if (rank == null || rank == m.From.GetY())
                            if (board[m.From].PieceType() == pieceChar)
                            {
                                if (move != null) throw new FormatException("Ambiguity");
                                move = m;
                            }
            if (move == null) return null;
            if (move.IsProposedPromotion)
            {
                move.ProposedPromotion = prom;
            }
            return move;
        }
    }
}