using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// This is an inspectable presentation of a grid coloring,
	/// using three parameters as explained here:
	/// <see href="http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/"/>
	/// </summary>
	/// <remarks>The three values represent two corners of a parallelogram
	///(x0, 0) and(x1, y1) that describe the patch to repeat for
	///the coloring.</remarks>
	[Version(2, 0)]
	[Serializable]
	//TODO make 1D and 3D versions
	public class ColorFunction
	{
		//TODO put in constraints
		public int x0;
		public int x1;
		public int y1;
	}

	/// <summary>
	/// This is an inspectable presentation of a grid coloring,
	/// using six parameters.
	/// </summary>
	/// <remarks>The six values represent three corners of a parallelepiped 
	///(x0, 0, 0), (x1, y1, 0), and (x2, y2, z2) that describe the patch to repeat for
	///the coloring.</remarks>
	///<seealso href="http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/"/>
	[Serializable]
	[Version(2, 3, 10)]
	public class ColorFunction3
	{
		public int x0;
		public int x1;
		public int y1;
		public int x2;
		public int y2;
		public int z2;
	}
}