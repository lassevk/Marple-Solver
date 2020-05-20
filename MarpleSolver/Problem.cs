using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using DryIoc;

using MarpleSolver.Constraints;

namespace MarpleSolver
{
    public class Problem
    {
        public List<Constraint> Constraints { get; }

        private Problem(IEnumerable<Constraint> constraints)
        {
            Constraints = constraints.ToList();
        }

        public static Problem LoadFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            IEnumerable<Constraint> constraints =
                from line in lines
                where !String.IsNullOrWhiteSpace(line)
                let trimmed = line.Trim()
                select Constraint.Parse(line);

            return new Problem(constraints);
        }

        public void Solve(Container container)
        {
            ProblemColumn[] columns = Enumerable.Range(1, 5).Select(_ => new ProblemColumn()).ToArray();

            var more = true;
            while (more)
            {
                more = false;
                foreach (Constraint constraint in Constraints)
                {
                    if (constraint.TrySolve(container, columns, out List<string> actions))
                    {
                        foreach (var action in actions)
                            Console.WriteLine(action);

                        more = true;
                    }
                }
            }
        }
    }

    public class ProblemColumn
    {
        public List<Piece> Pieces { get; } = Piece.AllPieces().ToList();
    }
}
