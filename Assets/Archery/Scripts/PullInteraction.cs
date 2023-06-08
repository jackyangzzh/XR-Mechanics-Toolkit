using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRMechanicsTookit.Archery
{
    [RequireComponent(typeof(LineRenderer))]
    public class PullInteraction : XRBaseInteractable
    {
        public static event Action<float> PullReleased;

        [SerializeField]
        private Transform start;

        [SerializeField]
        private Transform end;

        [SerializeField]
        private GameObject notch;

        private float PullAmount = 0.0f;
        private LineRenderer lineRenderer;
        private IXRSelectInteractor pullInteractor = null;

        protected override void Awake()
        {
            base.Awake();
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetPullInteractor(SelectEnterEventArgs args)
        {
            pullInteractor = args.interactorObject;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
            {
                Vector3 pullPos = pullInteractor.transform.position;
                PullAmount = CalculatePull(pullPos);
                UpdateString();
            }
        }

        public void Release()
        {
            PullReleased?.Invoke(PullAmount);
            pullInteractor = null;
            PullAmount = 0f;
            notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
            UpdateString();
        }

        private float CalculatePull(Vector3 pullPos)
        {
            Vector3 pullDir = pullPos - start.position;
            Vector3 targetDir = end.position - start.position;
            float maxLength = targetDir.magnitude;
            targetDir.Normalize();

            float pullVal = Vector3.Dot(pullDir, targetDir) / maxLength;
            return Mathf.Clamp(pullVal, 0, 1);
        }

        private void UpdateString()
        {
            Vector3 linePos = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, PullAmount);
            Vector3 notchPos = notch.transform.localPosition;
            notch.transform.localPosition = new Vector3(notchPos.x, notchPos.y, linePos.z + 0.2f);
            lineRenderer.SetPosition(1, linePos);
        }
    }
}