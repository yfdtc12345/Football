using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Internal;
using UnityEngine;
using Gamelogic.Extensions;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Class for creating and manipulating explicit shapes.
	/// </summary>
	public static class ExplicitShape
	{
		#region Public Static Types
		/// <summary>
		/// Class with methods for hex grid shapes.
		/// </summary>
		[Experimental]
		public static class Hex
		{
			/// <summary>
			/// Returns a new hexagon shape.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> Hexagon(int radius)
			{
				return ImplicitShape
				.Hexagon(radius)
				.ToExplicit(new GridRect(
					new GridPoint2(1 - radius, 1 - radius),
					new GridPoint2(2 * radius - 1, 2 * radius - 1)));
			}

			/// <summary>
			/// Returns a new thin rectangle suitable for a flat hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> FlatThinRectangle(GridPoint2 size)
			{
				var storageWidth = size.X;
				var storageHeight = size.Y + GLMathf.FloorDiv(size.X - 1, 2);
				var storageBottomLeft = new GridPoint2(0, -GLMathf.FloorDiv(size.X - 1, 2));

				return
					ImplicitShape.FlatHexThinRectangle(size)
						.ToExplicit(new GridRect(storageBottomLeft, new GridPoint2(storageWidth, storageHeight)));
			}

			/// <summary>
			/// Returns a new fat rectangle suitable for a flat hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> FlatFatRectangle(GridPoint2 size)
			{
				var storageWidth = size.X;
				var storageHeight = size.Y + GLMathf.FloorDiv(size.X, 2);
				var storageBottomLeft = new GridPoint2(0, -GLMathf.FloorDiv(size.X, 2));

				return
					ImplicitShape.FlatHexFatRectangle(size)
						.ToExplicit(new GridRect(storageBottomLeft, new GridPoint2(storageWidth, storageHeight)));
			}

			/// <summary>
			/// Returns a new rectangle suitable for a flat hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> FlatRectangle(GridPoint2 size)
			{
				var storageWidth = size.X;
				var storageHeight = size.Y + GLMathf.FloorDiv(size.X - 1, 2);
				var storageBottomLeft = new GridPoint2(0, -GLMathf.FloorDiv(size.X - 1, 2));

				return
					ImplicitShape.FlatHexRectangle(size)
						.ToExplicit(new GridRect(storageBottomLeft, new GridPoint2(storageWidth, storageHeight)));
			}

			/// <summary>
			/// Returns a new fat rectangle suitable for a pointy hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> PointyFatRectangle(GridPoint2 size)
			{
				int storageWidth = size.X + GLMathf.FloorDiv(size.Y, 2);
				int storageHeight = size.Y;
				var storagePoint = new GridPoint2(-GLMathf.FloorDiv(size.Y, 2), 0);

				return ImplicitShape.PointyHexFatRectangle(size).ToExplicit(new GridRect(storagePoint, new GridPoint2(storageWidth, storageHeight)));
			}

			/// <summary>
			/// Returns a new rectangle suitable for a pointy hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> PointyRectangle(GridPoint2 size)
			{
				int storageWidth = size.X + GLMathf.FloorDiv(size.Y - 1, 2);
				int storageHeight = size.Y;
				var storagePoint = new GridPoint2(-GLMathf.FloorDiv(size.Y - 1, 2), 0);

				return ImplicitShape.PointyHexRectangle(size).ToExplicit(new GridRect(storagePoint, new GridPoint2(storageWidth, storageHeight)));
			}

			/// <summary>
			/// Returns a new thin rectangle suitable for a pointy hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> PointyThinRectangle(GridPoint2 size)
			{
				int storageWidth = size.X + GLMathf.FloorDiv(size.Y - 1, 2);
				int storageHeight = size.Y;
				var storagePoint = new GridPoint2(-GLMathf.FloorDiv(size.Y - 1, 2), 0);

				return ImplicitShape.PointyHexThinRectangle(size).ToExplicit(new GridRect(storagePoint, new GridPoint2(storageWidth, storageHeight)));
			}

			/// <summary>
			/// Returns a new down triangle suitable for a pointy hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> DownTriangle(Vector2 center, float radius)
			{
				int iRadius = Mathf.CeilToInt(radius);
				int x = Mathf.RoundToInt(center.x);
				int y = Mathf.RoundToInt(center.y);

				return ImplicitShape
					.DownTriangle(center, radius)
					.ToExplicit(new GridRect(
						new GridPoint2(2 - 2 * iRadius + x, 2 - 2 * iRadius + y),
						new GridPoint2(3 * iRadius - 2, 3 * iRadius - 2)));
			}

			/// <summary>
			/// Returns a new up triangle suitable for a pointy hex grid.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> UpTriangle(Vector2 center, float radius)
			{
				int iRadius = Mathf.CeilToInt(radius);
				int x = Mathf.RoundToInt(center.x);
				int y = Mathf.RoundToInt(center.y);

				return ImplicitShape
					.UpTriangle(center, radius)
					.ToExplicit(new GridRect(
						new GridPoint2(1 - iRadius + x, 1 - iRadius + y),
						new GridPoint2(3 * iRadius - 2, 3 * iRadius - 2)));
			}

			/// <summary>
			/// Returns a new hexagon shape.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> Hexagon(Vector2 center, float radius)
			{
				int iRadius = Mathf.CeilToInt(radius);
				int x = Mathf.RoundToInt(center.x);
				int y = Mathf.RoundToInt(center.y);

				return ImplicitShape
					.Hexagon(center, radius)
					.ToExplicit(new GridRect(
						new GridPoint2(1 - iRadius + x, 1 - iRadius + y),
						new GridPoint2(2 * iRadius - 1, 2 * iRadius - 1)));
			}

			/// <summary>
			/// Returns a new star shape.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> Star(Vector2 center, float radius)
			{
				int iRadius = Mathf.CeilToInt(radius);
				int x = Mathf.RoundToInt(center.x);
				int y = Mathf.RoundToInt(center.y);

				return ImplicitShape
					.Star(center, radius)
					.ToExplicit(new GridRect(
						new GridPoint2(-2 * iRadius + x, -2 * iRadius + y),
						new GridPoint2(4 * iRadius + 1, 4 * iRadius + 1)));
			}
		}

		/// <summary>
		/// Class with methods for rect grid shapes.
		/// </summary>
		[Experimental]
		public static class Rect
		{
			/// <summary>
			/// Returns a new diamond shape.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> Diamond(Vector2 center, float radius)
			{
				int iRadius = Mathf.CeilToInt(radius);
				int x = Mathf.RoundToInt(center.x);
				int y = Mathf.RoundToInt(center.y);

				return ImplicitShape
					.RectDiamond(center, radius)
					.ToExplicit(new GridRect(
						new GridPoint2(1 - iRadius + x, 1 - iRadius + y),
						new GridPoint2(2 * iRadius - 1, 2 * iRadius - 1)));
			}

			/// <summary>
			/// Returns a new square shape.
			/// </summary>
			[Experimental]
			public static IExplicitShape<GridPoint2> Square(Vector2 center, float radius)
			{
				int iRadius = Mathf.CeilToInt(radius);
				int x = Mathf.RoundToInt(center.x);
				int y = Mathf.RoundToInt(center.y);

				return ImplicitShape
					.Square(center, radius)
					.ToExplicit(new GridRect(
						new GridPoint2(1 - iRadius + x, 1 - iRadius + y),
						new GridPoint2(2 * iRadius - 1, 2 * iRadius - 1)));
			}
		}
		#endregion

		#region Public Types

		/// <summary>
		/// Concrete implementation of a 1D ExplicitShape.
		/// </summary>
		/// <inheritdoc/>
		[Serializable]
		public sealed class ExplicitShape1 : IExplicitShape<int>
		{
			/// <summary>
			/// A big interval centered at the origin.
			/// </summary>
			/// <remarks>
			/// It is sometimes useful to use as a temporary, big, bounding box
			/// while debugging a shape class, especially when the bounds are calculated
			/// from parameters.
			/// </remarks>
			public static readonly GridInterval BigInterval = new GridInterval(-20, 2 * 20);

			[SerializeField]
			private readonly IImplicitShape<int> implicitShape;

			[SerializeField]
			private readonly AbstractBounds<int> storageBounds;

			/// <inheritdoc/>
			public AbstractBounds<int> Bounds
			{
				get { return storageBounds; }
			}

			/// <inheritdoc/>
			public IEnumerable<int> Points
			{
				get { return Bounds.Points.Where(Contains); }
			}

			/// <summary>
			/// Constructs a new explicit shape from the given implicit shape 
			/// and bounds.
			/// </summary>
			public ExplicitShape1(
				IImplicitShape<int> implicitShape, 
				AbstractBounds<int> storageBounds)
			{
				this.implicitShape = implicitShape;
				this.storageBounds = storageBounds;
			}

			/// <inheritdoc/>
			public ExplicitShape1 Union(ExplicitShape1 shape)
			{
				var newRect = GridInterval.UnionBoundingBox(storageBounds, shape.storageBounds);
				var newShape = ImplicitShape.Union(implicitShape, shape.implicitShape);

				return new ExplicitShape1(newShape, newRect);
			}

			/// <inheritdoc/>
			public ExplicitShape1 Intersection(ExplicitShape1 shape)
			{
				var newRect = GridInterval.Intersection(storageBounds, shape.storageBounds);
				var newShape = ImplicitShape.Intersection(implicitShape, shape.implicitShape);

				return new ExplicitShape1(newShape, newRect);
			}

			/// <inheritdoc/>
			public ExplicitShape1 Translate(int offset)
			{
				var map = Map.Translate(offset);
				var newRect = GridInterval.Translate(storageBounds, offset);
				var newShape = ImplicitShape.Transform(implicitShape, map);

				return new ExplicitShape1(newShape, newRect);
			}

			/// <inheritdoc/>
			public ExplicitShape1 InverseInRect()
			{
				var newShape = ImplicitShape.Inverse(implicitShape);

				return new ExplicitShape1(newShape, storageBounds);
			}

			/// <inheritdoc/>
			public bool Contains(int point)
			{
				return implicitShape.Contains(point);
			}
		}

		/// <summary>
		/// Concrete implementation of a 2D ExplicitShape.
		/// </summary>
		/// <inheritdoc/>
		[Serializable]
		public sealed class ExplicitShape2 : IExplicitShape<GridPoint2>
		{
			/// <summary>
			/// A big rectangle centered at the origin.
			/// </summary>
			/// <inheritdoc cref="ExplicitShape1.BigInterval"/>
			public static readonly GridRect BigRect = new GridRect(GridPoint2.One * -20, GridPoint2.One * 2 * 20);

			[SerializeField]
			private readonly IImplicitShape<GridPoint2> implicitShape;

			[SerializeField]
			private readonly AbstractBounds<GridPoint2> storageBounds;

			/// <summary>
			/// Holds an empty shape.
			/// </summary>
			[Obsolete("Use the static method ExplicitShape.Empty2() instead")]
			public static readonly ExplicitShape2 Empty = new ExplicitShape2(
				ImplicitShape.Func<GridPoint2>(p => false),
				new GridRect(GridPoint2.Zero, GridPoint2.Zero));

			/// <inheritdoc/>
			public AbstractBounds<GridPoint2> Bounds
			{
				get { return storageBounds; }
			}

			/// <inheritdoc/>
			public IEnumerable<GridPoint2> Points
			{
				get { return Bounds.Points.Where(Contains); }
			}

			/// <summary>
			/// Constructs a new explicit shape from the given implicit shape 
			/// and bounds.
			/// </summary>
			public ExplicitShape2(IImplicitShape<GridPoint2> implicitShape, AbstractBounds<GridPoint2> storageBounds)
			{
				this.implicitShape = implicitShape;
				this.storageBounds = storageBounds;
			}

			/// <inheritdoc/>
			public ExplicitShape2 Union(ExplicitShape2 shape2)
			{
				var newRect = GridRect.UnionBoundingBox(storageBounds, shape2.storageBounds);
				var newShape = ImplicitShape.Union(implicitShape, shape2.implicitShape);

				return new ExplicitShape2(newShape, newRect);
			}

			/// <inheritdoc/>
			public ExplicitShape2 Intersection(ExplicitShape2 shape2)
			{
				var newRect = GridRect.Intersection(storageBounds, shape2.storageBounds);
				var newShape = ImplicitShape.Intersection(implicitShape, shape2.implicitShape);

				return new ExplicitShape2(newShape, newRect);
			}

			/// <inheritdoc/>
			public ExplicitShape2 Translate(GridPoint2 offset)
			{
				var map = Map.Translate(offset);

				var newRect = GridRect.Translate(storageBounds, offset);
				var newShape = ImplicitShape.Transform(implicitShape, map);

				return new ExplicitShape2(newShape, newRect);
			}

			/// <inheritdoc/>
			public ExplicitShape2 InverseInRect()
			{
				var newShape = ImplicitShape.Inverse(implicitShape);

				return new ExplicitShape2(newShape, storageBounds);
			}

			/// <inheritdoc/>
			public bool Contains(GridPoint2 point)
			{
				return implicitShape.Contains(point);
			}
		}

		/// <summary>
		/// Concrete implementation of a 3D ExplicitShape.
		/// </summary>
		/// <inheritdoc/>
		[Serializable]
		public sealed class ExplicitShape3 : IExplicitShape<GridPoint3>
		{
			/// <summary>
			/// A big bounds centered at the origin.
			/// </summary>
			/// <inheritdoc cref="ExplicitShape1.BigInterval"/>
			public static readonly GridBounds BigBounds = new GridBounds(GridPoint3.One * -20, GridPoint3.One * 2 * 20);

			[SerializeField]
			private readonly IImplicitShape<GridPoint3> implicitShape;

			[SerializeField]
			private readonly AbstractBounds<GridPoint3> storageBounds;

			public static ExplicitShape3 empty = new ExplicitShape3(
				ImplicitShape.Func<GridPoint3>(p => false),
				new GridBounds(GridPoint3.Zero, GridPoint3.Zero));


			/// <inheritdoc />
			public AbstractBounds<GridPoint3> Bounds
			{
				get { return storageBounds; }
			}

			/// <inheritdoc />
			public IEnumerable<GridPoint3> Points
			{
				get { return Bounds.Points.Where(Contains); }
			}

			/// <summary>
			/// Constructs a new explicit shape from the given implicit shape 
			/// and bounds.
			/// </summary>
			public ExplicitShape3(IImplicitShape<GridPoint3> implicitShape, AbstractBounds<GridPoint3> storageBounds)
			{
				this.implicitShape = implicitShape;
				this.storageBounds = storageBounds;
			}

			/// <inheritdoc />
			public ExplicitShape3 Union(ExplicitShape3 shape2)
			{
				var newRect = GridBounds.UnionBoundingBox(storageBounds, shape2.storageBounds);
				var newShape = ImplicitShape.Union(implicitShape, shape2.implicitShape);

				return new ExplicitShape3(newShape, newRect);
			}

			/// <inheritdoc />
			public ExplicitShape3 Intersection(ExplicitShape3 shape2)
			{
				var newRect = GridBounds.Intersection(storageBounds, shape2.storageBounds);
				var newShape = ImplicitShape.Intersection(implicitShape, shape2.implicitShape);

				return new ExplicitShape3(newShape, newRect);
			}

			/// <inheritdoc />
			public ExplicitShape3 Translate(GridPoint3 offset)
			{
				var map = Map.Translate(offset);

				var newRect = GridBounds.Translate(storageBounds, offset);
				var newShape = ImplicitShape.Transform(implicitShape, map);

				return new ExplicitShape3(newShape, newRect);
			}

			/// <inheritdoc />
			public ExplicitShape3 InverseInRect()
			{
				var newShape = ImplicitShape.Inverse(implicitShape);

				return new ExplicitShape3(newShape, storageBounds);
			}

			/// <inheritdoc />
			public bool Contains(GridPoint3 point)
			{
				return implicitShape.Contains(point);
			}
		}

		#endregion

		#region Private Types
		private abstract class EmptyShape<T> : IExplicitShape<T>
		{
			public bool Contains(T point)
			{
				return false;
			}

			public IEnumerable<T> Points
			{
				get { yield break; }
			}

			public abstract AbstractBounds<T> Bounds { get; }
		}

		private sealed class EmptyShape1 : EmptyShape<int>
		{
			public override AbstractBounds<int> Bounds
			{
				get { return new GridInterval(0, 0); }
			}
		}

		private sealed class EmptyShape2 : EmptyShape<GridPoint2>
		{
			public override AbstractBounds<GridPoint2> Bounds
			{
				get { return new GridRect(GridPoint2.Zero, GridPoint2.Zero); }
			}
		}

		private sealed class EmptyShape3 : EmptyShape<GridPoint3>
		{
			public override AbstractBounds<GridPoint3> Bounds
			{
				get { return new GridBounds(GridPoint3.Zero, GridPoint3.Zero); }
			}
		}

		private sealed class BitmaskShape1 : IExplicitShape<int>
		{
			private readonly Grid1<bool> grid;

			public BitmaskShape1(bool[] shape)
			{
				var bounds = new GridInterval(0, shape.Length);
				grid = new Grid1<bool>(bounds);

				foreach (var point in grid.Points)
				{
					grid[point] = shape[point];
				}
			}

			public BitmaskShape1(int[] shape)
			{
				var bounds = new GridInterval(0, shape.Length);
				grid = new Grid1<bool>(bounds);

				foreach (var point in grid.Points)
				{
					grid[point] = shape[point] != 0;
				}
			}

			public BitmaskShape1(string shape)
			{
				var bounds = new GridInterval(0, shape.Length);
				grid = new Grid1<bool>(bounds);

				foreach (var point in grid.Points)
				{
					grid[point] = shape[point] != '0';
				}
			}

			public bool Contains(int point)
			{
				return grid.Contains(point) && grid[point];
			}

			public IEnumerable<int> Points
			{
				get { return grid.Points.Where(p => grid[p]); }
			}

			public AbstractBounds<int> Bounds
			{
				get { return grid.Bounds; }
			}
		}

		private sealed class BitmaskShape2 : IExplicitShape<GridPoint2>
		{
			private readonly Grid2<bool> grid;

			public BitmaskShape2(bool[][] shape)
			{
				int max = shape.Max(row => row.Length);
				var bounds = new GridRect(GridPoint2.Zero, new GridPoint2(max, shape.Length));

				grid = new Grid2<bool>(bounds);

				foreach (var point in grid.Points)
				{
					if (point.X < shape[point.Y].Length)
					{
						grid[point] = shape[point.Y][point.X];
					}
					else
					{
						grid[point] = false;
					}
				}
			}

			public BitmaskShape2(int[][] shape)
			{
				int max = shape.Max(row => row.Length);
				var bounds = new GridRect(GridPoint2.Zero, new GridPoint2(max, shape.Length));

				grid = new Grid2<bool>(bounds);

				foreach (var point in grid.Points)
				{
					if (point.X < shape[point.Y].Length)
					{
						grid[point] = shape[point.Y][point.X] != 0;
					}
					else
					{
						grid[point] = false;
					}
				}
			}

			public BitmaskShape2(string[] shape)
			{
				int max = shape.Max(row => row.Length);
				var bounds = new GridRect(GridPoint2.Zero, new GridPoint2(max, shape.Length));

				grid = new Grid2<bool>(bounds);

				foreach (var point in grid.Points)
				{
					if (point.X < shape[point.Y].Length)
					{
						grid[point] = shape[point.Y][point.X] != '0';
					}
					else
					{
						grid[point] = false;
					}
				}
			}

			public bool Contains(GridPoint2 point)
			{
				return grid.Contains(point) && grid[point];
			}

			public IEnumerable<GridPoint2> Points
			{
				get { return grid.Points.Where(p => grid[p]); }
			}

			public AbstractBounds<GridPoint2> Bounds
			{
				get { return grid.Bounds; }
			}
		}

		private sealed class BitmaskShape3 : IExplicitShape<GridPoint3>
		{
			private readonly Grid3<bool> grid;

			public BitmaskShape3(bool[][][] shape)
			{
				int columnMax = shape.Max(row => row.Length);
				int rowMax = shape.SelectMany(c => c).Max(c => c.Length);

				var bounds = new GridBounds(GridPoint3.Zero, new GridPoint3(rowMax, columnMax, shape.Length));

				grid = new Grid3<bool>(bounds);

				foreach (var point in grid.Points)
				{
					if (point.Y < shape[point.Z].Length)
					{
						if (point.X < shape[point.Z][point.Y].Length)
						{
							grid[point] = shape[point.Z][point.Y][point.X];
						}
						else
						{
							grid[point] = false;
						}
					}
					else
					{
						grid[point] = false;
					}
				}
			}

			public BitmaskShape3(int[][][] shape)
			{
				int columnMax = shape.Max(row => row.Length);
				int rowMax = shape.SelectMany(c => c).Max(c => c.Length);

				var bounds = new GridBounds(GridPoint3.Zero, new GridPoint3(rowMax, columnMax, shape.Length));

				grid = new Grid3<bool>(bounds);

				foreach (var point in grid.Points)
				{
					if (point.Y < shape[point.Z].Length)
					{
						if (point.X < shape[point.Z][point.Y].Length)
						{
							grid[point] = shape[point.Z][point.Y][point.X] != 0;
						}
						else
						{
							grid[point] = false;
						}
					}
					else
					{
						grid[point] = false;
					}
				}
			}

			public BitmaskShape3(string[][] shape)
			{
				int columnMax = shape.Max(row => row.Length);
				int rowMax = shape.SelectMany(c => c).Max(c => c.Length);

				var bounds = new GridBounds(GridPoint3.Zero, new GridPoint3(rowMax, columnMax, shape.Length));

				grid = new Grid3<bool>(bounds);

				foreach (var point in grid.Points)
				{
					if (point.Y < shape[point.Z].Length)
					{
						if (point.X < shape[point.Z][point.Y].Length)
						{
							grid[point] = shape[point.Z][point.Y][point.X] != '0';
						}
						else
						{
							grid[point] = false;
						}
					}
					else
					{
						grid[point] = false;
					}
				}
			}

			public bool Contains(GridPoint3 point)
			{
				return grid.Contains(point) && grid[point];
			}

			public IEnumerable<GridPoint3> Points
			{
				get { return grid.Points.Where(p => grid[p]); }
			}

			public AbstractBounds<GridPoint3> Bounds
			{
				get { return grid.Bounds; }
			}
		}

		private sealed class ListShape<T> : IExplicitShape<T>
		{
			private readonly List<T> listSet;
			private readonly AbstractBounds<T> bounds;

			public ListShape(IEnumerable<T> points, AbstractBounds<T> bounds)
			{
				listSet = new List<T>(points.Distinct());
				this.bounds = bounds;
			}

			public bool Contains(T point)
			{
				return bounds.Contains(point) && listSet.Contains(point);
			}

			public IEnumerable<T> Points
			{
				get { return listSet.ToList(); }
			}

			public AbstractBounds<T> Bounds
			{
				get { return bounds; }
			}
		}

		private sealed class HashShape<T> : IExplicitShape<T>
		{
			private readonly HashSet<T> hashSet;
			private readonly AbstractBounds<T> bounds;

			public HashShape(IEnumerable<T> points, AbstractBounds<T> bounds)
			{
				hashSet = new HashSet<T>(points);
				this.bounds = bounds;
			}

			public bool Contains(T point)
			{
				return bounds.Contains(point) && hashSet.Contains(point);
			}

			public IEnumerable<T> Points
			{
				get { return hashSet.ToList(); }
			}

			public AbstractBounds<T> Bounds
			{
				get { return bounds; }
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Creates a new explicit shape from the implicit shape that falls
		/// inside the bounds.
		/// </summary>
		public static IExplicitShape<int> ToExplicit(
			this IImplicitShape<int> shape, 
			AbstractBounds<int> bounds)
		{
			return new ExplicitShape1(shape, bounds);
		}

		/// <summary>
		/// Returns a new shape that only contains points in this shape that satisfies the predicate.
		/// </summary>
		/// <param name="shape">The shape to select points for the new shape from.</param>
		/// <param name="predicate">The predicate that all points in the new shape should satisfy.</param>
		/// <returns>A new explicit shape that contains only points from the original shape
		/// that satisfies the predicate.</returns>
		[Obsolete("Use Where instead. This function was misnamed.")]
		public static IExplicitShape<int> Select(this IExplicitShape<int> shape, Func<int, bool> predicate)
		{
			return ImplicitShape
				.Func<int>(x => shape.Contains(x) && predicate(x))
				.ToExplicit(shape.Bounds);
		}

		/// <summary>
		/// Returns a new shape that only contains points in this shape that satisfies the predicate.
		/// </summary>
		/// <param name="shape">The shape to select points for the new shape from.</param>
		/// <param name="predicate">The predicate that all points in the new shape should satisfy.</param>
		/// <returns>A new explicit shape that contains only points from the original shape
		/// that satisfies the predicate.</returns>
		[Version(2, 3)]
		public static IExplicitShape<int> Where(this IExplicitShape<int> shape, Func<int, bool> predicate)
		{

			return ImplicitShape
				.Func<int>(x => shape.Contains(x) && predicate(x))
				.ToExplicit(shape.Bounds);
		}

		/// <summary>
		/// Returns a new shape that only contains points in this shape that satisfies the predicate.
		/// </summary>
		/// <param name="shape">The shape to select points for the new shape from.</param>
		/// <param name="predicate">The predicate that all points in the new shape should satisfy.</param>
		/// <returns>A new explicit shape that contains only points from the original shape
		/// that satisfies the predicate.</returns>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> Where(this IExplicitShape<GridPoint2> shape, Func<GridPoint2, bool> predicate)
		{
			return ImplicitShape
				.Func<GridPoint2>(x => shape.Contains(x) && predicate(x))
				.ToExplicit(shape.Bounds);
		}

		/// <summary>
		/// Returns a new shape that only contains points in this shape that satisfies the predicate.
		/// </summary>
		/// <param name="shape">The shape to select points for the new shape from.</param>
		/// <param name="predicate">The predicate that all points in the new shape should satisfy.</param>
		/// <returns>A new explicit shape that contains only points from the original shape
		/// that satisfies the predicate.</returns>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> Where(this IExplicitShape<GridPoint3> shape, Func<GridPoint3, bool> predicate)
		{
			return ImplicitShape
				.Func<GridPoint3>(x => shape.Contains(x) && predicate(x))
				.ToExplicit(shape.Bounds);
		}

		/// <summary>
		/// Returns a new shape that only contains points in this shape that satisfies the predicate.
		/// </summary>
		/// <param name="shape">The shape to select points for the new shape from.</param>
		/// <param name="predicate">The predicate that all points in the new shape should satisfy.</param>
		/// <returns>A new explicit shape that contains only points from the original shape
		/// that satisfies the predicate.</returns>
		[Obsolete("Use Where instead. This function was misnamed.")]
		public static IExplicitShape<GridPoint2> Select(this IExplicitShape<GridPoint2> shape, Func<GridPoint2, bool> predicate)
		{
			return ImplicitShape
				.Func<GridPoint2>(x => shape.Contains(x) && predicate(x))
				.ToExplicit(shape.Bounds);
		}

		/// <summary>
		/// Returns a new shape that only contains points in this shape that satisfies the predicate.
		/// </summary>
		/// <param name="shape">The shape to select points for the new shape from.</param>
		/// <param name="predicate">The predicate that all points in the new shape should satisfy.</param>
		/// <returns>A new explicit shape that contains only points from the original shape
		/// that satisfies the predicate.</returns>
		[Obsolete("Use Where instead. This function was misnamed.")]
		public static IExplicitShape<GridPoint3> Select(this IExplicitShape<GridPoint3> shape, Func<GridPoint3, bool> predicate)
		{
			return ImplicitShape
				.Func<GridPoint3>(x => shape.Contains(x) && predicate(x))
				.ToExplicit(shape.Bounds);
		}

		/// <summary>
		/// Creates a new explicit shape from the implicit shape that falls
		/// inside the bounds.
		/// </summary>
		public static IExplicitShape<GridPoint2> ToExplicit(
			this IImplicitShape<GridPoint2> shape, 
			AbstractBounds<GridPoint2> bounds)
		{
			return new ExplicitShape2(shape, bounds);
		}

		/// <summary>
		/// Creates a new explicit shape from the implicit shape that falls
		/// inside the bounds.
		/// </summary>
		public static IExplicitShape<GridPoint3> ToExplicit(this IImplicitShape<GridPoint3> shape, AbstractBounds<GridPoint3> bounds)
		{
			return new ExplicitShape3(shape, bounds);
		}

		/// <summary>
		/// Returns a new empty 1D shape.
		/// </summary>
		/// <returns>IExplicitShape&lt;System.Int32&gt;.</returns>
		public static IExplicitShape<int> Empty1()
		{
			return new EmptyShape1();
		}

		/// <summary>
		/// Returns a new empty 2D shape.
		/// </summary>
		/// <returns>IExplicitShape&lt;GridPoint2&gt;.</returns>
		public static IExplicitShape<GridPoint2> Empty2()
		{
			return new EmptyShape2();
		}

		/// <summary>
		/// Returns a new empty 3D shape.
		/// </summary>
		/// <returns>IExplicitShape&lt;GridPoint3&gt;.</returns>
		public static IExplicitShape<GridPoint3> Empty3()
		{
			return new EmptyShape3();
		}

		/// <summary>
		/// Makes a new 3D shape by stacking copies of a 2D along the z-axis.
		/// </summary>
		/// <param name="shape"></param>
		/// <param name="layerCount"></param>
		/// <returns></returns>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint3> Layer(
			this IExplicitShape<GridPoint2> shape, 
			int layerCount)
		{
			var bounds = shape.Bounds;
			var newBounds = new GridBounds(bounds.Point.To3DXY(0), bounds.Size.To3DXY(layerCount));

			// static call is necessary so that this method does not get called instead
			//Remember: explicit shapes _are_ implicit shapes too.
			return ImplicitShape.Layer(shape, layerCount).ToExplicit(newBounds);
		}

		/// <summary>
		/// Constructs a new 2D shape from a given shape with the 
		/// X and Y coordinates swapped.
		/// </summary>
		/// <param name="shape"></param>
		/// <example>
		/// The rect with three points (0, 1), (1, 1), and (2, 1)
		/// will become the rect with the three points (1, 0), (1, 1) and (1, 2).
		/// This is the same as reflecting the shape through the line x = y.
		/// </example>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint2> SwapXY(this IExplicitShape<GridPoint2> shape)
		{
			var bounds = shape.Bounds;
			var point = bounds.Point;
			var size = bounds.Size;

			var newBounds = new GridRect(
				new GridPoint2(point.Y, point.X), new GridPoint2(size.Y, size.X));

			return ImplicitShape.SwapXY(shape).ToExplicit(newBounds);
		}

		/// <summary>
		/// Constructs a new 2D shape from a given shape with the coordinates
		/// changed to the order XZY.
		/// </summary>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint3> SwapToXZY(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var point = bounds.Point;
			var size = bounds.Size;

			var newBounds = new GridBounds(
				new GridPoint3(point.X, point.Z, point.Y), new GridPoint3(size.X, size.Z, size.Y));

			return ImplicitShape.SwapToXZY(shape).ToExplicit(newBounds);
		}

		/// <summary>
		/// Constructs a new 2D shape from a given shape with the coordinates
		/// changed to the order YXZ.
		/// </summary>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint3> SwapToYXZ(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var point = bounds.Point;
			var size = bounds.Size;

			var newBounds = new GridBounds(
				new GridPoint3(point.Y, point.X, point.Z), new GridPoint3(size.Y, size.X, size.Z));

			return ImplicitShape.SwapToYXZ(shape).ToExplicit(newBounds);
		}

		/// <summary>
		/// Constructs a new 2D shape from a given shape with the coordinates
		/// changed to the order YZX.
		/// </summary>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint3> SwapToYZX(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var point = bounds.Point;
			var size = bounds.Size;

			var newBounds = new GridBounds(
				new GridPoint3(point.Y, point.Z, point.X), new GridPoint3(size.Y, size.Z, size.X));

			return ImplicitShape.SwapToYZX(shape).ToExplicit(newBounds);
		}

		/// <summary>
		/// Constructs a new 2D shape from a given shape with the coordinates
		/// changed to the order ZXY.
		/// </summary>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint3> SwapToZXY(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var point = bounds.Point;
			var size = bounds.Size;

			var newBounds = new GridBounds(
				new GridPoint3(point.Z, point.X, point.Y), new GridPoint3(size.Z, size.X, size.Y));

			return ImplicitShape.SwapToZXY(shape).ToExplicit(newBounds);
		}

		/// <summary>
		/// Constructs a new 2D shape from a given shape with the coordinates
		/// changed to the order ZYX.
		/// </summary>
		[Version(2, 2)]
		public static IExplicitShape<GridPoint3> SwapToZYX(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var point = bounds.Point;
			var size = bounds.Size;

			var newBounds = new GridBounds(
				new GridPoint3(point.Z, point.Y, point.X), new GridPoint3(size.Z, size.Y, size.X));

			return ImplicitShape.SwapToZYX(shape).ToExplicit(newBounds);
		}

		/// <summary>
		/// Creates a new shape from a boolean mask where truth values denote points
		/// inside the resulting shape.
		/// </summary>
		/// <param name="mask">A mask representing a shape that has points where the values are true.
		/// The mask starts at the point 0, and extends in the positive direction.
		/// </param>
		/// <example>If the mask is {false, true, false, true}, the resulting shape will have
		/// the points 1 and 3.</example>
		[Version(2, 3)]
		public static IExplicitShape<int> Bitmask(bool[] mask)
		{
			return new BitmaskShape1(mask);
		}

		/// <summary>
		/// The same as <see cref="Bitmask(bool[])"/> but where true is replaced with 1 and
		/// false with 0.
		/// </summary>
		/// <example>If the mask is {0, 1, 0, 1}, the resulting shape will have
		/// the points 1 and 3.</example>
		[Version(2, 3)]
		public static IExplicitShape<int> Bitmask(int[] mask)
		{
			return new BitmaskShape1(mask);
		}

		/// <summary>
		/// The same as <see cref="Bitmask(bool[])"/> but where a true is replaced with 
		/// a '1' character and a false with a '0' character.
		/// </summary>
		/// <example>If the mask is "0101", the resulting shape will have
		/// the points 1 and 3.</example>
		[Version(2, 3)]
		public static IExplicitShape<int> Bitmask(string mask)
		{
			return new BitmaskShape1(mask);
		}

		/// <summary>
		/// Creates a new shape from a boolean mask where truth values denote points
		/// inside the resulting shape.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> Bitmask(bool[][] mask)
		{
			return new BitmaskShape2(mask);
		}

		/// <summary>
		/// The same as <see cref="Bitmask(bool[][])"/> but where true is replaced with 1 and
		/// false with 0.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> Bitmask(int[][] mask)
		{
			return new BitmaskShape2(mask);
		}

		/// <summary>
		/// The same as <see cref="Bitmask(bool[][])"/> but where a true is replaced with 
		/// a '1' character and a false with a '0' character.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> Bitmask(string[] mask)
		{
			return new BitmaskShape2(mask);
		}

		/// <summary>
		/// Creates a new shape from a boolean mask where truth values denote points
		/// inside the resulting shape.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> Bitmask(bool[][][] mask)
		{
			return new BitmaskShape3(mask);
		}

		/// <summary>
		/// The same as <see cref="Bitmask(bool[][][])"/> but where true is replaced with 1 and
		/// false with 0.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> Bitmask(int[][][] mask)
		{
			return new BitmaskShape3(mask);
		}

		/// <summary>
		/// The same as <see cref="Bitmask(bool[][][])"/> but where a true is replaced with 
		/// a '1' character and a false with a '0' character.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> Bitmask(string[][] mask)
		{
			return new BitmaskShape3(mask);
		}

		/// <summary>
		/// Returns a new shape with X reflected, but translated to keep it the same bounds
		/// as the original.
		/// </summary>
		/// <param name="shape">The shape to reflect.</param>
		/// <example>
		/// Suppose the input shape has the points {1, 2, 4}. Then the reflected shape 
		/// is {-4, -2, -1}, and translating this to be in the same bounds give
		/// the shape {1, 3, 4}.
		/// </example>
		[Version(2, 3)]
		public static IExplicitShape<int> ReflectXInBounds(this IExplicitShape<int> shape)
		{
			var bounds = shape.Bounds;
			var func = new Func<int, int>(p => bounds.Extreme - p + bounds.Point - 1);

			return bounds.Where(p => shape.Contains(func(p)));
		}

		/// <summary>
		/// Returns a new shape with X reflected, but translated to keep it the same bounds
		/// as the original.
		/// </summary>
		/// <param name="shape">The shape to reflect.</param>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> ReflectXInBounds(this IExplicitShape<GridPoint2> shape)
		{
			var bounds = shape.Bounds;
			var func = new Func<GridPoint2, GridPoint2>(p => new GridPoint2(bounds.Extreme.X - p.X + bounds.Point.X - 1, p.Y));

			return bounds.Where(p => shape.Contains(func(p)));
		}

		/// <summary>
		/// Returns a new shape with Y reflected, but translated to keep it the same bounds
		/// as the original.
		/// </summary>
		/// <param name="shape">The shape to reflect.</param>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> ReflectYInBounds(this IExplicitShape<GridPoint2> shape)
		{
			var bounds = shape.Bounds;
			var func = new Func<GridPoint2, GridPoint2>(p => new GridPoint2(p.X, bounds.Extreme.Y - p.Y + bounds.Point.Y - 1));

			return bounds.Where(p => shape.Contains(func(p)));
		}

		/// <summary>
		/// Returns a new shape with X reflected, but translated to keep it the same bounds
		/// as the original.
		/// </summary>
		/// <param name="shape">The shape to reflect.</param>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> ReflectXInBounds(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var func = new Func<GridPoint3, GridPoint3>(p => new GridPoint3(bounds.Extreme.X - p.X + bounds.Point.X - 1, p.Y, p.Z));

			return bounds.Where(p => shape.Contains(func(p)));
		}

		/// <summary>
		/// Returns a new shape with Y reflected, but translated to keep it the same bounds
		/// as the original.
		/// </summary>
		/// <param name="shape">The shape to reflect.</param>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> ReflectYInBounds(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var func = new Func<GridPoint3, GridPoint3>(p => new GridPoint3(p.X, bounds.Extreme.Y - p.Y + bounds.Point.Y - 1, p.Z));

			return bounds.Where(p => shape.Contains(func(p)));
		}

		/// <summary>
		/// Returns a new shape with Z reflected, but translated to keep it the same bounds
		/// as the original.
		/// </summary>
		/// <param name="shape">The shape to reflect.</param>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> ReflectZInBounds(this IExplicitShape<GridPoint3> shape)
		{
			var bounds = shape.Bounds;
			var func = new Func<GridPoint3, GridPoint3>(p => new GridPoint3(p.X, p.Y, bounds.Extreme.Z - p.Z + bounds.Point.Z - 1));

			return bounds.Where(p => shape.Contains(func(p)));
		}


		/// <summary>
		/// Returns a translated copy of the given shape.
		/// </summary>
		/// <param name="shape">The shape to copy and translate.</param>
		/// <param name="offset">The amount to translate the copy by</param>
		[Version(2, 3)]
		public static IExplicitShape<int> Translate(this IExplicitShape<int> shape, int offset)
		{
			var newBounds = GridInterval.Translate(shape.Bounds, offset);
			var newShape = ImplicitShape.Translate(shape, offset).ToExplicit(newBounds);

			return newShape;
		}

		/// <inheritdoc cref="Translate(IExplicitShape{int}, int)"/>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> Translate(
			this IExplicitShape<GridPoint2> shape, 
			GridPoint2 offset)
		{
			var newBounds = GridRect.Translate(shape.Bounds, offset);
			var newShape = ImplicitShape.Translate(shape, offset).ToExplicit(newBounds);

			return newShape;
		}

		/// <inheritdoc cref="Translate(IExplicitShape{int}, int)"/>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> Translate(
			this IExplicitShape<GridPoint3> shape, 
			GridPoint3 offset)
		{
			var newBounds = GridBounds.Translate(shape.Bounds, offset);
			var newShape = ImplicitShape.Translate(shape, offset).ToExplicit(newBounds);

			return newShape;
		}

		/// <summary>
		/// Gets the bounds of the given points.
		/// </summary>
		/// <remarks>The bounds are guaranteed to be tight, that is
		/// no smaller bounds will contain the shape.</remarks>
		[Version(2, 3)]
		public static GridInterval GetBounds(IEnumerable<int> points)
		{
			var minPoint = points.Aggregate(Mathf.Min);
			var maxPoint = points.Aggregate(Mathf.Max);
			var size = maxPoint - minPoint + 1;

			return new GridInterval(minPoint, size);
		}

		/// <inheritdoc cref="GetBounds(IEnumerable{int})"/>
		[Version(2, 3)]
		public static GridRect GetBounds(IEnumerable<GridPoint2> points)
		{
			var minPoint = points.Aggregate(GridPoint2.Min);
			var maxPoint = points.Aggregate(GridPoint2.Max);
			var size = maxPoint - minPoint + GridPoint2.One;

			return new GridRect(minPoint, size);
		}

		/// <inheritdoc cref="GetBounds(IEnumerable{int})"/>
		[Version(2, 3)]
		public static GridBounds GetBounds(IEnumerable<GridPoint3> points)
		{
			var minPoint = points.Aggregate(GridPoint3.Min);
			var maxPoint = points.Aggregate(GridPoint3.Max);
			var size = maxPoint - minPoint + GridPoint3.One;

			return new GridBounds(minPoint, size);
		}

		/// <summary>
		/// Gets a copy of the given shape translated so that the center of
		/// the shape (or the point closest to the center) falls on the origin.
		/// </summary>
		[Version(2, 3)]
		public static IExplicitShape<int> CenterOnOrigin(this IExplicitShape<int> shape)
		{
			var size = shape.Bounds.Size / 2;
			var newPoint = -size;
			var translation = newPoint - shape.Bounds.Point;

			return shape.Translate(translation);
		}

		///<inheritdoc cref="CenterOnOrigin(IExplicitShape{int})"/>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint2> CenterOnOrigin(this IExplicitShape<GridPoint2> shape)
		{
			var size = shape.Bounds.Size / 2;
			var newPoint = -size;
			var translation = newPoint - shape.Bounds.Point;

			return shape.Translate(translation);
		}

		///<inheritdoc cref="CenterOnOrigin(IExplicitShape{int})"/>
		[Version(2, 3)]
		public static IExplicitShape<GridPoint3> CenterOnOrigin(this IExplicitShape<GridPoint3> shape)
		{
			var size = shape.Bounds.Size / 2;
			var newPoint = -size;
			var translation = newPoint - shape.Bounds.Point;

			return shape.Translate(translation);
		}

		/// <summary>
		/// Creates a new shape that contains all the points in the given list.
		/// </summary>
		/// <param name="shape">The points that should make up the resulting shape.</param>
		public static IExplicitShape<int> Create(IEnumerable<int> shape)
		{
			return new HashShape<int>(shape, GetBounds(shape));
		}

		///<inheritdoc cref="Create(IEnumerable{int})"/>
		public static IExplicitShape<GridPoint2> Create(IEnumerable<GridPoint2> shape)
		{
			return new HashShape<GridPoint2>(shape, GetBounds(shape));
		}

		///<inheritdoc cref="Create(IEnumerable{int})"/>
		public static IExplicitShape<GridPoint3> Create(IEnumerable<GridPoint3> shape)
		{
			return new HashShape<GridPoint3>(shape, GetBounds(shape));
		}

		/// <summary>
		/// Creates a new axis-aligned parallelogram shape.
		/// </summary>
		/// <param name="dimensions">The dimensions of the parallelogram to create</param>
		/// <remarks>
		/// For rect grids, this will be a rectangle. For hex grids, this will be a 
		/// parallelogram with one corner 60 degrees. For polar grids, this will be a
		/// ring.</remarks>
		public static IExplicitShape<GridPoint2> Parallelogram(GridPoint2 dimensions)
		{
			return ImplicitShape
				.Parallelogram(dimensions)
				.ToExplicit(new GridRect(GridPoint2.Zero, dimensions));
		}

		/// <summary>
		/// Makes a new shape that contains the origin as its only point.
		/// </summary>
		public static IExplicitShape<int> Single1()
		{
			return ImplicitShape
				.Single1()
				.ToExplicit(new GridInterval(0, 1));
		}

		///<inheritdoc cref="Single1"/>
		public static IExplicitShape<GridPoint2> Single2()
		{
			return ImplicitShape
				.Single2()
				.ToExplicit(new GridRect(GridPoint2.Zero, GridPoint2.One));
		}

		///<inheritdoc cref="Single1"/>
		public static IExplicitShape<GridPoint3> Single3()
		{
			return ImplicitShape
				.Single3()
				.ToExplicit(new GridBounds(GridPoint3.Zero, GridPoint3.One));
		}

		/// <summary>
		/// Makes a new shape that is a contiguous interval of the given size, starting at 0
		/// and continuing in the positive direction.
		/// </summary>
		/// <param name="size">The size of the segment to make.</param>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// var segment = ExplicitShape.Segment(4);
		/// //will contain the points {0, 1, 2, 3} 
		/// ]]>
		///</code>
		///</example>
		public static IExplicitShape<int> Segment(int size)
		{
			var interval = new GridInterval(0, size);

			return ImplicitShape.Segment(interval).ToExplicit(interval);
		}

		/// <summary>
		/// Makes a new axis aligned parallelepiped shape.
		/// </summary>
		/// <param name="dimensions">The size of the parallelepiped.</param>
		/// <remarks>A parallelepiped is the 3D analog of a parallelogram. It has 6 faces, like a box, 
		/// and opposite face are parallel and parallelograms.</remarks>
		public static IExplicitShape<GridPoint3> Parallelepiped(GridPoint3 dimensions)
		{
			return ImplicitShape
				.Parallelepiped(dimensions)
				.ToExplicit(new GridBounds(
					GridPoint3.Zero,
					dimensions));
		}
	}
	#endregion
}