/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月29日
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
    public class FishFormationSaveData : SaveData
    {
        private const int maxCount = 3;

        /* private FishManager _manager = null;
         private FishManager manager
         {
             get
             {
                 if (_manager == null)
                 {
                     GameObject obj = GameObject.FindGameObjectWithTag("FishManager");
                     if (obj != null)
                         _manager = obj.GetComponent<FishManager>();
                 }
                 return _manager;
             }
         }*/

        public List<FishIndividualData> Fishes = new List<FishIndividualData>();

        public void Add(FishIndividualData fish)
        {
            Fishes.Add(fish);
            FishManager manager = GameObject.FindGameObjectWithTag("FishManager")?.GetComponent<FishManager>();
            Debug.Log("maxCount " + maxCount + " ,fish count " + Fishes.Count);
            while (Fishes.Count > maxCount)
            {
                Debug.Log("manager is null? " + (manager == null));
                manager?.RemoveTamedFish(Fishes[0].id);
                Fishes.RemoveAt(0);
            }
            Save();
        }

        public void Remove(FishIndividualData fish)
        {
            Fishes.Remove(fish);
            Save();
        }

        public override void Save()
        {
            SaveDataManager.Save(SaveDataManager.fishFormationSaveData);
        }

        public override void Load()
        {
            SaveDataManager.fishFormationSaveData = SaveDataManager.Load<FishFormationSaveData>();
        }
    }
}
