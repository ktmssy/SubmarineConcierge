/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月29日
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
using UnityEngine.UI;

namespace SubmarineConcierge.UI
{
    public class TextPPControl : MonoBehaviour
    {
        Text text;
        Image image;

        private void Start()
        {
            text = GetComponentInChildren<Text>();
            image = GetComponentInChildren<Image>();
        }

        private IEnumerator FadeOutAnm()
        {
            Color tColor = text.color;
            Color iColor = image.color;
            float a = tColor.a;
            while (a > 0f)
            {
                a -= 0.01f;
                tColor.a = a;
                iColor.a = a;
                text.color = tColor;
                image.color = iColor;
                yield return new WaitForSeconds(0.01f);
            }
            yield break;
        }

        private IEnumerator FadeInAnm()
        {
            Color tColor = text.color;
            Color iColor = image.color;
            float a = tColor.a;
            while (a < 1f)
            {
                a += 0.01f;
                tColor.a = a;
                iColor.a = a;
                text.color = tColor;
                image.color = iColor;
                yield return new WaitForSeconds(0.01f);
            }
            yield break;
        }

        public void FadeIn()
        {
            StopAllCoroutines();
            StartCoroutine(FadeInAnm());
        }

        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutAnm());
        }

    }
}
