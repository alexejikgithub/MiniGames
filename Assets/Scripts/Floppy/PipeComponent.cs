using UnityEngine;
using DG.Tweening;

namespace Minigames.Pipes
{
	public class PipeComponent : MonoBehaviour
	{

		
		[SerializeField] private Transform _upperPipe;
		[SerializeField] private Transform _lowerPipe;

		public Transform UpperPipe => _upperPipe;
		public Transform LowerPipe => _lowerPipe;

		private ObjectPoolController _pool;

		private float _movementSpeed;

		private void Update()
		{
			MoveLeft();
		}
		private void MoveLeft()
		{
			transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);
		}

		public void SetPool(ObjectPoolController pool)
		{
			_pool = pool;
		}
		public void SetSpeed(float speed)
		{
			_movementSpeed = speed;
		}

		public void RemovePipe()
		{
			if(_pool!=null)
			{
				_pool.ReturnPooledGameObject(this.gameObject);
			}
			else 
			{
				Destroy(this.gameObject);
			}
		}


	}
}