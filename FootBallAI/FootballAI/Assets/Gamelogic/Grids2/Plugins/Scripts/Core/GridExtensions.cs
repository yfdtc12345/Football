using Gamelogic.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Provides extension methods for grids.
	/// </summary>
	public static class GridExtensions
	{
		/// <summary>
		/// Gets the value at the specified point.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid in which to look up a value.</param>
		/// <param name="point">The point at which to get the value.</param>
		/// <returns>The value at the given point in the grid.</returns>
		/// <remarks>
		/// <para>Grid values are often referred to as cells.</para>
		/// <para>This method is equivalent to getting a value
		/// using the index operator: <c>grid[point]</c>, but is often
		/// more convenient when building lambda expressions.</para>
		/// </remarks>
		public static TCell GetValue<TPoint, TCell>(this IGrid<TPoint, TCell> grid, TPoint point)
		{
			return grid[point];
		}

		/// <summary>
		/// Sets the value of a grid at a specified point.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid in which to set the value.</param>
		/// <param name="point">The point at which to set the value.</param>
		/// <param name="value">The value to set at the given point.</param>
		/// <remarks>
		/// <para>Grid values are also referred to as cells.</para>
		/// <para>This method is equivalent to setting the value using 
		/// the index operator: <c>grid[point] = value;</c>, but is 
		/// often more convenient to use in lambda expressions.</para>
		/// </remarks>
		public static void SetValue<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			TPoint point,
			TCell value)
		{
			grid[point] = value;
		}

		/// <preliminary/>
		[Experimental]
		public static IEnumerable<TPoint> GetPoints<TPoint>(
			this IGrid<TPoint> grid,
			Func<TPoint, IEnumerable<TPoint>> getAllPoints,
			TPoint point)
		{
			return getAllPoints(point).In(grid); //Where(grid.Contains);
		}

		/// <summary>
		/// Selects all the points in the list also in the given shape.
		/// </summary>
		/// <typeparam name="TPoint">The point type.</typeparam>
		/// <param name="points">The points.</param>
		/// <param name="shape">The shape.</param>
		/// <remarks>
		/// <note type="note">
		/// You can also pass an explicit shape or grid for the shape parameter.
		/// </note>
		/// </remarks>
		public static IEnumerable<TPoint> In<TPoint>(
			this IEnumerable<TPoint> points, 
			IImplicitShape<TPoint> shape)
		{
			return points.Where(shape.Contains);
		}

		/// <summary>
		/// Creates a grid with a new cell type, and set
		/// the value of each cell to the given initial value.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the given grid.</typeparam>
		/// <typeparam name="TNewCell">The type of resulting grid.</typeparam>
		/// <param name="grid">The grid to clone.</param>
		/// <param name="initialValue">The value to assign to each cell in the grid.</param>
		/// <returns></returns>
		public static IGrid<TPoint, TNewCell> CloneStructure<TPoint, TNewCell>(
			this IGrid<TPoint> grid,
			TNewCell initialValue)
		{
			var newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = initialValue;
			}

			return newGrid;
		}

		/// <summary>
		/// Creates a grid with a new cell type, and set
		/// the value of each cell to the value return by a 
		/// given function.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TNewCell">The cell type of the resulting grid.</typeparam>
		/// <param name="grid">The grid to clone.</param>
		/// <param name="generateItem">A function that returns a value. The function
		/// is called for each point in the grid.</param>
		public static IGrid<TPoint, TNewCell> CloneStructure<TPoint, TNewCell>(
			this IGrid<TPoint> grid,
			Func<TNewCell> generateItem)
		{
			var newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = generateItem();
			}

			return newGrid;
		}

		/// <summary>
		/// Creates a grid with a new cell type, and set
		/// the value of each cell to the value return by a 
		/// given function.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TNewCell">The cell type of the resulting grid.</typeparam>
		/// <param name="grid">The grid to clone.</param>
		/// <param name="generateItem">A function that returns a value. The function
		/// is called for each point in the grid, and the point is passed as a parameter.</param>
		public static IGrid<TPoint, TNewCell> CloneStructure<TPoint, TNewCell>(
			this IGrid<TPoint> grid,
			Func<TPoint, TNewCell> generateItem)
		{
			var newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = generateItem(point);
			}

			return newGrid;
		}

		/// <summary>
		/// Sets each cell in the grid to the given value.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid to fill.</param>
		/// <param name="value">The value to fill the grid with.</param>
		public static void Fill<TPoint, TCell>(this IGrid<TPoint, TCell> grid, TCell value)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = value;
			}
		}

		/// <summary>
		/// Sets each cell in the grid to the value returned by a
		/// given function.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid to fill.</param>
		/// <param name="generateItem">A function that returns a value that is assigned to each cell.
		/// The function is called for each point in the grid.</param>
		public static void Fill<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TCell> generateItem)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = generateItem();
			}
		}

		/// <summary>
		/// Sets each cell in the grid to the value returned by a
		/// given function.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid to fill.</param>
		/// <param name="generateItem">A function that returns a value that is assigned to each cell.
		/// The function is called for each point in the grid, and the point is passed
		/// as parameter to the function.</param>
		public static void Fill<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TPoint, TCell> generateItem)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = generateItem(point);
			}
		}

		/// <summary>
		/// Applies an action for each cell in the grid.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid for which the action is applied.</param>
		/// <param name="action">The action to apply for each point. The action is 
		/// called with the point as parameter.</param>
		public static void Apply<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid, //TODO: Can be a explicit shape
			Action<TPoint> action)
		{
			foreach (var point in grid.Points)
			{
				action(point);
			}
		}

		/// <summary>
		/// Applies an action for each cell in the grid.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid for which the action is applied.</param>
		/// <param name="action">The action to apply for each point. The action is 
		/// called with the cell as parameter.</param>
		public static void Apply<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Action<TCell> action)
		{
			foreach (var point in grid.Points)
			{
				action(grid[point]);
			}
		}

		/// <summary>
		/// Transforms the contents of a grid using a given transform.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid to transform.</param>
		/// <param name="transform">The transform, called on each point in the grid
		/// with the cell at that point as parameter. The result is replaced at that 
		/// point.</param>
		public static void Transform<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TCell, TCell> transform)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = transform(grid[point]);
			}
		}

		/// <summary>
		/// Transforms the contents of a grid using a given transform.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid to transform.</param>
		/// <param name="transform">The transform, called on each point in the grid
		/// with the point and cell at that point as parameters. The result is replaced at that 
		/// point.</param>
		public static void Transform<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TPoint, TCell, TCell> transform)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = transform(point, grid[point]);
			}
		}

		/// <summary>
		/// Returns a shallow copy of the given grid.
		/// </summary>
		public static IGrid<TPoint, TCell> Clone<TPoint, TCell>(this IGrid<TPoint, TCell> grid)
		{
			if (grid == null)
			{
				throw new ArgumentNullException("grid");
			}

			var newGrid = grid.CloneStructure<TCell>();

			foreach (var point in grid.Points)
			{
				newGrid[point] = grid[point];
			}

			return newGrid;
		}

		/*public static IGrid<TPoint, TNewCell> CastValues<TPoint, TOldCell, TNewCell>(this IGrid<TPoint, TOldCell> grid)
		{
			if (grid == null)
			{
				throw new ArgumentNullException("grid");
			}

			var newGrid = grid as IGrid<TPoint, TNewCell>;

			if (newGrid != null) return newGrid;

			newGrid = grid.CloneStructure<TNewCell>();
			
			foreach (var point in grid.Points)
			{
				newGrid[point] = (TNewCell) grid[point];
			}

			return newGrid;
		}*/

		/// <summary>
		/// Creates a new grid in the given shape.
		/// </summary>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="shape">The shape of the new grid.</param>
		public static IGrid<int, TCell> ToGrid<TCell>(this IExplicitShape<int> shape)
		{
			return new Grid1<TCell>(shape);
		}

		/// <summary>
		/// Creates a new grid in the given shape.
		/// </summary>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="shape">The shape of the new grid.</param>
		public static IGrid<GridPoint2, TCell> ToGrid<TCell>(this IExplicitShape<GridPoint2> shape)
		{
			return new Grid2<TCell>(shape);
		}

		/// <summary>
		/// Creates a new grid in the given shape.
		/// </summary>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="shape">The shape of the new grid.</param>
		public static IGrid<GridPoint3, TCell> ToGrid<TCell>(this IExplicitShape<GridPoint3> shape)
		{
			return new Grid3<TCell>(shape);
		}
	}
}