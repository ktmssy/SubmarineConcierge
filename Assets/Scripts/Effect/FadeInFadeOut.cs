/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月13日
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

namespace SubmarineConcierge
{
    public class FadeInFadeOut : MonoBehaviour
    {
        public float fadeInDuration = 1f;
        public float showDuration = 1f;
        public float fadeOutDuration = 1f;
        public ComponentType type;

        public enum ComponentType { SpriteRender };

        private float timer = 0f;
        private float flag1, flag2, flag3;

        SpriteRenderer sr;
        Color color;

        private void Start()
        {
            flag1 = fadeInDuration;
            flag2 = flag1 + showDuration;
            flag3 = flag2 + fadeOutDuration;
            switch (type)
            {
                case ComponentType.SpriteRender:
                    sr = GetComponent<SpriteRenderer>();
                    color = sr.color;
                    color.a = 0f;
                    sr.color = color;
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer < flag1)
            {
                color.a = Mathf.Lerp(0f, 1f, timer / flag1);
            }
            else if (timer < flag2)
            {

            }
            else if (timer < flag3)
            {
                color.a = Mathf.Lerp(1f, 0f, (timer - flag2) / flag3);
            }
            else
            {
                Destroy(gameObject);
            }

            switch (type)
            {
                case ComponentType.SpriteRender:
                    sr.color = color;
                    break;
                default:
                    break;
            }
        }
    }
}
