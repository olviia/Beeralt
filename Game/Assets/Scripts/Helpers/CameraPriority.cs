using System;
using Modes.EditMode;
using Unity.Cinemachine;
using UnityEngine;

namespace Helpers
{
    public class CameraPriority:MonoBehaviour
    {
        [SerializeField] private CinemachineCamera flyCamera;
        [SerializeField] private CinemachineCamera editCamera;
        [SerializeField] private CinemachineCamera walkCamera;

        private int flyCameraPriority;
        private int editCameraPriority;
        private int walkCameraPriority;

        /// <summary>
        /// swap priorities, and then cinemachine will blend
        ///between them
        /// </summary>
        private void Awake()
        {
            flyCameraPriority = flyCamera.Priority.Value;
            editCameraPriority = editCamera.Priority.Value;
            walkCameraPriority = walkCamera.Priority.Value;
        }

        //todo:
        //can be rewritten into generic i think
        public void SetFlyCameraAsCurrent()
        {
            flyCamera.Priority.Value = 100;
            walkCamera.Priority.Value = walkCameraPriority;
            editCamera.Priority.Value = editCameraPriority;
        }

        public void SetWalkCameraAsCurrent()
        {
            walkCamera.Priority.Value = 100;
            flyCamera.Priority.Value = flyCameraPriority;
            editCamera.Priority.Value = editCameraPriority;
        }

        public void SetEditCameraAsCurrent()
        {
            editCamera.Priority.Value = 100;
            flyCamera.Priority.Value = flyCameraPriority;
            walkCamera.Priority.Value = walkCameraPriority;
        }
    }
}