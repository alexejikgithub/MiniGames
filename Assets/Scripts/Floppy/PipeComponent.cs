using UnityEngine;
using DG.Tweening;

namespace Minigames.Pipes
{
	public class PipeComponent : MonoBehaviour
	{

		[SerializeField] private float _movementSpeed;


		private void Update()
		{
			MoveLeft();
		}
		private void MoveLeft()
		{
			transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);
		}
	}
}