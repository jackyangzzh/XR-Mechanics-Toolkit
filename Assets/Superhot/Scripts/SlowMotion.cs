using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRMechanicsTookit.SuperHot
{
    public class SlowMotion : MonoBehaviour
    {
        [SerializeField]
        private CharacterController player = null;

        [SerializeField]
        private float slowTime = 0.1f;

        [SerializeField]
        private float normalTime = 1f;

        private bool isSlowMotion = false;

        private void Update()
        {
            Debug.Log(player.velocity.magnitude);
            if (player.velocity.magnitude > 0)
            {
                if (isSlowMotion)
                {
                    Time.timeScale = normalTime;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    isSlowMotion = false;
                }
            }
            else
            {
                if(!isSlowMotion)
                {
                    Time.timeScale = slowTime;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    isSlowMotion = true;
                }
            }
        }
    }
}