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
        public int logExp = 1;
        private GameObject prefabFirePowder;
        TouchManager tm;

        private void Start()
        {
            prefabFirePowder = Resources.Load<GameObject>("Effects/FirePowder");
            tm = SingletonMB<TouchManager>.Instance;
        }

        private void Process(Collider2D collision)
        {
            if (collision.CompareTag("LogDragging"))
            {
                tm.isDragging = false;
                tm.dragLog = null;
                Instantiate(prefabFirePowder, collision.transform.position, Quaternion.identity).transform.parent = transform;
                SaveData.SaveDataManager.fireLevelSaveData.AddExp(logExp);
                Destroy(collision.gameObject);
            }
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            Process(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Process(collision);
        }
    }
}
