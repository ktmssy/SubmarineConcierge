/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
 *　更新日：2022年01月20日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.親の座標も計算に入れる...楊志庄
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubmarineConcierge.Utilities;

namespace SubmarineConcierge.Plankton
{
#if UNITY_EDITOR
    /// <summary>
    /// ルートデータの可視化
    /// </summary>
    public class DrawRoute : MonoBehaviour
    {
        /// <summary>
        /// 描画しますか？
        /// </summary>
        [Tooltip("描画On/Off")]
        public bool DoDraw;

        /// <summary>
        /// ルートデータ
        /// </summary>
        [Tooltip("ルートデータ")]
        public RouteData Data;

        /// <summary>
        /// 頂点の半径
        /// </summary>
        [Tooltip("頂点の描画半径")]
        public float PointRadius = 0.1f;

        public void OnDrawGizmos()
        {
            if (!DoDraw)
                return;

            if (Data is LinearRouteData)
            {
                //辺の描画
                Gizmos.color = Color.white;
                foreach (var edge in ((LinearRouteData)Data).Edges)
                    Gizmos.DrawLine(CoordinateUtility.CalcPos(edge.Start, transform.localToWorldMatrix), CoordinateUtility.CalcPos(edge.End, transform.localToWorldMatrix));

                //頂点の描画
                Gizmos.color = Color.red;
                foreach (Vector2 pos in ((LinearRouteData)Data).Points)
                    Gizmos.DrawSphere(CoordinateUtility.CalcPos(pos, transform.localToWorldMatrix), PointRadius);
            }

        }

    }
#endif
}
