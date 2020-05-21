namespace Gamelogic.Grids2
{
	/// <summary>
	/// Simple struct that holds a read-only <typeparamref name="TPoint"/> and <typeparamref name="TCell"/>.
	/// This is the type over which grids are enumerable.
	/// </summary>
	/// <typeparam name="TPoint">Type of the Point.</typeparam>
	/// <typeparam name="TCell">Type of the Cell.</typeparam>
	/// <remarks>This class exists so that a grid mimics the implementation
	/// of <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> 
	/// and should only seldomly be useful.</remarks>
	public struct PointCellPair<TPoint, TCell>
	{
		public readonly TPoint point;
		public readonly TCell cell;

		/// <summary>
		/// Initializes a new instance of the <see cref="PointCellPair{TPoint, TCell}"/> struct.
		/// </summary>
		/// <param name="point">The point of this point cell pair.</param>
		/// <param name="cell">The cell of this point cell pair.</param>
		public PointCellPair(TPoint point, TCell cell)
		{
			this.point = point;
			this.cell = cell;
		}
	}
}