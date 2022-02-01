/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年1月21日
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// ルートデータの基底
    /// </summary>
    public class RouteData : ScriptableObject, IRoute
    {
        /// <summary>
        /// 周長
        /// </summary>
        [System.NonSerialized]
        public float totalDistance;

        /// <summary>
        /// 位相を基にローカル座標を算出する
        /// </summary>
        /// <param name="value">位相</param>
        /// <returns>ローカル座標</returns>
        public virtual Vector2 Lerp(float value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 相を基にワールド座標を算出する
        /// </summary>
        /// <param name="value">位相</param>
        /// <param name="localToWorldMatrix">親のtransform.localToWorldMatrix</param>
        /// <returns>ワールド座標</returns>
        public virtual Vector2 Lerp(float value, Matrix4x4 localToWorldMatrix)
        {
            throw new System.NotImplementedException();
        }

    }
}
