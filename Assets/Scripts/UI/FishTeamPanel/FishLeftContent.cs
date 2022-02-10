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
using SubmarineConcierge.Fish;
using SubmarineConcierge.SaveData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge.UI
{
    public class FishLeftContent : MonoBehaviour
    {
        public Image iIcon;
        public Text tName;
        public InputField ifName;
        public Button bJoin;
        public Button bChangeName;

        private FishData fd;
        private FishIndividualData fid;

        private bool isChangingName = false;

        private void JoinTeam()
        {
            SaveData.SaveDataManager.fishFormationSaveData.Add(fid);
            UEventDispatcher.dispatchEvent(SCEvent.OnFishJoinTeam, gameObject, fid);
        }

        private void ChangeName()
        {
            if(isChangingName)
            {
                OnNameChanged(ifName.text);
            }
            else
            {
                tName.gameObject.SetActive(false);
                ifName.gameObject.SetActive(true);
                ifName.ActivateInputField();
                isChangingName = true;
            }
        }

        private void OnNameChanged(string newName)
        {
            tName.gameObject.SetActive(true);
            ifName.gameObject.SetActive(false);
            SaveDataManager.fishSaveData.ChangeName(fid.id, newName);
            UEventDispatcher.dispatchEvent(SCEvent.OnFishNameChanged, gameObject, newName);
            isChangingName = false;
        }

        public void Init(FishIndividualData data)
        {
            fid = data;
            fd = SingletonMB<FishManager>.Instance.database.GetFishData(data.type);
            iIcon.sprite = fd.icon;
            tName.text = fid.name;
            ifName.text = fid.name;
            bJoin.onClick.AddListener(JoinTeam);
            bChangeName.onClick.AddListener(ChangeName);
            ifName.onEndEdit.AddListener(OnNameChanged);
        }
    }
}
