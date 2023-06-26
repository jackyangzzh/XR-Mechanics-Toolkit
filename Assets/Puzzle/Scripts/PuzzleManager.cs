using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace XRMechanicsTookit.Puzzle
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField]
        private List<PuzzleHelper> puzzleHelpers;

        [SerializeField]
        private TextMeshProUGUI stateText;

        private const string FailText = "Keep Trying!";
        private const string SuccessText = "You Did it!";

        private void Update()
        {
            if (IsPuzzleCompleted())
            {
                stateText.text = SuccessText;
                stateText.color = Color.green;
            }
            else
            {
                stateText.text = FailText;
                stateText.color = Color.yellow;
            }
        }

        private bool IsPuzzleCompleted()
        {
            foreach (var helper in puzzleHelpers)
            {
                if (!helper.IsCorrect)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
