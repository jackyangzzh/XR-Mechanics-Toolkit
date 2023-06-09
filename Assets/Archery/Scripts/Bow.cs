using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRMechanicsTookit.Archery
{
    public class Bow : XRGrabInteractable
    {
        [SerializeField]
        private GameObject arrow;

        [SerializeField]
        private Transform notch;

        private bool arrowNotched = false;
        private GameObject currArrow;

        protected override void OnEnable()
        {
            base.OnEnable();
            BowString.PullReleased += NotchEmpty;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            BowString.PullReleased -= NotchEmpty;
        }

        private void Update()
        {
            if (isSelected && !arrowNotched)
            {
                StartCoroutine(DelaySpawn());
            }
            if (!isSelected)
            {
                Destroy(currArrow);
            }
        }

        private void NotchEmpty(float val)
        {
            arrowNotched = false;
        }

        private IEnumerator DelaySpawn()
        {
            arrowNotched = true;
            yield return new WaitForSeconds(1f);
            currArrow = Instantiate(arrow, notch);
        }
    }
}
