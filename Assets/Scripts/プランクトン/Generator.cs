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
    public class Generator : MonoBehaviour
    {
        public GameObject prefab;
        public RouteData data;
        public int count;

        private void Start()
        {
            float value = 0f;
            float delta = data.totalDistance / count;
            for (int i = 0; i < count; ++i)
            {
                GameObject go = Instantiate(prefab, data.Lerp(value), Quaternion.identity);
                go.transform.parent = transform;
                MoveAlongRoute mar = go.GetComponent<MoveAlongRoute>();
                mar.distance = value;
                mar.data = data;
                value += delta;
            }
        }
    }
}
