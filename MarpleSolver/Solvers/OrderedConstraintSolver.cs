using DryIoc;

using MarpleSolver.Constraints;

namespace MarpleSolver.Solvers
{
    public abstract class OrderedConstraintSolver : Solver, ISolver<OrderedConstraint>
    {
        public override void Register(Container container)
        {
            container.RegisterInstance<ISolver<OrderedConstraint>>(this);
        }

        public abstract bool TryApply(ProblemColumn[] columns, OrderedConstraint constraint, out string actions);
    }
}