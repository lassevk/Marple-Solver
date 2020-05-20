using DryIoc;

namespace MarpleSolver.Solvers
{
    public abstract class Solver
    {
        public abstract void Register(Container container);
    }
}
