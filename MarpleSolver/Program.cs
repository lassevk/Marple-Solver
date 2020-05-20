using System;
using System.Reflection;

using DryIoc;

using MarpleSolver.Solvers;

namespace MarpleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var problem = Problem.LoadFromFile("Problem.txt");
            problem.Solve(RegisterAllSolvers());
        }

        private static Container RegisterAllSolvers()
        {
            var container = new Container();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (typeof(Solver).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    // Console.WriteLine($"Registering solver {type.Name}");
                    ((Solver) Activator.CreateInstance(type))?.Register(container);
                }

            return container;
        }
    }
}