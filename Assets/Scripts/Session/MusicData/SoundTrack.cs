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

using SubmarineConcierge.Fish;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Session
{
    [System.Serializable]
    public class SoundTrack
    {
        public InstrumentType type;
        public float volume = 1f;
        public AudioClip clipFirst;
        public AudioClip clipLoop;
    }
}
