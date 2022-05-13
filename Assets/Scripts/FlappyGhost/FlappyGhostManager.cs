using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		[SerializeField] private Text _topScoreText;
		[SerializeField] private InteractableCanvasObject _input;
		[SerializeField] private GameObject _GameOverScreen;
		[SerializeField] private GameObject _StartScreen;

		[Header("Sounds")]
		[SerializeField] private AudioSource _audioSourse;
		[SerializeField] private AudioClip _scoreSound;
		[SerializeField] private AudioClip _gameOverSound;


		private bool _isGameOver;
		private bool _isGameStarted;

		private int _score;
		private int _topScore;

		private void Awake()
		{
			_ghost.AddScore += AddScore;
			_ghost.GameOver += GameOver;
			_input.OnPointerDownEvent += OnTap;

			_topScore = PlayerPrefs.GetInt("TopScoreFlappyGhoast");
			_topScoreText.text = "Top: " + _topScore.ToString();
		}
		private void Start()
		{
			_isGameOver = false;
			_isGameStarted = false;

		}


		private void StartMovement()
		{
			_ghost.StartMovement();
			_pipesController.StartSpawning(_spawnInterval, _movementSpeed, _hightthreshold);
			_backgroundController.SetBackgroundSpeed(_movementSpeed);
			_isGameOver = false;

		}

		private void AddScore()
		{
			_score++;
			_scoreText.text = "Score: " +  _score.ToString();
			_audioSourse.PlayOneShot(_scoreSound);

			if (_score > _topScore)
			{
				_topScore = _score;
				PlayerPrefs.SetInt("TopScoreFlappyGhoast", _topScore);
				_topScoreText.text = "Top: " + _topScore.ToString();

			}
		}

		private void GameOver()
		{
			if (_isGameOver)
			{
				return;
			}
			_audioSourse.PlayOneShot(_gameOverSound);

			_isGameOver = true;
			_pipesController.StopSpawning();
			_backgroundController.SetBackgroundSpeed(0f);
			_ghost.OnGameOver();
			_GameOverScreen.SetActive(true);

		}

		private void Restart()
		{
			_pipesController.DestroyAllPipes();

			StartMovement();
			_GameOverScreen.SetActive(false);
			_score = 0;
			_scoreText.text = "Score: " + _score.ToString();
		}
		private void OnTap(Vector3 position)
		{
			if (!_isGameStarted)
			{
				_StartScreen.SetActive(false);
				StartMovement();
				_isGameStarted = true;

			}

			if (_isGameOver)
			{
				Restart();
			}
			else
			{
				_ghost.Jump();
			}
		}

		private void OnDestroy()
		{
			_ghost.AddScore -= AddScore;
			_ghost.GameOver -= GameOver;
			_input.OnPointerDownEvent -= OnTap;


		}
	}
}