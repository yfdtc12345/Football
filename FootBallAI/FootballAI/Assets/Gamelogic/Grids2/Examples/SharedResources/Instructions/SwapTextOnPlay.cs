using Gamelogic.Extensions;
using UnityEngine.UI;

namespace Gamelogic.Grids2.Examples.Editor
{
	/// <summary>
	/// Swaps the active status of all children when the application starts.
	/// </summary>
	public class SwapTextOnPlay : GLMonoBehaviour
	{
		private void Start()
		{
			gameObject.GetComponent<Text>().enabled = !gameObject.GetComponent<Text>().enabled;
			Destroy(this);
		}
	}
}