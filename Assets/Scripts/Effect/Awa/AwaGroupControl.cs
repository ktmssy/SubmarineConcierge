/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月17日
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
    public class AwaGroupControl : MonoBehaviour
    {
        // public Vector2 xSpeedRange;
        public Vector2 ySpeedRange;
        public Vector2 zRange;
        public Vector2 xPercentage;
        //public Vector2 scaleRange;
        //public Vector2 alphaRange;
        //public GameObject prefabAwa;

        public float deltaY = 1f;

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

            float x = Random.Range(xRange.y * xPercentage.x, xRange.y * xPercentage.y);
            if (Random.Range(0f, 1f) < 0.5f)
            {
                x = -x;
            }

            origin = new Vector3(x, yRange.x - deltaY, Random.Range(zRange.x, zRange.y));

            transform.position = origin;
        }

        private void OnDestroy()
        {
            UEventDispatcher.dispatchEvent(SCEvent.OnAwaGroupDestroying, gameObject);
        }

        private void Update()
        {
            transform.Translate(0f, ySpeed * Time.deltaTime, 0f);
            if (transform.position.y > yRange.y + deltaY)
            {
                Destroy(gameObject);
            }
        }
    }
}
