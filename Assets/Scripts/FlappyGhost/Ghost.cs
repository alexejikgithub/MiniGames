using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Minigames.FlappyGhost
{
	public class Ghost : MonoBehaviour
	{
		[SerializeField] private float _jumpSpeed;
		[SerializeField] private float _fallSpeed;
		[SerializeField] private float _jumpDuration;
		[SerializeField] private float _fallduration;
		[SerializeField] private Vector3 _maxRotation;
		[SerializeField] private Vector3 _minRotation;
		[SerializeField] private Rigidbody2D _rigidBody;

		[SerializeField] CustomSpriteAnimation _animation;

		[Header("Sounds")]
		[SerializeField] private AudioSource _audioSourse;
		[SerializeField] private AudioClip _jumpSound;

		private Coroutine _jumpCoroutine;
		private Coroutine _fallCoroutine;
		private Sequence _deathSequence;

		public Action AddScore;
		public Action GameOver;

		private void Awake()
		{
			
		}

		private void Start()
		{


		}
		private void Update()
		{


		}

		public void StartMovement()
		{
			if (_deathSequence != null)
			{
				_deathSequence.Kill();
			}
			DOTween.Kill(this);

			transform.eulerAngles = new Vector3(0, 0, -1);
			transform.position = Vector3.zero;
			_fallCoroutine = StartCoroutine(FallCoroutine());
			_animation.SetClip("Idle");

		}
		public void Jump()
		{

			if (_jumpCoroutine != null)
			{
				StopCoroutine(_jumpCoroutine);
			}
			_jumpCoroutine = StartCoroutine(JumpCoroutine());
			_audioSourse.PlayOneShot(_jumpSound);
		}

		private IEnumerator JumpCoroutine()
		{
			if (_fallCoroutine != null)
			{
				StopCoroutine(_fallCoroutine);
			}
			_rigidBody.velocity = Vector2.up * _jumpSpeed;

			float elapsedTime = 0;
			Vector3 startRotation = transform.eulerAngles;
			while (elapsedTime < _jumpDuration)
			{

				transform.eulerAngles = Vector3.Lerp(startRotation, _maxRotation, (elapsedTime / _jumpDuration));
				elapsedTime += Time.deltaTime;


				yield return null;
			}
			transform.eulerAngles = _maxRotation;

			_fallCoroutine = StartCoroutine(FallCoroutine());
		}

		private IEnumerator FallCoroutine()
		{

			float elapsedTime = 0;
			Vector2 startVelocity = _rigidBody.velocity;
			Vector3 startRotation = transform.eulerAngles;
			while (elapsedTime < _fallduration)
			{
				_rigidBody.velocity = Vector2.Lerp(startVelocity, Vector2.down * _fallSpeed, (elapsedTime / _fallduration));
				transform.eulerAngles = Vector3.Lerp(startRotation, _minRotation, (elapsedTime / _fallduration));
				elapsedTime += Time.deltaTime;


				yield return null;
			}
			_rigidBody.velocity = Vector2.down * _fallSpeed;
			transform.eulerAngles = _minRotation;
		}

		public void OnGameOver()
		{
			StopAllCoroutines();
			_rigidBody.velocity = Vector2.zero;
			transform.eulerAngles = Vector3.zero;
			_animation.SetClip("Death");


			_deathSequence.Kill();
			_deathSequence = DOTween.Sequence();
			_deathSequence.Append(transform.DOMoveY(transform.position.y + 1f, 0.5f));
			_deathSequence.Append(transform.DOMoveY(transform.position.y - 20f, 2f));
			_deathSequence.Play();

		}

		private void OnTriggerEnter2D(Collider2D collision)
		{

			ScorePoint scorePoint = collision.GetComponent<ScorePoint>();
			if (scorePoint != null)
			{
				AddScore.Invoke();
			}


			Obstacle obstacle = collision.GetComponent<Obstacle>();
			if (obstacle != null)
			{
				GameOver.Invoke();

			}
		}

	}
}