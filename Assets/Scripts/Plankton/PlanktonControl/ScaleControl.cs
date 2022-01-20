/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月20日
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
    /// スプライトのスケール調整
    /// </summary>
    public class ScaleControl : MonoBehaviour
    {
        /// <summary>
        /// 変化する過程の時間最小値
        /// </summary>
        [Tooltip("変化する過程の時間最小値")]
        public float TimeIntervalMin;

        /// <summary>
        /// 変化する過程の時間最大値
        /// </summary>
        [Tooltip("変化する過程の時間最大値")]
        public float TimeIntervalMax;

        /// <summary>
        /// 変化するスケールのスタートとエンド値
        /// </summary>
        [Tooltip("変化するスケールのスタートとエンド値")]
        public Vector3 A, B;

        /// <summary>
        /// "Aの持続時間最小値
        /// </summary>
        [Tooltip("Aの持続時間最小値")]
        public float RemainTimeMin;

        /// <summary>
        /// Aの持続時間最大値
        /// </summary>
        [Tooltip("Aの持続時間最大値")]
        public float RemainTimeMax;

        /// <summary>
        /// Aの持続時間
        /// </summary>
        private float remainTime;

        /// <summary>
        /// 変化過程の時間
        /// </summary>
        private float timeInterval;

        /// <summary>
        /// タイマー
        /// </summary>
        private float timer = 0f;

        private void Start()
        {
            timeInterval = Random.Range(TimeIntervalMin, TimeIntervalMax);
            remainTime = Random.Range(RemainTimeMin, RemainTimeMax);
            timer = Random.Range(0f, timeInterval);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            transform.localScale = timer / timeInterval <= 0.5f ? Vector3.Lerp(A, B, timer / timeInterval * 2f) : Vector3.Lerp(B, A, timer / timeInterval * 2f - 1f);
            if (timer > timeInterval+remainTime)
            {
                timeInterval = Random.Range(TimeIntervalMin, TimeIntervalMax);
                remainTime = Random.Range(RemainTimeMin, RemainTimeMax);
                timer = 0f;
            }
        }
    }
}
