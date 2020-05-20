using System.Collections.Generic;

using DryIoc;

using MarpleSolver.Solvers;
using MarpleSolver.Solvers.ConstraintSolvers;

namespace MarpleSolver.Constraints
{
    public class LeftToRightConstraint : Constraint
    {
        public LeftToRightConstraint(Piece first, Piece second)
        {
            (Piece1, Piece2) = (first, second);
        }

        public Piece Piece1 { get; }

        public Piece Piece2 { get; }

        public override Piece[] Pieces => new[] { Piece1, Piece2 };

        protected override void ApplySolvers(Container container, ProblemColumn[] columns, List<string> actions)
        {
            foreach (IConstraintSolver<LeftToRightConstraint> solver in container.Resolve<IEnumerable<IConstraintSolver<LeftToRightConstraint>>>())
                if (solver.TryApply(columns, this, out var action)) 
                    actions.Add(action);
        }

        public override string ToString() => $"{Piece1} left of {Piece2}";
    }
}