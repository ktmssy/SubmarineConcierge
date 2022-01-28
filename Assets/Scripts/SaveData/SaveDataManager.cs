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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{
	public class SaveDataManager
	{
		public const string Postfix = ".bin";

		public static FishSaveData fishSaveData = new FishSaveData();

		public static FishTameProgressSaveData fishTameProgressSaveData = new FishTameProgressSaveData();

		private static T Load<T>() where T : new()
		{
			try
			{
				string filename = fishTameProgressSaveData.GetType().Name + Postfix;
				string path = Application.persistentDataPath + "/" + filename;
				Debug.Log("Load " + path);
				BinaryFormatter bf = new BinaryFormatter();
				FileStream fs = File.Open(path, FileMode.OpenOrCreate);
				T ret = (T)bf.Deserialize(fs);
				fs.Close();
				return ret;
			}
			catch (Exception ex)
			{
				Debug.Log("Load exception " + ex.Message);
				return new T();
			}
		}

		public static void Load()
		{
			fishSaveData = Load<FishSaveData>();
			fishTameProgressSaveData = Load<FishTameProgressSaveData>();
		}

		private static void Save(object obj)
		{
			string filename = obj.GetType().Name + Postfix;
			string path = Application.persistentDataPath + "/" + filename;
			Debug.Log("Save " + path);
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Create(path);
			bf.Serialize(fs, obj);
			fs.Close();
		}

		public static void Save()
		{
			Save(fishSaveData);
			Save(fishTameProgressSaveData);
		}
	}
}
