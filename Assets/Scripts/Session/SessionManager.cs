/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月01日
 *　更新日：
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.野生魚が全部消えていなくてもセッションが始まるようにした...楊志庄
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
    public class SessionManager : SingletonMB<SessionManager>
    {
        public Button startButton;
        public Button stopButton;
        //public FishManager fishManager;
        public TextPPControl textPPControl;
        public PlanktonManager planktonManager;
        public MusicDatabase musicDatabase;

        public SessionStatus status { get; private set; } = SessionStatus.Stop;

        private float duration;

        //private bool isReady = false;

        public void SetStatus(SessionStatus status)
        {
            if (this.status == status)
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
            this.status = status;
        }

        private void PrepareSession()
        {
            Debug.Log("Prepare Session");
            startButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
            textPPControl.FadeOut();
            planktonManager.PrepareSession();
            MusicData music = musicDatabase.GetRandom();
            var track = music.soundTracks[0];
            duration = track.clipFirst.length + track.clipLoop.length;
            Debug.Log(track.clipFirst.length + " + " + track.clipLoop.length + " = " + duration);
            foreach (FishTamed fish in SingletonMB<FishManager>.Instance.tamedFishes.Values)
            {
                fish.PrepareSession(music);
            }
        }

        private void StartSession()
        {
            Debug.Log("Start Session ");
            SingletonMB<MiniGameManager>.Instance.OnSessionStart();
            foreach (FishTamed fish in SingletonMB<FishManager>.Instance.tamedFishes.Values)
            {
                fish.StartSession();
            }
            Invoke("OnSongOver", duration);
        }

        private void OnSongOver()
        {
            SetStatus(SessionStatus.Stop);
        }


        private void StopSession()
        {
            Debug.Log("Stop Session");
            startButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
            textPPControl.FadeIn();
            planktonManager.StopSession();
            foreach (FishTamed fish in SingletonMB<FishManager>.Instance.tamedFishes.Values)
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
            if (status == SessionStatus.Prepare)
            {
                bool __flag1 = true/*(fishManager.wildFishes.Count == 0)*/;

                bool __flag2 = true;
                foreach (FishTamed fish in SingletonMB<FishManager>.Instance.tamedFishes.Values)
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
