using System.Collections.Generic;
using System.Linq;

namespace MarpleSolver
{
    public class ProblemColumn
    {
        public ProblemColumn(string name) => Name = name;

        public string Name { get; }

        public List<Piece> Pieces { get; } = Piece.AllPieces().ToList();

        public override string ToString() => Name;
    }
}