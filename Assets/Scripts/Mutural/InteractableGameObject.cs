using UnityEngine;

namespace Minigames
{
	public class InteractableGameObject : GameplayInput
	{
		public delegate void OnMouseDownDelegate(Vector3 position);
		public event OnMouseDownDelegate OnMouseDownEvent;

		public delegate void OnMouseDragDelegate(Vector3 position);
		public event OnMouseDragDelegate OnMouseDragEvent;

		public delegate void OnMouseUpDelegate(Vector3 position);
		public event OnMouseUpDelegate OnMouseUpEvent;

		private bool _isGameplayInputOn = true;

		private void OnMouseDown()
		{
			if(!_isGameplayInputOn)
			{
				return;
			}

			OnMouseDownEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		private void OnMouseDrag()
		{
			if (!_isGameplayInputOn)
			{
				return;
			}
			OnMouseDragEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		private void OnMouseUp()
		{
			if (!_isGameplayInputOn)
			{
				return;
			}
			OnMouseUpEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}

		public override void GameplayInputOn()
		{
			_isGameplayInputOn = true;
		}

		public override void GameplayInputOff()
		{
			_isGameplayInputOn = false;
		}
	}

}