/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月02日
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

namespace SubmarineConcierge.Effect
{
	public class SessionEffect : MonoBehaviour
	{
		public Sprite[] sprites;
		private SpriteRenderer sr;
		public float lifeTime;
		public Vector3 targetScale;
		public Vector3 startScale = Vector3.one;
		private float timer = 0f;
		private float halfLifeTime;

		private void Start()
		{
			sr = GetComponent<SpriteRenderer>();
			if (sprites.Length > 0)
				sr.sprite = sprites[Random.Range(0, sprites.Length)];
			halfLifeTime = lifeTime / 2f;
		}

		private void Update()
		{
			timer += Time.deltaTime;
			if (timer < lifeTime)
			{
				transform.localScale = Vector3.Lerp(startScale, targetScale, timer / lifeTime);
				if (timer > halfLifeTime)
				{
					var color = sr.color;
					color.a = Mathf.Lerp(color.a, 0f, (timer - halfLifeTime) / halfLifeTime);
					sr.color = color;
				}
			}
			else
			{
				Destroy(gameObject);
			}
		}

	}
}
