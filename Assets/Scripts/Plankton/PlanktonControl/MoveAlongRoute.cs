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

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// プランクトンの移動制御
    /// </summary>
    public class MoveAlongRoute : MonoBehaviour
    {
        /// <summary>
        /// スピード
        /// </summary>
        public float Speed;

        /// <summary>
        /// ルートデータ
        /// </summary>
        public RouteData Data;

        /// <summary>
        /// 位相
        /// </summary>
        public float Distance = 0f;

        //public Matrix4x4 localToWorldMatrix;

        private void Update()
        {
            //位相の更新
            Distance += Speed * Time.deltaTime;
            //位相Clamp
            if (Distance > Data.TotalDistance)
                Distance -= Data.TotalDistance;
            else if (Distance < -Data.TotalDistance)
                Distance += Data.TotalDistance;
            //移動
            //transform.position = Data.Lerp(Distance, localToWorldMatrix);
            if (transform.parent)
                transform.position = Data.Lerp(Distance, transform.parent.localToWorldMatrix);
            else
                transform.position = Data.Lerp(Distance);
        }
    }
}
