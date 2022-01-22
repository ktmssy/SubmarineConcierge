/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月22日
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
    public class TouchManager : MonoBehaviour
    {
        public Transform target;

        private void CheckPlankton(Vector2 screenPos)
        {

            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, 1 << 10);
            if(hit.collider!=null)
            {
                hit.collider.gameObject.GetComponent<Plankton>().Collect(target.position);
            }
        }

        private void Update()
        {

#if UNITY_EDITOR||UNITY_STANDALONE
            if (Input.GetMouseButton(0))
            {
                CheckPlankton(Input.mousePosition);
            }
#endif
#if UNITY_IOS||UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    CheckPlankton touch.position);
                }
            }
#endif

        }
    }
}
