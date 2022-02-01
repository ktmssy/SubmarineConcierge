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
    [CustomEditor(typeof(LinearRouteData))]
    public class RouteDataEditor : Editor
    {
        LinearRouteData data;

        private void OnEnable()
        {
            data = target as LinearRouteData;
        }

        public override void OnInspectorGUI()
        {
            if(DrawDefaultInspector())
            {
                data.OnEnable();
            }

            if (GUILayout.Button("Smooth"))
            {
                if (data.smoothCount <= data.edges.Length)
                    return;
                Vector2[] newPoints = new Vector2[data.smoothCount];
                float value = 0f;
                float delta = data.totalDistance / data.smoothCount;
                for (int i = 0; i < data.smoothCount; ++i)
                {
                    newPoints[i] = data.Lerp(value);
                    value += delta;
                }
                data.points = newPoints;
                data.OnEnable();
            }
        }
    }
}
