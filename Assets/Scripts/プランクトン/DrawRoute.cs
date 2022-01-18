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
    public class DrawRoute : MonoBehaviour
    {
        public RouteData data;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(data.points[0], data.points[data.points.Length - 1]);
            for (int i = 1; i < data.points.Length; ++i)
                Gizmos.DrawLine(data.points[i], data.points[i - 1]);

            Gizmos.color = Color.red;
            foreach (Vector2 pos in data.points)
                Gizmos.DrawSphere(pos, 0.1f);
        }

    }
}
