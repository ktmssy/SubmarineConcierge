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

using SubmarineConcierge.Fish;
using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.UI
{
    public class FishRightManager : MonoBehaviour
    {
        List<FishIndividualData> teamFishes;
        [SerializeField] private GameObject prefabContent;

        private void ClearChildren()
        {
            FishRightContent[] frcs = GetComponentsInChildren<FishRightContent>();
            foreach (var frc in frcs)
            {
                Destroy(frc.gameObject);
            }
        }

        private void PrepareData()
        {
            teamFishes = SaveDataManager.fishFormationSaveData.fishes;
        }

        private void Generate(FishIndividualData fid)
        {
            GameObject obj = Instantiate(prefabContent, transform);
            FishRightContent frc = obj.GetComponent<FishRightContent>();
            frc.Init(fid);
        }

        private void GenerateBatch(List<FishIndividualData> fids)
        {
            foreach (var f in fids)
            {
                Generate(f);
            }
        }

        public void ReGenerate()
        {
            PrepareData();
            ClearChildren();
            GenerateBatch(teamFishes);
        }


        private void Start()
        {
            ReGenerate();
        }
    }
}
