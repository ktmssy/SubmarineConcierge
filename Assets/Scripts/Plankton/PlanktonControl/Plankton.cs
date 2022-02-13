/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月21日
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

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// プランクトンのコントローラー
    /// </summary>
    [AddComponentMenu("_SubmarineConcierge/Plankton/Plankton")]
    public class Plankton : MonoBehaviour
    {
        /// <summary>
        /// スピード
        /// </summary>
        public float speed;

        public float collectspeed;

        /// <summary>
        /// ルートデータ
        /// </summary>
        public RouteData data;

        /// <summary>
        /// 位相
        /// </summary>
        public float phase = 0f;

        public PlanktonManager manager;

        public AudioSource sound;

        private Vector2 startPoint;

        private Vector2 endPoint;

        private bool clicked;

        /// <summary>
        /// 位相の更新
        /// </summary>
        public virtual void Updatephase()
        {
            //位相の更新
            phase += speed * Time.deltaTime;

            //位相Clamp
            if (phase > data.totalDistance)
                phase -= data.totalDistance;
            else if (phase < -data.totalDistance)
                phase += data.totalDistance;
        }

        protected virtual void UpdatePositionUncollected()
        {
            //移動
            if (transform.parent)
                transform.position = data.Lerp(phase, transform.parent.localToWorldMatrix);
            else
                transform.position = data.Lerp(phase);
        }

        protected virtual void UpdatePositionCollected()
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint, collectspeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, endPoint) < 0.00001f)
            {
                GainPoint();
                Destroy(gameObject);
            }
        }

        protected virtual void GainPoint()
        {
            manager.GainPP(this);
        }

        /// <summary>
        /// 座標の更新
        /// </summary>
        protected virtual void UpdatePosition()
        {
            if (!clicked)
            {
                UpdatePositionUncollected();
            }
            else
            {
                UpdatePositionCollected();
            }
        }

        private IEnumerator FadeInAnm()
        {
            ColorControl cc = GetComponent<ColorControl>();
            cc.enabled = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color color = cc.a;
            float a = 0f;
            float target = cc.a.a;
            while (a < target)
            {
                a += 0.01f;
                color.a = a;
                sr.color = color;
                yield return new WaitForSeconds(0.02f);
            }
            cc.enabled = true;
            yield break;
        }

        private IEnumerator VanishAnm()
        {
            GetComponent<ColorControl>().enabled = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color color = sr.color;
            float a = color.a;
            while (a > 0f)
            {
                a -= 0.01f;
                color.a = a;
                sr.color = color;
                yield return new WaitForSeconds(0.02f);
            }
            Destroy(gameObject);
            yield break;
        }

        public virtual void Vanish()
        {
            StartCoroutine(VanishAnm());
        }

        public virtual void Collect(Vector2 target)
        {
            if (clicked)
                return;
            startPoint = transform.position;
            endPoint = target;
            clicked = true;
            sound?.Play();
        }

        protected void Update()
        {
            Updatephase();
            UpdatePosition();
        }

        protected void Start()
        {
            StartCoroutine(FadeInAnm());
        }
    }
}
