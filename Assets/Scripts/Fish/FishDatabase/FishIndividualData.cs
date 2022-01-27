/******************************
 *
 *　作成者：
 *　作成日：
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
    [System.Serializable]
    public class FishIndividualData
    {
        public FishType Type;
        public int Friendship;
        public string Name;
        public bool IsTamed;

        public FishIndividualData(FishType type,int friendship,string name,bool isTamed)
        {
            Type = type;
            Friendship = friendship;
            Name = name;
            IsTamed = isTamed;
        }
    }
}
