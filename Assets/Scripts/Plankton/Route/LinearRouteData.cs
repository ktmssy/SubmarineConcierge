/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
 *　更新日：2022年01月22日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.IRoute...楊志庄
 *　2.RouteData...楊志庄
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// プランクトンのルートデータ
    /// </summary>
    [CreateAssetMenu(fileName = "LinearRouteData", menuName = "SubmarineConcierge/Plankton/LinearRouteData")]
    public class LinearRouteData : RouteData
    {
        /// <summary>
        /// 頂点座標
        /// </summary>
        public Vector2[] points;

        public bool loop;

        /// <summary>
        /// 円滑化するときの頂点数
        /// </summary>
        public int smoothCount;

        /// <summary>
        /// 辺
        /// </summary>
        [System.NonSerialized]
        public RouteEdge[] edges;

        /// <summary>
        /// 自動で周長を算出し、辺の情報を用意する
        /// </summary>
        public void OnEnable()
        {
            //周長リセット
            totalDistance = 0f;

            //辺のArrayを用意
            if (loop)
            {
                edges = new RouteEdge[points.Length];

                //辺
                edges[points.Length - 1] = new RouteEdge(points[points.Length - 1], points[0]);

                //周長
                totalDistance += edges[points.Length - 1].distance;
            }
            else
            {
                edges = new RouteEdge[points.Length - 1];
            }

            for (int i = 0; i < points.Length - 1; ++i)
            {
                //辺
                edges[i] = new RouteEdge(points[i], points[i + 1]);

                //周長
                totalDistance += edges[i].distance;
            }
        }

        /// <summary>
        /// 入力した位相を0～周長の範囲内に変換
        /// </summary>
        /// <param name="value">任意の位相</param>
        /// <returns>0～周長の範囲内の位相</returns>
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

        /// <summary>
        /// 位相を基に座標を算出
        /// </summary>
        /// <param name="value">位相</param>
        /// <returns>ルート上の座標</returns>
        public override Vector2 Lerp(float value)
        {
            //位相を0～周長の範囲内に変換
            value = Clamp(value);

            foreach (RouteEdge edge in edges)
            {
                //何辺にあるの判定
                if (value > edge.distance)
                {
                    value -= edge.distance;
                    continue;
                }
                //その辺で座標をLerp
                return Vector2.Lerp(edge.start, edge.end, value / edge.distance);
            }

            return edges[0].start;
        }

        /// <summary>
        /// 親の偏位とスケールを考慮したLerp
        /// </summary>
        /// <param name="value">位相</param>
        /// <param name="localToWorldMatrix">変換Matrix</param>
        /// <returns>ルート上の座標</returns>
        public override Vector2 Lerp(float value, Matrix4x4 localToWorldMatrix)
        {
            return Utilities.CoordinateUtility.CalcWorldPosFromLocalPos(Lerp(value), localToWorldMatrix);
        }

        /// <summary>
        /// SLerpで座標を算出。お勧めしない。
        /// </summary>
        /// <param name="value">位相</param>
        /// <returns>座標</returns>
        [System.Obsolete]
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
