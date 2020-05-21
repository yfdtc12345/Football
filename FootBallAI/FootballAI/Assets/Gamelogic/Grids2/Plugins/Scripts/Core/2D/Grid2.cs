using System;
using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A 2D implementation of a <see cref="IGrid{TPoint,TCell}"/>.
	/// </summary>
	/// <inheritdoc/>
	public sealed class Grid2<TCell> : AbstractGrid<GridPoint2, TCell>
	{
		#region Constants

		private readonly TCell[,] cells;
		private readonly IExplicitShape<GridPoint2> shape;

		#endregion

		#region Public Properties
		/// <inheritdoc/>
		public override AbstractBounds<GridPoint2> Bounds
		{
			get { return shape.Bounds; }
		}

		/// <inheritdoc/>
		public override IEnumerable<GridPoint2> Points
		{
			get { return shape.Points; }
		}

		/// <inheritdoc/>
		public override TCell this[GridPoint2 point]
		{
			get
			{
				var accessPoint = point - shape.Bounds.Point;
				return cells[accessPoint.X, accessPoint.Y];
			}

			set
			{
				var accessPoint = point - shape.Bounds.Point;
				cells[accessPoint.X, accessPoint.Y] = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Grid1{TCell}"/> class with the specified
		/// shape. Cells are not initialized, they have the default value of type <c>TCell</c>.
		/// </summary>
		/// <param name="shape">The shape of this grid.</param>
		public Grid2(IExplicitShape<GridPoint2> shape)
		{
			this.shape = shape;
			var storageDimensions = shape.Bounds.Size;

			cells = new TCell[storageDimensions.X, storageDimensions.Y];
		}

		#endregion

		#region Public Methods
		/// <inheritdoc/>
		public override IGrid<GridPoint2, TNewCell> CloneStructure<TNewCell>()
		{
			return new Grid2<TNewCell>(shape);
		}
		/// <inheritdoc/>
		public override bool Contains(GridPoint2 point)
		{
			return shape.Contains(point);
		}

		#endregion

		#region Iterators

		/// <deprecated/>
		[Obsolete("Use PointyHexPoint.GetSpiralIterator instead.")]
		public IEnumerable<GridPoint2> GetHexSpiralIterator(GridPoint2 origin, int ringCount)
		{
			return PointyHexPoint.GetSpiralIterator(this, origin, ringCount);
		}

		/// <deprecated/>
		[Obsolete("Use RectPoint.GetSpiralIterator instead.")]
		public IEnumerable<GridPoint2> GetSpiralIterator(GridPoint2 origin, int ringCount)
		{
			return RectPoint.GetSpiralIterator(this, origin, ringCount);
		}

		#endregion
	}
}