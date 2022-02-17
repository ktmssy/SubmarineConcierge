/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年02月01日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.Splash中にセーブデータをロードする
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SubmarineConcierge
{
    public class SplashController : MonoBehaviour
    {
        public float fadeInTime;
        public float showTime;
        public float fadeOutTime;
        public GameObject textStart;

        private Image image;

        private float timer;
        private float flag1;

#if UNITY_IOS || UNITY_ANDROID
        private float flag2;
        private float flag3;
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
        private bool clicked = false;
#endif

        private AsyncOperation ao;

        private void Start()
        {
#if UNITY_STANDALONE
            Screen.SetResolution(2778 / 2, 1284 / 2, false);
#endif
            SaveData.SaveDataManager.LoadOnce();

            switch (SaveData.SaveDataManager.mapSaveData.currentPlace)
            {
                case Fish.FishAppearPlace.Shoal:
                default:
                    ao = SceneManager.LoadSceneAsync("ShoalScene");
                    break;
                case Fish.FishAppearPlace.Middle:
                    ao = SceneManager.LoadSceneAsync("MiddleScene");
                    break;
                case Fish.FishAppearPlace.Deep:
                    ao = SceneManager.LoadSceneAsync("DeepScene");
                    break;
            }
            ao.allowSceneActivation = false;

            image = GetComponent<Image>();

            timer = 0f;
            flag1 = fadeInTime;

#if UNITY_IOS || UNITY_ANDROID
            flag2 = flag1 + showTime;
            flag3 = flag2 + fadeOutTime;
            textStart.SetActive(false);
#endif
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private IEnumerator FadeOutAnm()
        {
            //var text = textStart.GetComponent<Text>();
            var color = image.color;
            while (color.a > 0f)
            {
                /*if (text != null && text.color.a < 0.1f)
                {
                    Destroy(text.gameObject);
                    text = null;
                }*/
                color.a -= 0.02f;
                image.color = color;
                yield return new WaitForSeconds(0.02f);
            }
            ao.allowSceneActivation = true;
            yield break;
        }
#endif

        private void Update()
        {

            timer += Time.deltaTime;
            if (timer < flag1)
            {
                var color = image.color;
                color.a = Mathf.Lerp(0f, 1f, timer / flag1);
                image.color = color;
            }
#if UNITY_IOS || UNITY_ANDROID
            else if (timer < flag2)
            {

            }
            else if (timer < flag3)
            {
                var color = image.color;
                color.a = Mathf.Lerp(1f, 0f, (timer - flag2) / fadeOutTime);
                image.color = color;
            }
            else
            {
                ao.allowSceneActivation = true;
            }
#endif
#if UNITY_EDITOR || UNITY_STANDALONE
            if (!clicked && Input.GetMouseButtonDown(0))
            {
                clicked = true;
                Destroy(textStart);
                StartCoroutine(FadeOutAnm());
            }
#endif
        }
    }
}
