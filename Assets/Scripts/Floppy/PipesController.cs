using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigames.Pipes
{
	public class PipesController : MonoBehaviour
	{

		[SerializeField] private float _spawnInterval;
		[SerializeField] private float _movementSpeed;
		[SerializeField] private float _hightthreshold;
		[SerializeField] private ObjectPoolController _pool;
		[SerializeField] private Transform _spawnPosition;
		[SerializeField] private Transform _upperLimit;
		[SerializeField] private Transform _lowerLimit;

		private bool _isSpawning;

		private float _upperY;
		private float _lowerY;
		private bool _isYDefined = false;

		private void Awake()
		{

		}
		private void Start()
		{
			_isSpawning = true;
			StartCoroutine(SpawnPipesCoroutine());
		}

		public Transform SpawnTube()
		{
			Transform pipe = _pool.GetPooledGameObject().transform;
			PipeComponent pipeComponent = pipe.GetComponent<PipeComponent>();

			if (pipeComponent != null)
			{
				pipeComponent.SetSpeed(_movementSpeed);
				pipeComponent.SetPool(_pool);

				SetPipePosition(pipeComponent, pipe);
			}

			return pipe;
		}

		private IEnumerator SpawnPipesCoroutine()
		{

			while (_isSpawning)
			{
				SpawnTube();
				yield return new WaitForSeconds(_spawnInterval);
			}
		}

		private void SetPipePosition(PipeComponent pipeComponent, Transform pipe)
		{

			if (!_isYDefined)
			{
				_upperY = _upperLimit.position.y - (pipeComponent.UpperPipe.transform.position.y - pipeComponent.UpperPipe.transform.localScale.y / 2);
				_lowerY = _lowerLimit.position.y - (pipeComponent.LowerPipe.transform.position.y + pipeComponent.LowerPipe.transform.localScale.y / 2);
				_isYDefined = true;
			}
			

			float yPos = Random.Range(_lowerY + _hightthreshold, _upperY - _hightthreshold);

			pipe.position = new Vector3(_spawnPosition.position.x, yPos, 0);
		}
	}
}