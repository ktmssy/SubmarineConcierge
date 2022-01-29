/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月29日
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
using SubmarineConcierge.Plankton;
using SubmarineConcierge.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge.Session
{
    [AddComponentMenu("_SubmarineConcierge/Session/SessionManager")]
    public class SessionManager : MonoBehaviour
    {
        public Button startButton;
        public Button stopButton;
        public FishManager fishManager;
        public TextPPControl textPPControl;
        public PlanktonManager planktonManager;
        public MusicDatabase musicDatabase;

        public SessionStatus Status { get; private set; } = SessionStatus.Stop;

        //private bool isReady = false;

        public void SetStatus(SessionStatus status)
        {
            if (Status == status)
                return;
            switch (status)
            {
                case SessionStatus.Prepare:
                    PrepareSession();
                    break;

                case SessionStatus.Play:
                    StartSession();
                    break;

                case SessionStatus.Stop:
                case SessionStatus.Pause:
                    StopSession();
                    break;
            }
            Status = status;
        }

        private void PrepareSession()
        {
            Debug.Log("Prepare Session");
            startButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
            textPPControl.FadeOut();
            planktonManager.PrepareSession();
            foreach (FishTamed fish in fishManager.tamedFishes.Values)
            {
                fish.PrepareSession();
            }
        }

        private void StartSession()
        {
            Debug.Log("Start Session ");
            MusicData music = musicDatabase.GetRandom();
            foreach (FishTamed fish in fishManager.tamedFishes.Values)
            {
                fish.StartSession(music);
            }
            //todo play music
        }

        private void StopSession()
        {
            Debug.Log("Stop Session");
            startButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
            textPPControl.FadeIn();
            planktonManager.StopSession();
            foreach (FishTamed fish in fishManager.tamedFishes.Values)
            {
                fish.StopSession();
            }
            //isReady = false;
        }

        private void Start()
        {
            startButton.onClick.AddListener(() => { SetStatus(SessionStatus.Prepare); });
            stopButton.onClick.AddListener(() => { SetStatus(SessionStatus.Stop); });
        }

        private void FixedUpdate()
        {
            if (Status == SessionStatus.Prepare)
            {
                bool __flag1 = (fishManager.wildFishes.Count == 0);

                bool __flag2 = true;
                foreach (FishTamed fish in fishManager.tamedFishes.Values)
                {
                    __flag2 &= fish.isReadyForSession;
                }

                //Debug.Log("flag1 " + __flag1 + " ,flag2 " + __flag2);
                if (__flag1 && __flag2)
                {
                    SetStatus(SessionStatus.Play);
                }
            }

        }
    }
}
