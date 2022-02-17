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
        private Dictionary<FishAppearPlace, List<FishData>> appearPlaceMap;

        public void OnEnable()
        {
            dataMap = new Dictionary<FishType, FishData>();
            appearPlaceMap = new Dictionary<FishAppearPlace, List<FishData>>();
            foreach (var fish in fishDatas)
            {
                dataMap.Add(fish.type, fish);

                if (fish.appearAtDeapSea)
                    AddToAppearPlaceMap(FishAppearPlace.Deep, fish);

                if (fish.appearAtMiddleSea)
                    AddToAppearPlaceMap(FishAppearPlace.Middle, fish);

                if (fish.appearAtShoal)
                    AddToAppearPlaceMap(FishAppearPlace.Shoal, fish);
            }
        }

        private void AddToAppearPlaceMap(FishAppearPlace place, FishData data)
        {
            if (appearPlaceMap.ContainsKey(place))
            {
                appearPlaceMap[place].Add(data);
                return;
            }
            var list = new List<FishData>();
            list.Add(data);
            appearPlaceMap.Add(place, list);
        }

        public List<FishData> GetFishListByPlace(FishAppearPlace place)
        {
            if (!appearPlaceMap.ContainsKey(place))
                appearPlaceMap.Add(place, new List<FishData>());
            return appearPlaceMap[place];
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
