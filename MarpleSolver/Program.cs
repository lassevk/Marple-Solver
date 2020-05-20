using System;
using System.Reflection;

using DryIoc;

using MarpleSolver.Constraints;
using MarpleSolver.Solvers;

namespace MarpleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                if (typeof(Solver).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    // Console.WriteLine($"Registering solver {type.Name}");
                    ((Solver)Activator.CreateInstance(type))?.Register(container);
                }

            var problem = Problem.LoadFromFile("Problem.txt");
            problem.Solve(container);
        }
    }
}