/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月17日
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
    public class AwaGroupGenerator : MonoBehaviour
    {
        public Vector2 startDelayRange;
        public GameObject[] prefabAwaGroups;

        private void GenerateAwa()
        {
            Instantiate(prefabAwaGroups[Random.Range(0, prefabAwaGroups.Length)], transform);
        }

        private void Start()
        {
            UEventDispatcher.addEventListener(SCEvent.OnAwaGroupDestroying, OnAwaGroupDestroying);
            Invoke("GenerateAwa", Random.Range(startDelayRange.x, startDelayRange.y));
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnAwaGroupDestroying, OnAwaGroupDestroying);
        }

        private void OnAwaGroupDestroying(UEvent e)
        {
            Invoke("GenerateAwa", Random.Range(startDelayRange.x, startDelayRange.y));
        }
    }
}
