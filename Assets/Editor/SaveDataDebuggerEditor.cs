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

using SubmarineConcierge.Dbg;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SubmarineConcierge
{
    [CustomEditor(typeof(SaveDataDebugger))]
    public class SaveDataDebuggerEditor : Editor
    {
        SaveDataDebugger debugger;

        private void OnEnable()
        {
            debugger = target as SaveDataDebugger;
        }

        public override void OnInspectorGUI()
        {
            if (DrawDefaultInspector())
            {
                debugger.Save();
            }
        }
    }
}
