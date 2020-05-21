using Gamelogic.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	/// <summary>
	/// Uses a mesh for an actual cell to generate full mesh data for a grid.
	/// </summary>
	/// <remarks>This class is suitable for uniform grids with complicated cells.</remarks>
	[CreateAssetMenu(fileName = "MeshData", menuName = "Grids/MeshData/From Mesh")]
	[Version(2, 3)]
	public class MeshDataFromMesh : MeshData
	{
		#region Public Fields

		/// <summary>
		/// The mesh to use for a cell.
		/// </summary>
		public Mesh mesh;

		/// <summary>
		/// The scaling factor to apply to the mesh to get it to "standard" scale. Standard scale for
		/// 1 square cell is 1 by 1 unit. If your mesh is 2by2 units, you can use a scale of 0.5 to
		/// get correct results.
		/// </summary>
		public float scale;

		#endregion

		#region Private Fields

		private bool flipped;

		#endregion

		#region Public Methods

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetNormals(IExplicitShape<int> shape, GridMap<int> map, bool flip)
		{
			return shape.Points.SelectMany(p => mesh.vertices.Select((q, i) => map.GridToWorld(q + map.DeRound(p) + (flip ? -mesh.normals[i] : mesh.normals[i])).normalized));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint2> explicitShape, GridMap<GridPoint2> gridMap, bool flip)
		{
			return explicitShape.Points.SelectMany(p => mesh.vertices.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -mesh.normals[i] : mesh.normals[i])).normalized));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint3> explicitShape, GridMap<GridPoint3> gridMap, bool flip)
		{
			return explicitShape.Points.SelectMany(p => mesh.vertices.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -mesh.normals[i] : mesh.normals[i])).normalized));
		}

		/// <inheritdoc/>
		//TODO doubleSided
		public override IEnumerable<int> GetTriangles(IExplicitShape<int> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) throw new NotSupportedException();

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);
		}

		/// <inheritdoc/>
		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint2> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);
		}

		/// <inheritdoc/>
		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint3> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector2> GetUVs(IExplicitShape<int> shape)
		{
			return shape.Points.SelectMany(p => mesh.uv);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			return shape.Points.SelectMany(p => mesh.uv);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			return shape.Points.SelectMany(p => mesh.uv);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			return shape.Points.SelectMany(p => mesh.vertices.Select(q => map.GridToWorld(q + scale * map.DeRound(p))));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			return shape.Points.SelectMany(p => mesh.vertices.Select(q => map.GridToWorld(q + scale * map.DeRound(p))));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			return shape.Points.SelectMany(p => mesh.vertices.Select(q => map.GridToWorld(q + scale * map.DeRound(p))));
		}

		#endregion

		#region Private methods

		/// <inheritdoc/>
		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<int> shape)
		{
			var triangles = mesh.triangles;

			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * mesh.vertices.Length));
		}

		/// <inheritdoc/>
		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint2> shape)
		{
			var triangles = mesh.triangles;

			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * mesh.vertices.Length));
		}

		/// <inheritdoc/>
		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint3> shape)
		{
			var triangles = mesh.triangles;
			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * mesh.vertices.Length));
		}

		/// <inheritdoc/>
		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<int> shape)
		{
			return shape.Points.SelectMany((p, i) => mesh.triangles.Select(t => t + i * mesh.vertices.Length));
		}

		/// <inheritdoc/>
		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint2> shape)
		{
			return shape.Points.SelectMany((p, i) => mesh.triangles.Select(t => t + i * mesh.vertices.Length));
		}

		/// <inheritdoc/>
		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint3> shape)
		{
			return shape.Points.SelectMany((p, i) => mesh.triangles.Select(t => t + i * mesh.vertices.Length));
		}

#endregion
	}
}