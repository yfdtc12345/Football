using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a axis-aligned bounding space. Concrete implementations for 1D (<see cref="GridInterval"/>), 2D (<see cref="GridRect"/>)
	/// and 3D (<see cref="GridRect"/>) is provided.
	/// </summary>
	/// <typeparam name="TPoint">The point type of the bounds.</typeparam>
	public abstract class AbstractBounds<TPoint> : IExplicitShape<TPoint>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractBounds{TPoint}"/> class.
		/// </summary>
		/// <param name="point">The position of the bounds.</param>
		/// <param name="size">The size of the bounds.</param>
		protected AbstractBounds(TPoint point, TPoint size)
		{
			Point = point;
			Size = size;
		}

		/// <summary>
		/// Gets the point which indicates where this bounds is located. 
		/// </summary>
		public TPoint Point { get; private set; }

		/// <summary>
		/// Gets the size of this bounds.
		/// </summary>
		public TPoint Size { get; private set; }

		/// <summary>
		/// Gets the extreme point of this bounds, lying on the opposite direction of <see cref="{Point}"/>. For discrete
		/// point types, this points fall just outside the bounds.
		/// </summary>
		/// <value>The extreme.</value>
		public abstract TPoint Extreme { get; }

		/// <summary>
		/// Determines whether this bounds contains the specified point.
		/// </summary>
		/// <param name="point">The point to check.</param>
		/// <returns><c>true</c> if this shape contains the specified point; otherwise, <c>false</c>.</returns>
		public abstract bool Contains(TPoint point);

		/// <summary>
		/// Gets all the points that this bounds contains.
		/// </summary>
		/// <value>The points contained in this bounds.</value>
		public abstract IEnumerable<TPoint> Points { get; }

		/// <summary>
		/// The bounds of a bounds object is itself.
		/// </summary>
		/// <value>The bounds.</value>
		public AbstractBounds<TPoint> Bounds
		{
			get { return this; }
		}
	}
}