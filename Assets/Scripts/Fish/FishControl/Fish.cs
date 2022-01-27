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
		public FishData Data;
		public int Friendship;
		public string Name;
		public bool IsTamed;
		//public Dictionary<ClothesType, ClothesData> Clothes;

		private bool inited = false;

		protected virtual void Init(FishData data, int friendship, string name, bool isTamed)
		{
			Data = data;
			Friendship = friendship;
			Name = name;
			IsTamed = isTamed;
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.sprite = Data.Sprite;
			sr.material = IsTamed ? Data.TamedMaterial : Data.WildMaterial;
			inited = true;
		}

	}
}
