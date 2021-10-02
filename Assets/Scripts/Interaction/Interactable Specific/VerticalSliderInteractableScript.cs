using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class VerticalSliderInteractableScript : MonoBehaviour
    {
        [SerializeField] private ClickableObject _sliderInteractable;
        [SerializeField] private GameObject _platformToMove;

        /// <summary>
        /// The distance from the sliders original position that it is allowed to move in either direction
        /// </summary>
        [SerializeField] private float _sliderBoundsFromCenter = 2f;
        [SerializeField] private float _platformBoundsFromCenter = 6f;

        private Vector2 _sliderOriginalPos2D;
        private Vector2 _platformOriginalPos2D;
        private bool _IamBeingClicked = false;

        private void Start()
        {
            _sliderInteractable.ClickedDownEvent += IwasClicked;
            _sliderInteractable.ClickedUpEvent += IwasUnclicked;

            _sliderOriginalPos2D = new Vector2(_sliderInteractable.transform.position.x, _sliderInteractable.transform.position.y);
            _platformOriginalPos2D = new Vector2(_platformToMove.transform.position.x, _platformToMove.transform.position.y);

        }

        private void FixedUpdate()
        {
            if (_IamBeingClicked)
            {
                Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 click2D = new Vector2(clickpos.x, clickpos.y);

                Vector2 newSliderPos;

                if (click2D.y < _sliderOriginalPos2D.y - _sliderBoundsFromCenter)
                {
                    newSliderPos = new Vector2(_sliderOriginalPos2D.x, _sliderOriginalPos2D.y - _sliderBoundsFromCenter);
                }
                else if (click2D.y > _sliderOriginalPos2D.y + _sliderBoundsFromCenter)
                {
                    newSliderPos = new Vector2(_sliderOriginalPos2D.x, _sliderOriginalPos2D.y + _sliderBoundsFromCenter);
                }
                else
                {
                    newSliderPos = new Vector2(_sliderOriginalPos2D.x, click2D.y);
                }

                _sliderInteractable.transform.position = new Vector3(_sliderOriginalPos2D.x, newSliderPos.y, _sliderInteractable.transform.position.z);

                //Normalizing slider position within its bounds to define platform position within its bounds
                float oldMax = _sliderOriginalPos2D.y + _sliderBoundsFromCenter;
                float oldMin = _sliderOriginalPos2D.y - _sliderBoundsFromCenter;
                float newMax = _platformOriginalPos2D.y + _platformBoundsFromCenter;
                float newMin = _platformOriginalPos2D.y - _platformBoundsFromCenter;
                float value = newSliderPos.y;
                float newValue;

                newValue = (newMax - newMin) / (oldMax - oldMin) * (value - oldMax) + newMax;

                _platformToMove.transform.position = new Vector3(_platformOriginalPos2D.x, newValue, _platformToMove.transform.position.x);
            }
        }

        private void IwasClicked()
        {
            _IamBeingClicked = true;
        }

        private void IwasUnclicked()
        {
            _IamBeingClicked = false;
        }
    }
}
