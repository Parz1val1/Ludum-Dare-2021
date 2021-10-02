using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction.Definitions
{
    public enum Visibility
    {
        _onlyEnable,
        _onlyDisable,
        _toggleVisibility
    }

    public static class VisibilityDefinitions
    {
        public static void ChangeVisibility(GameObject _g, Visibility _v)
        {
            if (_v == Visibility._onlyEnable)
            {
                _g.SetActive(true);
            }
            else if (_v == Visibility._onlyDisable)
            {
                _g.SetActive(false);
            }
            else if (_v == Visibility._toggleVisibility)
            {
                _g.SetActive(!_g.activeSelf);
            }
        }
    }
}
