using System;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Class that provides utility methods for working with grids.
	/// </summary>
	[Version(2)]
	public static class GridBuilderUtils
	{
		/// <summary>
		/// The screen bounds as a 3D bounds object that can be used for aligning
		/// grids inside the screen.
		/// </summary>
		public static Bounds ScreenBounds
		{
			get
			{
				var center = new Vector3(
					-Screen.width / 2f,
					-Screen.height / 2f,
					-(Screen.width * Screen.height) / 2f);

				var size = new Vector3(
					Screen.width,
					Screen.height,
					Screen.width * Screen.height
					);

				return new Bounds(center, size);
			}
		}

		/// <summary>
		/// A convenience method to initialize a grid of SpriteCells from code.
		/// </summary>
		/// <param name="grid">A grid in which newly instantiate cells will be placed.</param>
		/// <param name="map">A map that will convert between grid points and world points.</param>
		/// <param name="cellPrefab">The prefab that will be used to instantiate cells from.</param>
		/// <param name="gridRoot">The object to use as root for the instantiated cells.</param>
		/// <param name="initCellAction">A function that can be used to initialize the cell at the given point.</param>
		/// <typeparam name="TPoint">The point type of the grid to initialize.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		public static void InitTileGrid<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map,
			TCell cellPrefab,
			GameObject gridRoot,
			Action<TPoint, TCell> initCellAction)

			where TCell : Component
		{
			grid.Fill(p => InitCell(map, cellPrefab, gridRoot, initCellAction, p));
		}

		/// <summary>
		/// Orientates cells according to the map.
		/// </summary>
		/// <typeparam name="TPoint">The point type of the grid.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		/// <param name="grid">The grid that contains the cells to re-orientate.</param>
		/// <param name="map">The map used to do the orientation calculations.</param>
		/// <remarks>New forward, right and up vectors are calculated by applying the map
		/// to the end points. For example, the up vector for a cell is (0, 1, 0). If the cell
		/// is at the world position (2, 2, 2) before the space map is applied, the endpoint of 
		/// the up vector is (2, 3, 2). The map is applied to both these points, and their difference
		/// is the "new" up vector. The cell is rotated so that it matches (as far as is possible)
		/// the new orientation vectors. Applying this method on cubes in a polar grid will make them 
		/// face the center.
		/// </remarks>
		public static void UpdateTileGridOrientations<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map)

			where TCell : MonoBehaviour
		{
			grid.Apply(p => UpdateOrientation(grid, map, p));
		}

		private static TCell InitCell<TPoint, TCell>(
			GridMap<TPoint> map,
			TCell cellPrefab,
			GameObject gridRoot,
			Action<TPoint, TCell> initCellAction,
			TPoint point)

			where TCell : Component
		{
			var cell = GLMonoBehaviour.Instantiate(cellPrefab, gridRoot);
			cell.name = cellPrefab.name + " " + point;
			cell.transform.localPosition = map.GridToWorld(point);

			initCellAction(point, cell);

			return cell;
		}

		private static void UpdateOrientation<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map,
			TPoint point)

			where TCell : MonoBehaviour
		{
			var cell = grid[point];

			var newForward = map.GridToWorld(map.DeRound(point) + new Vector3(0, 0, 1)) - cell.transform.localPosition;
			cell.transform.forward = newForward;

			var newRight = map.GridToWorld(map.DeRound(point) + new Vector3(1, 0, 0)) - cell.transform.localPosition;
			cell.transform.right = newRight;

			var newUp = map.GridToWorld(map.DeRound(point) + new Vector3(0, 1, 0)) - cell.transform.localPosition;
			cell.transform.right = newUp;
		}

		/// <summary>
		/// Assigns the properties (such as vertices and triangles) for a mesh for a shape.
		/// </summary>
		/// <param name="mesh">The mesh to modify.</param>
		/// <param name="shape">The logical shape of the resulting mesh (that is, the
		/// set of grid points it is meant to represent).</param>
		/// <param name="map">The map to use for calculating vertex positions.</param>
		/// <param name="meshData">The mesh data to use to construct the mesh.</param>
		/// <param name="doubleSided">Whether the mesh should be double sided.</param>
		/// <param name="flipTriangles">Whether triangles should be flipped.</param>
		public static void InitMesh(
			Mesh mesh,
			IExplicitShape<int> shape,
			GridMap<int> map,
			MeshData meshData,
			bool doubleSided,
			bool flipTriangles)
		{
			mesh.vertices = meshData.GetVertices(shape, map).ToArray();
			mesh.triangles = meshData.GetTriangles(shape, doubleSided, flipTriangles).ToArray();
			mesh.uv = meshData.GetUVs(shape).ToArray();
			//mesh.normals = meshData1.GetNormals(grid, map, doubleSided, flipTriangles).ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			//mesh.normals
		}

		/// <inheritdoc cref="InitMesh(Mesh, IExplicitShape{int}, GridMap{int}, MeshData, bool, bool)"/>
		public static void InitMesh(
			Mesh mesh,
			IExplicitShape<GridPoint2> shape,
			GridMap<GridPoint2> map,
			MeshData meshData,
			bool doubleSided,
			bool flipTriangles)
		{
			mesh.vertices = meshData.GetVertices(shape, map).ToArray();
			mesh.triangles = meshData.GetTriangles(shape, doubleSided, flipTriangles).ToArray();
			mesh.uv = meshData.GetUVs(shape).ToArray();
			//mesh.normals = meshData1.GetNormals(grid, map, doubleSided, flipTriangles).ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			//mesh.normals
		}

		/// <inheritdoc cref="InitMesh(Mesh, IExplicitShape{int}, GridMap{int}, MeshData, bool, bool)"/>
		public static void InitMesh(
			Mesh mesh,
			IExplicitShape<GridPoint3> shape,
			GridMap<GridPoint3> map,
			MeshData meshData,
			bool doubleSided,
			bool flipTriangles)
		{
			mesh.vertices = meshData.GetVertices(shape, map).ToArray();
			mesh.triangles = meshData.GetTriangles(shape, doubleSided, flipTriangles).ToArray();
			mesh.uv = meshData.GetUVs(shape).ToArray();
			//mesh.normals = meshData1.GetNormals(grid, map, doubleSided, flipTriangles).ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			//mesh.normals
		}
	}
}