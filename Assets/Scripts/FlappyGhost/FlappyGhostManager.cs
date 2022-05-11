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
		[SerializeField] private BackgroundController _backgroundController;


		private int _score;

		private void Awake()
		{
			_ghost.AddScore += AddScore;
			_ghost.GameOver += GameOver;
		}
		private void Start()
		{
			_pipesController.StartSpawning(_spawnInterval, _movementSpeed, _hightthreshold);
			_backgroundController.SetBackgroundSpeed(_movementSpeed);
		}

		private void AddScore()
		{
			_score++;
			Debug.Log(_score);
		}

		private void GameOver()
		{
			_pipesController.StopSpawning();
			_backgroundController.SetBackgroundSpeed(0f);
		}

		private void OnDestroy()
		{
			_ghost.AddScore -= AddScore;
			_ghost.GameOver -= GameOver;

		}
	}
}