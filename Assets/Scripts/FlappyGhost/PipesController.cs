using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigames.FlappyGhost
{
	public class PipesController : MonoBehaviour
	{



		[SerializeField] private ObjectPoolController _pool;
		[SerializeField] private Transform _spawnPosition;
		[SerializeField] private Transform _upperLimit;
		[SerializeField] private Transform _lowerLimit;

		[SerializeField] private bool _isSpawning;

		private float _upperY;
		private float _lowerY;
		private bool _isYDefined = false;

		private float _spawnInterval;
		private float _movementSpeed;
		private float _hightThreshold;


		private Coroutine _spawnPipesCoroutine;

		public float MovementSpeed => _movementSpeed;

		private void Awake()
		{

		}
		private void Start()
		{
			
			
		}

		public void StartSpawning(float interval, float speed, float threshold)
		{
			 _spawnInterval = interval;
			 _movementSpeed= speed;
			 _hightThreshold= threshold;
			_isSpawning = true;
			_spawnPipesCoroutine = StartCoroutine(SpawnPipesCoroutine());
		}

		public void StopSpawning()
		{
			_movementSpeed= 0;
			StopCoroutine(_spawnPipesCoroutine);
			_isSpawning = false;
		}

		public Transform SpawnTube()
		{
			Transform pipe = _pool.GetPooledGameObject().transform;
			PipeComponent pipeComponent = pipe.GetComponent<PipeComponent>();

			if (pipeComponent != null)
			{
				pipeComponent.SetPipesController(this);
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
				_upperY = _upperLimit.position.y - (pipeComponent.UpperPipe.transform.position.y - pipeComponent.PipeHight / 2);
				_lowerY = _lowerLimit.position.y - (pipeComponent.LowerPipe.transform.position.y + pipeComponent.PipeHight / 2);
				_isYDefined = true;
			}


			float yPos = Random.Range(_lowerY + _hightThreshold, _upperY - _hightThreshold);

			pipe.position = new Vector3(_spawnPosition.position.x, yPos, 0);
		}
	}
}