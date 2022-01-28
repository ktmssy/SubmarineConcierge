/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年01月28日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.動作追加...神谷朋輝
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{
    public class FishWild : Fish
    {
        public float moveSpeed;    // 魚の移動速度
		private void Start()
		{
			// 右向きならスプライトを逆転させる
			if (moveSpeed > 0)
			{
				this.GetComponent<SpriteRenderer>().flipX = true;
			}
		}
		private void Update()
		{
			// 魚を等速で動かす
			this.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

			// 画面外に出たら消滅
			if (this.transform.position.x >= 20 || this.transform.position.x <= -20) 
			{
				FindObjectOfType<FishManager>().RemoveWildFish(gameObject);
				Destroy(this.gameObject);
			}
		}
	}
}
