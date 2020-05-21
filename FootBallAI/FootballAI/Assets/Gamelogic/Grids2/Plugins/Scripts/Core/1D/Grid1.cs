using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// 1D implementation of a <see cref="IGrid{TPoint,TCell}"/>.
	/// </summary>
	/// <inheritdoc/>
	public sealed class Grid1<TCell> : AbstractGrid<int, TCell>
	{
		#region Constants

		private readonly TCell[] cells;
		private readonly IExplicitShape<int> shape;

		#endregion

		#region Public Properties
		/// <inheritdoc/>
		public override TCell this[int point]
		{
			get
			{
				var accessPoint = point - shape.Bounds.Point;
				return cells[accessPoint];
			}
			set
			{
				var accessPoint = point - shape.Bounds.Point;
				cells[accessPoint] = value;
			}
		}

		/// <inheritdoc/>
		public override IEnumerable<int> Points
		{
			get { return shape.Points; }
		}

		/// <inheritdoc/>
		public override AbstractBounds<int> Bounds
		{
			get { return shape.Bounds; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Grid1{TCell}"/> class with
		/// the specified shape.
		/// Cells are not initialized, they have the default value of type <c>TCell</c>.
		/// </summary>
		/// <param name="shape">The shape of this grid.</param>
		public Grid1(IExplicitShape<int> shape)
		{
			this.shape = shape;
			var storageDimensions = shape.Bounds.Size;
			cells = new TCell[storageDimensions];
		}

		#endregion

		#region Public Methods

		/// <inheritdoc/>
		public override IGrid<int, TNewCell> CloneStructure<TNewCell>()
		{
			return new Grid1<TNewCell>(shape);
		}

		/// <inheritdoc/>
		public override bool Contains(int point)
		{
			return shape.Contains(point);
		}

		#endregion
	}
}