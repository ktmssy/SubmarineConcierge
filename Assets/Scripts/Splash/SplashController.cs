/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
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
using UnityEngine.UI;

namespace SubmarineConcierge
{
    public class SplashController : MonoBehaviour
    {
        public float FadeInTime;
        public float ShowTime;
        public float FadeOutTime;

        private Image image;
        private float timer;
        private float flag1;
        private float flag2;
        private float flag3;

        private AsyncOperation ao;

        private void Start()
        {
            image = GetComponent<Image>();
            timer = 0f;
            flag1 = FadeInTime;
            flag2 = flag1 + ShowTime;
            flag3 = flag2 + FadeOutTime;
            ao = SceneManager.LoadSceneAsync("MainScene");
            ao.allowSceneActivation = false;
        }
    
        private void Update()
        {
            timer += Time.deltaTime;
            if(timer< flag1)
            {
                var color = image.color;
                color.a = Mathf.Lerp(0f, 1f, timer / flag1);
                image.color = color;
			}
            else if(timer< flag2)
            {
                
			}
            else if(timer< flag3)
            {
                var color = image.color;
                color.a = Mathf.Lerp(1f, 0f, (timer-flag2) / FadeOutTime);
                image.color = color;
            }
            else
            {
                ao.allowSceneActivation = true;
			}
        }
    }
}
