using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class MouseClickListener : MonoBehaviour
    {
        private Stack<ClickableObject> _clickedObjectStack;
        private ClickableObject _clickedObject;

        private void Start()
        {
            _clickedObjectStack = new Stack<ClickableObject>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 click2D = new Vector2(clickpos.x, clickpos.y);

                RaycastHit2D[] hit = Physics2D.RaycastAll(click2D, Vector2.zero);

                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider != null)
                    {
                        _clickedObject = hit[i].transform.GetComponent<ClickableObject>();

                        if (_clickedObject != null)
                        {
                            _clickedObject.OnMouseClickDown();

                            _clickedObjectStack.Push(_clickedObject);

                            _clickedObject = null;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_clickedObjectStack.Count > 0)
                {
                    while(_clickedObjectStack.Count > 0)
                    {
                        _clickedObject = _clickedObjectStack.Pop();

                        _clickedObject.OnMouseClickUp();
                    }

                    _clickedObject = null;
                }
            }
        }
    }
}
