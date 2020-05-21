using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that takes two shapes as inputs,
	/// and put a copy of the second in each cell of a scaled copy of the first.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.ShapeNode{Gamelogic.Grids2.GridPoint2, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.GridPoint2}, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.GridPoint2}}" />
	[ShapeNode("Operator/Product", 2)]
	public class ProductShape2Node : ShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public InspectableGridPoint2 scaleFactor;

		public override void Recompute()
		{
			//
		}

		public override List<IExplicitShape<GridPoint2>> Execute(IEnumerable<IExplicitShape<GridPoint2>> input)
		{
			if(input == null)
			{
				return null;
			}

			var inputList = input.ToList();

			if(inputList.Count == 0)
			{
				return inputList; //leave it empty
			}
			
			if(inputList.Count ==1)
			{
				//product of 1 shape with empty is empty.
				return new List<IExplicitShape<GridPoint2>>();
			}

			var shape1 = inputList[0];
			var shape2 = inputList[1];
			var scale = scaleFactor.GetGridPoint();
			var shape = shape1.Product(shape2, scale);

			var storageRect = new GridRect(
				shape1.Bounds.Point.Mul(scale) - shape2.Bounds.Size,
				shape1.Bounds.Size.Mul(scale) + shape2.Bounds.Size * 2);

			return new List<IExplicitShape<GridPoint2>>
			{
				shape.ToExplicit(storageRect)
			};
		}
	}
}