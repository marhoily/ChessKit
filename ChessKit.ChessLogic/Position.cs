using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    /// Represents a position in the game
    /// (adding Properties to the position is a bit CPU consuming)
    public sealed class Position
    {
        /// Stuff that was calculated immediately with the legality check
        public PositionCore Core { get; }

        /// 50 moves rule counter
        public int HalfMoveClock { get; }

        /// Number of full moves (white, then black) counted
        public int FullMoveNumber { get; }

        /// Properties of the position like check, mate and stalemate
        public GameStates Properties { get; }

        /// The previous legal move if the position derives from
        /// some other position, -or- ...
        public LegalMove Move { get; }

        public Position(PositionCore core, int halfMoveClock, int fullMoveNumber, GameStates properties, LegalMove move)
        {
            Core = core;
            HalfMoveClock = halfMoveClock;
            FullMoveNumber = fullMoveNumber;
            Properties = properties;
            Move = move;
        }
    }
}