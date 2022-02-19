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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{
    [System.Serializable]
    public class MapSaveData : SaveData
    {
        public FishAppearPlace currentPlace { get; private set; } = FishAppearPlace.Shoal;

        public void SetCurrentPlace(FishAppearPlace place)
        {
            currentPlace = place;
            Save();
        }

        public override void Save()
        {
            SaveDataManager.Save(SaveDataManager.mapSaveData);
        }
    }
}
