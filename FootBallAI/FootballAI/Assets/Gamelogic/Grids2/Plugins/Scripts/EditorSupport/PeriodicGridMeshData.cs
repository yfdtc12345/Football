using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Used to specify mesh data for periodic grids.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.MeshData"/>
	/// <remarks>
	/// Periodic grids use the same grid data for cells with the same color. The color is determined
	/// by the specified coloring.
	/// </remarks>
	[CreateAssetMenu(fileName = "MeshData", menuName = "Grids/MeshData/Periodic")]
	[Serializable]
	public class PeriodicGridMeshData : MeshData
	{
		#region Constants

		private const float Half = 0.5f;

		#endregion

		#region Public Fields

		/// <summary>
		/// Color function to use for 1D grids.
		/// </summary>
		/// <remarks>
		/// Each point is classified by its color, and the corresponding data is used to generate
		/// vertices, triangles and UVs for that point.
		/// </remarks>
		public int colorFunction1;

		/// <summary>
		/// Color function to use for 2D grids.
		/// </summary>
		/// <remarks>
		/// Each point is classified by its color, and the corresponding data is used to generate
		/// vertices, triangles and UVs for that point.
		/// </remarks>
		[FormerlySerializedAs("colorFunction")]
		public ColorFunction colorFunction2;

		/// <remarks>
		/// Each point is classified by its color, and the corresponding data is used to generate
		/// vertices, triangles and UVs for that point.
		/// </remarks>
		public ColorFunction3 colorFunction3;

		/// <summary>
		/// The normals to use for the cell of each color type. The first entry corresponds to color
		/// 0, the second to color 1, and so on.
		/// </summary>
		public Vector3Array[] normals =
		{
			new Vector3Array
			{
				data = new List<Vector3>
				{
					Vector3.back,
					Vector3.back,
					Vector3.back,
					Vector3.back
				}
			}
		};

		/// <summary>
		/// The triangles to use for the cell of each color type. The first entry corresponds to
		/// color 0, the second to color 1, and so on.
		/// </summary>
		public IntArray[] triangles =
		{
			new IntArray
			{
				data = new List<int>
				{
					0, 3, 1,
					3, 2, 1
				}
			}
		};

		/// <summary>
		/// The UVs to use for the cell of each color type. The first entry corresponds to color 0,
		/// the second to color 1, and so on.
		/// </summary>
		public Vector2Array[] uvs =
		{
			new Vector2Array()
			{
				data = new List<Vector2>
				{
					new Vector2(0, 0),
					new Vector2(0, 1),
					new Vector3(1, 1),
					new Vector3(1, 0),
				}
			}
		};

		/// <summary>
		/// The vertices to use for the cell of each color type. The first entry corresponds to color
		/// 0, the second to color 1, and so on.
		/// </summary>
		public Vector3Array[] vertices =
		{
			new Vector3Array
			{
				//Use this type of initialization because
				//collection initializer does not work in inspector
				data = new List<Vector3>
				{
					new Vector3(-Half, -Half, 0),
					new Vector3(-Half, Half, 0),
					new Vector3(Half, Half, 0),
					new Vector3(Half, -Half, 0),
				}
			}
		};

		#endregion

		#region Private Fields

		private bool flipped;

		#endregion

		#region Public Methods

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetNormals(IExplicitShape<int> shape, GridMap<int> map, bool flip)
		{
			return shape
				.Points
				.SelectMany(p => vertices[GridPoint1.GetColor(p, colorFunction1)]
					.Select((q, i) => map.GridToWorld(q + map.DeRound(p) + (flip ? -normals[GridPoint1.GetColor(p, colorFunction1)][i] : normals[GridPoint1.GetColor(p, colorFunction1)][i])).normalized));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint2> explicitShape, GridMap<GridPoint2> gridMap, bool flip)
		{
			return
				explicitShape
					.Points
					.SelectMany(p => vertices[p.GetColor(colorFunction2)]
						.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[p.GetColor(colorFunction2)][i] : normals[p.GetColor(colorFunction2)][i])).normalized));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint3> explicitShape, GridMap<GridPoint3> gridMap, bool flip)
		{
			return explicitShape
				.Points
				.SelectMany(p => vertices[p.GetColor(colorFunction3)]
					.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[p.GetColor(colorFunction3)][i] : normals[p.GetColor(colorFunction3)][i])).normalized));
		}

		/// <inheritdoc/>
		//TODO doubleSided
		public override IEnumerable<int> GetTriangles(IExplicitShape<int> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

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
			return shape.Points.SelectMany(p => uvs[GridPoint1.GetColor(p, colorFunction1)]);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			return shape.Points.SelectMany(p => uvs[p.GetColor(colorFunction2)]);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			return shape.Points.SelectMany(p => uvs[p.GetColor(colorFunction3)]);
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			return shape
				.Points
				.SelectMany(p => vertices[GridPoint1.GetColor(p, colorFunction1)]
					.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			return shape
				.Points
				.SelectMany(p => vertices[p.GetColor(colorFunction2)]
					.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		/// <inheritdoc/>
		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			return shape
				.Points
				.SelectMany(p => vertices[p.GetColor(colorFunction3)]
					.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

#endregion

		#region Private Methods

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<int> shape)
		{
			var flippedTriangles = new IntArray[triangles.Length];

			for (int j = 0; j < triangles.Length; j++)
			{
				flippedTriangles[j] = new IntArray()
				{
					data = triangles[j].ToList()
				};

				for (int i = 0; i < triangles[j].Count; i += 3)
				{
					flippedTriangles[j][i] = triangles[j][i + 1];
					flippedTriangles[j][i + 1] = triangles[j][i];
					flippedTriangles[j][i + 2] = triangles[j][i + 2];
				}
			}

			return shape
				.Points
				.SelectMany((p, i) => flippedTriangles[GridPoint1.GetColor(p, colorFunction1)]
					.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint2> shape)
		{
			var flippedTriangles = new IntArray[triangles.Length];

			for (int j = 0; j < triangles.Length; j++)
			{
				flippedTriangles[j] = new IntArray()
				{
					data = triangles[j].ToList()
				};

				for (int i = 0; i < triangles[j].Count; i += 3)
				{
					flippedTriangles[j][i] = triangles[j][i + 1];
					flippedTriangles[j][i + 1] = triangles[j][i];
					flippedTriangles[j][i + 2] = triangles[j][i + 2];
				}
			}

			return shape
				.Points
				.SelectMany((p, i) => flippedTriangles[p.GetColor(colorFunction2)]
					.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint3> shape)
		{
			var flippedTriangles = new IntArray[triangles.Length];

			for (int j = 0; j < triangles.Length; j++)
			{
				flippedTriangles[j] = new IntArray()
				{
					data = triangles[j].ToList()
				};

				for (int i = 0; i < triangles[j].Count; i += 3)
				{
					flippedTriangles[j][i] = triangles[j][i + 1];
					flippedTriangles[j][i + 1] = triangles[j][i];
					flippedTriangles[j][i + 2] = triangles[j][i + 2];
				}
			}

			return shape
				.Points
				.SelectMany((p, i) => flippedTriangles[p.GetColor(colorFunction3)]
					.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<int> shape)
		{
			var points = shape.Points.ToArray();
			var triangleOffsets = new int[shape.Points.Count()];

			triangleOffsets[0] = 0;

			for (int i = 1; i < triangleOffsets.Length; i++)
			{
				triangleOffsets[i] = triangleOffsets[i - 1] + vertices[GridPoint1.GetColor(points[i - 1], colorFunction1)].Count;
			}

			return shape
				.Points
				.SelectMany((p, i) => triangles[GridPoint1.GetColor(p, colorFunction1)]
				.Select(t => t + triangleOffsets[i]));
		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint2> shape)
		{
			var points = shape.Points.ToArray();
			var triangleOffsets = new int[shape.Points.Count()];

			triangleOffsets[0] = 0;

			for (int i = 1; i < triangleOffsets.Length; i++)
			{
				triangleOffsets[i] = triangleOffsets[i - 1] + vertices[points[i - 1].GetColor(colorFunction2)].Count;
			}

			return shape
				.Points
				.SelectMany((p, i) => triangles[p.GetColor(colorFunction2)]
				.Select(t => t + triangleOffsets[i]));
		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint3> shape)
		{
			var points = shape.Points.ToArray();
			var triangleOffsets = new int[shape.Points.Count()];

			triangleOffsets[0] = 0;

			for (int i = 1; i < triangleOffsets.Length; i++)
			{
				triangleOffsets[i] = triangleOffsets[i - 1] + vertices[points[i - 1].GetColor(colorFunction3)].Count;
			}

			return shape
				.Points
				.SelectMany((p, i) => triangles[p.GetColor(colorFunction3)]
				.Select(t => t + triangleOffsets[i]));
		}

#endregion
	}
}