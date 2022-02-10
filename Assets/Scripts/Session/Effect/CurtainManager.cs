/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月10日
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
using SubmarineConcierge.Fish;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Session
{
    public class CurtainManager : MonoBehaviour
    {
        private Animator anm;

        private void OnSessionPrepare(UEvent e)
        {
            anm.SetTrigger("FadeIn");
        }

        private void PrepareStage()
        {
            UEventDispatcher.dispatchEvent(SCEvent.OnCurtainClosed, gameObject);
        }

        private void StartSession()
        {
            UEventDispatcher.dispatchEvent(SCEvent.OnCurtainOpened, gameObject);
        }

        private void Start()
        {
            anm = GetComponent<Animator>();
            UEventDispatcher.addEventListener(SCEvent.OnSessionPrepare, OnSessionPrepare);
        }

        private void OnDestroy()
        {

            UEventDispatcher.removeEventListener(SCEvent.OnSessionPrepare, OnSessionPrepare);
        }

    }
}
