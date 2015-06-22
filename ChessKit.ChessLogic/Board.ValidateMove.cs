using ChessKit.ChessLogic.Enums;
using static ChessKit.ChessLogic.Enums.MoveAnnotations;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private MoveAnnotations ValidateWhiteBishopMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            return Bishop | DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateWhiteKnightMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 33:
                    return Knight;
                case 31:
                    return Knight;
                case -31:
                    return Knight;
                case -33:
                    return Knight;
                case 18:
                    return Knight;
                case 14:
                    return Knight;
                case -14:
                    return Knight;
                case -18:
                    return Knight;
            }
            return Knight | DoesNotMoveThisWay;
        }
        private MoveAnnotations ValidateWhiteRookMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            return Rook | DoesNotMoveThisWay;
        }
        private MoveAnnotations ValidateWhiteQueenMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            return Queen | DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateWhiteKingMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 16:
                    return King;
                case 17:
                    return King;
                case 1:
                    return King;
                case -15:
                    return King;
                case -16:
                    return King;
                case -17:
                    return King;
                case -1:
                    return King;
                case 15:
                    return King;
            }
            return King | DoesNotMoveThisWay;
        }
        private MoveAnnotations ValidateBlackBishopMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return Bishop;
                        else if (this[i] != Piece.EmptyCell)
                            return Bishop | DoesNotJump;
            }
            return Bishop | DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackKnightMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 33:
                    return Knight;
                case 31:
                    return Knight;
                case -31:
                    return Knight;
                case -33:
                    return Knight;
                case 18:
                    return Knight;
                case 14:
                    return Knight;
                case -14:
                    return Knight;
                case -18:
                    return Knight;
            }
            return Knight | DoesNotMoveThisWay;
        }
        private MoveAnnotations ValidateBlackRookMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return Rook;
                        else if (this[i] != Piece.EmptyCell)
                            return Rook | DoesNotJump;
            }
            return Rook | DoesNotMoveThisWay;
        }
        private MoveAnnotations ValidateBlackQueenMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            if (dx % 16 == 0)
            {
                var steps = dx / 16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % 1 == 0)
            {
                var steps = dx / 1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -16 == 0)
            {
                var steps = dx / -16;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -1 == 0)
            {
                var steps = dx / -1;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % 17 == 0)
            {
                var steps = dx / 17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -15 == 0)
            {
                var steps = dx / -15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % -17 == 0)
            {
                var steps = dx / -17;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            if (dx % 15 == 0)
            {
                var steps = dx / 15;
                if (steps >= 0 && steps < 8)
                    for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                        if (i == toSquare) return Queen;
                        else if (this[i] != Piece.EmptyCell)
                            return Queen | DoesNotJump;
            }
            return Queen | DoesNotMoveThisWay;
        }
        private static MoveAnnotations ValidateBlackKingMove(int fromSquare, int toSquare)
        {
            var dx = toSquare - fromSquare;
            switch (dx)
            {
                case 16:
                    return King;
                case 17:
                    return King;
                case 1:
                    return King;
                case -15:
                    return King;
                case -16:
                    return King;
                case -17:
                    return King;
                case -1:
                    return King;
                case 15:
                    return King;
            }
            return King | DoesNotMoveThisWay;
        }

        private MoveAnnotations ValidateMove(Piece piece, int fromSquare, int toSquare, Piece toPiece, Castlings castlingAvailability)
        {
            switch (piece)
            {
                case Piece.WhitePawn:
                    return ValidateWhitePawnMove(fromSquare, toSquare, toPiece);

                case Piece.WhiteBishop:
                    return ValidateWhiteBishopMove(fromSquare, toSquare);

                case Piece.WhiteKnight:
                    return ValidateWhiteKnightMove(fromSquare, toSquare);

                case Piece.WhiteRook:
                    return ValidateWhiteRookMove(fromSquare, toSquare);

                case Piece.WhiteQueen:
                    return ValidateWhiteQueenMove(fromSquare, toSquare);

                case Piece.WhiteKing:
                    if (ValidateWhiteKingMove(fromSquare, toSquare) == King)
                        return King;
                    return ValidateWhiteCastlingMove(fromSquare, toSquare, castlingAvailability);

                case Piece.BlackPawn:
                    return ValidateBlackPawnMove(fromSquare, toSquare, toPiece);

                case Piece.BlackBishop:
                    return ValidateBlackBishopMove(fromSquare, toSquare);

                case Piece.BlackKnight:
                    return ValidateBlackKnightMove(fromSquare, toSquare);

                case Piece.BlackRook:
                    return ValidateBlackRookMove(fromSquare, toSquare);

                case Piece.BlackQueen:
                    return ValidateBlackQueenMove(fromSquare, toSquare);

                case Piece.BlackKing:
                    if (ValidateBlackKingMove(fromSquare, toSquare) == King)
                        return King;
                    return ValidateBlackCastlingMove(fromSquare, toSquare, castlingAvailability);

                default: throw new System.InvalidOperationException("Unknown piece: " + piece);
            }
        }
    }
}