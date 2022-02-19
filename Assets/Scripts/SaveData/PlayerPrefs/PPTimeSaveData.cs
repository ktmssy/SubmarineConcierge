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
using System;

namespace SubmarineConcierge.SaveData
{
    public class PPTimeSaveData
    {
        private const string SAVE_KEY = "PPTime";

        private static DateTime _time = new DateTime(0);

        public static DateTime time
        {
            get
            {
                if (_time.Ticks == 0)
                {
                    _time = Load();
                }
                return _time;
            }
            set
            {
                _time = value;
                Save(value);
            }
        }

        private static DateTime Load()
        {
            long now = DateTime.Now.Ticks;
            long ticks = long.Parse(PlayerPrefs.GetString(SAVE_KEY, now.ToString()));
            Debug.Log("LoadTicks: " + PlayerPrefs.GetString(SAVE_KEY, now.ToString()));
            Debug.Log("CurrentTicks: " + now);
            return new DateTime(ticks);
        }

        private static void Save(DateTime t)
        {
            PlayerPrefs.SetString(SAVE_KEY, t.Ticks.ToString());
        }
    }
}
