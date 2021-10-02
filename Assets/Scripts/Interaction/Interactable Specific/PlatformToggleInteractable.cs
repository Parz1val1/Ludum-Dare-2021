using UnityEngine;
using Interaction.Definitions;

namespace Interaction
{
    public class PlatformToggleInteractable : MonoBehaviour
    {
        [SerializeField] private ClickableObject _interactable;
        [SerializeField] private GameObject _checkMarkUI;
        [SerializeField] private Visibility _OnClickBehaviour = Visibility._toggleVisibility;

        [SerializeField] private GameObject[] _platformsToEnable;

        private void Start()
        {
            _interactable.ClickedDownEvent += IwasClicked;
            _interactable.ClickedUpEvent += IwasUnclicked;
        }

        private void IwasClicked()
        {
            for (int i = 0; i < _platformsToEnable.Length; i++)
            {
                VisibilityDefinitions.ChangeVisibility(_platformsToEnable[i], _OnClickBehaviour);
            }

            VisibilityDefinitions.ChangeVisibility(_checkMarkUI, _OnClickBehaviour);
        }

        private void IwasUnclicked()
        {
            // Not Implemented
        }
    }
}
