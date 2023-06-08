using UnityEngine;

namespace XRMechanicsTookit.Archery
{
    [RequireComponent(typeof(Rigidbody))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private Transform tip;

        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            PullInteraction.PullReleased += Release;
            Stop();
        }

        private void OnDestroy()
        {
            PullInteraction.PullReleased -= Release;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

        private void Release(float val)
        {
            PullInteraction.PullReleased -= Release;
            gameObject.transform.parent = null;
            SetPhysics(true);

            Vector3 force = transform.forward * val * speed;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void Stop()
        {
            SetPhysics(false);
        }

        private void SetPhysics(bool val)
        {
            rigidbody.useGravity = val;
            rigidbody.isKinematic = !val;
        }
    }
}
