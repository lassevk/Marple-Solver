using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using DryIoc;

namespace MarpleSolver.Constraints
{
    public abstract class Constraint
    {
        public abstract Piece[] Pieces { get; }

        public static Constraint Parse(string constraint)
        {
            Constraint result = TryParseOrdered(constraint)
                             ?? TryParseLeftToRight(constraint) ?? TryParseNextTo(constraint) ?? TryParseSameColumn(constraint);

            if (result is null)
                throw new InvalidOperationException($"Unable to parse constraint '{constraint}'");

            return result;
        }

        private static Constraint TryParseOrdered(string constraint)
        {
            var re = new Regex("^(?<p1>[^,]+),(?<p2>[^,]+),(?<p3>[^,]+)$");
            Match ma = re.Match(constraint);
            if (!ma.Success)
                return null;

            if (!Piece.TryParse(ma.Groups["p1"].Value, out Piece p1))
                return null;
            if (!Piece.TryParse(ma.Groups["p2"].Value, out Piece p2))
                return null;
            if (!Piece.TryParse(ma.Groups["p3"].Value, out Piece p3))
                return null;

            if (p1 != p2 && p1 != p3 && p2 != p3)
                return new OrderedConstraint(p1, p2, p3);

            return null;
        }

        private static Constraint TryParseNextTo(string constraint)
        {
            var re = new Regex("^(?<p1>[^,]+),(?<p2>[^,]+),(?<p3>[^,]+)$");
            Match ma = re.Match(constraint);
            if (!ma.Success)
                return null;

            if (!Piece.TryParse(ma.Groups["p1"].Value, out Piece p1))
                return null;
            if (!Piece.TryParse(ma.Groups["p2"].Value, out Piece p2))
                return null;
            if (!Piece.TryParse(ma.Groups["p3"].Value, out Piece p3))
                return null;

            if (p1 != p2 && p1 == p3)
                return new NextToConstraint(p1, p2);

            return null;
        }

        private static Constraint TryParseLeftToRight(string constraint)
        {
            var re = new Regex(@"^(?<p1>[^,\.]+)\.\.\.(?<p2>[^,\.]+)$");
            Match ma = re.Match(constraint);
            if (!ma.Success)
                return null;

            if (!Piece.TryParse(ma.Groups["p1"].Value, out Piece p1))
                return null;
            if (!Piece.TryParse(ma.Groups["p2"].Value, out Piece p2))
                return null;

            if (p1 != p2)
                return new LeftToRightConstraint(p1, p2);

            return null;
        }

        private static Constraint TryParseSameColumn(string constraint)
        {
            var re = new Regex(@"^(?<p1>[^,\.]+),(?<p2>[^,\.]+)$");
            Match ma = re.Match(constraint);
            if (!ma.Success)
                return null;

            if (!Piece.TryParse(ma.Groups["p1"].Value, out Piece p1))
                return null;
            if (!Piece.TryParse(ma.Groups["p2"].Value, out Piece p2))
                return null;

            if (p1 != p2)
                return new SameColumnConstraint(p1, p2);

            return null;
        }

        public bool TrySolve(Container container, ProblemColumn[] columns, out List<string> actions)
        {
            actions = new List<string>();
            ApplySolvers(container, columns, actions);
            return actions.Count > 0;
        }

        protected abstract void ApplySolvers(Container container, ProblemColumn[] columns, List<string> actions);
    }
}
