using UnityEngine;

namespace XRMechanicsTookit.Archery
{
    [RequireComponent(typeof(Rigidbody))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private float destroyTimer = 5f;

        private Rigidbody rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            BowString.PullReleased += Release;
            Stop();
        }

        private void OnDestroy()
        {
            BowString.PullReleased -= Release;
        }

        private void Release(float val)
        {
            BowString.PullReleased -= Release;
            gameObject.transform.parent = null;
            SetPhysics(true);
            rigidBody.AddForce(transform.forward * val * speed, ForceMode.Impulse);

            Destroy(gameObject, destroyTimer);
        }

        private void Stop()
        {
            SetPhysics(false);
        }

        private void SetPhysics(bool val)
        {
            rigidBody.useGravity = val;
            rigidBody.isKinematic = !val;
        }
    }
}
