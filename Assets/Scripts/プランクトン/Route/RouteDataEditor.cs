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

namespace YoShiSho
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
            base.OnInspectorGUI();
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
                /*RouteEdge[] newEdges = new RouteEdge[data.smoothCount];
                float value = 0f;
                float delta = data.totalDistance / data.smoothCount;
                for (int i = 0; i < data.smoothCount; ++i)
                {
                    newEdges[i] = new RouteEdge(data.Lerp(value), data.Lerp(value + delta));
                    value += delta;
                }
                data.edges = newEdges;*/
            }
        }
    }
}
