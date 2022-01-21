/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
 *　更新日：2022年01月21日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.クラス名を変更...楊志庄
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SubmarineConcierge.Plankton
{
    [CustomEditor(typeof(PlanktonGenerator))]
    public class PlanktonGeneratorEditor : Editor
    {
        PlanktonGenerator generator;

        private void OnEnable()
        {
            generator = target as PlanktonGenerator;
        }

        private void Generate()
        {
            if (Application.isPlaying && generator)
                generator.Generate();
        }

        private void Clear()
        {
            if (Application.isPlaying && generator.gameObject)
                foreach (var c in generator.gameObject.GetComponentsInChildren<Plankton>())
                    Destroy(c.gameObject);
        }

        private void Reset()
        {
            Clear();
            Generate();
        }

        public override void OnInspectorGUI()
        {
            if (DrawDefaultInspector())
            {
                Reset();
            }

            if (GUILayout.Button("Generate"))
            {
                Generate();
            }

            if (GUILayout.Button("Clear"))
            {
                Clear();
            }

            if (GUILayout.Button("Reset"))
            {
                Reset();
            }
        }
    }
}
