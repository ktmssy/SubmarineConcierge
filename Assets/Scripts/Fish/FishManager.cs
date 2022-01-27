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

using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{

    [AddComponentMenu("_SubmarineConcierge/Fish/FishManager")]
    public class FishManager : MonoBehaviour
    {

        private void Start()
        {
            foreach (var fish in FindObjectsOfType<Fish>())
            {
                SaveDataManager.fishSaveData.Add(fish);
            }
            SaveDataManager.fishSaveData.Save();
        }

        private void Update()
        {

        }
    }
}
