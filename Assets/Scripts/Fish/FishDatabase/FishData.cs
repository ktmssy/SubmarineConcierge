/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年01月29日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.Add z position...楊志庄
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{
	[CreateAssetMenu(fileName = "FishData", menuName = "SubmarineConcierge/Fish/FishData")]
	public class FishData : ScriptableObject
	{
		public FishType type;
		public InstrumentType instrument;
		public bool appearAtShoal;
		public bool appearAtMiddleSea;
		public bool appearAtDeapSea;
		public Sprite icon;
		public GameObject prefabWild;
		public GameObject prefabTamed;
		/* public Material TamedMaterial;
		 public Material WildMaterial;*/
		public int tamePP;
		public float z;
		public FishSize size;
	}
}
