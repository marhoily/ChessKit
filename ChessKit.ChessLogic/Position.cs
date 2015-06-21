using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ChessKit.ChessLogic
{
	/// <summary>Identifies chess board cell coordinates in a user-friendly manner</summary>
	/// <remarks>http://en.wikipedia.org/wiki/Chess</remarks>
	public struct Position
	{
		/// <summary>X coordinate</summary>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X",
			Justification = "X is quite meaningful")]
		public int X { get { return CompactValue & 7; } }
		/// <summary>Y coordinate</summary>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y",
		  Justification = "Y is quite meaningful")]
		public int Y { get { return CompactValue >> 4; } }
		
		public byte CompactValue { get; private set; }

		/// <param name="position">"a1" or "A1"</param>
		public static Position Parse(string position)
		{
			if (position == null) throw new ArgumentNullException("position");
			if (position.Length != 2) throw new ArgumentOutOfRangeException("position");
			var x = Char.ToLower(position[0]) - 'a';
			var y = Int32.Parse(position[1].ToString(), CultureInfo.InvariantCulture) - 1;
			if (x < 0 || x > 7) throw new ArgumentOutOfRangeException("position");
			if (y < 0 || y > 7) throw new ArgumentOutOfRangeException("position");
			return new Position(x, y);
		}
		public static bool TryParse(string position, out Position result)
		{
			if (position == null) throw new ArgumentNullException("position");
			result = new Position();
			if (position.Length != 2) return false;
			var x = Char.ToLower(position[0]) - 'a';
			var y = Int32.Parse(position[1].ToString(), CultureInfo.InvariantCulture) - 1;
			if (x < 0 || x > 7) return false;
			if (y < 0 || y > 7) return false;
			result = new Position(x, y);
			return true;
		}

		/// <summary>ctor</summary>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x",
		  Justification = "X is quite meaningful")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y",
		  Justification = "Y is quite meaningful")]
		public Position(int x, int y)
			: this()
		{
			CompactValue = (byte) (x + y*16);
			//      if (X < 0 || X > 7) throw new ArgumentOutOfRangeException("x");
			//      if (Y < 0 || Y > 7) throw new ArgumentOutOfRangeException("y");
		}

		Position(int compactPosition)
			: this()
		{
			CompactValue = (byte)compactPosition;
		}
		Position(byte compactPosition)
			: this()
		{
			CompactValue = compactPosition;
		}

		/// <summary>Column("a".."h")</summary>
		public string File
		{
			get { return ((char)(X + 'a')).ToString(); }
		}
		/// <summary>Row (1..8)</summary>
		public int Rank
		{
			get { return Y + 1; }
		}
		/// <summary>Gets user friendly transcription of the position ("a1")</summary>
		public override string ToString()
		{
			return File + Rank;
		}

		#region  ' Equality '

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		public bool Equals(Position other)
		{
			return other.CompactValue == CompactValue;
		}
		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			return obj is Position && Equals((Position)obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		public override int GetHashCode()
		{
			return CompactValue;
		}
		/// <summary>Indicates whether two instances are equal.</summary>
		public static bool operator ==(Position left, Position right)
		{
			return left.Equals(right);
		}
		/// <summary>Indicates whether two instances are not equal.</summary>
		public static bool operator !=(Position left, Position right)
		{
			return !left.Equals(right);
		}

		#endregion

		/// <summary>Gets all 64 positions on board</summary>
		public static IEnumerable<Position> All
		{
			get
			{
				for (var i = 0; i < 8; i++)
					for (var j = 0; j < 8; j++)
						yield return new Position(j, i);
			}
		}

		public static explicit operator Position(byte compactPosition)
		{
			return new Position(compactPosition);
		}
		public static explicit operator Position(int compactPosition)
		{
			return new Position(compactPosition);
		}
		public static explicit operator int(Position position)
		{
			return position.CompactValue; 
		}
		public static explicit operator byte(Position position)
		{
			return position.CompactValue; 
		}
	}
}