using MarpleSolver.Constraints;

namespace MarpleSolver.Solvers
{
    public class RemoveMiddlePiecesFromEndsOrderedConstraintSolver : OrderedConstraintSolver
    {
        public override bool TryApply(ProblemColumn[] columns, OrderedConstraint constraint, out string actions)
        {
            var isInFirst = columns[0].Pieces.Remove(constraint.Piece2);
            var isInFifth = columns[4].Pieces.Remove(constraint.Piece2);

            if (isInFirst && isInFifth)
                actions = $"Remove {constraint.Piece2} from column 1 and 5";
            else if (isInFirst)
                actions = $"Remove {constraint.Piece2} from column 1";
            else if (isInFifth)
                actions = $"Remove {constraint.Piece2} from column 5";
            else
                actions = null;

            return isInFirst || isInFifth;
        }
    }
}