using MarpleSolver.Constraints;

namespace MarpleSolver.Solvers
{
    public interface ISolver<in T> where T: Constraint
    {
        bool TryApply(ProblemColumn[] columns, T constraint, out string actions);
    }
}
