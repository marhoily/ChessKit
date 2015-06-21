using System;

namespace ChessKit.ChessLogic
{
    public static class PieceExtensions
    {
        public static CompactPiece Pack(this PieceType pieceType, Color color)
        {
            return (CompactPiece) ((MoveAnnotations) pieceType | (MoveAnnotations) color);
        }

        public static Color Color(this CompactPiece piece)
        {
            return (Color) ((MoveAnnotations) piece & (MoveAnnotations) ChessLogic.Color.Black);
        }

        public static PieceType PieceType(this CompactPiece piece)
        {
            return (PieceType) ((MoveAnnotations) piece & ~(MoveAnnotations) ChessLogic.Color.Black);
        }

        public static char GetSymbol(this CompactPiece piece)
        {
            switch (piece)
            {
                case CompactPiece.WhitePawn:
                    return 'P';
                case CompactPiece.WhiteKnight:
                    return 'N';
                case CompactPiece.WhiteBishop:
                    return 'B';
                case CompactPiece.WhiteRook:
                    return 'R';
                case CompactPiece.WhiteQueen:
                    return 'Q';
                case CompactPiece.WhiteKing:
                    return 'K';
                case CompactPiece.BlackPawn:
                    return 'p';
                case CompactPiece.BlackKnight:
                    return 'n';
                case CompactPiece.BlackBishop:
                    return 'b';
                case CompactPiece.BlackRook:
                    return 'r';
                case CompactPiece.BlackQueen:
                    return 'q';
                case CompactPiece.BlackKing:
                    return 'k';
                case CompactPiece.EmptyCell:
                    return ' ';
                default:
                    throw new Exception("Unexpected");
            }
        }

        public static char GetTypeSymbol(this CompactPiece piece)
        {
            switch (piece)
            {
                case CompactPiece.WhitePawn:
                    return 'P';
                case CompactPiece.WhiteKnight:
                    return 'N';
                case CompactPiece.WhiteBishop:
                    return 'B';
                case CompactPiece.WhiteRook:
                    return 'R';
                case CompactPiece.WhiteQueen:
                    return 'Q';
                case CompactPiece.WhiteKing:
                    return 'K';
                case CompactPiece.BlackPawn:
                    return 'P';
                case CompactPiece.BlackKnight:
                    return 'N';
                case CompactPiece.BlackBishop:
                    return 'B';
                case CompactPiece.BlackRook:
                    return 'R';
                case CompactPiece.BlackQueen:
                    return 'Q';
                case CompactPiece.BlackKing:
                    return 'K';
                default:
                    throw new Exception("Unexpected");
            }
        }

        public static bool TryParse(this char ch, out CompactPiece piece)
        {
            switch (ch)
            {
                case 'P':
                    piece = CompactPiece.WhitePawn;
                    break;
                case 'N':
                    piece = CompactPiece.WhiteKnight;
                    break;
                case 'B':
                    piece = CompactPiece.WhiteBishop;
                    break;
                case 'R':
                    piece = CompactPiece.WhiteRook;
                    break;
                case 'Q':
                    piece = CompactPiece.WhiteQueen;
                    break;
                case 'K':
                    piece = CompactPiece.WhiteKing;
                    break;
                case 'p':
                    piece = CompactPiece.BlackPawn;
                    break;
                case 'n':
                    piece = CompactPiece.BlackKnight;
                    break;
                case 'b':
                    piece = CompactPiece.BlackBishop;
                    break;
                case 'r':
                    piece = CompactPiece.BlackRook;
                    break;
                case 'q':
                    piece = CompactPiece.BlackQueen;
                    break;
                case 'k':
                    piece = CompactPiece.BlackKing;
                    break;
                default:
                    piece = CompactPiece.EmptyCell;
                    return false;
            }
            return true;
        }

        public static CompactPiece Parse(this char ch)
        {
            switch (ch)
            {
                case 'P':
                    return CompactPiece.WhitePawn;
                case 'N':
                    return CompactPiece.WhiteKnight;
                case 'B':
                    return CompactPiece.WhiteBishop;
                case 'R':
                    return CompactPiece.WhiteRook;
                case 'Q':
                    return CompactPiece.WhiteQueen;
                case 'K':
                    return CompactPiece.WhiteKing;
                case 'p':
                    return CompactPiece.BlackPawn;
                case 'n':
                    return CompactPiece.BlackKnight;
                case 'b':
                    return CompactPiece.BlackBishop;
                case 'r':
                    return CompactPiece.BlackRook;
                case 'q':
                    return CompactPiece.BlackQueen;
                case 'k':
                    return CompactPiece.BlackKing;
                default:
                    throw new Exception("Unexpected");
            }
        }
    }
}