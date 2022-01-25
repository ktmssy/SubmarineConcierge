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
    [AddComponentMenu("_SubmarineConcierge/Plankton/Plankton")]
    public class Plankton : MonoBehaviour
    {
        /// <summary>
        /// スピード
        /// </summary>
        public float Speed;

        public float CollectSpeed;

        /// <summary>
        /// ルートデータ
        /// </summary>
        public RouteData Data;

        /// <summary>
        /// 位相
        /// </summary>
        public float Phase = 0f;

        public PlanktonManager manager;

        public Plankton LastPlankton;

        private Vector2 startPoint;

        private Vector2 endPoint;

        private bool clicked;

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

        protected virtual void UpdatePositionUncollected()
        {
            //移動
            if (transform.parent)
                transform.position = Data.Lerp(Phase, transform.parent.localToWorldMatrix);
            else
                transform.position = Data.Lerp(Phase);
        }

        protected virtual void UpdatePositionCollected()
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint, CollectSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, endPoint) < 0.00001f)
            {
                GainPoint();
                Destroy(gameObject);
            }
        }

        protected virtual void GainPoint()
        {
            manager.GainPP(this);
        }

        /// <summary>
        /// 座標の更新
        /// </summary>
        protected virtual void UpdatePosition()
        {
            if (!clicked)
            {
                UpdatePositionUncollected();
            }
            else
            {
                UpdatePositionCollected();
            }
        }

        public virtual void Collect(Vector2 target)
        {
            if (clicked)
                return;
            startPoint = transform.position;
            endPoint = target;
            clicked = true;
        }

        protected void Update()
        {
            UpdatePhase();
            UpdatePosition();
        }
    }
}
