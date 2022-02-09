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
    public class FIshLeftManager : MonoBehaviour
    {
        List<FishIndividualData> allFishes;
        [SerializeField] private GameObject prefabContent;

        private void ClearChildren()
        {
            FishLeftContent[] flcs = GetComponentsInChildren<FishLeftContent>();
            foreach (var flc in flcs)
            {
                Destroy(flc.gameObject);
            }
        }

        private void PrepareData()
        {
            allFishes = new List<FishIndividualData>();
            SaveDataManager.fishSaveData.fishes.ForEach(f => allFishes.Add(f));
            List<FishIndividualData> toDelete = new List<FishIndividualData>();
            foreach (var teamd in SaveDataManager.fishFormationSaveData.fishes)
            {
                foreach (var tamed in allFishes)
                {
                    if (teamd.id == tamed.id)
                    {
                        toDelete.Add(tamed);
                        continue;
                    }
                }
            }
            foreach (var d in toDelete)
                allFishes.Remove(d);
        }

        private void Generate(FishIndividualData fid)
        {
            GameObject obj = Instantiate(prefabContent, transform);
            FishLeftContent flc = obj.GetComponent<FishLeftContent>();
            flc.Init(fid);
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
            GenerateBatch(allFishes);
        }


        private void Start()
        {
            ReGenerate();
        }
    }
}
