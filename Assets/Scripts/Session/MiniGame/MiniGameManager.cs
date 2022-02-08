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

        private void Start()
        {
            prefabLog = Resources.Load<GameObject>("MiniGame/Log");
            sessionManager = SingletonMB<SessionManager>.Instance;
            Vector2 posMax = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 1f)) + offset;
            Vector2 posMin = (Vector2)Camera.main.ViewportToWorldPoint(Vector3.zero) - offset;
            x = new float[] { posMin.x, posMax.x };
            y = new float[] { posMin.y, posMax.y };
        }

        private void GenerateLog()
        {
            if (sessionManager.status != SessionStatus.Play)
            {
                isGenerating = false;
                return;
            }
            Vector3 pos = new Vector3(x[Random.Range(0, x.Length)], y[Random.Range(0, y.Length)]);
            GameObject obj = Instantiate(prefabLog, pos, Quaternion.identity);
            obj.transform.parent = transform;
            Invoke("GenerateLog", interval);
        }

        public void OnSessionStart()
        {
            if (isGenerating)
                return;
            isGenerating = true;
            Invoke("GenerateLog", interval);
        }
    }
}
