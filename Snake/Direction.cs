using System.Windows.Controls;

namespace Snake
{
    public class Direction
    {
        // Grid coordinates start from upper left as (0, 0)

        /// <summary>
        /// Represents the left direction: one step to the left (same row, column - 1).
        /// </summary>
        public readonly static Direction Left = new Direction(0, -1);

        /// <summary>
        /// Represents the right direction: one step to the right (same row, column + 1).
        /// </summary>
        public readonly static Direction Right = new Direction(0, 1);

        /// <summary>
        /// Represents the upward direction: one step up (row - 1, same column).
        /// </summary>
        public readonly static Direction Up = new Direction(-1, 0);

        /// <summary>
        /// Represents the downward direction: one step down (row + 1, same column).
        /// </summary>
        public readonly static Direction Down = new Direction(1, 0);

        /// <summary>
        /// Row offset from the current position (e.g., -1 means move up).
        /// </summary>
        public int RowOffset { get; }

        /// <summary>
        /// Column offset from the current position (e.g., 1 means move right).
        /// </summary>
        public int ColOffset { get; }

        /// <summary>
        /// Private constructor to prevent external instantiation.
        /// Only predefined directions can be used.
        /// </summary>
        private Direction(int rowOffset, int colOffset)
        {
            RowOffset = rowOffset;
            ColOffset = colOffset;
        }

        /// <summary>
        /// Returns a new <see cref="Direction"/> that is opposite to the current one.
        /// E.g., if current is Up (-1,0), Opposite is Down (1,0).
        /// </summary>
        public Direction Opposite()
        {
            return new Direction(-RowOffset, -ColOffset);
        }

        /// <summary>
        /// Checks equality between this direction and another object.
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Direction direction &&
                   RowOffset == direction.RowOffset &&
                   ColOffset == direction.ColOffset;
        }

        /// <summary>
        /// Generates a hash code based on row and column offsets.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(RowOffset, ColOffset);
        }

        /// <summary>
        /// Operator overload for '==' to compare two Direction instances.
        /// </summary>
        public static bool operator ==(Direction? left, Direction? right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        /// <summary>
        /// Operator overload for '!=' to compare two Direction instances.
        /// </summary>
        public static bool operator !=(Direction? left, Direction? right)
        {
            return !(left == right);
        }
    }
}

/*
✅ Explanation
Namespace and Class Access

internal class Direction means this class is only accessible within the Snake assembly.

Static Fields (Left, Right, Up, Down)

These represent movement directions on a 2D grid.

For example, Left is new Direction(0, -1) indicating no change in row but move one column to the left.

Offsets

RowOffset and ColOffset describe how a direction changes the current position.

For example, Up changes the row index by -1.

Private Constructor

Prevents other classes from creating arbitrary Direction objects.

This enforces immutability and predefined valid directions.

Opposite Method

Returns the inverse direction (useful for preventing backtracking in games like Snake).

Equality & Hashing

Overrides Equals() and GetHashCode() so that Direction can be used in collections and comparisons.

Operator Overloads

== and != operators are overloaded for convenience in comparing Direction instances directly.
*/