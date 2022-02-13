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

using SubmarineConcierge.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge
{
    public class AwaGenerator : MonoBehaviour
    {
        public Vector2 startDelayRange;
        public GameObject prefabAwa;
        public int num;

        private void GenerateAwa()
        {

            Instantiate(prefabAwa, transform);
        }

        private void Start()
        {
            UEventDispatcher.addEventListener(SCEvent.OnAwaDestroying, OnAwaDestroying);
            for (int i = 0; i < num; i++)
                Invoke("GenerateAwa", Random.Range(startDelayRange.x, startDelayRange.y));
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnAwaDestroying, OnAwaDestroying);
        }

        private void OnAwaDestroying(UEvent e)
        {
            GenerateAwa();
        }
    }
}
