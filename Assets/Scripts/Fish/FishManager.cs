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
		private List<GameObject> wildFishes;


		private void Generate(FishIndividualData data)
		{
			FishData fishData = database.GetFishData(data.Type);
			GameObject obj;
			//Fish fish;
			if (data.IsTamed)
			{
				obj = Instantiate(fishData.PrefabTamed, Vector3.zero, Quaternion.identity);
				obj.transform.parent = transform;
			}
			else
			{
				obj = Instantiate(fishData.PrefabWild, Vector3.zero, Quaternion.identity);
				obj.transform.parent = transform;
			}
			obj.GetComponent<Fish>().Init(data, fishData);

		}

		private void Start()
		{
			/*SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
			SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
			SaveDataManager.fishSaveData.Add(new FishIndividualData(FishType.Iwashi, 0, "hya", true));
			SaveDataManager.Save();*/

			SaveDataManager.LoadOnce();
			//セーブデータを使って、テイムされた魚を生成する
			foreach (var fish in SaveDataManager.fishSaveData.Fishes)
				Generate(fish);
		}

		private void Update()
		{

		}
	}
}
