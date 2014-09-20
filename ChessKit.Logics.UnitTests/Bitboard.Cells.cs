
using System;

namespace ChessKit.ChessLogic.UnitTests
{
	public static class Square
	{
		public const int A1 = 0;
		public const int B1 = 1;
		public const int C1 = 2;
		public const int D1 = 3;
		public const int E1 = 4;
		public const int F1 = 5;
		public const int G1 = 6;
		public const int H1 = 7;
		public const int A2 = 8;
		public const int B2 = 9;
		public const int C2 = 10;
		public const int D2 = 11;
		public const int E2 = 12;
		public const int F2 = 13;
		public const int G2 = 14;
		public const int H2 = 15;
		public const int A3 = 16;
		public const int B3 = 17;
		public const int C3 = 18;
		public const int D3 = 19;
		public const int E3 = 20;
		public const int F3 = 21;
		public const int G3 = 22;
		public const int H3 = 23;
		public const int A4 = 24;
		public const int B4 = 25;
		public const int C4 = 26;
		public const int D4 = 27;
		public const int E4 = 28;
		public const int F4 = 29;
		public const int G4 = 30;
		public const int H4 = 31;
		public const int A5 = 32;
		public const int B5 = 33;
		public const int C5 = 34;
		public const int D5 = 35;
		public const int E5 = 36;
		public const int F5 = 37;
		public const int G5 = 38;
		public const int H5 = 39;
		public const int A6 = 40;
		public const int B6 = 41;
		public const int C6 = 42;
		public const int D6 = 43;
		public const int E6 = 44;
		public const int F6 = 45;
		public const int G6 = 46;
		public const int H6 = 47;
		public const int A7 = 48;
		public const int B7 = 49;
		public const int C7 = 50;
		public const int D7 = 51;
		public const int E7 = 52;
		public const int F7 = 53;
		public const int G7 = 54;
		public const int H7 = 55;
		public const int A8 = 56;
		public const int B8 = 57;
		public const int C8 = 58;
		public const int D8 = 59;
		public const int E8 = 60;
		public const int F8 = 61;
		public const int G8 = 62;
		public const int H8 = 63;
	}
	partial class Bitboard
	{
		public const UInt64 A1 = 0x0000000000000001;
		public const UInt64 B1 = 0x0000000000000002;
		public const UInt64 C1 = 0x0000000000000004;
		public const UInt64 D1 = 0x0000000000000008;
		public const UInt64 E1 = 0x0000000000000010;
		public const UInt64 F1 = 0x0000000000000020;
		public const UInt64 G1 = 0x0000000000000040;
		public const UInt64 H1 = 0x0000000000000080;
		public const UInt64 A2 = 0x0000000000000100;
		public const UInt64 B2 = 0x0000000000000200;
		public const UInt64 C2 = 0x0000000000000400;
		public const UInt64 D2 = 0x0000000000000800;
		public const UInt64 E2 = 0x0000000000001000;
		public const UInt64 F2 = 0x0000000000002000;
		public const UInt64 G2 = 0x0000000000004000;
		public const UInt64 H2 = 0x0000000000008000;
		public const UInt64 A3 = 0x0000000000010000;
		public const UInt64 B3 = 0x0000000000020000;
		public const UInt64 C3 = 0x0000000000040000;
		public const UInt64 D3 = 0x0000000000080000;
		public const UInt64 E3 = 0x0000000000100000;
		public const UInt64 F3 = 0x0000000000200000;
		public const UInt64 G3 = 0x0000000000400000;
		public const UInt64 H3 = 0x0000000000800000;
		public const UInt64 A4 = 0x0000000001000000;
		public const UInt64 B4 = 0x0000000002000000;
		public const UInt64 C4 = 0x0000000004000000;
		public const UInt64 D4 = 0x0000000008000000;
		public const UInt64 E4 = 0x0000000010000000;
		public const UInt64 F4 = 0x0000000020000000;
		public const UInt64 G4 = 0x0000000040000000;
		public const UInt64 H4 = 0x0000000080000000;
		public const UInt64 A5 = 0x0000000100000000;
		public const UInt64 B5 = 0x0000000200000000;
		public const UInt64 C5 = 0x0000000400000000;
		public const UInt64 D5 = 0x0000000800000000;
		public const UInt64 E5 = 0x0000001000000000;
		public const UInt64 F5 = 0x0000002000000000;
		public const UInt64 G5 = 0x0000004000000000;
		public const UInt64 H5 = 0x0000008000000000;
		public const UInt64 A6 = 0x0000010000000000;
		public const UInt64 B6 = 0x0000020000000000;
		public const UInt64 C6 = 0x0000040000000000;
		public const UInt64 D6 = 0x0000080000000000;
		public const UInt64 E6 = 0x0000100000000000;
		public const UInt64 F6 = 0x0000200000000000;
		public const UInt64 G6 = 0x0000400000000000;
		public const UInt64 H6 = 0x0000800000000000;
		public const UInt64 A7 = 0x0001000000000000;
		public const UInt64 B7 = 0x0002000000000000;
		public const UInt64 C7 = 0x0004000000000000;
		public const UInt64 D7 = 0x0008000000000000;
		public const UInt64 E7 = 0x0010000000000000;
		public const UInt64 F7 = 0x0020000000000000;
		public const UInt64 G7 = 0x0040000000000000;
		public const UInt64 H7 = 0x0080000000000000;
		public const UInt64 A8 = 0x0100000000000000;
		public const UInt64 B8 = 0x0200000000000000;
		public const UInt64 C8 = 0x0400000000000000;
		public const UInt64 D8 = 0x0800000000000000;
		public const UInt64 E8 = 0x1000000000000000;
		public const UInt64 F8 = 0x2000000000000000;
		public const UInt64 G8 = 0x4000000000000000;
		public const UInt64 H8 = 0x8000000000000000;

		public const UInt64 Row1 = A1 | B1 | C1 | D1 | E1 | F1 | G1 | H1;
		public const UInt64 Row2 = A2 | B2 | C2 | D2 | E2 | F2 | G2 | H2;
		public const UInt64 Row3 = A3 | B3 | C3 | D3 | E3 | F3 | G3 | H3;
		public const UInt64 Row4 = A4 | B4 | C4 | D4 | E4 | F4 | G4 | H4;
		public const UInt64 Row5 = A5 | B5 | C5 | D5 | E5 | F5 | G5 | H5;
		public const UInt64 Row6 = A6 | B6 | C6 | D6 | E6 | F6 | G6 | H6;
		public const UInt64 Row7 = A7 | B7 | C7 | D7 | E7 | F7 | G7 | H7;
		public const UInt64 Row8 = A8 | B8 | C8 | D8 | E8 | F8 | G8 | H8;
		public static readonly UInt64[] Rows = new []
			{
				Row1,
				Row2,
				Row3,
				Row4,
				Row5,
				Row6,
				Row7,
				Row8,
			};

		public const UInt64 ColA = A1 | A2 | A3 | A4 | A5 | A6 | A7 | A8;
		public const UInt64 ColB = B1 | B2 | B3 | B4 | B5 | B6 | B7 | B8;
		public const UInt64 ColC = C1 | C2 | C3 | C4 | C5 | C6 | C7 | C8;
		public const UInt64 ColD = D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8;
		public const UInt64 ColE = E1 | E2 | E3 | E4 | E5 | E6 | E7 | E8;
		public const UInt64 ColF = F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8;
		public const UInt64 ColG = G1 | G2 | G3 | G4 | G5 | G6 | G7 | G8;
		public const UInt64 ColH = H1 | H2 | H3 | H4 | H5 | H6 | H7 | H8;

		public static readonly UInt64[] Cols = new []
			{
				ColA,
				ColB,
				ColC,
				ColD,
				ColE,
				ColF,
				ColG,
				ColH,
			};

	}
}