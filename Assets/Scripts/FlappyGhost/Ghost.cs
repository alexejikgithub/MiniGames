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
		[SerializeField] InteractableCanvasObject _input;
		[SerializeField] CustomSpriteAnimation _animation;

		private Coroutine _jumpCoroutine;
		private Coroutine _fallCoroutine;

		public Action AddScore;
		public Action GameOver;

		private void Awake()
		{
			_input.OnPointerDownEvent += Jump;
		}

		private void Start()
		{
			_fallCoroutine = StartCoroutine(FallCoroutine());
			_animation.SetClip("Idle");

		}
		private void Update()
		{


		}
		public void Jump(Vector3 position)
		{

			if (_jumpCoroutine != null)
			{
				StopCoroutine(_jumpCoroutine);
			}
			_jumpCoroutine = StartCoroutine(JumpCoroutine());
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
			Debug.Log(transform.eulerAngles);
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
				transform.eulerAngles = Vector3.Lerp(startRotation,_minRotation, (elapsedTime / _fallduration));
				elapsedTime += Time.deltaTime;


				yield return null;
			}
			_rigidBody.velocity = Vector2.down * _fallSpeed;
			transform.eulerAngles = _minRotation;
		}

		public void OnGameOver()
		{
			Debug.Log("Death");
			StopAllCoroutines();
			_rigidBody.velocity = Vector2.zero;
			transform.eulerAngles = _maxRotation;
			_animation.SetClip("Death");

			transform.DOMoveY(transform.position.y + 1f, 0.5f).OnComplete(()=>transform.DOMoveY(transform.position.y - 20f, 2f));
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{

			ScorePoint scorePoint = collision.GetComponent<ScorePoint>();
			if(scorePoint != null)
			{
				AddScore.Invoke();
			}


			Obstacle obstacle = collision.GetComponent<Obstacle>();
			if (obstacle != null)
			{
				GameOver.Invoke();

			}
		}

		private void OnDestroy()
		{
			_input.OnPointerDownEvent -= Jump;
		}
	}
}