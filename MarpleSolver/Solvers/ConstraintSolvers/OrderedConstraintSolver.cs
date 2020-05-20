using DryIoc;

using MarpleSolver.Constraints;
using MarpleSolver.Solvers.GeneralSolvers;

namespace MarpleSolver.Solvers.ConstraintSolvers
{
    public abstract class OrderedConstraintSolver : Solver, IConstraintSolver<OrderedConstraint>
    {
        public override void Register(Container container)
        {
            container.RegisterInstance<IConstraintSolver<OrderedConstraint>>(this);
        }

        public abstract bool TryApply(ProblemColumn[] columns, OrderedConstraint constraint, out string actions);
    }
}