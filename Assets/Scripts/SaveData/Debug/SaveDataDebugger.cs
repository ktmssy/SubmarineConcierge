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
        public int fireLevel;
        public int gainedPP;
        public int holdPP;
        [ReadOnly]
        public string lastTime;

    
        private void Update()
        {
            fireLevel = FireLevelSaveData.level;
            gainedPP = PPSaveData.gained;
            holdPP = PPSaveData.hold;
            lastTime = PPTimeSaveData.time.ToString();
        }

        public void Save()
        {
            FireLevelSaveData.level = fireLevel;
            PPSaveData.gained = gainedPP;
            PPSaveData.hold = holdPP;
        }
#endif
    }
}
