/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年2月7日
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

using SubmarineConcierge.Event;
using SubmarineConcierge.Fish;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Session
{
    public class LogController : MonoBehaviour
    {
        public float speedMax;
        public float speedMin;

        public float torqueMin;
        public float torqueMax;

        SessionManager manager;

        private void Start()
        {
            manager = SingletonMB<SessionManager>.Instance;
            //方向決定
            Vector2 dir = Vector2.zero;
            var fishes = SingletonMB<FishManager>.Instance.tamedFishes.Values;
            int index = Random.Range(0, fishes.Count);
            int i = 0;
            foreach (var fish in fishes)
            {
                if (i++ != index)
                    continue;
                var targetPos = fish.transform.position;
                var currentPos = transform.position;
                dir = (targetPos - currentPos).normalized;
                transform.Translate(0f, 0f, targetPos.z - currentPos.z);
            }
            //スピード計算
            var rb2d = GetComponent<Rigidbody2D>();
            float speed = Random.Range(speedMin, speedMax);
            rb2d.velocity = dir * speed;

            rb2d.AddTorque(Random.Range(torqueMin, torqueMax));
        }

        private void Update()
        {
            if (manager.status != SessionStatus.Play)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            UEventDispatcher.dispatchEvent(SCEvent.OnLogDestroying, gameObject);
        }
    }
}
