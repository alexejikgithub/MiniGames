using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.FlappyGhost
{
	public class Flapper : MonoBehaviour
	{
		[SerializeField] private float _jumpSpeed;
		[SerializeField] private float _fallSpeed;
		[SerializeField] private float _jumpDuration;
		[SerializeField] private float _fallduration;
		[SerializeField] private Rigidbody2D _rigidBody;

		private Coroutine _jumpCoroutine;
		private Coroutine _fallCoroutine;

		private bool _isJumping = false;
		private void Start()
		{
			_fallCoroutine  = StartCoroutine(FallCoroutine());

		}
		private void Update()
		{
			

		}
		public void Jump()
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

			yield return new WaitForSeconds(_jumpDuration);
			_fallCoroutine = StartCoroutine(FallCoroutine());


		}

		private IEnumerator FallCoroutine()
		{
			float elapsedTime = 0;
			Vector2 startVelocity = _rigidBody.velocity;
			while (elapsedTime < _fallduration)
			{
				Debug.Log(_rigidBody.velocity);
				_rigidBody.velocity = Vector2.Lerp(startVelocity, Vector2.down* _fallSpeed, (elapsedTime / _fallduration));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			_rigidBody.velocity = Vector2.down * _fallSpeed;
		}
	}
}