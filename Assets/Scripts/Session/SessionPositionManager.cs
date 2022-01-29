/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月29日
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

namespace SubmarineConcierge
{
    [AddComponentMenu("_SubmarineConcierge/Session/SessionPositionManager")]
    public class SessionPositionManager : MonoBehaviour
    {
        private static List<Vector3> positions = new List<Vector3>();

        private static int top = 0;

        private void Start()
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t == transform)
                    continue;
                positions.Add(t.position);
            }
        }

        public static Vector3 GetSessionPosition()
        {
            if (positions.Count == 0)
                return Vector3.zero;
            Vector3 ret = positions[top];
            if (++top >= positions.Count)
                top = 0;
            return ret;
        }
    }
}
