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
using UnityEngine;

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// ルートの辺情報
    /// </summary>
    public class RouteEdge 
    {
        /// <summary>
        /// スタートポイント
        /// </summary>
        public Vector2 Start;

        /// <summary>
        /// エンドポイント
        /// </summary>
        public Vector2 End;

        /// <summary>
        /// 長さ
        /// </summary>
        public float Distance;

        /// <summary>
        /// 構造関数コンストラクタ
        /// </summary>
        /// <param name="start">スタート座標</param>
        /// <param name="end">エンド座標</param>
        public RouteEdge(Vector2 start, Vector2 end)
        {
            this.Start = start;
            this.End = end;
            Distance = Vector2.Distance(start, end);
        }

    }
}
