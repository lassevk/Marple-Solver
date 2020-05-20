using DryIoc;

using MarpleSolver.Constraints;

namespace MarpleSolver.Solvers
{
    public abstract class LeftToRightConstraintSolver : Solver, ISolver<LeftToRightConstraint>
    {
        public override void Register(Container container)
        {
            container.RegisterInstance<ISolver<LeftToRightConstraint>>(this);
        }

        public abstract bool TryApply(ProblemColumn[] columns, LeftToRightConstraint constraint, out string actions);
    }
}