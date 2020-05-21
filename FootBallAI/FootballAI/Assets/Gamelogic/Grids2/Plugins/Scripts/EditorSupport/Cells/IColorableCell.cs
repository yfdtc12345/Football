using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a cell whose color can be set.
	/// </summary>
	[Version(1, 8)]
	public interface IColorableCell
	{
		/// <summary>
		/// The main color of the tile. This is used to set 
		/// the color of tiles dynamically.
		/// </summary>
		Color Color { get; set; }
	}
}
