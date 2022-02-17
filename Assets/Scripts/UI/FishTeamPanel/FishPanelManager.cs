/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月09日
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge.UI
{
    public class FishPanelManager : SingletonMB<FishPanelManager>
    {
        public Button bOpen;
        public Button bClose;
        public FIshLeftManager flm;
        public FishRightManager frm;
        public AudioSource buttonSound;

        public bool isOpen { get; private set; } = false;


        private void RefreshAll()
        {
            RefreshLeft();
            RefreshRight();
        }

        private void RefreshLeft()
        {
            flm.ReGenerate();
        }

        private void RefreshRight()
        {
            frm.ReGenerate();
        }

        private void Open()
        {
            isOpen = true;
            gameObject.SetActive(true);
            bOpen.gameObject.SetActive(false);
            flm.ReGenerate();
            frm.ReGenerate();
            buttonSound.Play();
        }

        private void Close()
        {
            isOpen = false;
            gameObject.SetActive(false);
            bOpen.gameObject.SetActive(true);
            buttonSound.Play();
        }

        private void RefreshAll(UEvent e)
        {
            RefreshAll();
        }

        private void OnFishNameChanged(UEvent e)
        {
            RefreshLeft();
        }

        private void OnSessionPrepare(UEvent e)
        {
            bOpen.gameObject.SetActive(false);
        }

        private void OnSessionEnd(UEvent e)
        {
            bOpen.gameObject.SetActive(true);
        }

        private void Start()
        {
            base.Init();
            isOpen = false;
            gameObject.SetActive(false);
            bOpen.gameObject.SetActive(true);

            bOpen.onClick.AddListener(Open);
            bClose.onClick.AddListener(Close);
            UEventDispatcher.addEventListener(SCEvent.OnFishJoinTeam, RefreshAll);
            UEventDispatcher.addEventListener(SCEvent.OnFishLeaveTeam, RefreshAll);
            UEventDispatcher.addEventListener(SCEvent.OnFishNameChanged, OnFishNameChanged);
            UEventDispatcher.addEventListener(SCEvent.OnSessionPrepare, OnSessionPrepare);
            UEventDispatcher.addEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UEventDispatcher.removeEventListener(SCEvent.OnFishJoinTeam, RefreshAll);
            UEventDispatcher.removeEventListener(SCEvent.OnFishLeaveTeam, RefreshAll);
            UEventDispatcher.removeEventListener(SCEvent.OnFishNameChanged, OnFishNameChanged);
            UEventDispatcher.removeEventListener(SCEvent.OnSessionPrepare, OnSessionPrepare);
            UEventDispatcher.removeEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (!Input.GetMouseButtonDown(0))
                return;

            if (RectTransformUtility.RectangleContainsScreenPoint(
                transform as RectTransform, Input.mousePosition))
                return;

            Close();
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;

            if (RectTransformUtility.RectangleContainsScreenPoint(
            transform as RectTransform, Input.mousePosition))
                return;

            Close();
#endif
        }

    }
}
