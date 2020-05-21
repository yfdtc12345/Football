using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A tile cell that uses a mesh for its visual presentation.
	/// </summary>
	[AddComponentMenu("Gamelogic/Grids 2/Cells/Mesh Tile Cell")]
	public sealed class MeshTileCell : TileCell, IColorableCell
	{
		#region Private Fields
		private MeshRenderer meshRenderer;
		private MeshFilter meshFilter;
		#endregion

		#region Properties
		/// <summary>
		/// Gets this cell's mesh renderer.
		/// </summary>
		private MeshRenderer MeshRenderer
		{
			get
			{
				if (meshRenderer == null)
				{
					meshRenderer = this.GetRequiredComponent<MeshRenderer>();
				}

				return meshRenderer;
			}
		}

		/// <summary>
		/// Gets the mesh filter used by this cell.
		/// </summary>
		private MeshFilter MeshFilter
		{
			get
			{
				if (meshFilter == null)
				{
					meshFilter = this.GetRequiredComponent<MeshFilter>();
				}

				return meshFilter;
			}
		}

		/// <summary>
		/// Gets the shared material used by this cell.
		/// </summary>
		public Material SharedMaterial
		{
			get { return MeshRenderer.sharedMaterial; }
			set { MeshRenderer.sharedMaterial = value; }
		}

		/// <summary>
		/// Gets the shared mesh used by this cell.
		/// </summary>
		public Mesh SharedMesh
		{
			get { return MeshFilter.sharedMesh; }
			set { MeshFilter.sharedMesh = value; }
		}

		/// <inheritdoc/>
		public Color Color
		{
			get { return SharedMaterial.color; }
			set
			{
				var material = new Material(MeshRenderer.sharedMaterial) { color = value };
				MeshRenderer.sharedMaterial = material;
			}
		}
		#endregion
	}
}