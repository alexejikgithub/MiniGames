using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.FlappyGhost
{
	public class FlappyGhostManager : MonoBehaviour
	{
		[Header("PipesController")]
		[SerializeField] private float _spawnInterval;
		[SerializeField] private float _movementSpeed;
		[SerializeField] private float _hightthreshold;

		[Header("GameObjects")]
		[SerializeField] private Ghost _ghost;
		[SerializeField] private PipesController _pipesController;


		private int _score;

		private void Awake()
		{
			_ghost.AddScore += AddScore;
			_ghost.GameOver += GameOver;
		}
		private void Start()
		{
			_pipesController.StartSpawning(_spawnInterval, _movementSpeed, _hightthreshold);
		}

		private void AddScore()
		{
			_score++;
			Debug.Log(_score);
		}

		private void GameOver()
		{
			_pipesController.StopSpawning();
		}

		private void OnDestroy()
		{
			_ghost.AddScore -= AddScore;
			_ghost.GameOver -= GameOver;

		}
	}
}