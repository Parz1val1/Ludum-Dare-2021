using UnityEngine;
using Interaction.Definitions;

namespace Interaction
{
    public class RecycleBinInteractable : MonoBehaviour
    {
        [SerializeField] private ClickableObject _interactable;
        [SerializeField] private Visibility _OnClickBehaviour = Visibility._onlyDisable;

        [SerializeField] private GameObject[] _obstacleToDisable;

        private int _clickCounter = 0;

        private void Start()
        {
            _interactable.ClickedDownEvent += IwasClicked;
            _interactable.ClickedUpEvent += IwasUnclicked;
        }

        private void IwasClicked()
        {
            if (_clickCounter < _obstacleToDisable.Length)
            {
                VisibilityDefinitions.ChangeVisibility(_obstacleToDisable[_clickCounter], _OnClickBehaviour);

                _clickCounter++;
            }
            else
            {
                //Play some sound effect or visual effect to signify that the user has finished using this object
            }
        }

        private void IwasUnclicked()
        {
            // Not Implemented
        }
    }
}
