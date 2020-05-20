using System;
using System.Collections.Generic;
using System.Linq;

using DryIoc;

namespace MarpleSolver.Solvers.GeneralSolvers
{
    public class RemoveFromOthersWhenDualMatchingSolver : Solver, ISolver
    {
        public override void Register(Container container)
        {
            container.RegisterInstance<ISolver>(this);
        }

        public bool TryApply(ProblemColumn[] columns, List<string> actions)
        {
            if (TryApply(columns, new[] { Piece.A, Piece.B, Piece.C, Piece.D, Piece.E }, actions))
                return true;

            if (TryApply(columns, new[] { Piece.G1, Piece.G2, Piece.G3, Piece.G4, Piece.G5 }, actions))
                return true;

            if (TryApply(columns, new[] { Piece.B1, Piece.B2, Piece.B3, Piece.B4, Piece.B5 }, actions))
                return true;

            if (TryApply(columns, new[] { Piece.Arrow, Piece.Star, Piece.Square, Piece.O, Piece.Plus }, actions))
                return true;

            return false;
        }

        private bool TryApply(ProblemColumn[] columns, Piece[] pieces, List<string> actions)
        {
            var candidates = (
                from column in columns
                let matchingPieces = column.Pieces.Where(pieces.Contains).ToArray()
                where matchingPieces.Length == 2
                select new { column, matchingPieces }).ToList();

            if (candidates.Count < 2)
                return false;

            for (var index1 = 0; index1 < candidates.Count - 1; index1++)
            {
                var pieces1 = String.Join(", ", candidates[index1].matchingPieces);
                for (var index2 = index1 + 1; index2 < candidates.Count; index2++)
                {
                    var pieces2 = String.Join(", ", candidates[index2].matchingPieces);
                    if (pieces1 == pieces2)
                    {
                        bool anyRemoved = false;
                        foreach (ProblemColumn column in columns)
                            if (column != candidates[index1].column && column != candidates[index2].column)
                            {
                                foreach (Piece piece in candidates[index1].matchingPieces)
                                    if (column.Pieces.Remove(piece))
                                    {
                                        actions.Add($"Removed {piece} from column {column.Name}");
                                        anyRemoved = true;
                                    }
                            }

                        if (anyRemoved)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
