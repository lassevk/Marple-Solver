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

        public static Piece A { get; } = new Piece("A");
        public static Piece B { get; } = new Piece("B");
        public static Piece C { get; } = new Piece("C");
        public static Piece D { get; } = new Piece("D");
        public static Piece E { get; } = new Piece("E");

        public static Piece B1 { get; } = new Piece("B1");
        public static Piece B2 { get; } = new Piece("B2");
        public static Piece B3 { get; } = new Piece("B3");
        public static Piece B4 { get; } = new Piece("B4");
        public static Piece B5 { get; } = new Piece("B5");

        public static Piece G1 { get; } = new Piece("G1");
        public static Piece G2 { get; } = new Piece("G2");
        public static Piece G3 { get; } = new Piece("G3");
        public static Piece G4 { get; } = new Piece("G4");
        public static Piece G5 { get; } = new Piece("G5");

        public static Piece Arrow { get; } = new Piece(">");
        public static Piece Square { get; } = new Piece("[]");
        public static Piece O { get; } = new Piece("O");
        public static Piece Star { get; } = new Piece("*");
        public static Piece Plus { get; } = new Piece("+");


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
