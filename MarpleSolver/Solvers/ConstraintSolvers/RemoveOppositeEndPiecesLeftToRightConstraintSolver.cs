using MarpleSolver.Constraints;

namespace MarpleSolver.Solvers.ConstraintSolvers
{
    public class RemoveOppositeEndPiecesLeftToRightConstraintSolver : LeftToRightConstraintSolver
    {
        public override bool TryApply(ProblemColumn[] columns, LeftToRightConstraint constraint, out string actions)
        {
            var isInFirst = columns[0].Pieces.Remove(constraint.Piece2);
            var isInFifth = columns[4].Pieces.Remove(constraint.Piece1);

            if (isInFirst && isInFifth)
                actions = $"Remove {constraint.Piece2} from column 1, and {constraint.Piece1} from column 5";
            else if (isInFirst)
                actions = $"Remove {constraint.Piece2} from column 1";
            else if (isInFifth)
                actions = $"Remove {constraint.Piece1} from column 5";
            else
                actions = null;

            return isInFirst || isInFifth;
        }
    }
}