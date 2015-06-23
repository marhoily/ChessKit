using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessKit.ChessLogic.N;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
    public sealed class ZobristTests
    {
        [Fact] void NoCastling() => Check(
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 0x463b96181691fc9c);

        private void Check(string fen, ulong expectedHash)
        {
            fen.ParseFen().FromBoard().Core.GetHash()
                .Should().Be(expectedHash);
        }

    /*    new TestHashData(01, "No castling", "r1bqkb1r/pppppppp/2n2n2/8/PP6/N7/2PPPPPP/R1BQKBNR b - - 0 3", 1896291952),
					new TestHashData(02, "White en-passant", "r1bqkb1r/pppppppp/2n2n2/8/PP6/N7/2PPPPPP/R1BQKBNR b KQkq b3 0 3", 1008746661),
					new TestHashData(03, "Black en-passant", "r1bqkb1r/pppppppp/2n2n2/8/PP6/N7/2PPPPPP/R1BQKBNR w KQkq b6 0 3", 1863078203),
					new TestHashData(04, "The very last index", "r1bqkb1r/pppppppp/2n2n2/8/PP6/N7/2PPPPPP/R1BQKBNR w KQkq h6 0 3", 919105805),
*/
    }
}
