    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartNewGame : MonoBehaviour
    {
        public void OnStartButtonClick()
        {
            LoadInitialScene();
        }

        private void LoadInitialScene()
        {      
            SceneManager.LoadScene("SunnyVillage");
        }
    }
