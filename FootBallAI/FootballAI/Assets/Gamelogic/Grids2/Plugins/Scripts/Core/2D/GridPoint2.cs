using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A discrete 2D vector, used to index into 2D grids. 
	/// </summary>
	/// <seealso cref="System.IEquatable{GridPoint2}" />
	/// <inheritdoc/>
	/// <remarks>Grid points work like vectors, except their components are integers. Grid points can be used 
	/// on their own, but they are frequently used with grids and shapes.</remarks>
	/// <seealso cref="Gamelogic.Grids2.RectPoint"/>
	/// <seealso cref="Gamelogic.Grids2.PointyHexPoint"/>
	[Serializable]
	public struct GridPoint2 : IEquatable<GridPoint2>
	{
		#region Types
		private sealed class VectorLine : IMap<GridPoint2, GridPoint2>
		{
			private readonly GridPoint2 direction;

			public VectorLine(GridPoint2 direction)
			{
				this.direction = direction;
			}

			public GridPoint2 Forward(GridPoint2 input)
			{
				return input + direction;
			}

			public GridPoint2 Reverse(GridPoint2 output)
			{
				return output - direction;
			}
		}
		#endregion

		#region Constants

		/// <summary>
		/// The zero grid point, also called the origin. All coordinates are 0.
		/// </summary>
		public static readonly GridPoint2 Zero = new GridPoint2(0, 0);

		/// <summary>
		/// The grid point with all coordinates equal to 1.
		/// </summary>
		public static readonly GridPoint2 One = new GridPoint2(1, 1);
		#endregion

		#region Fields

		[SerializeField]
		private readonly int x;

		[SerializeField]
		private readonly int y;

		#endregion

		#region Properties		
		/// <summary>
		/// The X coordinate of this point.
		/// </summary>
		public int X
		{
			get { return x; }
		}

		/// <summary>
		/// The Y coordinate of this point.
		/// </summary>
		public int Y
		{
			get { return y; }
		}

		//The Z coordinate for hex points.
		//TODO: 
		//Q: Should this be exposed?
		//A: Probably, and better as H
		//	So it does not cause confusion with Z in 3D.
		private int Z
		{
			get { return -x - y; }
		}
		#endregion

		#region Constructors		
		/// <summary>
		/// Creates a new instance of the <see cref="GridPoint2"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public GridPoint2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		#endregion

		#region Equality		
		/// <summary>
		/// Compares this grid point with another grid point for 
		/// equality. Two grid points are equal if their respective coordinates are equal.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns><c>true</c> if both points' x-coordinates are equal and their y-coordinates are equal, <c>false</c> otherwise.</returns>
		public bool Equals(GridPoint2 other)
		{
			bool areEqual = (x == other.x) && (y == other.y);
			return areEqual;
		}

		/// <inheritdoc/>
		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(GridPoint2))
			{
				return false;
			}

			var point = (GridPoint2)other;
			return Equals(point);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			//return x ^ y;

			unchecked // Wrap when overflows
			{
				int hash = (int)2166136261;

				hash = (hash * 16777619) ^ x.GetHashCode();
				hash = (hash * 16777619) ^ y.GetHashCode();

				return hash;
			}
		}

		#endregion
  
		#region Arithmetic		
		/// <summary>
		/// Adds an other grid point to this grid point and returns the result.
		/// </summary>
		/// <param name="other">The other grid point to add to this grid point.</param>
		
		public GridPoint2 Add(GridPoint2 other)
		{
			return new GridPoint2(x + other.X, y + other.Y);
		}

		/// <summary>
		/// Returns the point with each coordinate negated.
		/// </summary>
		/// <returns>GridPoint2.</returns>
		/// <example>
		/// <code>
		/// var p1 = new GridPoint2(3, -5);
		/// var p2 = p1.Negate(); //the point (-3, 5)
		/// var zero = p1 + p2; //the vectors add to GridPoint2.zero
		/// </code>
		/// </example>
		public GridPoint2 Negate()
		{
			return new GridPoint2(-x, -y);
		}

		/// <inheritdoc/>
		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint2 Div(int r)
		{
			return new GridPoint2(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		/// <summary>
		/// Divides each coordinate with r, using integer floor division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint2 FloorDiv(int r)
		{
			return new GridPoint2(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		/// <summary>
		/// Divides each coordinate with r, using integer trunc division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint2 TruncDiv(int r)
		{
			return new GridPoint2(x / r, y / r);
		}

		/// <summary>
		/// Multiplies each coordinate with the specified integer.
		/// </summary>
		public GridPoint2 Mul(int r)
		{
			return new GridPoint2(x * r, y * r);
		}

		/// <summary>
		/// Subtracts the other point from this point, and returns the result.
		/// </summary>
		public GridPoint2 Subtract(GridPoint2 other)
		{
			return new GridPoint2(x - other.X, y - other.Y);
		}

		/// <summary>
		/// Calculates the dot product between this grid point and another.
		/// </summary>
		public int Dot(GridPoint2 other)
		{
			return x * other.X + y * other.Y;
		}

		/// <summary>
		/// Calculates the perp dot product between this grid point and another.
		/// </summary>
		/// <seealso href="http://mathworld.wolfram.com/PerpDotProduct.html"/>
		public int PerpDot(GridPoint2 other)
		{
			return x * other.Y - y * other.x;
		}

		/// <summary>
		/// Calculates the hex-grid equivalent for the dot product.
		/// </summary>
		/// <seealso href="http://www.gamelogic.co.za/downloads/HexMath2.pdf"/>
		public int HexDot(GridPoint2 other)
		{
			return Dot(other) + Z * other.Z;
		}

		/// <summary>
		/// Calculates the vector perpendicular to this vector (rotated counter-clockwise).
		/// </summary>
		public GridPoint2 Perp()
		{
			return new GridPoint2(-y, x);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// reminder when the first point is divided
		/// by the second point component-wise.The
		///	division is integer division.
		/// </summary>
		[Obsolete("Use FloorMod or TruncMod instead")]
		public GridPoint2 Mod(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorMod(X, otherPoint.X),
				GLMathf.FloorMod(Y, otherPoint.Y));
		}
		///<summary>
		/// Gives a new point that represents the 
		///	reminder when the first point is divided
		///	by the second point	component-wise. The
		///	division is integer floor division.
		///	</summary>
		[Version(2, 2)]
		public GridPoint2 FloorMod(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorMod(X, otherPoint.X),
				GLMathf.FloorMod(Y, otherPoint.Y));
		}

		///<summary>
		/// Gives a new point that represents the 
		///	reminder when the first point is divided
		///	by the second point	component-wise. The
		///	division is integer trunc division.
		///	</summary>
		[Version(2, 2)]
		public GridPoint2 TruncMod(GridPoint2 otherPoint)
		{
			return new GridPoint2(X % otherPoint.X, Y % otherPoint.Y);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise.The division is integer
		/// division.
		/// </summary>
		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint2 Div(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorDiv(X, otherPoint.X),
				GLMathf.FloorDiv(Y, otherPoint.Y));
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise.The division is integer
		/// division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint2 FloorDiv(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorDiv(X, otherPoint.X),
				GLMathf.FloorDiv(Y, otherPoint.Y));
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise.The division is integer
		/// division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint2 TruncDiv(GridPoint2 otherPoint)
		{
			return new GridPoint2(X / otherPoint.X, Y / otherPoint.Y);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point multiplied by the second point
		///	component-wise.
		/// </summary>
		/// <param name="otherPoint">The other point.</param>
		public GridPoint2 Mul(GridPoint2 otherPoint)
		{
			return new GridPoint2(X * otherPoint.X, Y * otherPoint.Y);
		}

		/// <summary>
		/// Calculates a new grid point with coordinates the minimum of the respective coordinates of
		/// two grid points.
		/// </summary>
		public static GridPoint2 Min(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(Mathf.Min(point1.x, point2.x), Mathf.Min(point1.y, point2.y));
		}

		/// <summary>
		/// Calculates a new grid point with coordinates the maximum of the respective coordinates of
		/// two grid points.
		/// </summary>
		public static GridPoint2 Max(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(Mathf.Max(point1.x, point2.x), Mathf.Max(point1.y, point2.y));
		}

		[Obsolete("Use Mul instead")]
		public static GridPoint2 HadamardMul(GridPoint2 p1, GridPoint2 p2)
		{
			return new GridPoint2(p1.X * p2.X, p1.Y * p2.Y);
		}

		/// <summary>
		/// Converts this grid point to a <see cref="Vector2"/>.
		/// </summary>
		/// <remarks><see cref="Vector2"/> is to <see cref="GridPoint2"/> 
		/// what <see cref="float"/> is to <see cref="int"/>.</remarks>
		public Vector2 ToVector2()
		{
			return new Vector2(x, y);
		}

		#endregion

		#region Conversions		
		/// <summary>
		/// Converts this point to GridPoint 3.
		/// </summary>
		/// <param name="newZ">The new z coordinate.</param>
		public GridPoint3 To3DXY(int newZ = 0)
		{
			return new GridPoint3(x, y, newZ);
		}

		/// <summary>
		/// Converts the point to GridPoint2.
		/// </summary>
		/// <param name="newY">The new y coordinate.</param>
		public GridPoint3 To3DXZ(int newY = 0)
		{
			return new GridPoint3(x, newY, y);
		}

		/// <summary>
		/// Converts this grid point to a <see cref="Vector2"/>.
		/// </summary>
		/// <remarks><see cref="Vector2"/> is to <see cref="GridPoint2"/> 
		/// what <see cref="float"/> is to <see cref="int"/>.</remarks>
		public static implicit operator Vector2(GridPoint2 point)
		{
			return point.ToVector2();
		}

		#endregion

		#region Utility
		/// <inheritdoc/>
		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}

		#endregion

		#region Operators
		/// <summary>
		/// Compares two points for equality.
		/// </summary>
		/// <remarks>Two points are equal if their respective coordinates are equal.</remarks>
		public static bool operator ==(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Equals(point2);
		}

		/// <summary>
		/// Checks whether two points are not equal.
		/// </summary>
		/// <remarks>Two points are not equal when either of their coordinates don't match.</remarks>
		public static bool operator !=(GridPoint2 point1, GridPoint2 point2)
		{
			return !point1.Equals(point2);
		}

		/// <summary>
		/// This operations does not affect the point.
		/// </summary>
		public static GridPoint2 operator +(GridPoint2 point)
		{
			return point;
		}

  
		/// <summary>
		/// Negates the given vector.
		/// </summary>
		public static GridPoint2 operator -(GridPoint2 point)
		{
			return point.Negate();
		}
		/// <summary>
		/// Adds two points by adding their corresponding coordinates.
		/// </summary>
		public static GridPoint2 operator +(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Add(point2);
		}
		/// <summary>
		/// Subtracts one point from another by subtracting corresponding coordinates.
		/// </summary>
		public static GridPoint2 operator -(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Subtract(point2);
		}

		/// <summary>
		/// Multiplies each coordinate with the integer.
		/// </summary>
		public static GridPoint2 operator *(GridPoint2 point, int n)
		{
			return point.Mul(n);
		}

		/// <summary>
		/// Multiplies each coordinate with the integer.
		/// </summary>
		public static GridPoint2 operator *(int n, GridPoint2 point)
		{
			return point.Mul(n);
		}

		/// <summary>
		/// Divides each coordinate by the integer using integer division.
		/// </summary>
		public static GridPoint2 operator /(GridPoint2 point, int n)
		{
			return new GridPoint2(point.x / n, point.y / n);
		}

		/// <summary>
		/// Multiplies corresponding coordinates.
		/// </summary>
		public static GridPoint2 operator *(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Mul(point2);
		}

		/// <summary>
		/// Divides corresponding coordinates using integer division.
		/// </summary>
		public static GridPoint2 operator /(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(point1.x / point2.x, point1.y / point2.y);
		}

		/// <summary>
		/// Divides corresponding coordinates using integer division and return the
		/// remainders as a new grid point.
		/// </summary>
		public static GridPoint2 operator %(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(point1.x % point2.x, point1.y % point2.y);
		}
		#endregion

		#region Colorings

		/// <summary>
		/// Gets the color index for this point for the given color function.
		/// </summary>
		/// <param name="colorFunction">The color function used to partition points.</param>
		/// <returns>An integer that represents this points "color".</returns>
		public int GetColor(ColorFunction colorFunction)
		{
			return GetColor(colorFunction.x0, colorFunction.x1, colorFunction.y1);
		}


		/// <summary>
		/// Gets the color index for this point for the given coloring parameters.
		///</summary>
		///<remarks>
		///Gives a coloring of the grid such that 
		///if a point p has color k, then all points
		///p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.More information about grid colorings:
		///http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///</remarks>
		[Version(1, 7)]
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		#endregion
		
		#region Neighbors and Lines
		/// <summary>
		/// Gets the vector neighbors of this point in the given direction.
		/// </summary>
		/// <param name="directions">A list of points that represents where the neighbors are.</param>
		/// <remarks>
		/// The vector neighbors of a point is calculated by adding the point to each of the vector directions.
		/// For example, in a square grid, the orthogonal neighbors are given by the four points
		/// (1, 0), (0, 1), (-1, 0) and (0, -1). The vector neighbors of (3, 4) in these directions
		/// are the points (4, 4), (3, 5), (2, 4) and (3, 3).
		/// </remarks>
		/// <conceptualLink target="58886188-fde9-4b64-bb40-77c9d402a93d"/>
		public IEnumerable<GridPoint2> GetVectorNeighbors(IEnumerable<GridPoint2> directions)
		{
			var thisCopy = this;

			return directions.Select(p => thisCopy + p);
		}

		/// <summary>
		/// Gets the map that represents a line through this point in the given direction.
		/// </summary>
		/// <param name="direction">The direction of the returned line.</param>
		/// <remarks>
		/// The vector line of a point p in the direction d is the set of points given
		/// by p + d*n, where n is over all integers. The map returned by this function can generate
		/// (a portion) of this line by repeatedly applying it on the point (either in the forward or
		/// backward direction).
		/// </remarks>
		public static IMap<GridPoint2, GridPoint2> GetVectorLine(GridPoint2 direction)
		{
			return new VectorLine(direction);
		}

		/// <summary>
		/// Gets the maps that represent the vector lines through this point in the given list of directions.
		/// </summary>
		/// <param name="directions">The directions of the lines.</param>
		/// <seealso cref="GetVectorLine(GridPoint2)"/>
		/// <conceptualLink target="d700703a-8be9-49d3-a9b8-653021ed7843"/>
		public static IEnumerable<IMap<GridPoint2, GridPoint2>> GetVectorLines(IEnumerable<GridPoint2> directions)
		{
			return directions.Select<GridPoint2, IMap<GridPoint2, GridPoint2>>(GetVectorLine);
		}

		/// <summary>
		/// Gets the maps that represent the forward vector rays through this point in the given list of directions.
		/// </summary>
		/// <param name="directions">The directions of the rays.</param>
		/// <conceptualLink target="d700703a-8be9-49d3-a9b8-653021ed7843"/>
		public IEnumerable<IForwardMap<GridPoint2, GridPoint2>> GetForwardVectorRays(IEnumerable<GridPoint2> directions)
		{
			return directions.Select(d => (IForwardMap<GridPoint2, GridPoint2>)GetVectorLine(d));
		}

		/// <summary>
		/// Gets the maps that represent the reverse vector lines through this point in the given list of directions.
		/// </summary>
		/// <param name="directions">The directions of the rays.</param>
		/// <conceptualLink target="d700703a-8be9-49d3-a9b8-653021ed7843"/>
		public IEnumerable<IReverseMap<GridPoint2, GridPoint2>> GetReverseVectorRays(IEnumerable<GridPoint2> directions)
		{
			return directions.Select(d => (IReverseMap<GridPoint2, GridPoint2>)GetVectorLine(d));
		}
		#endregion
	}
}
