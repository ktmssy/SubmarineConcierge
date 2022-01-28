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

		public void RemoveWildFish(GameObject fish)
		{
			wildFishes.Remove(fish);
		}

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
			//todo 魚の生成場所をランダムに、向きの初期化
			// 魚の向きを決定
			int direction = UnityEngine.Random.Range(-1, 2);
			while(direction == 0)
				direction = UnityEngine.Random.Range(-1, 2);
			// 魚の生成位置を決定
			Vector3 GeneratePos = new Vector3(18.0f * direction, UnityEngine.Random.Range(-3.0f, 9.0f));
			// 魚を生成
			GameObject obj = Instantiate(data.PrefabWild, GeneratePos, Quaternion.identity);
			obj.transform.parent = transform;
			obj.GetComponent<Fish>().Init(fish, data);
			obj.GetComponent<FishWild>().moveSpeed = UnityEngine.Random.Range(2.0f, 3.5f);
			obj.GetComponent<FishWild>().moveSpeed *= -direction;
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

			foreach (var fish in Database.FishDatas)
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
