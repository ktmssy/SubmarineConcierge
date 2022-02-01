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
        public FishType type;
        public int friendship;
        public string name;
        public bool isTamed;

        public FishIndividualData(FishType type, int friendship, string name, bool isTamed)
        {
            id = System.Guid.NewGuid().ToString();
            this.type = type;
            this.friendship = friendship;
            this.name = name;
            this.isTamed = isTamed;
        }
    }
}
