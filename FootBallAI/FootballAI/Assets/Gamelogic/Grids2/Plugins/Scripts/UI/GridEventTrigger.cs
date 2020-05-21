using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// This is a simple event trigger for 2D grids. To use it, add it on your grid builder, and
	/// link in the methods you want to trigger in the respective fields.
	/// </summary>
	/// <seealso cref="GridBehaviour{TPoint, TCell}" />
	/// <conceptualLink target="12acc43b-6ea5-4828-85bd-0f9e2f9fdd25"/>
	public class GridEventTrigger : GridBehaviour<GridPoint2, TileCell>
	{
		//TODO: Should this class be sealed?
		#region Inspector
		[SerializeField]
		private Camera uiCamera;

		[SerializeField]
		private GridEvent onLeftMouseButtonDown;

		[SerializeField]
		private GridEvent onRightMouseButtonDown;
		#endregion

		#region Properties		
		/// <summary>
		/// Gets the mouse position as a grid point.
		/// </summary>
		/// <value>The mouse position.</value>
		public GridPoint2 MousePosition
		{
			get
			{
				return GridUtils.MousePositionToGrid(transform, uiCamera, GridMap);
			}
		}

		/// <summary>
		/// Gets or sets the camera used to interpret mouse events, usually
		/// the camera used to render the UI.
		/// </summary>
		public Camera UICamera
		{
			get { return uiCamera; }
			set { uiCamera = value; }
		}

		/// <summary>
		/// Gets the GridEvent associate with clicking
		/// the left mouse button.
		/// </summary>
		public GridEvent OnLeftMouseButtonDown
		{
			get
			{
				if (onLeftMouseButtonDown == null)
				{
					onLeftMouseButtonDown = new GridEvent();
				}

				return onLeftMouseButtonDown;
			}
		}

		/// <summary>
		/// Gets the GridEvent associate with clicking
		/// the right mouse button.
		/// </summary>
		public GridEvent OnRightMouseButtonDown
		{
			get
			{
				if (onRightMouseButtonDown == null)
				{
					onRightMouseButtonDown = new GridEvent();
				}

				return onRightMouseButtonDown;
			}
		}

		#endregion

		#region Messages
		/// <summary>
		/// Called by the game engine.
		/// </summary>
		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (Grid.Contains(MousePosition))
				{
					if (onLeftMouseButtonDown != null)
					{
						onLeftMouseButtonDown.Invoke(MousePosition);
					}
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				if (Grid.Contains(MousePosition))
				{
					if (onRightMouseButtonDown != null)
					{
						onRightMouseButtonDown.Invoke(MousePosition);
					}
				}
			}
		}

		#endregion
	}
}