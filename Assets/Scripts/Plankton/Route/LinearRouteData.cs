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
        public Vector2[] Points;

        /// <summary>
        /// 円滑化するときの頂点数
        /// </summary>
        public int SmoothCount;

        /// <summary>
        /// 辺
        /// </summary>
        [System.NonSerialized]
        public RouteEdge[] Edges;

        /// <summary>
        /// 自動で周長を算出し、辺の情報を用意する
        /// </summary>
        public void OnEnable()
        {
            //周長リセット
            TotalDistance = 0f;

            //辺のArrayを用意
            Edges = new RouteEdge[Points.Length];

            for (int i = 0; i < Points.Length; ++i)
            {
                //辺
                Edges[i] = new RouteEdge(Points[i], Points[i + 1 == Points.Length ? 0 : i + 1]);

                //周長
                TotalDistance += Edges[i].Distance;
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
                value -= Mathf.FloorToInt(value / TotalDistance) * TotalDistance;
            }
            else
            {
                value += Mathf.CeilToInt(-value / TotalDistance) * TotalDistance;
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

            foreach (RouteEdge edge in Edges)
            {
                //何辺にあるの判定
                if (value > edge.Distance)
                {
                    value -= edge.Distance;
                    continue;
                }
                //その辺で座標をLerp
                return Vector2.Lerp(edge.Start, edge.End, value / edge.Distance);
            }

            return Edges[0].Start;
        }

        /// <summary>
        /// 親の偏位とスケールを考慮したLerp
        /// </summary>
        /// <param name="value">位相</param>
        /// <param name="localToWorldMatrix">変換Matrix</param>
        /// <returns>ルート上の座標</returns>
        public override Vector2 Lerp(float value, Matrix4x4 localToWorldMatrix)
        {
            return Utilities.CoordinateUtility.CalcPos(Lerp(value), localToWorldMatrix);
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

            foreach (RouteEdge edge in Edges)
            {
                if (value > edge.Distance)
                {
                    value -= edge.Distance;
                    continue;
                }
                return Vector3.Slerp(new Vector3(edge.Start.x, edge.Start.y, 0f), new Vector3(edge.End.x, edge.End.y, 0f), value / edge.Distance);
            }

            return Edges[0].Start;
        }
    }
}
