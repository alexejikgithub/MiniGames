using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigames.FlappyGhost
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private BackgroundPart[] _backgroundParts;

        public void SetBackgroundSpeed(float speed)
		{
            foreach( BackgroundPart part in _backgroundParts)
			{
                part.SetBaseSpeed(speed);

            }
		}
    }
}