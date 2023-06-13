using UnityEngine;

namespace XRMechanicsTookit.Utilities
{
    public class SineMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.0f;

        [SerializeField]
        private float amplitude = 1.0f;

        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            transform.position = startPos + new Vector3(Mathf.Sin(Time.time * speed) * amplitude, 0, 0);
        }
    }
}
