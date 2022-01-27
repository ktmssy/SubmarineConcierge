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
        public static FishSaveData fishSaveData = new FishSaveData();
        public const string FishSaveDataFilename = "FishSaveData.bin";

        private static T Load<T>(string filename) where T : new()
        {
            try
            {
                string path = Application.persistentDataPath + "/" + filename;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(path, FileMode.OpenOrCreate);
                T ret = (T)bf.Deserialize(fs);
                fs.Close();
                return ret;
            }
            catch (Exception ex)
            {
                return new T();
            }
        }

        public static void Load()
        {
            fishSaveData = Load<FishSaveData>(FishSaveDataFilename);
        }

        private static void Save(string filename, object obj)
        {
            string path = Application.persistentDataPath + "/" + filename;
            Debug.Log("Save " + path);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(path);
            bf.Serialize(fs, obj);
            fs.Close();
        }

        public static void Save()
        {
            Save(FishSaveDataFilename, fishSaveData);
        }
    }
}
