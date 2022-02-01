/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
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

using SubmarineConcierge.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{
    [AddComponentMenu("_SubmarineConcierge/Fish/FishTamed")]
    public class FishTamed : Fish
    {
        //public bool DoMove;
        public float speed;
        public float waitTimeMin;
        public float waitTimeMax;
        public Vector2 margin;
        public bool isReadyForSession = false;
        private bool isPreparingForSessiong = false;
        private bool isInSession = false;

        private Vector2 posMin;
        private Vector2 posMax;

        private float timer;
        private float waitTime;
        private Vector3 targetPos;
        private AudioSource sessionFirst;
        private AudioSource sessionLoop;

        private float loopDelay = 0f;

        private Vector3 GetSessionPos()
        {
            Vector3 ret = SessionPositionManager.GetSessionPosition();
            ret.z = data.z;
            return ret;
        }

        public void PrepareSession(MusicData music)
        {
            isReadyForSession = false;
            isInSession = false;
            isPreparingForSessiong = true;
            targetPos = GetSessionPos();

            SoundTrack track = music.GetTrack(data.instrument);
            if (track == null)
                return;

            sessionFirst = gameObject.AddComponent<AudioSource>();
            sessionFirst.clip = track.clipFirst;
            sessionFirst.loop = false;
            sessionFirst.volume = track.volume;
            sessionFirst.playOnAwake = false;
            sessionFirst.Stop();

            sessionLoop = gameObject.AddComponent<AudioSource>();
            sessionLoop.clip = track.clipLoop;
            sessionLoop.loop = true;
            sessionLoop.volume = track.volume;
            sessionLoop.playOnAwake = false;
            sessionLoop.Stop();

            loopDelay = track.clipFirst.length - 0.2f;
        }

        public void StartSession()
        {
            isPreparingForSessiong = false;
            isInSession = true;
            //todo animation

            sessionFirst.Play();

            Invoke("DoLoop", loopDelay);
        }

        private void DoLoop()
        {
            sessionFirst?.Stop();
            sessionLoop?.Play();
            Destroy(sessionFirst);
            sessionFirst = null;
        }

        public void StopSession()
        {
            isReadyForSession = false;
            isInSession = false;
            sessionFirst?.Stop();
            sessionLoop?.Stop();
            Destroy(sessionFirst);
            sessionFirst = null;
            Destroy(sessionLoop);
            sessionLoop = null;
            NewTargetPos();
            NewWaitTime();
        }

        private float NewWaitTime()
        {
            timer = 0f;
            waitTime = Random.Range(waitTimeMin, waitTimeMax);
            return waitTime;
        }

        private Vector3 NewTargetPos()
        {
            float x = Random.Range(posMin.x, posMax.x);
            float y = Random.Range(posMin.y, posMax.y);
            targetPos = new Vector3(x, y, data.z);
            return targetPos;
        }

        public override void Init(FishIndividualData fish, FishData data)
        {
            base.Init(fish, data);
            posMax = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 1f)) + margin;
            posMin = (Vector2)Camera.main.ViewportToWorldPoint(Vector3.zero) - margin;
            NewWaitTime();
            NewTargetPos();
        }

        public void Init(FishIndividualData fish, FishData data, bool randomStart)
        {
            Init(fish, data);
            if (randomStart)
            {
                waitTime = 0f;
                transform.position = targetPos;
            }
        }

        private void FixedUpdate()
        {
            /*if (!DoMove)
                return;*/
            if (isInSession || isReadyForSession)
                return;

            Vector2 direction = targetPos - transform.position;
            if (direction.sqrMagnitude < 1f)
            {
                if (isPreparingForSessiong)
                {
                    transform.localScale = new Vector3(transform.position.x > 0 ? 1f : -1f, 1f, 1f);
                    isReadyForSession = true;
                    return;
                }
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {

                    NewTargetPos();
                    NewWaitTime();
                }
            }
            else
            {
                transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
            }
            transform.localScale = new Vector3(direction.x > 0 ? -1f : 1f, 1f, 1f);
        }

        private IEnumerator VanishAnimation()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color color = sr.color;
            while (color.a > 0)
            {
                color.a -= 0.1f;
                sr.color = color;
                yield return new WaitForSeconds(0.02f);
            }
            Destroy(gameObject);
            yield break;
        }

        public void Vanish()
        {
            StartCoroutine(VanishAnimation());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 leftBottom = posMin;
            Vector3 rightTop = posMax;
            Vector3 leftTop = new Vector3(posMin.x, posMax.y);
            Vector3 rightBottom = new Vector3(posMax.x, posMin.y);
            Gizmos.DrawLine(leftBottom, leftTop);
            Gizmos.DrawLine(rightTop, leftTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(leftBottom, rightBottom);
        }
    }
}
