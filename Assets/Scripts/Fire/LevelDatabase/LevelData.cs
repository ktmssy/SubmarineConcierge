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
using UnityEngine;

namespace SubmarineConcierge.Fire
{

    /// <summary>
    /// 火のレベルごとのデータ
    /// </summary>
    [System.Serializable]
    public class LevelData
    {
        /// <summary>
        /// レベル
        /// </summary>
        public int Level;

        /// <summary>
        /// プランクトンの数
        /// </summary>
        public int PlanktonAmount;

        /// <summary>
        /// 1匹のプランクトンが1秒間産出するプランクトンポイント数
        /// </summary>
        public float PlanktonPointPerSecond;
    }
}
