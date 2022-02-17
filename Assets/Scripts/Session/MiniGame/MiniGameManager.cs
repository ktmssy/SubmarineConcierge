/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年2月7日
 *　更新日：2022年02月17日
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Session
{
    public class MiniGameManager : SingletonMB<MiniGameManager>
    {
        public float interval = 2f;
        public Vector2 offset = Vector2.one;
        private GameObject prefabLog;
        private SessionManager sessionManager;

        private float[] x;
        private float[] y;

        private bool isGenerating = false;
        private bool canGenerate = true;
        private float timer = 0f;

        private void Start()
        {
            base.Init();
            prefabLog = Resources.Load<GameObject>("MiniGame/Log");
            sessionManager = SingletonMB<SessionManager>.Instance;
            Vector2 posMax = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 1f)) + offset;
            Vector2 posMin = (Vector2)Camera.main.ViewportToWorldPoint(Vector3.zero) - offset;
            x = new float[] { posMin.x, posMax.x };
            y = new float[] { posMin.y, posMax.y };
            UEventDispatcher.addEventListener(SCEvent.OnSessionStart, OnSessionStart);
            UEventDispatcher.addEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
            UEventDispatcher.addEventListener(SCEvent.OnLogDestroying, OnLogDestroying);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UEventDispatcher.removeEventListener(SCEvent.OnSessionStart, OnSessionStart);
            UEventDispatcher.removeEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
            UEventDispatcher.removeEventListener(SCEvent.OnLogDestroying, OnLogDestroying);
        }

        private void OnLogDestroying(UEvent e)
        {
            canGenerate = true;
        }

        private void GenerateLog()
        {
            /*if (sessionManager.status != SessionStatus.Play)
            {
                isGenerating = false;
                return;
            }*/
            Vector3 pos = new Vector3(x[Random.Range(0, x.Length)], y[Random.Range(0, y.Length)]);
            GameObject obj = Instantiate(prefabLog, pos, Quaternion.identity);
            obj.transform.parent = transform;
            canGenerate = false;
            timer = 0f;
        }

        private void Update()
        {
            if (!isGenerating)
                return;

            if (timer < interval)
            {
                timer += Time.deltaTime;
            }
            else if (canGenerate)
            {
                GenerateLog();
            }
        }

        private void OnSessionStart(UEvent e)
        {
            if (isGenerating)
                return;
            isGenerating = true;
            canGenerate = true;
            timer = interval;
        }

        private void OnSessionEnd(UEvent e)
        {
            isGenerating = false;
        }
    }
}
