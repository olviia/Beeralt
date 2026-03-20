using UnityEngine;

namespace Content.Registry
{
    //here we select what to save and what to load
    public class RegistryController:MonoBehaviour
    {
        //assigned in inspector
        public PrefabRegistry prefabRegistry;

        private void Awake()
        {
            prefabRegistry.Initialize();
        }
    }
}