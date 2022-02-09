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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge.UI
{
    public class FishRightContent : MonoBehaviour
    {
        public Image iIcon;
        public Text tName;
        public Button bLeave;

        private FishData fd;
        private FishIndividualData fid;

        private void LeaveTeam()
        {
            SaveData.SaveDataManager.fishFormationSaveData.Remove(fid);
            UEventDispatcher.dispatchEvent(SCEvent.OnFishLeaveTeam, gameObject, fid);
        }

        public void Init(FishIndividualData data)
        {
            fid = data;
            fd = SingletonMB<FishManager>.Instance.database.GetFishData(data.type);
            iIcon.sprite = fd.icon;
            tName.text = fid.name;
            bLeave.onClick.AddListener(LeaveTeam);
        }
    }
}
