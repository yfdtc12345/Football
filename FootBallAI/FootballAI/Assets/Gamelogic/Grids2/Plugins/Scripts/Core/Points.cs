using System.Collections.Generic;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;
using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Provides constants and methods for working with points in rect grids.
	/// </summary>
	public static class RectPoint
	{
		#region Constants		
		/// <summary>
		/// The point one unit to the north of the origin.
		/// </summary>
		public static readonly GridPoint2 North = new GridPoint2(0, 1); //TODO: use unity directions instead?

		/// <summary>
		/// The point one unit to the east of the origin.
		/// </summary>
		public static readonly GridPoint2 East = new GridPoint2(1, 0);

		/// <summary>
		/// The point one unit to the south of the origin.
		/// </summary>
		public static readonly GridPoint2 South = new GridPoint2(0, -1);

		/// <summary>
		/// The point one unit to the west of the origin.
		/// </summary>
		public static readonly GridPoint2 West = new GridPoint2(-1, 0);

		/// <summary>
		/// The point one unit to the north and one unit to the east of the origin.
		/// </summary>
		public static readonly GridPoint2 NorthEast = North + East;

		/// <summary>
		/// The point one unit to the north and one unit to the west of the origin.
		/// </summary>
		public static readonly GridPoint2 NorthWest = North + West;

		/// <summary>
		/// The point one unit to the south and one unit to the west of the origin.
		/// </summary>
		public static readonly GridPoint2 SouthWest = South + West;

		/// <summary>
		/// The point one unit to the south and one unit to the east of the origin.
		/// </summary>
		public static readonly GridPoint2 SouthEast = South + East;

		/// <summary>
		/// A map that represents a horizontal line.
		/// </summary>
		/// <remarks>The next point of the line can be obtained by applying this map to the previous point.</remarks>
		public static readonly IMap<GridPoint2, GridPoint2> HorizontalLine = GridPoint2.GetVectorLine(East);

		/// <summary>
		/// A map that represents a vertical line.
		/// </summary>
		/// <remarks>The next point of the line can be obtained by applying this map to the previous point.</remarks>
		public static readonly IMap<GridPoint2, GridPoint2> VerticalLine = GridPoint2.GetVectorLine(North);

		/// <summary>
		/// A list containing maps that represent orthogonal lines, that is, horizontal lines  and vertical lines.
		/// </summary>
		public static readonly IEnumerable<IMap<GridPoint2, GridPoint2>> OrthogonalLines
			= GridPoint2.GetVectorLines(new List<GridPoint2>() { East, North });

		/// <summary>
		/// A list containing maps that represent diagonal lines.
		/// </summary>
		public static readonly IEnumerable<IMap<GridPoint2, GridPoint2>> DiagonalLines
			= GridPoint2.GetVectorLines(new List<GridPoint2>() { NorthEast, NorthWest });

		/// <summary>
		/// A list of orthogonal directions in a rect grid.
		/// </summary>
		public static readonly StructList<GridPoint2> OrthogonalDirections = new StructList<GridPoint2>
		{
			East,
			North,
			West,
			South
		};

		/// <summary>
		/// A list of diagonal directions in a rect grid.
		/// </summary>
		public static readonly StructList<GridPoint2> DiagonalDirections = new StructList<GridPoint2>
		{
			NorthEast,
			NorthWest,
			SouthWest,
			SouthEast
		};

		/// <summary>
		/// A list of orthogonal and diagonal directions in a rect grid.
		/// </summary>
		public static readonly StructList<GridPoint2> OrthogonalAndDiagonalDirections = new StructList<GridPoint2>
		{
			East,
			NorthEast,
			North,
			NorthWest,
			West,
			SouthWest,
			South,
			SouthEast
		};

		private static readonly GridPoint2[] SpiralIteratorDirections =
		{
			RectPoint.East,
			RectPoint.South,
			RectPoint.West,
			RectPoint.North,
		};

		#endregion

		#region NeighborFunctions 		
		/// <summary>
		/// Gets the orthogonal neighbors of a point.
		/// </summary>
		public static IEnumerable<GridPoint2> GetOrthogonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(OrthogonalDirections);
		}

		/// <summary>
		/// Gets the diagonal neighbors of a point.
		/// </summary>

		public static IEnumerable<GridPoint2> GetDiagonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(DiagonalDirections);
		}

		/// <summary>
		/// Gets the orthogonal and diagonal neighbors of a point.
		/// </summary>

		public static IEnumerable<GridPoint2> GetOrthogonalAndDiagonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(OrthogonalAndDiagonalDirections);
		}
		#endregion

		#region Transforms		
		/// <summary>
		/// Rotates the specified point by 180 degrees around the origin.
		/// </summary>
		public static GridPoint2 Rotate180(GridPoint2 point)
		{
			return new GridPoint2(-point.X, -point.Y);
		}

		/// <summary>
		/// Rotates the specified point counterclockwise by 270 degrees around the origin.
		/// </summary>
		public static GridPoint2 Rotate270(GridPoint2 point)
		{
			return new GridPoint2(point.Y, -point.X);
		}

		/// <summary>
		/// Rotates the specified point about the X axis.
		/// </summary>
		public static GridPoint2 ReflectAboutX(GridPoint2 point)
		{
			return new GridPoint2(point.X, -point.Y);
		}

		/// <summary>
		/// Rotates the specified point about the X axis.
		/// </summary>
		public static GridPoint2 ReflectAboutY(GridPoint2 point)
		{
			return new GridPoint2(-point.X, point.Y);
		}

		/// <summary>
		/// Rotates the specified point counterclockwise by 90 degrees around the origin.
		/// </summary>
		public static GridPoint2 Rotate90(GridPoint2 point)
		{
			return new GridPoint2(-point.Y, point.X);
		}
		#endregion

		#region Norms		
		/// <summary>
		/// Returns the Euclidean norm of a point, defined as the square root 
		/// of the sum of the squares of the coordinates.
		/// </summary>
		public static float EuclideanNorm(GridPoint2 point)
		{
			return Mathf.Sqrt(point.X * point.X + point.Y * point.Y);
		}

		/// <summary>
		/// Returns the Manhattan norm of a point, defined as the sum of the absolute values of the coordinates.
		/// </summary>

		public static int ManhattanNorm(GridPoint2 point)
		{
			return Mathf.Abs(point.X) + Mathf.Abs(point.Y);
		}

		/// <summary>
		/// Returns the Euclidean norm of a point, defined as the maximum of the absolute values of the coordinates.
		/// </summary>

		public static int ChebychevNorm(GridPoint2 point)
		{
			return Mathf.Max(Mathf.Abs(point.X), Mathf.Abs(point.Y));
		}

		/// <summary>
		/// Returns the Euclidean norm of a point, defined as the square root 
		/// of the sum of the squares of the coordinates.
		/// </summary>
		[Version(2, 3, 10)]
		public static float EuclideanNorm(Vector2 point)
		{
			return Mathf.Sqrt(point.x * point.x + point.y * point.y);
		}

		/// <summary>
		/// Returns the Manhattan norm of a point, defined as the sum of the absolute values of the coordinates.
		/// </summary>
		[Version(2, 3, 10)]
		public static float ManhattanNorm(Vector2 point)
		{
			return Mathf.Abs(point.x) + Mathf.Abs(point.y);
		}

		/// <summary>
		/// Returns the Euclidean norm of a point, defined as the maximum of the absolute values of the coordinates.
		/// </summary>
		[Version(2, 3, 10)]
		public static float ChebychevNorm(Vector2 point)
		{
			return Mathf.Max(Mathf.Abs(point.x), Mathf.Abs(point.y));
		}

		/// <summary>
		/// Returns the knight norm of a point, defined by the minimum number of moves a knight chess-piece 
		/// will need to reach it starting at the origin.
		/// </summary>
		public static int KnightNorm(GridPoint2 point)
		{

			int x = Mathf.Abs(point.X);
			int y = Mathf.Abs(point.Y);

			if (y > x)
			{
				x = Mathf.Abs(point.Y);
				y = Mathf.Abs(point.X);
			}

			if (x == 2 && y == 2)
				return 4;

			if (x == 1 && y == 0)
				return 3;

			if (y == 0 || y / (float)x <= 0.5f)
			{
				int initX = 0;
				int xClass = x % 4;

				if (xClass == 0)
					initX = x / 2;
				else if (xClass == 1)
					initX = 1 + (x / 2);
				else if (xClass == 2)
					initX = 1 + (x / 2);
				else
					initX = 1 + ((x + 1) / 2);


				if (xClass > 1)
					return initX - (y % 2);
				else
					return initX + (y % 2);
			}
			else
			{
				int diagonal = x - ((x - y) / 2);

				if ((x - y) % 2 == 0)
				{
					if (diagonal % 3 == 0)
						return (diagonal / 3) * 2;
					if (diagonal % 3 == 1)
						return ((diagonal / 3) * 2) + 2;
					else
						return ((diagonal / 3) * 2) + 2;
				}
				else
				{
					return ((diagonal / 3) * 2) + 1;
				}
			}

		}

		#endregion

		#region Round
		/// <summary>
		/// Rounds a  Vector2 to a grid point by rounding each coordinate.
		/// </summary>
		public static GridPoint2 RoundToGridPoint(Vector2 point)
		{
			return new GridPoint2(
				Mathf.RoundToInt(point.x),
				Mathf.RoundToInt(point.y));
		}

		#endregion

		#region Iterators
		/// <summary>
		/// Get's an enumerable of shape points in a square spiral outwards from a given point, assuming the points
		/// represents coordinates in a rect grid.
		/// </summary>
		/// <param name="shape">The shape to return points from.</param>
		/// <param name="origin">Where the spiral should start.</param>
		/// <param name="ringCount">How many rings there should be in the spiral. The origin is the first ring, so when ringCount is
		/// 1, only the origin will be returned.</param>
		/// <remarks>
		/// <note type="note">
		/// Remember that <see cref="IExplicitShape{TPoint}"/>, <see cref="IGrid{TPoint, TCell}"/> 
		/// and <see cref="Grid2{TCell}"/> all are also of type <see cref="IImplicitShape{TPoint}"/>, 
		/// so this method can be used with explicit shapes and grids too.
		/// </note>
		/// </remarks>
		public static IEnumerable<GridPoint2> GetSpiralIterator(IImplicitShape<GridPoint2> shape, GridPoint2 origin, int ringCount)
		{
			var point = origin;
			yield return point;

			for (var k = 1; k < ringCount; k++)
			{
				point += RectPoint.NorthWest;

				for (var i = 0; i < 4; i++)
				{
					for (var j = 0; j < 2 * k; j++)
					{
						point += SpiralIteratorDirections[i];

						if (shape.Contains(point))
						{
							yield return point;
						}
					}
				}
			}
		}
		#endregion
	}

	/// <summary>
	/// Provides constants and methods for working with points in pointy hex grids.
	/// </summary>
	/// <conceptualLink target="d30f00b0-81c4-4683-b65d-c6a6236d297b"/>
	/// <seealso href="http://www.gamelogic.co.za/downloads/HexMath2.pdf">Hex Geometry for Game Developers</seealso>
	public static class PointyHexPoint
	{
		#region Constants
		/// <summary>
		/// The square root of 3, which is often used in raw math calculations involving hex grids.
		/// </summary>
		public static readonly float Sqrt3 = Mathf.Sqrt(3);

		/// <summary>
		/// The grid point to the east of the origin, that is, the point (1, 0).
		/// </summary>
		public static readonly GridPoint2 East = new GridPoint2(1, 0);

		/// <summary>
		/// The grid point to the north-east of the origin, that is, the point (0, 1).
		/// </summary>
		public static readonly GridPoint2 NorthEast = new GridPoint2(0, 1);

		/// <summary>
		/// The grid point to the north-west of the origin, that is, the point (-1, 1).
		/// </summary>
		public static readonly GridPoint2 NorthWest = new GridPoint2(-1, 1);
		
		/// <summary>
		/// The grid point to the west of the origin, that is, the point (-1, 0).
		/// </summary>
		public static readonly GridPoint2 West = new GridPoint2(-1, 0);
		
		/// <summary>
		/// The grid point to the south-west of the origin, that is, the point (0, -1).
		/// </summary>
		public static readonly GridPoint2 SouthWest = new GridPoint2(0, -1);
		
		/// <summary>
		/// The grid point to the south-east of the origin, that is, the point (1, -1).
		/// </summary>
		public static readonly GridPoint2 SouthEast = new GridPoint2(1, -1);

		/// <summary>
		/// The default space map transform for hex grids. This should be used with a uniform scale
		/// matrix (with the value of the cell width) to get a regular hex grid.
		/// </summary>
		/// <remarks>This is typically useful when constructing mesh cells.</remarks>
		public static readonly Matrixf33 SpaceMapTransform = new Matrixf33(
			1, 0, 0,
			0.5f, 1.5f / Sqrt3, 0,
			0, 0, 1);

		/// <summary>
		/// The raw space map transform for hex grids. This should be used with a scale matrix with
		/// values (width, height, 1) to get cells with the desired with and height.
		/// </summary>
		/// <remarks>This is typically useful when constructing maps for tile grids.</remarks>
		public static readonly Matrixf33 RawSpaceMapTransform = new Matrixf33(
			1, 0, 0,
			0.5f, 0.75f, 0,
			0, 0, 1);

		/// <summary>
		/// The a list of the six cardinal directions of a hex grid, two directions for each axis.
		/// </summary>
		public static readonly IEnumerable<GridPoint2> Directions = new StructList<GridPoint2>
		{
			East,
			NorthEast,
			NorthWest,
			West,
			SouthWest,
			SouthEast
		};

		/// <summary>
		/// A list of the three cardinal lines of a hex grid, that is, the axes.
		/// </summary>
		public static readonly IEnumerable<IMap<GridPoint2, GridPoint2>> OrthogonalLines
			= GridPoint2.GetVectorLines(new List<GridPoint2>() { East, NorthEast, NorthWest });

		//A list of cardinal directions in the right order for the get spiral iterator.
		private static readonly GridPoint2[] SpiralIteratorDirections =
		{
			PointyHexPoint.East,
			PointyHexPoint.SouthEast,
			PointyHexPoint.SouthWest,
			PointyHexPoint.West,
			PointyHexPoint.NorthWest,
			PointyHexPoint.NorthEast
		};
		#endregion

		#region Neighbors
		/// <summary>
		/// Gets the "orthogonal"  neighbors for the given point, that is, the 
		/// six neighbors in the cardinal directions.
		/// </summary>
		/// <remarks>
		/// The term "orthogonal" is used to be consistent with the method in 
		/// RectPoint, and is meant to mean axis-aligned.
		/// </remarks>
		public static IEnumerable<GridPoint2> GetOrthogonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(Directions);
		}

		#endregion

		#region Transforms
		/// <summary>
		/// Rotates the given point by 60 degrees counter clock-wise.
		/// </summary>
		public static GridPoint2 Rotate60(GridPoint2 point)
		{
			return new GridPoint2(-point.Y, point.X + point.Y);
		}

		/// <summary>
		/// Rotates the given point by 120 degrees counter clock-wise.
		/// </summary>
		public static GridPoint2 Rotate120(GridPoint2 point)
		{
			return new GridPoint2(-point.X - point.Y, point.X);
		}

		/// <summary>
		/// Rotates the given point by 180 degrees counter clock-wise.
		/// </summary>
		public static GridPoint2 Rotate180(GridPoint2 point)
		{
			return new GridPoint2(-point.X, -point.Y);
		}

		/// <summary>
		/// Rotates the given point by 240 degrees counter clock-wise.
		/// </summary>
		public static GridPoint2 Rotate240(GridPoint2 point)
		{
			return new GridPoint2(point.Y, -point.X - point.Y);
		}

		/// <summary>
		/// Rotates the given point by 300 degrees counter clock-wise.
		/// </summary>
		public static GridPoint2 Rotate300(GridPoint2 point)
		{
			return new GridPoint2(point.X + point.Y, -point.X);
		}

		/// <summary>
		/// Reflects the given point about the Y-axis.
		/// </summary>
		public static GridPoint2 ReflectAboutY(GridPoint2 point)
		{
			return new GridPoint2(-point.X - point.Y, point.Y);
		}

		/// <summary>
		/// Reflects the given point about the X-axis.
		/// </summary>
		public static GridPoint2 ReflectAboutX(GridPoint2 point)
		{
			return new GridPoint2(point.X + point.Y, -point.Y);
		}

		[Obsolete]
		public static GridPoint2 Rotate60AndReflectAboutY(GridPoint2 point)
		{//Why did we provide these methods?
			return ReflectAboutY(Rotate60(point));
		}

		[Obsolete]
		public static GridPoint2 Rotate120AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate120(point));
		}

		[Obsolete]
		public static GridPoint2 Rotate180AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate180(point));
		}

		[Obsolete]
		public static GridPoint2 Rotate240AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate240(point));
		}

		[Obsolete]
		public static GridPoint2 Rotate300AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate300(point));
		}
		#endregion

		#region Norms		
		/// <summary>
		/// Returns the Euclidean norm of this point, treating it as lying in a (normalized) hex grid.
		/// </summary>
		/// <remarks>Note that this does not give the same result as <see cref="RectPoint.EuclideanNorm(GridPoint2)"/>. For example, 
		/// the point (1, 1) has <c>RectPoint.EuclideanNorm</c> equal to <c>sqrt(2)</c>, but 
		/// the <c>PointyHexPoint.EuclideanNorm</c> is <c>sqrt(3)</c>.</remarks>
		public static float EuclideanNorm(GridPoint2 point)
		{
			int minusZ = point.X + point.Y; //no need to actually make is minus since the result is squared

			return Mathf.Sqrt((point.X * point.X + point.Y * point.Y + minusZ * minusZ)/2f);
		}

		/// <summary>
		/// Returns the Hex-Manhattan norm of a point, defined by how many 
		/// edges you have to cross in a hex grid moving from the origin to the point.
		/// </summary>
		/// <remarks>Equivalent to the <see cref="HexNorm(GridPoint2)"/>.</remarks>
		public static int ManhattanNorm(GridPoint2 point)
		{
			return (Mathf.Abs(point.X) + Mathf.Abs(point.Y) + Mathf.Abs(point.X + point.Y)) / 2;
		}

		/// <summary>
		/// Returns the hex norm of a point, defined by how many 
		/// edges you have to cross in a hex grid moving from the origin to the point.
		/// </summary>
		/// <remarks>Equivalent to the <see cref="ManhattanNorm(GridPoint2)"/>.</remarks>
		public static int HexNorm(GridPoint2 point)
		{
			return Mathf.Max(Mathf.Abs(point.X), Mathf.Abs(point.Y), Mathf.Abs(point.X + point.Y));
		}

		/// <summary>
		/// Returns the up-triangle pseudonorm of the given point. 
		/// </summary>
		/// <remarks>
		/// All the points that satisfy <c>UpTriPseudoNorm(p) &lt; r</c> form a up triangle
		/// in a pointy hex grid.
		/// </remarks>
		public static int UpTriPseudoNorm(GridPoint2 point)
		{
			return Mathf.Max(point.X, point.Y, -point.X - point.Y);
		}

		/// <summary>
		/// Returns the down-triangle pseudonorm of the given point. 
		/// </summary>
		/// <remarks>
		/// All the points that satisfy <c>DownTriPseudoNorm(p) &lt; r</c> form a down triangle
		/// in a pointy hex grid.
		/// </remarks>
		public static int DownTriPseudoNorm(GridPoint2 point)
		{
			return Mathf.Max(-point.X, -point.Y, point.X + point.Y);
		}

		/// <summary>
		/// Returns the star norm of the point.
		/// </summary>
		/// <remarks>
		/// All the points that satisfy <c>DownTriPseudoNorm(p) &lt; r</c> form a six-pointed star
		/// in a pointy hex grid.
		/// </remarks>
		public static int StarNorm(GridPoint2 point)
		{
			return Mathf.Min(UpTriPseudoNorm(point), DownTriPseudoNorm(point));
		}

		/// <summary>
		/// Returns the Euclidean norm of this point, treating it as lying in a (normalized) hex grid.
		/// </summary>
		/// <remarks>Note that this does not give the same result as <see cref="RectPoint.EuclideanNorm(GridPoint2)"/>. For example, 
		/// the point (1, 1) has <c>RectPoint.EuclideanNorm</c> equal to <c>sqrt(2)</c>, but 
		/// the <c>PointyHexPoint.EuclideanNorm</c> is <c>sqrt(3)</c>.</remarks>
		public static float EuclideanNorm(Vector2 point)
		{
			float minusZ = point.x + point.y; //no need to actually make is minus since the result is squared

			return Mathf.Sqrt((point.x * point.x + point.y * point.y + minusZ * minusZ) / 2f);
		}

		/// <summary>
		/// Returns the Hex-Manhattan norm of a point, defined by how many 
		/// edges you have to cross in a hex grid moving from the origin to the point.
		/// </summary>
		/// <remarks>Equivalent to the <see cref="HexNorm(Vector2)"/>.</remarks>
		public static float ManhattanNorm(Vector2 point)
		{
			return (Mathf.Abs(point.x) + Mathf.Abs(point.y) + Mathf.Abs(point.x + point.y)) / 2;
		}

		/// <summary>
		/// Returns the hex norm of a point, defined by how many 
		/// edges you have to cross in a hex grid moving from the origin to the point.
		/// </summary>
		/// <remarks>Equivalent to the <see cref="ManhattanNorm(GridPoint2)"/>.</remarks>
		public static float HexNorm(Vector2 point)
		{
			return Mathf.Max(Mathf.Abs(point.x), Mathf.Abs(point.y), Mathf.Abs(point.x + point.y));
		}

		/// <summary>
		/// Returns the up-triangle pseudonorm of the given point. 
		/// </summary>
		/// <remarks>
		/// All the points that satisfy <c>UpTriPseudoNorm(p) &lt; r</c> form a up triangle
		/// in a pointy hex grid.
		/// </remarks>
		public static float UpTriPseudoNorm(Vector2 point)
		{
			return Mathf.Max(point.x, point.y, -point.x - point.y);
		}

		/// <summary>
		/// Returns the down-triangle pseudonorm of the given point. 
		/// </summary>
		/// <remarks>
		/// All the points that satisfy <c>DownTriPseudoNorm(p) &lt; r</c> form a down triangle
		/// in a pointy hex grid.
		/// </remarks>
		public static float DownTriPseudoNorm(Vector2 point)
		{
			return Mathf.Max(-point.x, -point.y, point.x + point.y);
		}

		/// <summary>
		/// Returns the star norm of the point.
		/// </summary>
		/// <remarks>
		/// All the points that satisfy <c>DownTriPseudoNorm(p) &lt; r</c> form a six-pointed star
		/// in a pointy hex grid.
		/// </remarks>
		public static float StarNorm(Vector2 point)
		{
			return Mathf.Min(UpTriPseudoNorm(point), DownTriPseudoNorm(point));
		}
		#endregion

		#region Round
		/// <summary>
		/// Rounds a vector to the closest grid point if the points represent a hex grid.
		/// </summary>
		public static GridPoint2 RoundToGridPoint(Vector2 vec)
		{
			var rx = Mathf.RoundToInt(vec.x);
			var ry = Mathf.RoundToInt(vec.y);
			var z = -vec.x - vec.y;
			var rz = Mathf.RoundToInt(z);

			var xDelta = Mathf.Abs(rx - vec.x);
			var yDelta = Mathf.Abs(ry - vec.y);
			var zDelta = Mathf.Abs(rz - z);

			if (xDelta > yDelta && xDelta > zDelta)
			{
				rx = -ry - rz;
			}
			else if (yDelta > zDelta)
			{
				ry = -rx - rz;
			}

			return new GridPoint2(rx, ry);
		}

		#endregion

		#region Iterators
		/// <summary>
		/// Get's an enumerable of shape points in a spiral outwards from a given point, assuming the points are coordinates of 
		/// a hex grid.
		/// </summary>
		/// <param name="shape">The shape to return points from.</param>
		/// <param name="origin">Where the spiral should start.</param>
		/// <param name="ringCount">How many rings there should be in the spiral. The origin is the first ring, so when ringCount is
		/// 1, only the origin will be returned.</param>
		/// <remarks>
		/// <note type="note">
		/// Remember that <see cref="IExplicitShape{TPoint}"/>, <see cref="IGrid{TPoint, TCell}"/> 
		/// and <see cref="Grid2{TCell}"/> all are also of type <see cref="IImplicitShape{TPoint}"/>, 
		/// so this method can be used with explicit shapes and grids too.
		/// </note>
		/// <note type="tip">You can transform the grid shape to get transformed spirals, such 
		/// as spirals going in the opposite direction, using <see cref="ImplicitShape.ReverseSelect{TPoint}(IImplicitShape{TPoint}, Func{TPoint, TPoint})"/>. For example:
		/// <code>
		/// <![CDATA[
		/// var reverseSpiral = PointyHexPoint
		///		.GetSpiralIterator(
		///			grid.ReverseSelect(p => PointyHexPoint.ReflectAboutX(p)),
		///			center, 
		///			radius);
		/// ]]>
		/// </code>
		/// </note>
		/// </remarks>
		//TODO: make sure this example works as expected!g
		public static IEnumerable<GridPoint2> GetSpiralIterator(IImplicitShape<GridPoint2> shape, GridPoint2 origin, int ringCount)
		{
			var point = origin;
			yield return point;

			for (var k = 1; k < ringCount; k++)
			{
				point += NorthWest;

				for (var i = 0; i < 6; i++)
				{
					for (var j = 0; j < k; j++)
					{
						point += SpiralIteratorDirections[i];

						if (shape.Contains(point))
						{
							yield return point;
						}
					}
				}
			}
		}
		#endregion

	}

	/// <summary>
	/// Provides constants and methods for working with points in block grids.
	/// </summary>
	public static class BlockPoint
	{
		#region Constant
		/// <summary>
		/// The point one unit to the north of the origin.
		/// </summary>
		public static readonly GridPoint3 North = new GridPoint3(0, 0, 1); //TODO: use unity directions instead?

		/// <summary>
		/// The point one unit to the east of the origin.
		/// </summary>
		public static readonly GridPoint3 East = new GridPoint3(1, 0, 0);
		
		/// <summary>
		/// The point one unit to the south of the origin.
		/// </summary>
		public static readonly GridPoint3 South = new GridPoint3(0, 0, -1);
		
		/// <summary>
		/// The point one unit to the west of the origin.
		/// </summary>
		public static readonly GridPoint3 West = new GridPoint3(-1, 0, 0);
		
		/// <summary>
		/// The point one unit up from the origin.
		/// </summary>
		public static readonly GridPoint3 Up = new GridPoint3(0, 1, 0);
		
		/// <summary>
		/// The point one unit down from the origin.
		/// </summary>
		public static readonly GridPoint3 Down = new GridPoint3(0, -1, 0);

		/// <summary>
		/// The orthogonal directions in 3D.
		/// </summary>
		public static readonly StructList<GridPoint3> OrthogonalDirections = new StructList<GridPoint3>
		{
			East,
			North,
			Up,
			West,
			South,
			Down
		};

		#endregion

		#region Norms		
		/// <summary>
		/// Returns the Euclidean norm of a bock point, defined as
		/// the square root of the sum of the squares of the coordinates.
		/// </summary>
		public static float EuclideanNorm(GridPoint3 point)
		{
			var squareMagnitude =
				point.X * point.X +
				point.Y * point.Y +
				point.Z * point.Z;

			return Mathf.Sqrt(squareMagnitude);
		}

		/// <summary>
		/// Returns the Manhattan norm of a point, defined as the sum of the absolute values of the coordinates.
		/// </summary>
		public static int ManhattanNorm(GridPoint3 point)
		{
			return Mathf.Abs(point.X) + Mathf.Abs(point.Y) + Mathf.Abs(point.Z);
		}

		/// <summary>
		/// Returns the Euclidean norm of a point, defined as the maximum of the absolute values of the coordinates.
		/// </summary>
		public static int ChebychevNorm(GridPoint3 point)
		{
			return Mathf.Max(Mathf.Abs(point.X), Mathf.Abs(point.Y), Mathf.Abs(point.Z));
		}

		#endregion

		#region Round
		/// <summary>
		/// Rounds a  Vector3 to a grid point by rounding each coordinate.
		/// </summary>
		public static GridPoint3 RoundToGridPoint(Vector3 point)
		{
			return new GridPoint3(
				Mathf.RoundToInt(point.x),
				Mathf.RoundToInt(point.y),
				Mathf.RoundToInt(point.z));
		}

		#endregion
	}

	/// <summary>
	/// Class with methods for working with hex block points. 
	/// </summary>
	/// <remarks>Hex block points are 
	/// layers of 2D hexagonal lattices stacked (and aligned) vertically.</remarks>
	[Version(2, 2)]
	public static class HexBlockPoint
	{
		#region Constants
		/// <summary>
		/// The grid point to the east of the origin, that is, the point (1, 0, 0).
		/// </summary>
		public static readonly GridPoint3 East = new GridPoint3(1, 0, 0);
		/// <summary>
		/// The grid point to the north-east of the origin, that is, the point (0, 0, 1).
		/// </summary>
		public static readonly GridPoint3 NorthEast = new GridPoint3(0, 0, 1);
		/// <summary>
		/// The grid point to the north-west of the origin, that is, the point (-1, 0, 1).
		/// </summary>
		public static readonly GridPoint3 NorthWest = new GridPoint3(-1, 0, 1);
		/// <summary>
		/// The grid point to the west of the origin, that is, the point (-1, 0, 0).
		/// </summary>
		public static readonly GridPoint3 West = new GridPoint3(-1, 0, 0);
		/// <summary>
		/// The grid point to the south-west of the origin, that is, the point (0, 0, -1).
		/// </summary>
		public static readonly GridPoint3 SouthWest = new GridPoint3(0, 0, -1);
		/// <summary>
		/// The grid point to the south-east of the origin, that is, the point (1, 0, -1).
		/// </summary>
		public static readonly GridPoint3 SouthEast = new GridPoint3(1, 0, -1);

		/// <summary>
		/// The grid point one unit up from the origin, that is, the point (0, 1, 0).
		/// </summary>
		public static readonly GridPoint3 Up = new GridPoint3(0, 1, 0);
		/// <summary>
		/// The grid point one unit down from the origin, that is, the point (0, -1, 0).
		/// </summary>
		public static readonly GridPoint3 Down = new GridPoint3(0, -1, 0);

		/// <summary>
		/// The a list of the eight cardinal directions of a hex-block grid, two directions for each axis.
		/// </summary>
		public static readonly IEnumerable<GridPoint3> Directions = new StructList<GridPoint3>
		{
			East,
			NorthEast,
			NorthWest,
			West,
			SouthWest,
			SouthEast,
			Up,
			Down
		};

		/// <summary>
		/// A list of the three cardinal lines of a hex-block grid, that is, the axes.
		/// </summary>
		public static readonly IEnumerable<IMap<GridPoint3, GridPoint3>> OrthogonalLines
			= GridPoint3.GetVectorLines(new List<GridPoint3>() { East, NorthEast, NorthWest, Up });

		#endregion

		#region Neighbors
		/// <summary>
		/// Gets the "orthogonal"  neighbors for the given point, that is, the 
		/// eight neighbors in the cardinal directions.
		/// </summary>
		/// <remarks>
		/// The term "orthogonal" is used to be consistent with the method in 
		/// RectPoint, and is meant to mean axis-aligned.
		/// </remarks>
		public static IEnumerable<GridPoint3> GetOrthogonalNeighbors(GridPoint3 point)
		{
			return point.GetVectorNeighbors(Directions);
		}

		#endregion

		#region Round
		/// <summary>
		/// Rounds a vector to the closest grid point if the points represent a hex-block grid.
		/// </summary>
		public static GridPoint3 RoundToGridPoint(Vector3 vec)
		{
			var vec2D = vec.To2DXZ();
			var point2D = PointyHexPoint.RoundToGridPoint(vec2D);
			var y = Mathf.RoundToInt(vec.y);

			return point2D.To3DXZ(y);
		}

		#endregion
	}
}