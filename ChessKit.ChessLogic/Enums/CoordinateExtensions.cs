using System;
using System.Collections.Generic;
using System.Globalization;

namespace ChessKit.ChessLogic.Enums
{
    /// <summary>Identifies chess board cell coordinates in a user-friendly manner</summary>
    /// <remarks>http://en.wikipedia.org/wiki/Chess</remarks>
    public static class CoordinateExtensions
    {
        /// <param name="position">"a1" or "A1"</param>
        public static int ParseCoordinate(this string position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (position.Length != 2) throw new ArgumentOutOfRangeException(nameof(position));
            var x = char.ToLower(position[0]) - 'a';
            var y = int.Parse(position[1].ToString(), CultureInfo.InvariantCulture) - 1;
            if (x < 0 || x > 7) throw new ArgumentOutOfRangeException(nameof(position));
            if (y < 0 || y > 7) throw new ArgumentOutOfRangeException(nameof(position));
            return x + y * 16;
        }

        public static bool TryParseCoordinate(this string position, out int result)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            result = -1;
            if (position.Length != 2) return false;
            var x = char.ToLower(position[0]) - 'a';
            var y = int.Parse(position[1].ToString(), CultureInfo.InvariantCulture) - 1;
            if (x < 0 || x > 7) return false;
            if (y < 0 || y > 7) return false;
            result = x + y*16;
            return true;
        }
        /// <summary>Gets all 64 positions on board</summary>
        public static IEnumerable<int> All
        {
            get
            {
                for (var i = 0; i < 8; i++)
                    for (var j = 0; j < 8; j++)
                        yield return j + i * 16;
            }
        }

        public static int GetX(this int x) => x & 7;
        public static int GetY(this int x) => x >> 4;

        public static char GetFile(this int x) => (char)((x & 7) + 'a');
        public static int GetRank(this int x) => (x >> 4) + 1;
        public static string ToCoordinateString(this int x)
            => x.GetFile().ToString()+ x.GetRank();

    }
}