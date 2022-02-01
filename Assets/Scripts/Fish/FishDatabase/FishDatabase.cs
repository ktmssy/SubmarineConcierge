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

namespace SubmarineConcierge.Fish
{
    [CreateAssetMenu(fileName = "FishDatabase", menuName = "SubmarineConcierge/Fish/FishDatabase")]
    public class FishDatabase : ScriptableObject
    {
        public FishData[] fishDatas;
        private Dictionary<FishType, FishData> dataMap;

        public void OnEnable()
        {
            dataMap = new Dictionary<FishType, FishData>();
            foreach (var fish in fishDatas)
            {
                dataMap.Add(fish.type, fish);
            }
        }

        public FishData GetFishData(FishType type)
        {
            if (dataMap == null)
                return null;
            if (!dataMap.ContainsKey(type))
                return null;
            return dataMap[type];
        }
    }
}
