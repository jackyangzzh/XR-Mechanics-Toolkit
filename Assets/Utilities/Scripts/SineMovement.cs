using UnityEngine;

namespace XRMechanicsTookit.Utilities
{
    public enum Axis
    {
        XAxis,
        YAxis,
        ZAxis
    }

    public class SineMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.0f;

        [SerializeField]
        private float amplitude = 1.0f;

        [SerializeField]
        private Axis axis = Axis.XAxis;

        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            switch (axis)
            {
                case Axis.XAxis:
                    transform.position = startPos + new Vector3(Mathf.Sin(Time.time * speed) * amplitude, 0, 0);
                    break;
                case Axis.YAxis:
                    transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * speed) * amplitude, 0);
                    break;
                case Axis.ZAxis:
                    transform.position = startPos + new Vector3(0, 0, Mathf.Sin(Time.time * speed) * amplitude);
                    break;
            }
        }
    }
}
