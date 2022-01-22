/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月20日
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

namespace SubmarineConcierge.Utilities
{
    /// <summary>
    /// 座標系関係
    /// </summary>
    public class CoordinateUtility
    {
        /// <summary>
        /// 親のtransformの影響を受けた座標の算出
        /// </summary>
        /// <param name="pos">ローカル座標</param>
        /// <param name="offset">親のワールド座標</param>
        /// <param name="scale">親のスケール</param>
        /// <returns>ワールド座標</returns>
        public static Vector2 CalcWorldPosFromLocalPos(Vector2 pos, Vector2 offset, Vector3 scale)
        {
            return (pos + offset) * scale;
        }

        /// <summary>
        /// 親のtransformの影響を受けた座標の算出
        /// </summary>
        /// <param name="pos">ローカル座標</param>
        /// <param name="localToWorldMatrix">親のlocalToWorldMatrix</param>
        /// <returns>ワールド座標</returns>
        public static Vector2 CalcWorldPosFromLocalPos(Vector2 pos, Matrix4x4 localToWorldMatrix)
        {
            Vector4 local = new Vector4(pos.x, pos.y, 0f, 1f);
            Vector4 world = localToWorldMatrix * local;
            return (Vector2)world;
        }
    }
}
