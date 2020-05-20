using DryIoc;

using MarpleSolver.Constraints;
using MarpleSolver.Solvers.GeneralSolvers;

namespace MarpleSolver.Solvers.ConstraintSolvers
{
    public abstract class LeftToRightConstraintSolver : Solver, IConstraintSolver<LeftToRightConstraint>
    {
        public override void Register(Container container)
        {
            container.RegisterInstance<IConstraintSolver<LeftToRightConstraint>>(this);
        }

        public abstract bool TryApply(ProblemColumn[] columns, LeftToRightConstraint constraint, out string actions);
    }
}