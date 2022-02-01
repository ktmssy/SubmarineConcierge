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

namespace SubmarineConcierge.SaveData
{
    public class FireLevelSaveData
    {
        private const string SAVE_KEY = "Firelevel";

        private const int MAX_LEVEL = 3;

        private static int _level = 0;

        public static int level
        {
            get
            {
                if (_level == 0)
                {
                    _level = Load();
                }
                return _level;
            }
            set
            {
                _level = value;
                Save(value);
            }
        }

        private static int Load()
        {
            return PlayerPrefs.GetInt(SAVE_KEY, 1);
        }

        private static void Save(int value)
        {
            PlayerPrefs.SetInt(SAVE_KEY, value);
        }

        public static int AddLevel(int value)
        {
            int ret = level + value;
            if (ret > MAX_LEVEL || ret <= 0)
                return level;
            level = ret;
            return ret;
        }
    }
}
