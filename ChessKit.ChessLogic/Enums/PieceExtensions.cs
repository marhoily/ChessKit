using System;

namespace ChessKit.ChessLogic.Enums
{
    public static class PieceExtensions
    {
        public static Piece Pack(this PieceType pieceType, Color color)
        {
            return (Piece) ((MoveAnnotations) pieceType | (MoveAnnotations) color);
        }

        public static Color Color(this Piece piece)
        {
            return (Color) ((MoveAnnotations) piece & (MoveAnnotations) Enums.Color.Black);
        }

        public static PieceType PieceType(this Piece piece)
        {
            return (PieceType) ((MoveAnnotations) piece & ~(MoveAnnotations) Enums.Color.Black);
        }

        public static char GetSymbol(this Piece piece)
        {
            switch (piece)
            {
                case Piece.WhitePawn:
                    return 'P';
                case Piece.WhiteKnight:
                    return 'N';
                case Piece.WhiteBishop:
                    return 'B';
                case Piece.WhiteRook:
                    return 'R';
                case Piece.WhiteQueen:
                    return 'Q';
                case Piece.WhiteKing:
                    return 'K';
                case Piece.BlackPawn:
                    return 'p';
                case Piece.BlackKnight:
                    return 'n';
                case Piece.BlackBishop:
                    return 'b';
                case Piece.BlackRook:
                    return 'r';
                case Piece.BlackQueen:
                    return 'q';
                case Piece.BlackKing:
                    return 'k';
                case Piece.EmptyCell:
                    return ' ';
                default:
                    throw new Exception("Unexpected");
            }
        }

        public static char GetTypeSymbol(this Piece piece)
        {
            switch (piece)
            {
                case Piece.WhitePawn:
                    return 'P';
                case Piece.WhiteKnight:
                    return 'N';
                case Piece.WhiteBishop:
                    return 'B';
                case Piece.WhiteRook:
                    return 'R';
                case Piece.WhiteQueen:
                    return 'Q';
                case Piece.WhiteKing:
                    return 'K';
                case Piece.BlackPawn:
                    return 'P';
                case Piece.BlackKnight:
                    return 'N';
                case Piece.BlackBishop:
                    return 'B';
                case Piece.BlackRook:
                    return 'R';
                case Piece.BlackQueen:
                    return 'Q';
                case Piece.BlackKing:
                    return 'K';
                default:
                    throw new Exception("Unexpected");
            }
        }

        public static bool TryParse(this char ch, out Piece piece)
        {
            switch (ch)
            {
                case 'P':
                    piece = Piece.WhitePawn;
                    break;
                case 'N':
                    piece = Piece.WhiteKnight;
                    break;
                case 'B':
                    piece = Piece.WhiteBishop;
                    break;
                case 'R':
                    piece = Piece.WhiteRook;
                    break;
                case 'Q':
                    piece = Piece.WhiteQueen;
                    break;
                case 'K':
                    piece = Piece.WhiteKing;
                    break;
                case 'p':
                    piece = Piece.BlackPawn;
                    break;
                case 'n':
                    piece = Piece.BlackKnight;
                    break;
                case 'b':
                    piece = Piece.BlackBishop;
                    break;
                case 'r':
                    piece = Piece.BlackRook;
                    break;
                case 'q':
                    piece = Piece.BlackQueen;
                    break;
                case 'k':
                    piece = Piece.BlackKing;
                    break;
                default:
                    piece = Piece.EmptyCell;
                    return false;
            }
            return true;
        }

        public static Piece Parse(this char ch)
        {
            switch (ch)
            {
                case 'P':
                    return Piece.WhitePawn;
                case 'N':
                    return Piece.WhiteKnight;
                case 'B':
                    return Piece.WhiteBishop;
                case 'R':
                    return Piece.WhiteRook;
                case 'Q':
                    return Piece.WhiteQueen;
                case 'K':
                    return Piece.WhiteKing;
                case 'p':
                    return Piece.BlackPawn;
                case 'n':
                    return Piece.BlackKnight;
                case 'b':
                    return Piece.BlackBishop;
                case 'r':
                    return Piece.BlackRook;
                case 'q':
                    return Piece.BlackQueen;
                case 'k':
                    return Piece.BlackKing;
                default:
                    throw new Exception("Unexpected");
            }
        }
    }
}