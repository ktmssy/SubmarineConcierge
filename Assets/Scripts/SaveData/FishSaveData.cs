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
	public class FishSaveData
	{
		private List<Fish.Fish> fishes = null;

		public List<Fish.Fish> Fishes
		{
			get
			{
				if (fishes == null)
					return Load();
				return fishes;
			}
		}

		public void Add(Fish.Fish fish)
		{
			fishes.Add(fish);
			Save();
		}

		public void Remove(Fish.Fish fish)
		{
			fishes.Remove(fish);
			Save();
		}

		private List<Fish.Fish> Load()
		{
			// todo
			fishes = new List<Fish.Fish>();
			return fishes;
		}

		private void Save()
		{
			// todo
		}
	}
}
