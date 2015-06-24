using System;
using ChessKit.ChessLogic.Algorithms;
using Xunit;

namespace ChessKit.ChessLogic.Usability
{
    public sealed class Basic
    {
        [Fact]
        public void First()
        {
            // Fool's Mate
            var position = Board.StartPosition
                .MakeMove("f3")
                .MakeMove("e5")
                .MakeMove("g4")
                .MakeMove("Qh4#");
            Console.WriteLine(position.Dump());
            Console.WriteLine(position.Properties);
        }
    }
}
