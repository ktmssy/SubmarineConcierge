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

using SubmarineConcierge.Event;
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
        private float clipVolume;
        private float clipVolumeLow;

        private float loopDelay = 0f;
        private GameObject prefabSessionEffect;
        private const float effectRadius = 2f;
        private const float effectIntervalMin = 1.5f;
        private const float effectIntervalMax = 3f;

        private GameObject fishStage;

        private Vector3 GetSessionPos()
        {
            Vector3 ret = SessionPositionManager.GetSessionPosition();
            ret.z = transform.position.z;
            return ret;
        }

        private void Start()
        {
            UEventDispatcher.addEventListener(SCEvent.OnCurtainClosed, OnCurtainClosed);
            UEventDispatcher.addEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnCurtainClosed, OnCurtainClosed);
            UEventDispatcher.removeEventListener(SCEvent.OnSessionEnd, OnSessionEnd);
        }

        private void OnCurtainClosed(UEvent e)
        {
            transform.position = targetPos;
            fishStage = Instantiate(SingletonMB<FishManager>.Instance.prefabFishStage, transform);
        }

        private void OnSessionEnd(UEvent e)
        {
            if (fishStage)
                Destroy(fishStage);
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
            clipVolume = track.volume;
            clipVolumeLow = 0f;

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
            Invoke("GenerateSessionEffect", Random.Range(effectIntervalMin, effectIntervalMax));
        }

        private IEnumerator VolumnDown(AudioSource source)
        {
            if (source == null)
                yield break;
            while (source.volume > clipVolumeLow)
            {
                if (source == null)
                    yield break;
                source.volume -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            yield break;
        }

        private IEnumerator VolumnUp(AudioSource source)
        {
            if (source == null)
                yield break;
            while (source.volume < clipVolume)
            {
                if (source == null)
                    yield break;
                source.volume += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            yield break;
        }

        private IEnumerator DamageProcess()
        {
            /*StartCoroutine(VolumnDown(sessionFirst));
			StartCoroutine(VolumnDown(sessionLoop));*/
            if (sessionFirst)
                sessionFirst.volume = clipVolumeLow;

            if (sessionLoop)
                sessionLoop.volume = clipVolumeLow;

            yield return new WaitForSeconds(1f);

            clipVolume = Mathf.Max(0f, clipVolume - 0.1f);

            if (sessionFirst)
                sessionFirst.volume = clipVolume;

            if (sessionLoop)
                sessionLoop.volume = clipVolume;
            /*StartCoroutine(VolumnUp(sessionFirst));
			StartCoroutine(VolumnUp(sessionLoop));*/
            yield break;
        }

        public void Damage()
        {
            Debug.Log("Fish damaged");
            StopAllCoroutines();
            StartCoroutine(DamageProcess());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Log"))
                return;
            Damage();
            Destroy(collision.gameObject);
        }

        private void GenerateSessionEffect()
        {
            //Debug.Log("GenerateSessionEffect " + (prefabSessionEffect != null) + " " + isInSession);
            if (prefabSessionEffect == null)
                return;
            if (!isInSession)
                return;
            GameObject obj = Instantiate(prefabSessionEffect, transform);
            Vector3 pos = obj.transform.position + (Vector3)(Random.insideUnitCircle * effectRadius);
            pos.z -= 0.01f;
            obj.transform.position = pos;
            Invoke("GenerateSessionEffect", Random.Range(effectIntervalMin, effectIntervalMax));
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
            targetPos = new Vector3(x, y, transform.position.z);
            return targetPos;
        }

        public override void Init(FishIndividualData fish, FishData data)
        {
            base.Init(fish, data);
            // todo 性能アップ
            posMax = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 1f)) + margin;
            posMin = (Vector2)Camera.main.ViewportToWorldPoint(Vector3.zero) - margin;
            NewWaitTime();
            NewTargetPos();
            prefabSessionEffect = Resources.Load<GameObject>("Effects/SessionEffect");
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
