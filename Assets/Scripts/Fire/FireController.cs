/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月07日
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

namespace SubmarineConcierge
{
    public class FireController : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("LogDragging"))
            {
                TouchManager tm = SingletonMB<TouchManager>.Instance;
                tm.isDragging = false;
                tm.dragLog = null;
                Destroy(collision.gameObject);
            }
        }
    }
}
