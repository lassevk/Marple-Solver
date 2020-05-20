using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using DryIoc;

using MarpleSolver.Constraints;
using MarpleSolver.Solvers.GeneralSolvers;

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
            ProblemColumn[] columns = Enumerable.Range(1, 5).Select(index => new ProblemColumn($"C{index}")).ToArray();

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

                foreach (ISolver solver in container.Resolve<IEnumerable<ISolver>>())
                {
                    var actions = new List<string>();
                    if (solver.TryApply(columns, actions))
                    {
                        foreach (var action in actions)
                            Console.WriteLine(action);
                        more = true;
                    }
                }
            }
        }
    }
}
