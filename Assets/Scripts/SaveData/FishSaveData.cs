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

using Newtonsoft.Json;
using SubmarineConcierge.Fish;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{

    [System.Serializable]
    public class FishSaveData : SaveData
    {
        public List<FishIndividualData> Fishes = new List<FishIndividualData>();

        public void Add(FishIndividualData fish)
        {
            Fishes.Add(fish);
        }

        public void Remove(FishIndividualData fish)
        {
            Fishes.Remove(fish);
        }
    }
}
