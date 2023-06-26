using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRMechanicsTookit.Puzzle
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class PuzzleHelper : MonoBehaviour
    {
        [SerializeField]
        private string correctPieceTag = "";

        private bool isCorrect = false;
        public bool IsCorrect => isCorrect;

        private XRSocketInteractor socketInteractor;

        private void Awake()
        {
            socketInteractor = GetComponent<XRSocketInteractor>();
        }

        public void CheckCorrectness()
        {
            var selectedInteractable = socketInteractor.firstInteractableSelected;

            if (selectedInteractable == null || selectedInteractable.transform.gameObject.CompareTag(correctPieceTag))
            {
                isCorrect = false;
                return;
            }

            isCorrect = true;
        }
    }

}