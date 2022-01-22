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
using UnityEditor;
using UnityEngine;

namespace SubmarineConcierge.Fire
{
    /// <summary>
    /// レベルデータベースのインスペクター拡張
    /// </summary>
    [CustomEditor(typeof(LevelDatabase))]
    public class LevelDatabaseEditor : Editor
    {
        /// <summary>
        /// レベルデータベース
        /// </summary>
        LevelDatabase database;

        private void OnEnable()
        {
            //レベルデータベースを取得
            database = target as LevelDatabase;
        }

        public override void OnInspectorGUI()
        {
            //編集された度にデータを更新する
            if (DrawDefaultInspector())
            {
                database.OnEnable();
            }
        }
    }
}
