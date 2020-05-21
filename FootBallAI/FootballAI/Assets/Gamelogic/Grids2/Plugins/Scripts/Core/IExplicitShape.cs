using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a point set that is an implicit shape, and in addition,
	/// can generate all the points it contains.
	/// </summary>
	/// <typeparam name="TPoint">The point type.</typeparam>
	public interface IExplicitShape<TPoint> : IImplicitShape<TPoint>
	{
		/// <summary>
		/// Gets all the points this shape contains.
		/// </summary>
		/// <value>The points.</value>
		IEnumerable<TPoint> Points { get; }

		/// <summary>
		/// Returns a bounding shape that contains this explicit shape.
		/// </summary>
		/// <remarks>The bounding box is not guarenteed to be tight, that is, there
		/// may be a smaller bounding box that also contains the shape.</remarks>
		AbstractBounds<TPoint> Bounds { get; }
	}
}