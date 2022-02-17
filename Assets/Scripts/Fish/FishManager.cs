/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年02月10日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.魚のZ軸をランダムにずれる...楊志庄
 *　2.魚に名前を付ける...楊志庄
 *　3.
 *
 ******************************/

using SubmarineConcierge.Event;
using SubmarineConcierge.SaveData;
using SubmarineConcierge.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{

    [AddComponentMenu("_SubmarineConcierge/Fish/FishManager")]
    public class FishManager : SingletonMB<FishManager>
    {
        [Header("Manage")]
        public FishAppearPlace place;
        public FishDatabase database;
        [System.NonSerialized] public List<FishWild> wildFishes = new List<FishWild>();
        public int wildFishCount;
        private List<FishType> fishTypes = new List<FishType>();
        [System.NonSerialized] public Dictionary<string, FishTamed> tamedFishes = new Dictionary<string, FishTamed>();
        private SessionManager sessionManager;
        public FishNameList nameList;
        private Dictionary<FishSize, List<FishData>> sizeDic;
        private List<FishSize> noSizeDataList = new List<FishSize>();
        private int sizeTypeCount;

        [Header("Sound")]
        public AudioSource tameSound;
        public AudioSource tameSuccessSound;
        public AudioSource tameChargeSound;

        [Header("Effect")]
        public GameObject tameEffectPrefab;

        [Header("Resources")]
        internal GameObject prefabFishStage;

        public void Tame(string id, FishTamed fish)
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
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, fishData.z + UnityEngine.Random.Range(0f, 0.5f));
            FishTamed fish = obj.GetComponent<FishTamed>();
            fish.Init(data, fishData, true);
            tamedFishes.Add(data.id, fish);
        }

        private void GenerateWild(FishType type)
        {
            FishIndividualData fish = new FishIndividualData(type, 0, nameList.GetRandom(), false);
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
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, data.z + 0.5f + UnityEngine.Random.Range(0f, 0.5f));

            FishWild fishWild = obj.GetComponent<FishWild>();
            fishWild.Init(fish, data/*, this*/);
            fishWild.moveSpeed = UnityEngine.Random.Range(2.0f, 3.5f);
            fishWild.moveSpeed *= -direction;
            wildFishes.Add(fishWild);
        }

        private void GenerateWildRandom()
        {
            FishSize targetSize = FishSize.Small;

            float random = UnityEngine.Random.Range(0f, 1f);

            if (!noSizeDataList.Contains(FishSize.Small) && random < 0.6f)
            {
                targetSize = FishSize.Small;
            }
            else if (!noSizeDataList.Contains(FishSize.Middle) && random < 0.9f)
            {
                targetSize = FishSize.Middle;
            }
            else if (!noSizeDataList.Contains(FishSize.Big))
            {
                targetSize = FishSize.Big;
            }
            else
            {
                return;
            }

            if (!sizeDic.ContainsKey(targetSize))
                return;

            var list = sizeDic[targetSize];
            if (list.Count == 0)
            {
                if (!noSizeDataList.Contains(targetSize))
                    noSizeDataList.Add(targetSize);
                return;
            }

            FishType type = list[UnityEngine.Random.Range(0, list.Count)].type;
            GenerateWild(type);
        }

        private void Awake()
        {
            SaveDataManager.LoadOnce();
            SaveDataManager.mapSaveData.SetCurrentPlace(place);
        }

        private void OnFishJoinTeam(UEvent e)
        {
            FishIndividualData fid = e.eventParams as FishIndividualData;
            GenerateTamed(fid);
        }

        private void OnFishLeaveTeam(UEvent e)
        {
            FishIndividualData fid = e.eventParams as FishIndividualData;
            RemoveTamedFish(fid.id);
        }

        private void Start()
        {
            base.Init();
            UEventDispatcher.dispatchEvent(SCEvent.OnMapChanged, gameObject);
            sizeDic = new Dictionary<FishSize, List<FishData>>();
            sizeTypeCount = 0;
            foreach (FishSize key in Enum.GetValues(typeof(FishSize)))
            {
                sizeDic.Add(key, new List<FishData>());
                sizeTypeCount++;
            }

            foreach (FishData fish in database.GetFishListByPlace(place))
            {
                fishTypes.Add(fish.type);
                sizeDic[fish.size]?.Add(fish);
            }

            foreach (FishSize key in sizeDic.Keys)
            {
                if (sizeDic[key].Count != 0)
                    continue;

                if (noSizeDataList.Contains(key))
                    continue;

                noSizeDataList.Add(key);
            }

            //セーブデータを使って、テイムされた魚を生成する
            foreach (FishIndividualData fish in SaveDataManager.fishFormationSaveData.fishes)
            {
                GenerateTamed(fish);
            }
            sessionManager = SingletonMB<SessionManager>.Instance;
            UEventDispatcher.addEventListener(SCEvent.OnFishJoinTeam, OnFishJoinTeam);
            UEventDispatcher.addEventListener(SCEvent.OnFishLeaveTeam, OnFishLeaveTeam);

            prefabFishStage = Resources.Load<GameObject>("Effects/FishStage");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UEventDispatcher.removeEventListener(SCEvent.OnFishJoinTeam, OnFishJoinTeam);
            UEventDispatcher.removeEventListener(SCEvent.OnFishLeaveTeam, OnFishLeaveTeam);
        }

        private void FixedUpdate()
        {
            if (sessionManager.status != SessionStatus.Stop)
                return;

            if (noSizeDataList.Count >= sizeTypeCount)
                return;

            while (wildFishes.Count < wildFishCount)
            {
                GenerateWildRandom();
            }
        }
    }
}
