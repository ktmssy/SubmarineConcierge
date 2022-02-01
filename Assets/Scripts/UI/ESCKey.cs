/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月01日
 *　更新日：
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SubmarineConcierge.UI
{
#if UNITY_STANDALONE||UNITY_EDITOR
    public class ESCKey : MonoBehaviour
    {
        public GameObject fadeOut;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "SplashScene":
                        Debug.Log("Application Quit");
                        Application.Quit();
                        break;

                    default:
                    case "MainScene":
                        fadeOut?.SetActive(true);
                        Invoke("GoToSplash", 1f);
                        break;
                }
            }
        }

        private void GoToSplash()
        {
            Debug.Log("Back to Splash");
            SceneManager.LoadSceneAsync("SplashScene");
        }
    }
#endif
}
