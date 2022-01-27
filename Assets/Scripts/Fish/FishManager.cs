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

using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{

    [AddComponentMenu("_SubmarineConcierge/Fish/FishManager")]
    public class FishManager : MonoBehaviour
    {
        [Header("Manage")]
        public GameObject FishPrefab;
        public FishDatabase database;

        [Header("Tamed")]
        public bool DoMove;
        public float Speed;
        public float WaitTimeMin;
        public float WaitTimeMax;
        public Vector2 Margin;

        private void Generate(FishIndividualData data)
        {
            GameObject obj = Instantiate(FishPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.parent = transform;
            Fish fish;
            if (data.IsTamed)
            {
                FishTamed f = obj.AddComponent<FishTamed>();
                f.DoMove = DoMove;
                f.Speed = Speed;
                f.WaitTimeMin = WaitTimeMin;
                f.WaitTimeMax = WaitTimeMax;
                f.Margin = Margin;
                fish = f;
            }
            else
            {
                fish = obj.AddComponent<FishWild>();
            }
            fish.Init(data, database.GetFishData(data.Type));

        }

        private void Start()
        {
           /* SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
            SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
            SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
            SaveDataManager.Save();*/

            SaveDataManager.Load();
            foreach (var fish in SaveDataManager.fishSaveData.Fishes)
                Generate(fish);

            /*Generate(new FishIndividualData(FishType.Iwashi, 0, "hya", true));

            foreach (var fish in FindObjectsOfType<Fish>())
            {
                SaveDataManager.fishSaveData.Add(fish.fish);
            }
            SaveDataManager.Save();*/
        }

        private void Update()
        {

        }
    }
}
