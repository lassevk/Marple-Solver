using System.Collections.Generic;

using DryIoc;

using MarpleSolver.Solvers;

namespace MarpleSolver.Constraints
{
    public class OrderedConstraint : Constraint
    {
        public OrderedConstraint(Piece piece1, Piece second, Piece third)
        {
            (Piece1, Piece2, Piece3) = (piece1, second, third);
        }

        public Piece Piece1 { get; }

        public Piece Piece2 { get; }

        public Piece Piece3 { get; }

        public override Piece[] Pieces => new[] { Piece1, Piece2, Piece3 };

        protected override void ApplySolvers(Container container, ProblemColumn[] columns, List<string> actions)
        {
            foreach (ISolver<OrderedConstraint> solver in container.Resolve<IEnumerable<ISolver<OrderedConstraint>>>())
                if (solver.TryApply(columns, this, out var action))
                    actions.Add(action);
        }

        public override string ToString() => $"Ordered {Piece1}, {Piece2}, {Piece3}";
    }
}
