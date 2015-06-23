﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.Primitives;
using MoreLinq;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;
using static ChessKit.ChessLogic.Primitives.GameStates;

namespace ChessKit.ChessLogic.N
{
    public static class EndGame
    {
        public static IEnumerable<Position> GetHistory(this Position position)
        {
            return new HistoryEnumerable(position);
        }

        public static Position ToPosition(this LegalMove legalMove)
        {
            var core = legalMove.ResultPosition;
            var prev = legalMove.OriginalPosition;
            var piece = legalMove.Piece;
            var obs = legalMove.Annotations;
            var color = prev.Core.ActiveColor;
            var tempPosition = new Position(core, 0, 0, 0, 
                legalMove, prev.WhiteKing, prev.BlackKing);

            var newHalfMoveClock =
                piece == PieceType.Pawn || (obs & Capture) != 0
                    ? 0
                    : prev.HalfMoveClock + 1;

            var newMoveNumber =
                prev.FullMoveNumber + (color == Color.Black ? 1 : 0);

            var isCheck = core.IsInCheck(core.ActiveColor);
            var noMoves = tempPosition.GetAllLegalMoves().Count == 0;

            var newState = default(GameStates);
            if (isCheck && noMoves) newState |= Mate;
            if (isCheck && !noMoves) newState |= Check;
            if (!isCheck && noMoves) newState |= Stalemate;
            if (newHalfMoveClock >= 50) newState |= FiftyMoveRule;

            var isRepetition = tempPosition.GetHistory()
                .Prepend(tempPosition)
                .Select(p => p.Core)
                .CountBy()
                .MaxBy(x => x.Value).Value > 2;
            if (isRepetition) newState |= Repetition;

            var whiteKing = Coordinates.All.SingleOrDefault(
                p => (Piece)core.Squares[p] == Piece.WhiteKing);
            var blackKing = Coordinates.All.SingleOrDefault(
                p => (Piece)core.Squares[p] == Piece.BlackKing);

            return new Position(core, newHalfMoveClock,
                newMoveNumber, newState, legalMove, whiteKing, blackKing);
        }


        sealed class HistoryEnumerable : IEnumerable<Position>
        {
            private readonly Position _position;

            public HistoryEnumerable(Position position)
            {
                _position = position;
            }

            public IEnumerator<Position> GetEnumerator()
                => new HistoryEnumerator(_position);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        sealed class HistoryEnumerator : IEnumerator<Position>
        {
            public HistoryEnumerator(Position position)
            {
                Current = position;
            }

            public bool MoveNext()
            {
                Current = Current?.Move?.OriginalPosition;
                return Current != null;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public Position Current { get; private set; }
            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        private static Dictionary<T, int> CountBy<T>(this IEnumerable<T> source)
        {
            var res = new Dictionary<T, int>();
            foreach (var item in source)
            {
                int counter;
                if (res.TryGetValue(item, out counter))
                    res.Add(item, 1);
                else
                    res[item] = counter + 1;
            }
            return res;
        }
    }
}
