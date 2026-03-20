using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    //this should be inactive on the scene from the beginning
    public class PauseMenu:PausingMenu
    {

        public override void Open()
        {
            base.Open();
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            base.Close();
            gameObject.SetActive(false);
        }

        public void Continue()
        {
            RequestClose();
        }

        public void Restart()
        {            
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}