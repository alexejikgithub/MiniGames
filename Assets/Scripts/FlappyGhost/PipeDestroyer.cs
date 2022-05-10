using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigames.FlappyGhost
{
	public class PipeDestroyer : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			
			PipeComponent pipe = collision.gameObject.GetComponent<PipeComponent>();
			if (pipe != null)
			{
				pipe.RemovePipe();
			}
		}
	}
}