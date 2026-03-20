using Helpers;
using UnityEngine;

namespace SceneSetup
{
    
    //In this script i turn on or off the elements in the scene depending on the platform.
    public class PlatformSetupManager : MonoBehaviour
    {
        [SerializeField] private GameObject xrOrigin;
        [SerializeField] private GameObject playerCameraRig;
        [SerializeField] private GameObject uiJoystickCanvas;
        void Start()
        {
            PlatformType currentPlatform = DeviceDetector.GetCurrentPlatform();
            Debug.Log($"Current platform: {currentPlatform}");
            SetupForPlatform(currentPlatform);

        }
        private void SetupForPlatform(PlatformType platform)
        {
            // Switch statement approach
            switch (platform)
            {
                case PlatformType.Desktop:
                    // Regular Camera ON, XR Origin OFF, Joystick Canvas OFF
                    xrOrigin.SetActive(false);
                    playerCameraRig.SetActive(true);
                    uiJoystickCanvas.SetActive(false);
                    break;
            
                case PlatformType.DesktopVR:
                    // XR Origin ON, Regular Camera OFF, Joystick Canvas OFF
                    xrOrigin.SetActive(true);
                    playerCameraRig.SetActive(false);
                    uiJoystickCanvas.SetActive(false);
                    break;
            
                case PlatformType.AndroidVR:
                    // Same as DesktopVR
                    xrOrigin.SetActive(true);
                    playerCameraRig.SetActive(false);
                    uiJoystickCanvas.SetActive(false);
                    break;
            
                case PlatformType.Mobile:
                    // Regular Camera ON, XR Origin OFF, Joystick Canvas ON
                    xrOrigin.SetActive(false);
                    playerCameraRig.SetActive(true);
                    uiJoystickCanvas.SetActive(true);
                    break;
            }
        }

    }
}
