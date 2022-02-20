/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月17日
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
using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SubmarineConcierge
{
    public class MapSubMenu : MonoBehaviour
    {
        public Button shoalButton;
        public Button middleButton;
        public Button deepButton;
        public Image shoalImage;
        public Image middleImage;
        public Image deepImage;

        public AudioSource soundOpen;
        public AudioSource soundClose;

        public bool isOpen { get; private set; } = false;
        public bool isAnimating { get; private set; } = false;

        private Animator anm;

        private void Start()
        {
            anm = GetComponent<Animator>();
            switch (SaveDataManager.mapSaveData.currentPlace)
            {
                case FishAppearPlace.Shoal:
                default:
                    shoalButton.interactable = false;
                    break;
                case FishAppearPlace.Middle:
                    middleButton.interactable = false;
                    break;
                case FishAppearPlace.Deep:
                    deepButton.interactable = false;
                    break;
            }
            shoalButton.onClick.AddListener(() => { SaveDataManager.mapSaveData.SetCurrentPlace(FishAppearPlace.Shoal); SceneManager.LoadScene("ShoalScene"); });
            middleButton.onClick.AddListener(() => { SaveDataManager.mapSaveData.SetCurrentPlace(FishAppearPlace.Middle); SceneManager.LoadScene("MiddleScene"); });
            deepButton.onClick.AddListener(() => { SaveDataManager.mapSaveData.SetCurrentPlace(FishAppearPlace.Deep); SceneManager.LoadScene("DeepScene"); });
        }

        public void Switch()
        {
            if (isAnimating)
                return;
            isAnimating = true;
            if (isOpen)
                Close();
            else
                Open();
        }

        private void Open()
        {
            if (isOpen)
                return;
            isOpen = true;
            soundOpen.PlayDelayed(0.1f);
            anm.Play("Open");
        }

        private void Close()
        {
            if (!isOpen)
                return;
            isOpen = false;
            soundClose.PlayDelayed(0.1f);
            anm.Play("Close");
        }

        private void SetButtonState()
        {
            shoalImage.raycastTarget = isOpen;
            middleImage.raycastTarget = isOpen;
            deepImage.raycastTarget = isOpen;

            shoalButton.enabled = isOpen;
            middleButton.enabled = isOpen;
            deepButton.enabled = isOpen;
        }

        private void OnAnimationEnd()
        {
            isAnimating = false;
            SetButtonState();
        }
    }
}
