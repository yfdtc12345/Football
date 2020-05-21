using System;
using UnityEngine;

using Gamelogic.Extensions;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for aligning a grid in a rectangle.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	/// //TODO Does not work
	[SpaceMapNode("Misc/Alignment Map", 2)]
	public class AlignmentSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		[Header("Grid Data")]
		public SpriteCell cellPrefab;
		public Bounds screenBounds;
		public GridShapeGraph gridShapeGraph;

		[Header("Grid Alignment")]
		public HorizontalAlignment horizontalAlign;
		public VerticalAlignment verticalAlign;

		[Header("Cell Anchoring")]
		public HorizontalAlignment horizontalAnchor;
		public VerticalAlignment verticalAnchor;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			//throw new NotImplementedException();
			
			var cellDimensions = cellPrefab.Sprite.rect.size.To3DXY();

			
			switch (gridShapeGraph.gridType)
			{
				case GridType.Grid1:
					return
						input
							.AlignGridInRect(
								gridShapeGraph.shape1Graph.GetShape(),
								p => cellDimensions,
								p => new Vector3(p, 0, 0),
								screenBounds, 
								horizontalAlign,
								verticalAlign)
							.AnchorPivotInRect<int>(
								gridShapeGraph.shape1Graph.GetShape(), 
								p => cellDimensions,
								horizontalAnchor,
							    verticalAnchor);

				case GridType.Grid2:
					return
						input
							.AlignGridInRect(
								gridShapeGraph.shape2Graph.GetShape(), 
								p => cellDimensions,
								p => p.ToVector2().To3DXY(), 
								screenBounds, 
								horizontalAlign,
								verticalAlign)
							.AnchorPivotInRect(
								gridShapeGraph.shape2Graph.GetShape(), 
								p => cellDimensions, 
								horizontalAnchor,
							verticalAnchor);

				case GridType.Grid3:
					return
						input
							.AlignGridInRect(
								gridShapeGraph.shape3Graph.GetShape(), 
								p => cellDimensions,
								p => p.ToVector3(),
								screenBounds, 
								horizontalAlign,
								verticalAlign)
							.AnchorPivotInRect(
								gridShapeGraph.shape3Graph.GetShape(), 
								p => cellDimensions, 
								horizontalAnchor,
								verticalAnchor);

				default:
					throw new NotImplementedException();
			}
		}
	}
}