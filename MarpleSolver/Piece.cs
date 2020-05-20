using System;
using System.Collections.Generic;
using System.Linq;

namespace MarpleSolver
{
    public struct Piece : IEquatable<Piece>
    {
        private static readonly string[] _LegalPieces =
        {
            "A", "B", "C", "D", "E", "G1", "G2", "G3", "G4", "G5", "B1", "B2", "B3", "B4", "B5", "[]", ">", "O", "*", "+"
        };

        private Piece(string value)
        {
            if (!_LegalPieces.Contains(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        public static bool TryParse(string value, out Piece piece)
        {
            if (_LegalPieces.Contains(value))
            {
                piece = new Piece(value);
                return true;
            }

            piece = default;
            return false;
        }

        public string Value { get; }

        public bool Equals(Piece other) => Value == other.Value;

        public override bool Equals(object obj) => obj is Piece other && Equals(other);

        public override int GetHashCode() => (Value != null ? Value.GetHashCode() : 0);

        public static bool operator ==(Piece left, Piece right) => left.Equals(right);

        public static bool operator !=(Piece left, Piece right) => !left.Equals(right);

        public override string ToString() => Value;

        public static IEnumerable<Piece> AllPieces() => _LegalPieces.Select(value => new Piece(value));
    }
}
