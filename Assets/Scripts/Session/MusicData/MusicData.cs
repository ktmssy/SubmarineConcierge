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
    [CreateAssetMenu(fileName = "MusicData", menuName = "SubmarineConcierge/Session/MusicData")]
    public class MusicData : ScriptableObject
    {
        public string musicName;
        public string author;
        public SoundTrack[] soundTracks;
        private Dictionary<InstrumentType, SoundTrack> tracks;

        public void OnEnable()
        {
            tracks = new Dictionary<InstrumentType, SoundTrack>();
            foreach (SoundTrack track in soundTracks)
            {
                tracks.Add(track.type, track);
            }
        }

        public SoundTrack GetTrack(InstrumentType type)
        {
            if (!tracks.ContainsKey(type))
                return null;
            return tracks[type];
        }
    }
}
