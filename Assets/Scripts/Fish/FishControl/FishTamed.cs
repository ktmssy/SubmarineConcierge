/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
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

namespace SubmarineConcierge.Fish
{
    public class FishTamed : Fish
    {
        public bool DoMove;
        public float Speed;
        public float WaitTimeMin;
        public float WaitTimeMax;
        public Vector2 Margin;

        private Vector2 PosMin;
        private Vector2 PosMax;

        private float timer;
        private float waitTime;
        private Vector2 targetPos;

        private float NewWaitTime()
        {
            timer = 0f;
            waitTime = Random.Range(WaitTimeMin, WaitTimeMax);
            return waitTime;
        }

        private Vector2 NewTargetPos()
        {
            float x = Random.Range(PosMin.x, PosMax.x);
            float y = Random.Range(PosMin.y, PosMax.y);
            targetPos = new Vector2(x, y);
            return targetPos;
        }

        public override void Init(FishIndividualData fish, FishData data)
        {
            base.Init(fish, data);
            PosMax = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 1f)) + Margin;
            PosMin = (Vector2)Camera.main.ViewportToWorldPoint(Vector3.zero) - Margin;
            NewWaitTime();
            NewTargetPos();
        }

        private void FixedUpdate()
        {
            if (!DoMove)
                return;
            Vector2 direction = targetPos - (Vector2)transform.position;
            if (direction.sqrMagnitude < 1f)
            {
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {

                    NewTargetPos();
                    NewWaitTime();
                }
            }
            else
            {
                transform.position += (Vector3)direction.normalized * Speed * Time.deltaTime;
            }
            transform.localScale = new Vector3(direction.x > 0 ? -1f : 1f, 1f, 1f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 leftBottom = PosMin;
            Vector3 rightTop = PosMax;
            Vector3 leftTop = new Vector3(PosMin.x, PosMax.y);
            Vector3 rightBottom = new Vector3(PosMax.x, PosMin.y);
            Gizmos.DrawLine(leftBottom, leftTop);
            Gizmos.DrawLine(rightTop, leftTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(leftBottom, rightBottom);
        }
    }
}
