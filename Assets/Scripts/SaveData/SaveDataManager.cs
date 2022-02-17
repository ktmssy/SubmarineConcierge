/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年01月29日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.Reflectionで自動セーブロード...楊志庄
 *　2.
 *　3.
 *
 ******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{
    public class SaveDataManager
    {
        private const string Postfix = ".bin";

        private static bool inited = false;

        public static FireLevelSaveData fireLevelSaveData= new FireLevelSaveData();

        public static FishSaveData fishSaveData = new FishSaveData();

        public static FishTameProgressSaveData fishTameProgressSaveData = new FishTameProgressSaveData();

        public static FishFormationSaveData fishFormationSaveData = new FishFormationSaveData();

        public static MapSaveData mapSaveData = new MapSaveData();


        public static T Load<T>() where T : new()
        {
            try
            {
                //セーブデータの保存パスを構築
                string filename = typeof(T).Name + Postfix;
                string path = Application.persistentDataPath + "/" + filename;
                Debug.Log("Load " + path);

                //ファイルからバイナリデータを読み込む
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

        public static void LoadOnce()
        {
            if (!inited)
                Load();
        }

        public static void Load()
        {
            //SaveDataManagerのすべてのpublic static fieldを取得
            foreach (FieldInfo info in typeof(SaveDataManager)
                .GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                Type type = info.FieldType;
                try
                {
                    //セーブデータの保存パスを構築
                    string filename = type.Name + Postfix;
                    string path = Application.persistentDataPath + "/" + filename;
                    Debug.Log("Load " + path);

                    //ファイルからバイナリデータを読み込む
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs = File.Open(path, FileMode.OpenOrCreate);
                    info.SetValue(new SaveDataManager(), bf.Deserialize(fs));

                    fs.Close();
                }
                catch (Exception ex)
                {
                    Debug.Log("Load exception " + ex.Message);
                }

            }
            inited = true;
        }

        /* public static void LoadOld()
         {
             fishSaveData = Load<FishSaveData>();
             fishTameProgressSaveData = Load<FishTameProgressSaveData>();
             inited = true;

         }*/

        public static void Save(object obj)
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
            //SaveDataManagerのすべてのpublic static fieldを取得
            foreach (FieldInfo info in typeof(SaveDataManager).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                Type type = info.FieldType;
                try
                {
                    //セーブデータの保存パスを構築
                    string filename = type.Name + Postfix;
                    string path = Application.persistentDataPath + "/" + filename;
                    Debug.Log("Save " + path);

                    //バイナリデータをファイルに書き込む
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs = File.Create(path);
                    bf.Serialize(fs, info.GetValue(new SaveDataManager()));
                    fs.Close();
                }
                catch (Exception ex)
                {
                    Debug.Log("Save exception " + ex.Message);
                }

            }
        }

        /*public static void SaveOld()
        {
            Save(fishSaveData);
            Save(fishTameProgressSaveData);
        }
*/

    }
}
