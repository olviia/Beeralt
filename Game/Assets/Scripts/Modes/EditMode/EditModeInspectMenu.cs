using System;
using Content.ObjectsThatExistOnScene;
using Input;
using UI;
using UnityEngine;

namespace Modes.EditMode
{
    public class EditModeInspectMenu:PausingMenu
    {
        PlacedObjectMarker objectMarker;
        private void Awake()
        {
            //gameObject.SetActive(false);
        }

        public void Open(PlacedObjectMarker marker)
        {
            objectMarker = marker;
            RequestOpen();
        }
    }
}