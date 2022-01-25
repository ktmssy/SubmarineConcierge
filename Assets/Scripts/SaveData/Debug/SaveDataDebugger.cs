/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月25日
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

using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Dbg
{
    public class SaveDataDebugger : MonoBehaviour
    {
#if UNITY_EDITOR
        public int FireLevel;
        public int GainedPP;
        public int HoldPP;
        [ReadOnly]
        public string LastTime;

    
        private void Update()
        {
            FireLevel = FireLevelSaveData.Level;
            GainedPP = PPSaveData.Gained;
            HoldPP = PPSaveData.Hold;
            LastTime = PPTimeSaveData.Time.ToString();
        }

        public void Save()
        {
            FireLevelSaveData.Level = FireLevel;
            PPSaveData.Gained = GainedPP;
            PPSaveData.Hold = HoldPP;
        }
#endif
    }
}
