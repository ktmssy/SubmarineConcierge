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
    /// 火のレベルデータのデータベース
    /// </summary>
    [CreateAssetMenu(fileName = "FireLevelDatabase", menuName = "SubmarineConcierge/Fire/LevelDataBase")]
    public class LevelDatabase : ScriptableObject
    {
        /// <summary>
        /// レベルごとのデータ
        /// </summary>
        public LevelData[] LevelDatas;

        /// <summary>
        /// Query用のマップ
        /// </summary>
        private Dictionary<int, LevelData> dataMap;

        public void OnEnable()
        {
            //レベルデータがないと処理しない
            if (LevelDatas == null)
                return;

            //マップの初期化
            dataMap = new Dictionary<int, LevelData>();
            foreach(LevelData data in LevelDatas)
            {
                dataMap.Add(data.Level, data);
            }
        }

        /// <summary>
        /// レベルを使ってレベルデータをQueryする
        /// </summary>
        /// <param name="level">レベル数</param>
        /// <returns>レベルデータ</returns>
        public LevelData GetLevelData(int level)
        {
            //マップがないとnull
            if (dataMap == null)
                return null;

            //キーがあればレベルデータを取り出す
            if (dataMap.ContainsKey(level))
                return dataMap[level];

            //キーがないとnull
            return null;
        }
    }
}
