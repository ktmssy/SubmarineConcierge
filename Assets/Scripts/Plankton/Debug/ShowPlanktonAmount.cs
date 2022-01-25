/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月25日
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

namespace SubmarineConcierge.Dbg
{
    public class ShowPlanktonAmount : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), FindObjectsOfType<Plankton.Plankton>().Length.ToString());
        }
#endif
    }
}
