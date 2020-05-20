using MarpleSolver.Constraints;

namespace MarpleSolver.Solvers.ConstraintSolvers
{
    public interface IConstraintSolver<in T> where T: Constraint
    {
        bool TryApply(ProblemColumn[] columns, T constraint, out string action);
    }
}
