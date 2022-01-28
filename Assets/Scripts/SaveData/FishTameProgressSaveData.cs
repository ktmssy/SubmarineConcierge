/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月28日
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
using SubmarineConcierge.Fish;
using System;

namespace SubmarineConcierge.SaveData
{
	[System.Serializable]
	public class FishTameProgressSaveData : SaveData
	{
		private Dictionary<FishType, int> progress;

		public FishTameProgressSaveData()
		{
			progress = new Dictionary<FishType, int>();
			foreach (FishType type in Enum.GetValues(typeof(FishType)))
			{
				progress.Add(type, 0);
			}
		}

		public void AddProgress(FishType type, int value)
		{
			progress[type] += value;
		}

		public int GetProgress(FishType type)
		{
			return progress[type];
		}
	}
}
