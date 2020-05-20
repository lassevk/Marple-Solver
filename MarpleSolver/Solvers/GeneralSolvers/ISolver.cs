using System.Collections.Generic;

namespace MarpleSolver.Solvers.GeneralSolvers
{
    public interface ISolver
    {
        bool TryApply(ProblemColumn[] columns, List<string> actions);
    }
}