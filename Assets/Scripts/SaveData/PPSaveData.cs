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

using SubmarineConcierge.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{
    public class PPSaveData
    {
        private const string SAVE_KEY_HOLD = "PPhold";
        private const string SAVE_KEY_GAINED = "PPgained";

        private static int _hold = -1;
        private static int _gained = -1;

        public static int hold
        {
            get
            {
                if (_hold == -1)
                {
                    _hold = Load();
                }
                return _hold;
            }
            set
            {
                _hold = value;
                SaveHold(value);
            }
        }

        public static int gained
        {
            get
            {
                if (_gained == -1)
                {
                    _gained = LoadGained();
                }
                return _gained;
            }
            set
            {
                _gained = value;
                SaveGained(value);
            }
        }

        private static int Load()
        {
            return PlayerPrefs.GetInt(SAVE_KEY_HOLD, 0);
        }

        private static void SaveHold(int value)
        {
            PlayerPrefs.SetInt(SAVE_KEY_HOLD, value);
        }

        private static int LoadGained()
        {
            return PlayerPrefs.GetInt(SAVE_KEY_GAINED, 0);
        }

        private static void SaveGained(int value)
        {
            PlayerPrefs.SetInt(SAVE_KEY_GAINED, value);
        }

        public static int AddHold(int value)
        {
            hold = MathUtility.SafeAdd(hold, value);
            return hold;
        }

        public static int AddGained(int value)
        {
            gained = MathUtility.SafeAdd(gained, value);
            //Debug.Log("AddGained: " + value + " , " + gained);
            return gained;
        }

        public static bool UsePP(int value)
        {
            if (hold < value)
                return false;
            AddHold(value * -1);
            return true;
        }
    }
}
