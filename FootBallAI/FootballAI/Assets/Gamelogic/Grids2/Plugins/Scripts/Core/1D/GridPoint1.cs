using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;

namespace Gamelogic.Grids2
{
	// The methods in this class are not extension methods
	// to avoid cluttering methods that apply to int.

	/// <summary>
	/// Provides constants and methods for points of 1D Grids, that is, integers.
	/// </summary>
	public static class GridPoint1
	{
		#region Types

		private sealed class LineImpl : IMap<int, int>
		{
			private readonly int direction;

			public LineImpl(int direction)
			{
				this.direction = direction;
			}

			public int Forward(int input)
			{
				return input + direction;
			}

			public int Reverse(int output)
			{
				return output - direction;
			}
		}
		#endregion

		#region Neighbors
		public static IEnumerable<int> GetVectorNeighbors(int point, IEnumerable<int> directions)
		{
			return directions.Select(p => point + p);
		}

		//Q: Should these be extension methods or not?
		//A: Not, otherwise they clutter up int.
		public static IEnumerable<int> GetNeighbors(int point)
		{
			yield return point - 1;
			yield return point + 1;
		}
		#endregion

		#region Rays and Lines

		public static IMap<int, int> GetLine(int direction)
		{
			return new LineImpl(direction);
		}

		public static IEnumerable<IMap<int, int>> GetVectorLines(IEnumerable<int> directions)
		{
			return directions.Select<int, IMap<int, int>>(GetLine);
		}

		public static IEnumerable<IForwardMap<int, int>> GetForwardVectorRays(IEnumerable<int> directions)
		{
			return directions.Select(d => (IForwardMap<int, int>)GetLine(d));
		}

		public static IEnumerable<IReverseMap<int, int>> GetReverseVectorRays(IEnumerable<int> directions)
		{
			return directions.Select(d => (IReverseMap<int, int>)GetLine(d));
		}
		#endregion

		#region Colors
		public static int GetColor(int point, int colorFunction)
		{
			return GLMathf.FloorMod(point, colorFunction);
		}
		#endregion
	}
}