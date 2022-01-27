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

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SubmarineConcierge.Fish
{
    [CustomEditor(typeof(FishDatabase))]
    public class FishDatabaseEditor : Editor
    {
        FishDatabase database;
		private void OnEnable()
		{
            database = target as FishDatabase;
		}

        public override void OnInspectorGUI()
        {
            if (DrawDefaultInspector())
            {
                database.OnEnable();
            }
        }
    }
}
