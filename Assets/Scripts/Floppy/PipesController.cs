using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigames.Pipes
{
    public class PipesController : MonoBehaviour
    {

		[SerializeField] private float _spawnInterval;
		[SerializeField] private float _hightthreshold;
		[SerializeField] private ObjectPoolController _pool;
		[SerializeField] private Transform _spawnPosition;
		[SerializeField] private Transform _upperLimit;
		[SerializeField] private Transform _lowerLimit;

		private bool _isSpawning;

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

			if(pipeComponent!=null)
			{
				
				pipeComponent.SetPool(_pool);

				// TODO Fix
				float yPos = Random.Range(_lowerLimit.position.y + pipeComponent.LowerPipe.transform.position.y + _hightthreshold, _upperLimit.position.y + pipeComponent.UpperPipe.transform.position.y - _hightthreshold);

				pipe.position = new Vector3(_spawnPosition.position.x, yPos, 0) ;
			}
			

			

			return pipe;
		}

		private IEnumerator SpawnPipesCoroutine()
		{
			
			while(_isSpawning)
			{
				SpawnTube();
				yield return new WaitForSeconds(_spawnInterval);
			}
		}
	}
}