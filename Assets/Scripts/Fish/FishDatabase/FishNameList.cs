/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月09日
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
    [CreateAssetMenu(fileName = "FishNameList", menuName = "SubmarineConcierge/Fish/FishNameList")]
    public class FishNameList : ScriptableObject
    {
        [SerializeField] private string[] names;

        public string GetRandom()
        {
            if (names == null || names.Length == 0)
                return "";
            return names[Random.Range(0, names.Length)];
        }
    }
}
