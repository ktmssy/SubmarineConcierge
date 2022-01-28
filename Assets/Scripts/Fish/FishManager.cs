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
		public FishDatabase Database;
		private List<GameObject> wildFishes = new List<GameObject>();
		public int WildFishCount;
		private List<FishType> fishTypes = new List<FishType>();


		private void GenerateTamed(FishIndividualData data)
		{
			FishData fishData = Database.GetFishData(data.Type);
			GameObject obj = Instantiate(fishData.PrefabTamed, Vector3.zero, Quaternion.identity);
			obj.transform.parent = transform;
			obj.GetComponent<Fish>().Init(data, fishData);
		}

		private void GenerateWild(FishType type)
		{
			FishIndividualData fish = new FishIndividualData(type, SaveDataManager.fishTameProgressSaveData.GetProgress(type), "", false);
			FishData data = Database.GetFishData(type);
			Debug.Log(type);
			/*if (data == null)
				return;*/
			GameObject obj = Instantiate(data.PrefabWild, Vector3.zero, Quaternion.identity);
			obj.transform.parent = transform;
			obj.GetComponent<Fish>().Init(fish, data);
			wildFishes.Add(obj);
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

			foreach(var fish in Database.FishDatas)
			{
				fishTypes.Add(fish.Type);
			}

			//セーブデータを使って、テイムされた魚を生成する
			foreach (var fish in SaveDataManager.fishSaveData.Fishes)
				GenerateTamed(fish);
		}

		private void FixedUpdate()
		{
			while (wildFishes.Count < WildFishCount)
			{
				GenerateWildRandom();
			}
		}
	}
}
