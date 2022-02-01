/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年01月28日
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
using SubmarineConcierge.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{

    [AddComponentMenu("_SubmarineConcierge/Fish/FishManager")]
    public class FishManager : MonoBehaviour
    {
        [Header("Manage")]
        public FishDatabase database;
        [System.NonSerialized] public List<FishWild> wildFishes = new List<FishWild>();
        public int wildFishCount;
        private List<FishType> fishTypes = new List<FishType>();
        [System.NonSerialized] public Dictionary<string, FishTamed> tamedFishes = new Dictionary<string, FishTamed>();
        public SessionManager sessionManager;

        [Header("Sound")]
        public AudioSource tameSound;
        public AudioSource tameSuccessSound;
        public AudioSource tameChargeSound;

        [Header("Effect")]
        public GameObject tameEffectPrefab;

        public void Tame(string id,FishTamed fish)
        {
            tamedFishes.Add(id, fish);
        }

        public void RemoveWildFish(FishWild fish)
        {
            wildFishes.Remove(fish);
        }

        public void RemoveTamedFish(string id)
        {
            Debug.Log("Removing " + id + " " + tamedFishes.ContainsKey(id));
            if (!tamedFishes.ContainsKey(id))
                return;
            tamedFishes[id].Vanish();
            //Destroy(tamedFishes[id]);
            tamedFishes.Remove(id);
        }

        private void GenerateTamed(FishIndividualData data)
        {
            FishData fishData = database.GetFishData(data.type);
            GameObject obj = Instantiate(fishData.prefabTamed, Vector3.zero, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, fishData.z);
            FishTamed fish = obj.GetComponent<FishTamed>();
            fish.Init(data, fishData, true);
            tamedFishes.Add(data.id, fish);
        }

        private void GenerateWild(FishType type)
        {
            FishIndividualData fish = new FishIndividualData(type, SaveDataManager.fishTameProgressSaveData.GetProgress(type), "", false);
            FishData data = database.GetFishData(type);

            // 魚の向きを決定
            int direction = UnityEngine.Random.Range(-1, 2);
            while (direction == 0)
                direction = UnityEngine.Random.Range(-1, 2);

            // 魚の生成位置を決定
            Vector3 GeneratePos = new Vector3(18.0f * direction, UnityEngine.Random.Range(-3.0f, 9.0f));

            // 魚を生成
            GameObject obj = Instantiate(data.prefabWild, GeneratePos, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, data.z + 0.5f);

            FishWild fishWild = obj.GetComponent<FishWild>();
            fishWild.Init(fish, data, this);
            fishWild.moveSpeed = UnityEngine.Random.Range(2.0f, 3.5f);
            fishWild.moveSpeed *= -direction;
            wildFishes.Add(fishWild);
        }

        private void GenerateWildRandom()
        {
            FishType type = fishTypes[UnityEngine.Random.Range(0, fishTypes.Count)];
            GenerateWild(type);
        }

        private void Start()
        {
            /*SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
			SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
			SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
			SaveDataManager.Save();*/
            SaveDataManager.LoadOnce();

            foreach (FishData fish in database.fishDatas)
            {
                fishTypes.Add(fish.type);
            }

            //セーブデータを使って、テイムされた魚を生成する
            foreach (FishIndividualData fish in SaveDataManager.fishFormationSaveData.fishes)
            {
                GenerateTamed(fish);
            }
        }

        private void FixedUpdate()
        {
            if (sessionManager.Status != SessionStatus.Stop)
                return;
            while (wildFishes.Count < wildFishCount)
            {
                GenerateWildRandom();
            }
        }
    }
}
