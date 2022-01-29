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

namespace SubmarineConcierge.Session
{
    [CreateAssetMenu(fileName = "MusicDatabase", menuName = "SubmarineConcierge/Session/MusicDatabase")]
    public class MusicDatabase : ScriptableObject
    {
        public MusicData[] musicDatas;

        public MusicData GetRandom()
        {
            if (musicDatas.Length == 0)
                return null;
            return musicDatas[Random.Range(0, musicDatas.Length)];
        }
    }
}
