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
    public class FishPanelManager : MonoBehaviour
    {
        public Button bOpen;
        public Button bClose;
        public FIshLeftManager flm;
        public FishRightManager frm;

        private bool isOpen = false;

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
        }

        private void Close()
        {
            isOpen = false;
            gameObject.SetActive(false);
            bOpen.gameObject.SetActive(true);
        }

        private void RefreshAll(UEvent e)
        {
            RefreshAll();
        }

        private void OnFishNameChanged(UEvent e)
        {
            RefreshLeft();
        }

        private void Start()
        {
            Close();
            bOpen.onClick.AddListener(Open);
            bClose.onClick.AddListener(Close);
            UEventDispatcher.addEventListener(SCEvent.OnFishJoinTeam, RefreshAll);
            UEventDispatcher.addEventListener(SCEvent.OnFishLeaveTeam, RefreshAll);
            UEventDispatcher.addEventListener(SCEvent.OnFishNameChanged, OnFishNameChanged);
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnFishJoinTeam, RefreshAll);
            UEventDispatcher.removeEventListener(SCEvent.OnFishLeaveTeam, RefreshAll);
            UEventDispatcher.addEventListener(SCEvent.OnFishNameChanged, OnFishNameChanged);
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition))
                {
                    Close();
                }
            }
#endif
        }

    }
}
