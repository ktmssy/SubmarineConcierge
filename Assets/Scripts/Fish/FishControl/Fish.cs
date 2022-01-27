/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月26日
 *　更新日：2022年01月27日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.データストラクチャー変更...楊志庄
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{
    public class Fish : MonoBehaviour
    {
        public FishIndividualData fish;
        private FishData data;
        //public Dictionary<ClothesType, ClothesData> Clothes;

        private bool inited = false;

        public virtual void Init(FishIndividualData fish,FishData data)
        {
            this.fish = fish;
            this.data = data;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = data.Sprite;
            sr.material = fish.IsTamed ? data.TamedMaterial : data.WildMaterial;
            inited = true;
        }

    }
}
