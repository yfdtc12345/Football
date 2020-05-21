using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// This class provides methods that
	/// can be used to construct a mesh for a grid, and is used
	/// in mesh builders.
	/// </summary>
	/// <remarks>
	/// <para> Some common cases are provided:</para>
	/// <list type="bullet">
	/// <item>
	/// <see cref="UniformGridMeshData"/> for grids where all cells are identical
	/// such as rect and hex grids.</item>
	///	<item> <see cref="PeriodicGridMeshData"/> for grids where patches of the 
	///	grid repeat periodically, such as triangular grids.
	///	</item>
	///	<item> <see cref="MeshStripMeshData"/> for grids that require automatic 
	///	division of cells along one dimension to look good, such as polar grids.
	///	</item>
	/// </list>
	/// <note type="implement">
	/// Not all methods need to be implemented for the data to be useful. 
	/// </note>
	/// <note type="implement">
	/// <para>The returned lists must be consistent.</para>
	/// <list type="bullet">
	/// <item>
	/// The number of vertices must match the number of UVs and normals.
	/// </item>
	/// <item>
	/// The order of the UVs, normals and vertex colors must match the order of the 
	/// vertices so that the nth UV and normal corresponds to the nth 
	/// vertex.
	/// </item>
	/// <item>
	/// The triangle indices must correspond to the order of the vertices, and so cannot
	/// exceed or equal the vertex count, and must be positive.
	/// </item>
	/// <item>Normals should correspond to the triangle winding.
	/// </item>
	/// </list>
	/// </note>
	/// </remarks>
	/// <seealso cref="MeshGridBuilder"/>
	[Serializable, Abstract]
	public class MeshData : ScriptableObject
	{
		/// <exlude />
		internal MeshData() { }

		/// <summary>
		/// Gets a list of vertices of a mesh for the given shape.
		/// </summary>
		/// <param name="shape">The shape for which vertices should be returned.</param>
		/// <param name="map">The map the mesh should be created with, so that centers of cells for a grid
		/// point match the world space position returned by the map.</param>
		/// <remarks> 
		/// <note type="implement">Override this method to support 1D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetVertices(IExplicitShape{int}, GridMap{int})"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 2D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetVertices(IExplicitShape{int}, GridMap{int})"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 3D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets a list of triangle indices for the given shape.
		/// </summary>
		/// <param name="shape">The shape for which vertices should be returned.</param>
		/// <param name="doubleSided">Whether the returned triangles should be double sided.</param>
		/// <param name="flip">Whether the returned triangles should be flipped.</param>
		/// <remarks> 
		/// <note type="implement">Override this method to support 1D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<int> GetTriangles(IExplicitShape<int> shape, bool doubleSided, bool flip)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetTriangles(IExplicitShape{int}, bool, bool)"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 2D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<int> GetTriangles(IExplicitShape<GridPoint2> shape, bool doubleSided, bool flip)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetTriangles(IExplicitShape{int}, bool, bool)"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 3D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<int> GetTriangles(IExplicitShape<GridPoint3> shape, bool doubleSided, bool flip)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets the UVs for the given shape.
		/// </summary>
		/// <param name="shape">The shape for which UVs should be returned.</param>
		/// <remarks> 
		/// <note type="implement">Override this method to support 1D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector2> GetUVs(IExplicitShape<int> shape)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetUVs(IExplicitShape{int})"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 2D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetUVs(IExplicitShape{int})"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 3D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets the normals for the given shape.
		/// </summary>
		/// <param name="shape">The shape for which to return the normals of.</param>
		/// <param name="gridMap">The grid map to be used to calculate normals.</param>
		/// <param name="flip">Whether normals should be flipped or not.</param>
		/// <remarks> 
		/// <note type="implement">Override this method to support 1D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector3> GetNormals(
			IExplicitShape<int> shape, 
			GridMap<int> gridMap, 
			bool flip)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetNormals(IExplicitShape{int}, GridMap{int}, bool)"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 2D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector3> GetNormals(
			IExplicitShape<GridPoint2> shape, 
			GridMap<GridPoint2> gridMap, 
			bool flip)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="GetNormals(IExplicitShape{int}, GridMap{int}, bool)"/>
		/// <remarks> 
		/// <note type="implement">Override this method to support 3D grids.</note>
		/// </remarks>
		[Abstract]
		public virtual IEnumerable<Vector3> GetNormals(
			IExplicitShape<GridPoint3> shape, 
			GridMap<GridPoint3> gridMap, 
			bool flip)
		{
			throw new NotSupportedException();
		}
	}
}