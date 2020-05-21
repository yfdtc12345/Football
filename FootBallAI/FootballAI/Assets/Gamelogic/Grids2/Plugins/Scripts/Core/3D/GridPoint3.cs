using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A discrete 3D vector, used to index into 3D grids.
	/// </summary>
	/// <seealso cref="System.IEquatable{GridPoint3}" />
	[Serializable]
	public struct GridPoint3 : IEquatable<GridPoint3>
	{
		#region Types
		private sealed class VectorLine : IMap<GridPoint3, GridPoint3>
		{
			private readonly GridPoint3 direction;

			public VectorLine(GridPoint3 direction)
			{
				this.direction = direction;
			}

			public GridPoint3 Forward(GridPoint3 input)
			{
				return input + direction;
			}

			public GridPoint3 Reverse(GridPoint3 output)
			{
				return output - direction;
			}
		}
		#endregion

		#region Constants

		/// <inheritdoc cref="Grids2.GridPoint2.Zero"/>
		public static readonly GridPoint3 Zero = new GridPoint3(0, 0, 0);

		/// <inheritdoc cref="Grids2.GridPoint2.One"/>
		public static readonly GridPoint3 One = new GridPoint3(1, 1, 1);

		#endregion

		#region Fields

		[SerializeField]
		private readonly int x;

		[SerializeField]
		private readonly int y;

		[SerializeField]
		private readonly int z;

		#endregion

		#region Properties

		/// <inheritdoc cref="Grids2.GridPoint2.X"/>
		public int X
		{
			get { return x; }
		}

		/// <inheritdoc cref="Grids2.GridPoint2.Y"/>
		public int Y
		{
			get { return y; }
		}

		/// <summary>
		/// The Z coordinate of this point.
		/// </summary>
		public int Z
		{
			get { return z; }
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Constructs a new instance of GridPoint3 with the given coordinates.
		/// </summary>
		public GridPoint3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#endregion

		#region Equality
		/// <inheritdoc/>
		public bool Equals(GridPoint3 other)
		{
			var areEqual = (x == other.x) && (y == other.y) && (z == other.z);
			return areEqual;
		}

		/// <inheritdoc/>
		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(GridPoint3))
			{
				return false;
			}

			var point = (GridPoint3)other;
			return Equals(point);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			unchecked // Wrap when overflows
			{
				int hash = (int)2166136261;

				hash = (hash * 16777619) ^ x.GetHashCode();
				hash = (hash * 16777619) ^ y.GetHashCode();
				hash = (hash * 16777619) ^ z.GetHashCode();

				return hash;
			}
		}

		#endregion

		#region Arithmetic
		/// <inheritdoc cref="GridPoint2.Add(GridPoint2)"/>
		public GridPoint3 Add(GridPoint3 other)
		{
			return new GridPoint3(x + other.X, y + other.Y, z + other.z);
		}

		/// <inheritdoc cref="GridPoint2.Negate"/>
		public GridPoint3 Negate()
		{
			return new GridPoint3(-x, -y, -z);
		}

		/// <inheritdoc cref="GridPoint2.Div(int)"/>
		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint3 Div(int r)
		{
			return new GridPoint3(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r), GLMathf.FloorDiv(z, r));
		}

		/// <inheritdoc cref="GridPoint2.FloorDiv(int)"/>
		[Version(2, 2)]
		public GridPoint3 FloorDiv(int r)
		{
			return new GridPoint3(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r), GLMathf.FloorDiv(z, r));
		}

		/// <inheritdoc cref="GridPoint2.TruncDiv(int)"/>
		[Version(2, 2)]
		public GridPoint3 TruncDiv(int r)
		{
			return new GridPoint3(x / r, y / r, z / r);
		}

		/// <inheritdoc cref="GridPoint2.Mul(int)"/>
		public GridPoint3 Mul(int r)
		{
			return new GridPoint3(x * r, y * r, z * r);
		}

		/// <inheritdoc cref="GridPoint2.Subtract(GridPoint2)"/>
		public GridPoint3 Subtract(GridPoint3 other)
		{
			return new GridPoint3(x - other.X, y - other.Y, z - other.Z);
		}

		/// <inheritdoc cref="GridPoint2.Dot(GridPoint2)"/>
		public int Dot(GridPoint3 other)
		{
			return x * other.X + y * other.Y + z * other.Z;
		}

		/// <summary>
		/// Calculates the cross product of this vector with another.
		/// </summary>
		public GridPoint3 CrossProduct(GridPoint3 other)
		{
			var crossProductX = y * other.z - z * other.y;
			var crossProductY = z * other.y - y * other.z;
			var crossProductZ = x * other.y - y * other.x;

			return new GridPoint3(crossProductX, crossProductY, crossProductZ);
		}

		/// <inheritdoc cref="GridPoint2.Mod(GridPoint2)"/>
		[Obsolete("Use FloorMod or TruncMod instead")]
		public GridPoint3 Mod(GridPoint3 otherPoint)
		{
			var modX = GLMathf.FloorMod(X, otherPoint.X);
			var modY = GLMathf.FloorMod(Y, otherPoint.Y);
			var modZ = GLMathf.FloorMod(Z, otherPoint.Z);

			return new GridPoint3(modX, modY, modZ);
		}

		/// <inheritdoc cref="GridPoint2.FloorMod(GridPoint2)"/>
		[Version(2, 2)]
		public GridPoint3 FloorMod(GridPoint3 otherPoint)
		{
			var modX = GLMathf.FloorMod(X, otherPoint.X);
			var modY = GLMathf.FloorMod(Y, otherPoint.Y);
			var modZ = GLMathf.FloorMod(Z, otherPoint.Z);

			return new GridPoint3(modX, modY, modZ);
		}

		/// <inheritdoc cref="GridPoint2.TruncMod(GridPoint2)"/>
		[Version(2, 2)]
		public GridPoint3 TruncMod(GridPoint3 otherPoint)
		{
			var modX = X % otherPoint.X;
			var modY = Y % otherPoint.Y;
			var modZ = Z % otherPoint.Z;

			return new GridPoint3(modX, modY, modZ);
		}

		/// <inheritdoc cref="GridPoint2.Div(GridPoint2)"/>
		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint3 Div(GridPoint3 otherPoint)
		{
			var divX = GLMathf.FloorDiv(X, otherPoint.X);
			var divY = GLMathf.FloorDiv(Y, otherPoint.Y);
			var divZ = GLMathf.FloorDiv(Z, otherPoint.Z);

			return new GridPoint3(divX, divY, divZ);
		}

		/// <inheritdoc cref="GridPoint2.FloorDiv(GridPoint2)"/>
		[Version(2, 2)]
		public GridPoint3 FloorDiv(GridPoint3 otherPoint)
		{
			var divX = GLMathf.FloorDiv(X, otherPoint.X);
			var divY = GLMathf.FloorDiv(Y, otherPoint.Y);
			var divZ = GLMathf.FloorDiv(Z, otherPoint.Z);

			return new GridPoint3(divX, divY, divZ);
		}

		/// <inheritdoc cref="GridPoint2.TruncDiv(GridPoint2)"/>
		[Version(2, 2)]
		public GridPoint3 TruncDiv(GridPoint3 otherPoint)
		{
			var divX = X % otherPoint.X;
			var divY = Y % otherPoint.Y;
			var divZ = Z % otherPoint.Z;

			return new GridPoint3(divX, divY, divZ);
		}

		/// <inheritdoc cref="GridPoint2.Mul(GridPoint2)"/>
		public GridPoint3 Mul(GridPoint3 otherPoint)
		{
			var resX = X * otherPoint.X;
			var resY = Y * otherPoint.Y;
			var resZ = Z * otherPoint.Z;

			return new GridPoint3(resX, resY, resZ);
		}

		/// <inheritdoc cref="GridPoint2.Min(GridPoint2, GridPoint2)"/>
		public static GridPoint3 Min(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(
				Mathf.Min(point1.x, point2.x),
				Mathf.Min(point1.y, point2.y),
				Mathf.Min(point1.z, point2.z));
		}

		/// <inheritdoc cref="GridPoint2.Max(GridPoint2, GridPoint2)"/>
		public static GridPoint3 Max(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(
				Mathf.Max(point1.x, point2.x),
				Mathf.Max(point1.y, point2.y),
				Mathf.Max(point1.z, point2.z));
		}

		/// <inheritdoc cref="GridPoint2.HadamardMul(GridPoint2, GridPoint2)"/>
		[Obsolete("Use Mul instead")]
		public static GridPoint3 HadamardMul(GridPoint3 p1, GridPoint3 p2)
		{
			return new GridPoint3(p1.X * p2.X, p1.Y * p2.Y, p1.Z * p2.Z);
		}

		/// <summary>
		/// Converts a grid point to a Vector3 with the same 
		/// coordinates (as floats).
		/// </summary>
		public Vector3 ToVector3()
		{
			return new Vector3(x, y, z);
		}

		#endregion

		#region Conversions		
		/// <summary>
		/// Convert this point to GridPoint2, keeping the x and y coordinates.
		/// </summary>
		public GridPoint2 To2DXY()
		{
			return new GridPoint2(x, y);
		}

		/// <summary>
		/// Convert this point to GridPoint2, keeping the x and z coordinates.
		/// </summary>
		public GridPoint2 TO2DXZ()
		{
			return new GridPoint2(x, z);
		}

		/// <summary>
		/// Converts a grid point to a Vector3 with the same 
		/// coordinates (as floats).
		/// </summary>
		/// <remarks><see cref="Vector3"/> is to <see cref="GridPoint3"/> 
		/// what <see cref="float"/> is to <see cref="int"/>.</remarks>
		public static implicit operator Vector3(GridPoint3 point)
		{
			return point.ToVector3();
		}
		#endregion

		#region Utility
		/// <inheritdoc/>
		public override string ToString()
		{
			return "(" + x + ", " + y + "," + z + ")";
		}
		#endregion

		#region Operators

		/// <inheritdoc cref="GridPoint2.operator==(GridPoint2, GridPoint2)" />

		public static bool operator ==(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Equals(point2);
		}

		/// <inheritdoc cref="GridPoint2.operator!=(GridPoint2, GridPoint2)" />
		public static bool operator !=(GridPoint3 point1, GridPoint3 point2)
		{
			return !point1.Equals(point2);
		}

		/// <inheritdoc cref="GridPoint2.operator+(GridPoint2)" />
		public static GridPoint3 operator +(GridPoint3 point)
		{
			return point;
		}

		/// <inheritdoc cref="GridPoint2.operator-(GridPoint2)" />
		public static GridPoint3 operator -(GridPoint3 point)
		{
			return point.Negate();
		}

		/// <inheritdoc cref="GridPoint2.operator+(GridPoint2, GridPoint2)" />
		public static GridPoint3 operator +(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Add(point2);
		}

		/// <inheritdoc cref="GridPoint2.operator-(GridPoint2, GridPoint2)" />
		public static GridPoint3 operator -(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Subtract(point2);
		}

		/// <inheritdoc cref="GridPoint2.operator*(GridPoint2, GridPoint2)" />
		public static GridPoint3 operator *(GridPoint3 point, int n)
		{
			return point.Mul(n);
		}

		/// <inheritdoc cref="GridPoint2.operator*(int, GridPoint2)" />
		public static GridPoint3 operator *(int n, GridPoint3 point)
		{
			return point.Mul(n);
		}

		/// <inheritdoc cref="GridPoint2.operator/(GridPoint2, int)" />
		public static GridPoint3 operator /(GridPoint3 point, int n)
		{
			return new GridPoint3(point.x / n, point.y / n, point.z / n);
		}

		/// <inheritdoc cref="GridPoint2.operator*(GridPoint2, GridPoint2)" />
		public static GridPoint3 operator *(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Mul(point2);
		}

		/// <inheritdoc cref="GridPoint2.operator/(GridPoint2, GridPoint2)" />
		public static GridPoint3 operator /(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(point1.x / point2.x, point1.y / point2.y, point1.z / point2.z);
		}

		/// <inheritdoc cref="GridPoint2.operator%(GridPoint2, GridPoint2)" />
		public static GridPoint3 operator %(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(point1.x % point2.x, point1.y % point2.y, point1.z % point2.z);
		}
		#endregion

		#region Colorings
		/// <inheritdoc cref="GridPoint2.GetColor(ColorFunction)"/>
		[Obsolete("Use the overload the ColorFunction2 parameter instead")]
		public int GetColor(ColorFunction colorFunction)
		{
			return GetColor(colorFunction.x0, colorFunction.x1, colorFunction.y1);
		}

		/// <inheritdoc cref="GridPoint2.GetColor(int, int, int)"/>
		public int GetColor(int x0, int x1, int y1)
		{
			int colorCount = x0 * y1;

			float a = (x * y1 - y * x1) / (float)colorCount;
			float b = (y * x0) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * x0 + n * x1;
			int baseVectorY = n * y1;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, x0);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * x0);

			return colorIndex;
		}

		/// <inheritdoc cref="GridPoint2.GetColor(ColorFunction)"/>
		public int __GetColor_Reference(int ax, int bx, int by, int cx, int cy, int cz)
		{
			var parm = new Matrixf33(ax, 0, 0, bx, by, 0, cx, cy, cz);
			var parmInv = parm.Inv();
			var pointInUnitCube =  this.ToVector3().Mul(parmInv);
			var rect = new Matrixf33(ax, 0, 0, 0, by, 0, 0, 0, cz);
			var pointInRect = pointInUnitCube.Mul(rect);

			var rectX = GLMathf.FloorMod(Mathf.FloorToInt(pointInRect.x), ax);
			var rectY = GLMathf.FloorMod(Mathf.FloorToInt(pointInRect.y), by);
			var rectZ = GLMathf.FloorMod(Mathf.FloorToInt(pointInRect.z), cz);

			return rectX + rectY * ax + rectZ * ax * by;
		}

		//See the reference implementation
		//The implementation below is derived from that
		//where matrix computations has been replaced 
		//with direct calculations.
		/// <inheritdoc cref="GridPoint2.GetColor(ColorFunction)"/>
		public int GetColor(int x0, int x1, int y1, int x2, int y2, int z2)
		{
			int xi = Mathf.FloorToInt(x - x1 * y / (float) y1 - z * (y1 * x2 - y2 * x1) / (float)(y1 * z2));
			int yi = Mathf.FloorToInt(y - y2 * z / (float)z2);
			int zi = z;

			var rectX = GLMathf.FloorMod(xi, x0);
			var rectY = GLMathf.FloorMod(yi, y1);
			var rectZ = GLMathf.FloorMod(zi, z2);

			return rectX + rectY * x0 + rectZ * x0 * y1;
		}

		public int GetColor(ColorFunction3 colorFunction)
		{
			return GetColor(
				colorFunction.x0,
				colorFunction.x1,
				colorFunction.y1,
				colorFunction.x2,
				colorFunction.y2,
				colorFunction.z2);
		}
		#endregion

		#region Neighbors and Lines
		/// <inheritdoc cref="GridPoint2.GetVectorNeighbors(IEnumerable{GridPoint2})"/>
		public IEnumerable<GridPoint3> GetVectorNeighbors(IEnumerable<GridPoint3> directions)
		{
			var thisCopy = this;

			return directions.Select(p => thisCopy + p);
		}

		/// <inheritdoc cref="GridPoint2.GetVectorLine(GridPoint2)"/>
		public static IMap<GridPoint3, GridPoint3> GetVectorLine(GridPoint3 direction)
		{
			return new VectorLine(direction);
		}

		/// <inheritdoc cref="GridPoint2.GetVectorLines(IEnumerable{GridPoint2})"/>
		public static IEnumerable<IMap<GridPoint3, GridPoint3>> GetVectorLines(IEnumerable<GridPoint3> directions)
		{
			return directions.Select(d => GetVectorLine(d));
		}

		/// <inheritdoc cref="GridPoint2.GetForwardVectorRays(IEnumerable{GridPoint2})"/>
		public static IEnumerable<IForwardMap<GridPoint3, GridPoint3>> GetForwardVectorRays(IEnumerable<GridPoint3> direction)
		{
			return direction.Select(d => (IForwardMap<GridPoint3, GridPoint3>)GetVectorLine(d));
		}

		/// <inheritdoc cref="GridPoint2.GetReverseVectorRays(IEnumerable{GridPoint2})"/>
		public static IEnumerable<IReverseMap<GridPoint3, GridPoint3>> GetReverseVectorRays(IEnumerable<GridPoint3> direction)
		{
			return direction.Select(d => (IReverseMap<GridPoint3, GridPoint3>)GetVectorLine(d));
		}
		#endregion
	}
}