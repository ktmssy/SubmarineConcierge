/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
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

namespace SubmarineConcierge.Plankton
{
    [CustomEditor(typeof(RouteData))]
    public class RouteDataEditor : Editor
    {
        RouteData data;

        private void OnEnable()
        {
            data = target as RouteData;
        }

        public override void OnInspectorGUI()
        {
            if(DrawDefaultInspector())
            {
                data.OnEnable();
            }

            if (GUILayout.Button("Smooth"))
            {
                if (data.SmoothCount <= data.Edges.Length)
                    return;
                Vector2[] newPoints = new Vector2[data.SmoothCount];
                float value = 0f;
                float delta = data.TotalDistance / data.SmoothCount;
                for (int i = 0; i < data.SmoothCount; ++i)
                {
                    newPoints[i] = data.Lerp(value);
                    value += delta;
                }
                data.Points = newPoints;
                data.OnEnable();
            }
        }
    }
}
