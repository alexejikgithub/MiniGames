
using UnityEngine;
using DG.Tweening;

namespace Minigames.FlappyGhost
{
	public class PipeComponent : MonoBehaviour
	{

		
		[SerializeField] private Transform _upperPipe;
		[SerializeField] private Transform _lowerPipe;
		[SerializeField] private float _pipeHight;

		public Transform UpperPipe => _upperPipe;
		public Transform LowerPipe => _lowerPipe;
		public float PipeHight => _pipeHight;

		private ObjectPoolController _pool;

		private PipesController _controller;



		private void Update()
		{
			MoveLeft();
		}
		private void MoveLeft()
		{
			transform.Translate(Vector3.left * _controller.MovementSpeed * Time.deltaTime);
		}

		public void SetPool(ObjectPoolController pool)
		{
			_pool = pool;
		}
		public void SetPipesController(PipesController controller)
		{
			_controller = controller;
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