using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRMechanicsTookit.Archery
{
    public class PullInteraction : XRBaseInteractable
    {
        public Transform Start;
        public Transform End;

        public GameObject Notch;
        public float PullAmount = 0.0f;

        public static event Action<float> PullReleased;

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
            Notch.transform.localPosition = new Vector3(Notch.transform.localPosition.x, Notch.transform.localPosition.y, 0f);
            UpdateString();
        }

        private float CalculatePull(Vector3 pullPos)
        {
            Vector3 pullDir = pullPos - Start.position;
            Vector3 targetDir = End.position - Start.position;
            float maxLength = targetDir.magnitude;
            targetDir.Normalize();

            float pullVal = Vector3.Dot(pullDir, targetDir) / maxLength;
            return Mathf.Clamp(pullVal, 0, 1);
        }

        private void UpdateString()
        {
            Vector3 linePos = Vector3.forward * Mathf.Lerp(Start.transform.localPosition.z, End.transform.localPosition.z, PullAmount);
            Vector3 notchPos = Notch.transform.localPosition;
            Notch.transform.localPosition = new Vector3(notchPos.x, notchPos.y, linePos.z + 0.2f);
            lineRenderer.SetPosition(1, linePos);
        }
    }
}