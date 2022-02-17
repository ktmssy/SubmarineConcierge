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

using SubmarineConcierge.Event;
using SubmarineConcierge.Fire;
using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge
{
    public class FireUI : MonoBehaviour
    {
        public GameObject fire;
        public Text textExp;
        public Text textLevel;
        public Slider slider;

        private bool isAnimating = false;
        private Animator anm;

        private void Start()
        {
            anm = GetComponent<Animator>();
            transform.position = fire.transform.position + new Vector3(0f, 2f);
            UEventDispatcher.addEventListener(SCEvent.OnFireLevelUp, OnFireLevelUp);
            UEventDispatcher.addEventListener(SCEvent.OnFireExpChanged, OnFireExpChanged);
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnFireLevelUp, OnFireLevelUp);
            UEventDispatcher.removeEventListener(SCEvent.OnFireExpChanged, OnFireExpChanged);
        }

        private void Refresh()
        {
            FireLevelSaveData save = SaveDataManager.fireLevelSaveData;
            LevelData data = save.currentLevelData;
            int level = data.level;
            int needExp = data.expToNextLevel;
            int exp = save.exp;
            textLevel.text = level.ToString();
            textExp.text = exp + "/" + needExp;
            slider.maxValue = needExp;
            slider.value = exp;
            if (!isAnimating)
                anm.Play("Appear");
        }

        private void OnFireExpChanged(UEvent e)
        {
            Refresh();
        }

        private void OnFireLevelUp(UEvent e)
        {
            Refresh();
        }

        private void OnAnimationEnd()
        {
            isAnimating = false;
        }
    }
}
