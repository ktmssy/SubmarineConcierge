/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月13日
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

using SubmarineConcierge.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge
{
    public class AwaControl : MonoBehaviour
    {
        // public Vector2 xSpeedRange;
        public Vector2 ySpeedRange;
        public Vector2 zRange;
        public Vector2 scaleRange;
        public Vector2 alphaRange;
        public GameObject prefabAwa;

        private const float deltaY = 1f;

        // private float xSpeed;
        private float ySpeed;
        private Vector2 xRange;
        private Vector2 yRange;
        private Vector2 origin;


        private void Start()
        {
            var zero = Camera.main.ViewportToWorldPoint(Vector3.zero);
            var one = Camera.main.ViewportToWorldPoint(Vector3.one);
            xRange.x = zero.x;
            xRange.y = one.x;
            yRange.x = zero.y;
            yRange.y = one.y;

            ySpeed = Random.Range(ySpeedRange.x, ySpeedRange.y);

            float scale = Random.Range(scaleRange.x, scaleRange.y);
            transform.localScale = new Vector3(scale, scale, scale);

            var sr = GetComponent<SpriteRenderer>();
            var c = sr.color;
            c.a = Random.Range(alphaRange.x, alphaRange.y);
            sr.color = c;

            origin = new Vector3(Random.Range(xRange.x, xRange.y), yRange.x - deltaY, Random.Range(zRange.x, zRange.y));

            transform.position = origin;
        }

        private void Update()
        {
            transform.Translate(0f, ySpeed * Time.deltaTime, 0f);
            if (transform.position.y > yRange.y + deltaY)
            {
                UEventDispatcher.dispatchEvent(SCEvent.OnAwaDestroying, gameObject);
                Destroy(gameObject);
            }
        }
    }
}
