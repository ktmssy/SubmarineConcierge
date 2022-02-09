/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月22日
 *　更新日：2022年01月29日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.テイムチェック....楊志庄
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge
{

    [AddComponentMenu("_SubmarineConcierge/TouchManager")]
    public class TouchManager : SingletonMB<TouchManager>
    {
        public Transform target;
        private Session.SessionManager sessionManager;

        public GameObject dragLog;
        public bool isDragging = false;

        private void Start()
        {
            sessionManager = SingletonMB<Session.SessionManager>.Instance;
        }

        private bool CheckUI(Ray ray)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, 1 << 5);
            return hit.collider != null;
        }

        private void CheckPlankton(Vector2 screenPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, 1 << 10);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Plankton.Plankton>().Collect(target.position);
            }
        }

        private void CheckDownFishWild(Vector2 screenPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, 1 << 11);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Fish.FishWild>().StartTame();
            }
        }

        private void CheckUpFishWild(Vector2 screenPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, 1 << 11);
            foreach (var hit in hits)
            {
                hit.collider.gameObject.GetComponent<Fish.FishWild>().EndTame();
            }
        }

        private void Update()
        {
            if (sessionManager.status == Session.SessionStatus.Play)
            {
#if UNITY_EDITOR || UNITY_STANDALONE

                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, 1 << 9);
                    if (hit.collider != null)
                    {
                        isDragging = true;
                        dragLog = hit.collider.gameObject;
                        dragLog.gameObject.tag = "LogDragging";
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isDragging = false;
                    if (dragLog != null)
                    {
                        dragLog.gameObject.tag = "Log";
                        dragLog = null;
                    }
                }
                else if (isDragging)
                {
                    if (dragLog == null)
                    {
                        isDragging = false;
                        return;
                    }
                    var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pos.z = dragLog.transform.position.z;
                    dragLog.transform.position = pos;
                }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    CheckPlankton(touch.position);
                }
                if (touch.phase == TouchPhase.Began)
                {
                    CheckDownFishWild(touch.position);
                }
                /*if (touch.phase == TouchPhase.Ended)
                {
                    CheckUpFishWild(touch.position);
                }*/
            }
#endif
            }
            else if (sessionManager.status == Session.SessionStatus.Stop)
            {

#if UNITY_EDITOR || UNITY_STANDALONE
                if (Input.GetMouseButton(0))
                {
                    CheckPlankton(Input.mousePosition);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    CheckDownFishWild(Input.mousePosition);
                }
                /*if (Input.GetMouseButtonUp(0))
                {
                    CheckUpFishWild(Input.mousePosition);
                }*/
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    CheckPlankton(touch.position);
                }
                if (touch.phase == TouchPhase.Began)
                {
                    CheckDownFishWild(touch.position);
                }
                /*if (touch.phase == TouchPhase.Ended)
                {
                    CheckUpFishWild(touch.position);
                }*/
            }
#endif
            }
        }
    }
}
