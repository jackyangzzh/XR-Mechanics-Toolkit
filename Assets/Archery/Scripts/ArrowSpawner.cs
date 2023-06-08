using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRMechanicsTookit.Archery
{
    public class ArrowSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject arrow;

        [SerializeField]
        private GameObject notch;

        private XRGrabInteractable bow;
        private bool arrowNotched = false;
        private GameObject currArrow;

        private void Awake()
        {
            bow = GetComponentInParent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            PullInteraction.PullReleased += NotchEmpty;
        }

        private void OnDestroy()
        {
            PullInteraction.PullReleased -= NotchEmpty;
        }

        private void Update()
        {
            if (bow.isSelected && !arrowNotched)
            {
                StartCoroutine(DelaySpawn());
            }
            if (!bow.isSelected)
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
            yield return new WaitForSeconds(0.5f);
            arrow = Instantiate(arrow, notch.transform);
        }
    }
}
