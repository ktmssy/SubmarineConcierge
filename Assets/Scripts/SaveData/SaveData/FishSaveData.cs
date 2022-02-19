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

using SubmarineConcierge.Fish;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{

    [System.Serializable]
    public class FishSaveData : SaveData
    {
        public List<FishIndividualData> fishes = new List<FishIndividualData>();

        public void Add(FishIndividualData fish)
        {
            if (fishes == null)
                fishes = new List<FishIndividualData>();
            fishes.Add(fish);
            Save();
        }

        public void Remove(FishIndividualData fish)
        {
            fishes?.Remove(fish);
            Save();
        }

        public override void Save()
        {
            SaveDataManager.Save(SaveDataManager.fishSaveData);
        }

        public override void Load()
        {
            SaveDataManager.fishSaveData = SaveDataManager.Load<FishSaveData>();
        }

        public void ChangeName(string id, string name)
        {
            foreach (FishIndividualData fid in fishes)
            {
                if (fid.id != id)
                    continue;
                fid.name = name;
                Save();
                break;
            }
        }
    }
}
