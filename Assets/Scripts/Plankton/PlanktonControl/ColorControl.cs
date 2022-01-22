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

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// スプライトの色調整
    /// </summary>
    [AddComponentMenu("_SubmarineConcierge/Plankton/ColorControl")]
    public class ColorControl : MonoBehaviour
    {
        public float TimeIntervalMin;
        public float TimeIntervalMax;
        public Color A, B;
        private float timeInterval;
        private float timer = 0f;
        private SpriteRenderer sr;

        private void Start()
        {
            timeInterval = Random.Range(TimeIntervalMin, TimeIntervalMax);
            timer = Random.Range(0f, timeInterval);
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            sr.color = timer / timeInterval <= 0.5f ? Color32.Lerp(A, B, timer / timeInterval * 2f) : Color32.Lerp(B, A, timer / timeInterval * 2f - 1f);
            if (timer > timeInterval)
            {
                timeInterval = Random.Range(TimeIntervalMin, TimeIntervalMax);
                timer = 0f;
            }
        }
    }
}
