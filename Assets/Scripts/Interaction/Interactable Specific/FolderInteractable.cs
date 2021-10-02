using UnityEngine;
using Interaction.Definitions;

namespace Interaction
{
    public enum FolderType
    {
        PowerUp,
        PlatformBuilder
    }

    public class FolderInteractable: MonoBehaviour
    {
        [SerializeField] private ClickableObject _interactable;
        [SerializeField] private Visibility _OnClickBehaviour = Visibility._onlyEnable;
        [SerializeField] private FolderType _myFolderType = FolderType.PlatformBuilder;

        [SerializeField] private GameObject[] _objectsToEnable;

        private int _clickCounter = 0;

        private void Start()
        {
            _interactable.ClickedDownEvent += IwasClicked;
            _interactable.ClickedUpEvent += IwasUnclicked;
        }

        private void IwasClicked()
        {
            if (_myFolderType == FolderType.PowerUp)
            {
                //Power Up functionality needs to be fleshed out
                //Could be extra time
                //Could be movement speed
                //Could be faster shooting
            }
            else if (_myFolderType == FolderType.PlatformBuilder)
            {
                if (_clickCounter < _objectsToEnable.Length)
                {
                    VisibilityDefinitions.ChangeVisibility(_objectsToEnable[_clickCounter], _OnClickBehaviour);

                    _clickCounter++;
                }
                else
                {
                    //Play some sound effect or visual effect to signify that the user has finished using this object
                }
            }
        }

        private void IwasUnclicked()
        {
            // Not Implemented
        }
    }
}
