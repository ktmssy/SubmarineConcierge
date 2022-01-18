/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
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

namespace YoShiSho
{
    public class ColorControl : MonoBehaviour
    {
        public float timeIntervalMin;
        public float timeIntervalMax;
        public Color a, b;
        private float timeInterval;
        private float timer = 0f;
        private SpriteRenderer sr;

        private void Start()
        {
            timeInterval = Random.Range(timeIntervalMin, timeIntervalMax);
            timer = Random.Range(0f, timeInterval);
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            sr.color = timer / timeInterval <= 0.5f ? Color32.Lerp(a, b, timer / timeInterval * 2f) : Color32.Lerp(b, a, timer / timeInterval * 2f - 1f);
            if (timer > timeInterval)
            {
                timeInterval = Random.Range(timeIntervalMin, timeIntervalMax);
                timer = 0f;
            }
        }
    }
}
