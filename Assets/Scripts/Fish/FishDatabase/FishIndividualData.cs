/******************************
 *
 *　作成者：楊志庄
 *　作成日：
 *　更新日：2022年01月29日
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
    [System.Serializable]
    public class FishIndividualData
    {
        public string id;
        public FishType Type;
        public int Friendship;
        public string Name;
        public bool IsTamed;

        public FishIndividualData(FishType type, int friendship, string name, bool isTamed)
        {
            id = System.Guid.NewGuid().ToString();
            Type = type;
            Friendship = friendship;
            Name = name;
            IsTamed = isTamed;
        }
    }
}
