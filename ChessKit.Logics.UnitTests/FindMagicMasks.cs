using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApprovalUtilities.Utilities;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
    [TestFixture]
    public class FindMagicMasks
    {
       // [Test]
        public void Estimate_Size()
        {
            var set = GetDistinctRects();
            set.Count.Should().Be(1296);
        }
       // [Test]
        public void Test_GetDistinctRectsEncoded()
        {
            GetDistinctRects().OrderBy(x => x).ForEach(Console.WriteLine);
            GetDistinctRectsEncoded().Distinct().Count().Should().Be(1296);
        }
        /*   [Test]
           public void Find_Magic32()
           {
               var firstLevel = FindNumber32(GetDistinctRectsEncoded().ToArray(), 8);

           }*/
//        [Test]
        public void Find_Magic()
        {
            FindNumber16(GetDistinctRectsEncoded()
                .Select(r => (UInt16)((7343629895176496559 * r) >> (64 - 16)))
                .ToArray(), 8);

        }

      //  [Test]
        public void Find_Magic_Evo()
        {
            FindNumberEvo(GetDistinctRectsEncoded().ToArray(), 12);
        }

     //   [Test]
        public void Find_Magic_Second_Level()
        {
            FindNumber32(GetDistinctRectsEncoded()
                .Select(r => (UInt32)((7343629895176496559 * r) >> (64 - 16)))
                .ToArray(), 12);
            // firstLevel.Should().Be(8673919118407924701);
            // var groupBy = GroupBy(8673919118407924701, 8);
            // FindNumber(groupBy.Select(g => g.ToArray()).ToArray());

        }

        private static IEnumerable<IGrouping<ulong, ulong>> GroupBy(ulong firstLevel, int bits)
        {
            return GetDistinctRectsEncoded()
                .GroupBy(r => (firstLevel * r) >> (64 - bits))
                .OrderByDescending(g => g.Count());
        }

        private static ulong FindNumberEvo(ulong[] src, int bits)
        {
            var rnd = new Random(2);
            var sw = Stopwatch.StartNew();
            var population = new ulong[100];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = 16530902731908392367;
            }
            //5155427764000001
            //16530902731908392367
            int counter = 0;
            while (true)
            {
                // spread
                for (int i = 10; i < population.Length; i++)
                {
                    var r = rnd.NextDouble();
                    var body = population[(int)(r * r * r * 10)];
                    body |= (1ul << rnd.Next(64));
                    body &= ~(1ul << rnd.Next(64));
                    population[i] = body;
                }
                // live
                population = population
                    .OrderByDescending(b => FitChar(src, b, bits))
                    .ToArray();
            
                if (sw.Elapsed > TimeSpan.FromSeconds(10)) break;
                counter++;
            }
            Console.WriteLine(counter);
            Console.WriteLine("{0} of {2}- {1}", FitChar(src, population[0], bits), population[0], src.Length);
            return population[0];
        }

        private static ulong NextU64(Random rnd)
        {
            ulong n1 = ((ulong)rnd.Next() << 32) | (ulong)rnd.Next();
            ulong n2 = ((ulong)rnd.Next() << 32) | (ulong)rnd.Next();
            var candidate = n1 | n2;
            return candidate;
        }

        private static ulong FindNumber(ulong[] src, int bits)
        {
            var rnd = new Random(2);
            var sw = Stopwatch.StartNew();
            var max = 0;
            var best = 0ul;
            while (true)
            {
                var magicNumber = NextUniversalNumber(rnd, bits);
                int fitChar = FitChar(src, magicNumber, bits);
                if (fitChar > max)
                {
                    max = fitChar;
                    best = magicNumber;
                    if (max == src.Length)
                    {
                        Console.WriteLine("best: {1}", max, best, src.Length);
                        return best;
                    }
                }
                if (sw.Elapsed > TimeSpan.FromSeconds(10)) break;
            }
            Console.WriteLine("{0} of {2}- {1}", max, best, src.Length);
            return best;
        }

        private static UInt32 FindNumber32(UInt32[] src, int bits)
        {
            var rnd = new Random(2);
            var sw = Stopwatch.StartNew();
            var max = 0;
            UInt32 best = 0;
            while (true)
            {
                UInt32 n1 = (UInt32)rnd.Next();
                UInt32 n2 = (UInt32)rnd.Next();
                var magicNumber = n1 | n2;

                int fitChar = FitChar32(src, magicNumber, bits);
                if (fitChar > max)
                {
                    max = fitChar;
                    best = magicNumber;
                    if (max == src.Length)
                    {
                        Console.WriteLine("best: {1}", max, best, src.Length);
                        return best;
                    }
                }
                if (sw.Elapsed > TimeSpan.FromSeconds(10)) break;
            }
            Console.WriteLine("{0} of {2}- {1}", max, best, src.Length);
            return best;
        }
        private static UInt16 FindNumber16(UInt16[] src, int bits)
        {
            var rnd = new Random(2);
            var sw = Stopwatch.StartNew();
            var max = 0;
            var limit = 1 << bits;
            UInt16 best = 0;
            while (true)
            {
                var n1 = rnd.Next();
                var n2 = rnd.Next();
                var magicNumber = (UInt16)(n1 | n2);

                int fitChar = FitChar16(src, magicNumber, bits);
                if (fitChar > max)
                {
                    max = fitChar;
                    best = magicNumber;
                    if (max == src.Length || max == limit)
                    {
                        Console.WriteLine("best: {1}", max, best, src.Length);
                        return best;
                    }
                }
                if (sw.Elapsed > TimeSpan.FromSeconds(10)) break;
            }
            Console.WriteLine("{0} of {2}- {1}", max, best, src.Length);
            return best;
        }
        private static ulong FindNumber(ulong[][] src)
        {
            var rnd = new Random(2);
            var sw = Stopwatch.StartNew();
            var max = 0;
            var best = 0ul;
            while (true)
            {
                var magicNumber = NextUniversalNumber(rnd, 4);
                var counter = Count(src, magicNumber);
                if (counter > max)
                {
                    max = counter;
                    best = magicNumber;
                    Console.WriteLine("{0} of {2}- {1}", max, best, src.Length);
                }
                if (sw.Elapsed > TimeSpan.FromSeconds(55)) break;
            }
            return best;
        }

        private static int Count(ulong[][] src, ulong magicNumber)
        {
            var counter = 0;
            foreach (var g in src)
            {
                if (g.Length == 1) continue;
                if (Fits(g, magicNumber)) counter++;
            }
            return counter;
        }

        private static bool Fits(ulong[] g, ulong magicNumber)
        {
            if (g.Length > 8) return Fits(g, magicNumber, 4);
            else if (g.Length > 4) return Fits(g, magicNumber, 3);
            else if (g.Length > 2) return Fits(g, magicNumber, 2);
            return false;
        }

        private static bool Fits(ulong[] src, ulong magicNumber, int bits)
        {
            var set = new bool[1 << bits];
            for (var i = 0; i < src.Length; i++)
            {
                var index = (magicNumber * src[i]) >> (64 - bits);
                if (set[index]) return false;
                set[index] = true;
            }
            return true;
        }
        private static int FitChar(ulong[] src, ulong magicNumber, int bits)
        {
            int counter = 0;
            var set = new bool[1 << bits];
            for (var i = 0; i < src.Length; i++)
            {
                var index = (magicNumber * src[i]) >> (64 - bits);
                if (!set[index])
                {
                    set[index] = true;
                    counter++;
                }
            }
            return counter;
        }

        private static int FitChar32(UInt32[] src, UInt32 magicNumber, int bits)
        {
            int counter = 0;
            var set = new bool[1 << bits];
            for (var i = 0; i < src.Length; i++)
            {
                var index = (magicNumber * src[i]) >> (32 - bits);
                if (!set[index])
                {
                    set[index] = true;
                    counter++;
                }
            }
            return counter;
        }
        private static int FitChar16(UInt16[] src, UInt16 magicNumber, int bits)
        {
            int counter = 0;
            var set = new bool[1 << bits];
            for (var i = 0; i < src.Length; i++)
            {
                var index = (UInt16)(magicNumber * src[i]) >> (16 - bits);
                if (!set[index])
                {
                    set[index] = true;
                    counter++;
                }
            }
            return counter;
        }




        private static IEnumerable<ulong> GetDistinctRectsEncoded()
        {
            return GetDistinctRects().Select(r => (1ul << (r / 64)) | (1ul << (r % 64)));
        }
        private static HashSet<int> GetDistinctRects()
        {
            var set = new HashSet<int>();
            for (int i = 0; i < 64; i++)
                for (int j = 0; j < 64; j++)
                    set.Add(RectId(i, j));
            return set;
        }

        private static int RectId(int i, int j)
        {
            var x1 = i / 8;
            var y1 = i % 8;
            var x2 = j / 8;
            var y2 = j % 8;
            var p1 = Math.Min(x1, x2) * 8 + Math.Min(y1, y2);
            var p2 = Math.Max(x1, x2) * 8 + Math.Max(y1, y2);
            int item = p1 * 64 + p2;
            return item;
        }

        public void Find()
        {
            var rnd = new Random(2);
            const ulong firstMagicNumber = 4092486708021557359ul;
            var firstGr = GetDistinctRectsEncoded()
                    .GroupBy(n => (firstMagicNumber * n) >> 58).ToList();

            var list = firstGr.Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList();

            ulong findFirstMagicNumber = FindFirstMagicNumber(firstGr[0], 58);
        }

        private static ulong FindFirstMagicNumber(IEnumerable<ulong> getDistinctRectsEncoded, int bits)
        {
            var rnd = new Random(2);
            var mixin = (ulong)rnd.Next();
            var max = 0;
            var src = getDistinctRectsEncoded as ulong[] ?? getDistinctRectsEncoded.ToArray();
            while (true)
            {
                var magicNumber = NextUniversalNumber(rnd, bits);
                var gr = src
                    .Select(r => (magicNumber * r) >> 58)
                    .GroupBy(n => n).ToList();
                if (gr.Count > max) max = gr.Count;
            }
        }

        private static ulong NextUniversalNumber(Random rnd, int bits)
        {
            while (true)
            {
                ulong n1 = ((ulong)rnd.Next() << 32) | (ulong)rnd.Next();
                ulong n2 = ((ulong)rnd.Next() << 32) | (ulong)rnd.Next();
                var candidate = n1 | n2;

                if (IsGood(candidate, bits))
                    return candidate;
            }
        }
        private static ulong NextUniversalNumber32(Random rnd, int bits)
        {
            while (true)
            {
                ulong n1 = (ulong)rnd.Next();
                ulong n2 = (ulong)rnd.Next();
                var candidate = n1 | n2;

                if (IsGood(candidate, bits))
                    return candidate;
            }
        }

        private static bool IsGood(ulong candidate, int bits)
        {
            for (int x1 = 0; x1 < 8; x1++)
                for (int x2 = 0; x2 < 8; x2++)
                    for (int y1 = 0; y1 < 8; y1++)
                        for (int y2 = 0; y2 < 8; y2++)
                        {
                            var k1 = (1ul << (x1 + y1 * 8)) | (1ul << (x2 + y2 * 8));
                            var k2 = (1ul << (x1 + y2 * 8)) | (1ul << (x2 + y1 * 8));
                            var k3 = (1ul << (x2 + y1 * 8)) | (1ul << (x1 + y2 * 8));
                            var k4 = (1ul << (x2 + y2 * 8)) | (1ul << (x1 + y1 * 8));

                            var index = (k1 * candidate) >> bits;
                            if (((k2 * candidate) >> bits) != index) continue;
                            if (((k3 * candidate) >> bits) != index) continue;
                            if (((k4 * candidate) >> bits) != index) continue;

                            return true;
                        }
            return false;
        }
    }
}