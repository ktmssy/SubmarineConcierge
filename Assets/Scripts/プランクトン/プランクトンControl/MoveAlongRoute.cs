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
    public class MoveAlongRoute : MonoBehaviour
    {
        public float speed;
        public RouteData data;
        public float distance = 0f;

        private void Update()
        {
            distance += speed * Time.deltaTime;
            if (distance > data.totalDistance)
                distance -= data.totalDistance;
            else if (distance < -data.totalDistance)
                distance += data.totalDistance;
            transform.position = data.Lerp(distance);
        }
    }
}
