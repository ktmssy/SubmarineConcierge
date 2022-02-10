/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月10日
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
using SubmarineConcierge.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge.UI
{
    public class SessionSeekBar : MonoBehaviour
    {
        public Slider slider;
        private float timer;

        private SessionManager sessionManager;

        private void OnSessionStart(UEvent e)
        {
            timer = 0f;
            slider.maxValue = sessionManager.duration;
            gameObject.SetActive(true);
        }

        private void OnSessionEnd(UEvent e)
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            sessionManager = SingletonMB<SessionManager>.Instance;
            UEventDispatcher.addEventListener(SCEvent.OnSessionStart, OnSessionStart);
            UEventDispatcher.addEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnSessionStart, OnSessionStart);
            UEventDispatcher.removeEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
        }

        private void Update()
        {
            if(sessionManager.status==SessionStatus.Play)
            {
                timer += Time.deltaTime;
                slider.value = timer;
            }
        }
    }
}
