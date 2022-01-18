/******************************
 *
 *　作成者：楊志庄
 *　作成日：#DATE#
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

namespace YoShiSho
{
    public class RouteEdge 
    {
        public Vector2 start;
        public Vector2 end;
        public float distance;

        public RouteEdge(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
            distance = Vector2.Distance(start, end);
        }
    }
}
