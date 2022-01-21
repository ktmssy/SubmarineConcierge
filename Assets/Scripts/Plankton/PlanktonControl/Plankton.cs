/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月21日
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
    /// プランクトンのコントローラー
    /// </summary>
    public class Plankton : MonoBehaviour
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
        public float Phase = 0f;

        /// <summary>
        /// 位相の更新
        /// </summary>
        public virtual void UpdatePhase()
        {
            //位相の更新
            Phase += Speed * Time.deltaTime;

            //位相Clamp
            if (Phase > Data.TotalDistance)
                Phase -= Data.TotalDistance;
            else if (Phase < -Data.TotalDistance)
                Phase += Data.TotalDistance;
        }

        /// <summary>
        /// 座標の更新
        /// </summary>
        public virtual void UpdatePosition()
        {
            //移動
            if (transform.parent)
                transform.position = Data.Lerp(Phase, transform.parent.localToWorldMatrix);
            else
                transform.position = Data.Lerp(Phase);
        }

        protected void Update()
        {
            UpdatePhase();
            UpdatePosition();
        }
    }
}
