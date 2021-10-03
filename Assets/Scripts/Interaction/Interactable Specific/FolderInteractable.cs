using System;
using UnityEngine;
using Interaction.Definitions;
using GameSystem;

namespace Interaction
{
    public enum FolderType
    {
        PowerUp,
        PlatformBuilder
    }

    public enum PowerUpType
    {
        ExtraTime,
        ExtraSpeed,
        FasterShooting
    }

    public class FolderInteractable: MonoBehaviour
    {
        [SerializeField] private ClickableObject _interactable;
        [SerializeField] private Visibility _OnClickBehaviour = Visibility._onlyEnable;
        [SerializeField] private FolderType _myFolderType = FolderType.PlatformBuilder;
        [SerializeField] private PowerUpType _myPowerUpType = PowerUpType.ExtraTime;

        [SerializeField] private GameObject[] _objectsToEnable;
        [SerializeField] private float _timeToAdd = 10;

        private int _clickCounter = 0;

        private void Start()
        {
            _interactable.ClickedDownEvent += IwasClicked;
            _interactable.ClickedUpEvent += IwasUnclicked;
        }

        private void IwasClicked()
        {
            if (_myFolderType == FolderType.PowerUp && _clickCounter < 1)
            {
                _clickCounter++;

                if (_myPowerUpType == PowerUpType.ExtraTime)
                {
                    TimerManager.AddTimeRemaining(_timeToAdd);
                }
                else if (_myPowerUpType == PowerUpType.ExtraSpeed)
                {
                    //Need access to run speed variable
                }
                else if (_myPowerUpType == PowerUpType.FasterShooting)
                {
                    //Need access to shooting speed variable
                }
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
