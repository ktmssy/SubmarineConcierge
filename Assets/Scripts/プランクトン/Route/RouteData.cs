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
    [CreateAssetMenu(menuName = "YoShiSho/RouteData")]
    public class RouteData : ScriptableObject
    {
        public Vector2[] points;

        public int smoothCount;

        //[System.NonSerialized]
        public RouteEdge[] edges;

        [System.NonSerialized]
        public float totalDistance;


        public void OnEnable()
        {
            totalDistance = 0f;
            edges = new RouteEdge[points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                edges[i] = new RouteEdge(points[i], points[i + 1 == points.Length ? 0 : i + 1]);
                totalDistance += edges[i].distance;
            }
        }

        private float Clamp(float value)
        {
            if (value >= 0)
            {
                value -= Mathf.FloorToInt(value / totalDistance) * totalDistance;
            }
            else
            {
                value += Mathf.CeilToInt(-value / totalDistance) * totalDistance;
            }
            return value;
        }

        public Vector2 Lerp(float value)
        {
            value = Clamp(value);

            foreach (RouteEdge edge in edges)
            {
                if (value > edge.distance)
                {
                    value -= edge.distance;
                    continue;
                }
                return Vector2.Lerp(edge.start, edge.end, value / edge.distance);
            }

            return edges[0].start;
        }

        public Vector2 SLerp(float value)
        {
            value = Clamp(value);

            foreach (RouteEdge edge in edges)
            {
                if (value > edge.distance)
                {
                    value -= edge.distance;
                    continue;
                }
                return Vector3.Slerp(new Vector3(edge.start.x, edge.start.y, 0f), new Vector3(edge.end.x, edge.end.y, 0f), value / edge.distance);
            }

            return edges[0].start;
        }
    }
}
