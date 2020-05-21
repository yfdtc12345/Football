using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A cell that is rendered as a sprite.
	/// </summary>
	public sealed class SpriteCell : TileCell, IColorableCell
	{
		private SpriteRenderer spriteRenderer;

		private SpriteRenderer SpriteRenderer
		{
			get
			{
				if (spriteRenderer == null)
				{
					spriteRenderer = GetComponent<SpriteRenderer>();
				}

				return spriteRenderer;
			}
		}

		/// <summary>
		/// Gets the sprite for this cell.
		/// </summary>
		public Sprite Sprite
		{
			get { return SpriteRenderer.sprite; }
			set { SpriteRenderer.sprite = value; }
		}

		/// <inheritdoc/>
		public Color Color
		{
			get { return SpriteRenderer.color; }
			set { SpriteRenderer.color = value; }
		}
	}
}