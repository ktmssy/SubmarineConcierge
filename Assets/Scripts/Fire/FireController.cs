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

using SubmarineConcierge.Event;
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
        private GameObject prefabLevelUpEffect;

        private void Start()
        {
            prefabFirePowder = Resources.Load<GameObject>("Effects/FirePowder");
            prefabLevelUpEffect = Resources.Load<GameObject>("Effects/FireLevelUp");
            tm = SingletonMB<TouchManager>.Instance;
            UEventDispatcher.addEventListener(SCEvent.OnFireLevelUp, OnFireLevelUp);
        }

        private void OnDestroy()
        {
            UEventDispatcher.removeEventListener(SCEvent.OnFireLevelUp, OnFireLevelUp);
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

        private void OnFireLevelUp(UEvent e)
        {
            Instantiate(prefabLevelUpEffect, transform).transform.Translate(0f, 0f, -0.5f);
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
