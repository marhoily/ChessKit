/* This code is auto-generated! 
 * It is strongly adviced not to change it manually! */

using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.Algorithms
{
    static partial class MoveLegality
    {
        private static MoveAnnotations ValidateWhiteBishopMove(byte[] cells, int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateWhiteKnightMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 33: return MoveAnnotations.Knight;
                case 31: return MoveAnnotations.Knight;
                case -31: return MoveAnnotations.Knight;
                case -33: return MoveAnnotations.Knight;
                case 18: return MoveAnnotations.Knight;
                case 14: return MoveAnnotations.Knight;
                case -14: return MoveAnnotations.Knight;
                case -18: return MoveAnnotations.Knight;
            }
            return MoveAnnotations.Knight | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateWhiteRookMove(byte[] cells, int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            return MoveAnnotations.Rook | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateWhiteQueenMove(byte[] cells, int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            return MoveAnnotations.Queen | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateWhiteKingMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 16: return MoveAnnotations.King;
                case 17: return MoveAnnotations.King;
                case 1: return MoveAnnotations.King;
                case -15: return MoveAnnotations.King;
                case -16: return MoveAnnotations.King;
                case -17: return MoveAnnotations.King;
                case -1: return MoveAnnotations.King;
                case 15: return MoveAnnotations.King;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackBishopMove(byte[] cells, int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return MoveAnnotations.Bishop;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotJump;
            }
            return MoveAnnotations.Bishop | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackKnightMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 33: return MoveAnnotations.Knight;
                case 31: return MoveAnnotations.Knight;
                case -31: return MoveAnnotations.Knight;
                case -33: return MoveAnnotations.Knight;
                case 18: return MoveAnnotations.Knight;
                case 14: return MoveAnnotations.Knight;
                case -14: return MoveAnnotations.Knight;
                case -18: return MoveAnnotations.Knight;
            }
            return MoveAnnotations.Knight | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackRookMove(byte[] cells, int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return MoveAnnotations.Rook;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Rook | MoveAnnotations.DoesNotJump;
            }
            return MoveAnnotations.Rook | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackQueenMove(byte[] cells, int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return MoveAnnotations.Queen;
                        else if (cells[i] != 0)
                            return MoveAnnotations.Queen | MoveAnnotations.DoesNotJump;
            }
            return MoveAnnotations.Queen | MoveAnnotations.DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackKingMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 16: return MoveAnnotations.King;
                case 17: return MoveAnnotations.King;
                case 1: return MoveAnnotations.King;
                case -15: return MoveAnnotations.King;
                case -16: return MoveAnnotations.King;
                case -17: return MoveAnnotations.King;
                case -1: return MoveAnnotations.King;
                case 15: return MoveAnnotations.King;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }

        public static MoveAnnotations ValidateMove(byte[] cells, Piece piece, int fromSquare, int toSquare, Piece toPiece, Castlings castlingAvailability)
        {
            switch (piece)
            {
                case Piece.WhitePawn:
                    return ValidateWhitePawnMove(cells, fromSquare, toSquare, toPiece);

                case Piece.WhiteBishop:
                    return ValidateWhiteBishopMove(cells, fromSquare, toSquare);

                case Piece.WhiteKnight:
                    return ValidateWhiteKnightMove(fromSquare, toSquare);

                case Piece.WhiteRook:
                    return ValidateWhiteRookMove(cells, fromSquare, toSquare);

                case Piece.WhiteQueen:
                    return ValidateWhiteQueenMove(cells, fromSquare, toSquare);

                case Piece.WhiteKing:
                    if (ValidateWhiteKingMove(fromSquare, toSquare) == MoveAnnotations.King)
                        return MoveAnnotations.King;
                    return ValidateWhiteCastlingMove(cells, fromSquare, toSquare, castlingAvailability);

                case Piece.BlackPawn:
                    return ValidateBlackPawnMove(cells, fromSquare, toSquare, toPiece);

                case Piece.BlackBishop:
                    return ValidateBlackBishopMove(cells, fromSquare, toSquare);

                case Piece.BlackKnight:
                    return ValidateBlackKnightMove(fromSquare, toSquare);

                case Piece.BlackRook:
                    return ValidateBlackRookMove(cells, fromSquare, toSquare);

                case Piece.BlackQueen:
                    return ValidateBlackQueenMove(cells, fromSquare, toSquare);

                case Piece.BlackKing:
                    if (ValidateBlackKingMove(fromSquare, toSquare) == MoveAnnotations.King)
                        return MoveAnnotations.King;
                    return ValidateBlackCastlingMove(cells, fromSquare, toSquare, castlingAvailability);

                default: throw new System.InvalidOperationException("Unknown piece: " + piece);
            }
        }
    }
}