using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeExercise
{
    public class Generator
    {
        private static readonly Random Random = new Random();
        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => Random.Next(1, 6))
                .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    public class MagicSquareGenerator
    {
        List<List<int>> generated = new List<List<int>>();
        List<List<int>> result = new List<List<int>>();
        public List<List<int>> Generate(int size)
        {
            var generator = new Generator();
            var splitter = new Splitter();
            var verifier = new Verifier();

            for (int i = 0; i < size; i++)
            {
                generated.Add(generator.Generate(size));
            }
            result = splitter.Split(generated);
            Console.WriteLine(verifier.Verify(result));
            return result;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var c in result)
            {
                foreach (var i in c)
                    sb.Append(i);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var msg = new MagicSquareGenerator();
            msg.Generate(4);
            Console.WriteLine(msg);
        }
    }
}
