/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{
    public class SaveDataManager : ISaveData
    {
        public static FishSaveData fishSaveData = new FishSaveData();

        public void Load()
        {
            fishSaveData.Load();
        }

        public void Save()
        {
            fishSaveData.Save();
        }
    }
}
