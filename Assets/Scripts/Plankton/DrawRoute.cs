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
    [AddComponentMenu("_SubmarineConcierge/Plankton/Route/DrawRoute")]
    public class DrawRoute : MonoBehaviour
    {
        /// <summary>
        /// 描画しますか？
        /// </summary>
        [Tooltip("描画On/Off")]
        public bool doDraw;

        /// <summary>
        /// ルートデータ
        /// </summary>
        [Tooltip("ルートデータ")]
        public RouteData data;

        /// <summary>
        /// 頂点の半径
        /// </summary>
        [Tooltip("頂点の描画半径")]
        public float pointRadius = 0.1f;

        public void OnDrawGizmos()
        {
            if (!doDraw)
                return;

            if (data is LinearRouteData)
            {
                //辺の描画
                Gizmos.color = Color.white;
                foreach (var edge in ((LinearRouteData)data).edges)
                    Gizmos.DrawLine(CoordinateUtility.CalcWorldPosFromLocalPos(edge.start, transform.localToWorldMatrix), CoordinateUtility.CalcWorldPosFromLocalPos(edge.end, transform.localToWorldMatrix));

                //頂点の描画
                Gizmos.color = Color.red;
                foreach (Vector2 pos in ((LinearRouteData)data).points)
                    Gizmos.DrawSphere(CoordinateUtility.CalcWorldPosFromLocalPos(pos, transform.localToWorldMatrix), pointRadius);
            }

        }

    }
#endif
}
