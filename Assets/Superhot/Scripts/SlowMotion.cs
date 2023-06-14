using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRMechanicsTookit.SuperHot
{
    public class SlowMotion : MonoBehaviour
    {
        [SerializeField]
        private float slowTime = 0.2f;

        [SerializeField]
        private float normalTime = 1f;

        private bool isSlowMotion = false;
        private Vector3 lastPosition;
        private Vector3 velocity;

        private void Start()
        {
            lastPosition = transform.position;
        }

        private void Update()
        {
            velocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;

            if (velocity.magnitude > 0)
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
                if (!isSlowMotion)
                {
                    Time.timeScale = slowTime;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    isSlowMotion = true;
                }
            }
        }
    }
}