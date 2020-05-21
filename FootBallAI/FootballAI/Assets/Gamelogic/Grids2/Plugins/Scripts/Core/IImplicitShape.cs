namespace Gamelogic.Grids2
{
	/// <summary>
	/// An implicit shape is a representation of a (discrete) point set
	/// that can determine whether a given point is in the shape or not.
	/// </summary>
	/// <typeparam name="TPoint">The point type for this shape. Usually one of <see cref="System.Int32"/>,
	/// <see cref="GridPoint2"/> or <see cref="GridPoint3"/></typeparam>
	/// <conceptualLink target="8d09b1d9-492b-4aa0-96a1-982171ea3e7c"/>
	/// <remarks><see cref="ImplicitShape"/> provides many methods for creating
	/// and manipulating implicit shapes.</remarks>
	public interface IImplicitShape<in TPoint>
	{
		/// <summary>
		/// Determines whether this implicit shape contains the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns><c>true</c> if this shape contains the specified point; otherwise, <c>false</c>.</returns>
		bool Contains(TPoint point);
	}
}