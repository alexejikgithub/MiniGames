using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPart : MonoBehaviour
{
	[SerializeField] private float _speedMultiplier;
	[SerializeField] private float _width;
	[SerializeField] private BackgroundPart _anotherBackground;
	[SerializeField] private bool _isFirst;

	

	private float _baseSpeed;
	
	private float _speed => _baseSpeed * _speedMultiplier;

	public float Width => _width;
	public float LeftEdgePosition { get; set; }


	private void Awake()
	{
		if (_isFirst)
		{
			LeftEdgePosition = transform.position.x;
			_anotherBackground.LeftEdgePosition = LeftEdgePosition;
			
		}
	}

	private void Update()
	{
		if (transform.position.x < LeftEdgePosition - _width)
		{



			float width = _anotherBackground.Width;
			transform.position = _anotherBackground.transform.position + new Vector3(width - 0.01f, 0);
		}

		transform.Translate(Vector3.left * Time.deltaTime * _speed);
	}
	public void SetBaseSpeed(float speed)
	{
		_baseSpeed = speed;
	}



}
