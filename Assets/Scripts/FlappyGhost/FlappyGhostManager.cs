using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		[SerializeField] private Text _scoreText;
		[SerializeField] private InteractableCanvasObject _input;

		private bool _isGameplayOn;

		private int _score;

		private void Awake()
		{
			_ghost.AddScore += AddScore;
			_ghost.GameOver += GameOver;
		}
		private void Start()
		{
			_isGameplayOn = true;
			_pipesController.StartSpawning(_spawnInterval, _movementSpeed, _hightthreshold);
			_backgroundController.SetBackgroundSpeed(_movementSpeed);
		}

		private void AddScore()
		{
			_score++;
			_scoreText.text = _score.ToString();
		}

		private void GameOver()
		{
			if(!_isGameplayOn)
			{
				return;
			}
			_isGameplayOn = false;
			_pipesController.StopSpawning();
			_backgroundController.SetBackgroundSpeed(0f);
			_input.GameplayInputOff();
			_ghost.OnGameOver();

		}

		private void OnDestroy()
		{
			_ghost.AddScore -= AddScore;
			_ghost.GameOver -= GameOver;

		}
	}
}