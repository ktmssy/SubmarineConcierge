/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月13日
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

using SubmarineConcierge.Fish;
using SubmarineConcierge.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge
{
    public class RingControl : MonoBehaviour
    {
        public Vector2 delayRange;
        public FishTamed fish;
        private SessionManager sm;

        private void Start()
        {
            sm = SingletonMB<SessionManager>.Instance;
        }

        private void Goodbye()
        {
            if (sm.status != SessionStatus.Play)
                return;
            fish.GenerateRing();
            Destroy(gameObject);
        }

        public void Next()
        {
            Invoke("Goodbye", Random.Range(delayRange.x, delayRange.y));
        }
    }
}
