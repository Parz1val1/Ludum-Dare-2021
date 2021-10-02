using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class MouseClickListener : MonoBehaviour
    {
        private ClickableObject _clickedObject;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 click2D = new Vector2(clickpos.x, clickpos.y);

                RaycastHit2D hit = Physics2D.Raycast(click2D, Vector2.zero);

                if (hit.collider != null)
                {
                    _clickedObject = hit.transform.GetComponent<ClickableObject>();

                    if (_clickedObject != null)
                    {
                        _clickedObject.OnClickDown();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_clickedObject != null)
                {
                    _clickedObject.OnMouseClickUp();

                    _clickedObject = null;
                }
            }
        }
    }
}
