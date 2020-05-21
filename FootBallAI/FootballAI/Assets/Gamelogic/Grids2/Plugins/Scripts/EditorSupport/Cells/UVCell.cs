using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Similar to a sprite cell, but with custom UV coordinates.
	/// This type of cell is useful when placing a single texture
	/// across multiple cells.
	/// </summary>
	[Experimental]
	public sealed class UVCell : TileCell, IColorableCell
	{
		#region Inspector Fields
		[SerializeField]
		private Color color;

		[SerializeField]
		private Texture2D texture;

		[SerializeField]
		private Vector2 textureScale;

		[SerializeField]
		private Vector2 textureOffset;
		#endregion 

		#region Private Fields
		[SerializeField]
		[HideInInspector]
		private Material material;
		#endregion

		#region Properties
		/// <inheritdoc/>
		public Color Color
		{
			get { return color; }

			set
			{
				color = value;
				UpdatePresentation();
			}
		}

		/// <summary>
		/// Gets the material used by this cell.
		/// </summary>
		public Material Material
		{
			get { return material; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Sets the texture to be used by this cell
		/// </summary>
		/// <param name="texture">The texture to use.</param>
		public void SetTexture(Texture2D texture)
		{
			this.texture = texture;
			UpdatePresentation();
		}

		/// <summary>
		/// Sets the UV scale and offset 
		/// to use in the material for this cell.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="scale"></param>
		public void SetUVs(Vector2 offset, Vector2 scale)
		{
			textureOffset = offset;
			textureScale = scale;
			UpdatePresentation();
		}
		#endregion

		#region Unity messages
		/// <summary>
		/// Called by the game engine.
		/// </summary>
		public void OnDestroy() //TODO: consider making this private
		{
			DestroyImmediate(material);
		}
		#endregion

		#region Private methods
		private void UpdatePresentation()
		{
			if (material == null)
			{
				material = new Material(GetComponent<Renderer>().sharedMaterial); //only duplicate once
			}

			material.color = color;
			material.mainTexture = texture;
			material.mainTextureOffset = textureOffset;
			material.mainTextureScale = textureScale;

			GetComponent<Renderer>().material = material;
		}
		#endregion
	}
}