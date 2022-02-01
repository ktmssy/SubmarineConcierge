/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月22日
 *　更新日：2022年01月25日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.PP産出アルゴリズムの変更に伴うデータストラクチャーの変更
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
        public int level;

        /// <summary>
        /// 1秒間産出するプランクトンポイント数
        /// </summary>
        public float planktonPointPerSecond;

        /// <summary>
        /// 1匹のプランクトンが持つPPの上限値
        /// </summary>
        public float planktonPointPerPlankton;
    }
}
