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
        private const string SAVE_KEY_HOLD = "PPHold";
        private const string SAVE_KEY_GAINED = "PPGained";

        private static int hold = -1;
        private static int gained = -1;

        public static int Hold
        {
            get
            {
                if (hold == -1)
                {
                    hold = LoadHold();
                }
                return hold;
            }
            set
            {
                hold = value;
                SaveHold(value);
            }
        }

        public static int Gained
        {
            get
            {
                if (gained == -1)
                {
                    gained = LoadGained();
                }
                return gained;
            }
            set
            {
                gained = value;
                SaveGained(value);
            }
        }

        private static int LoadHold()
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
            Hold = MathUtility.SafeAdd(Hold, value);
            return Hold;
        }

        public static int AddGained(int value)
        {
            Gained = MathUtility.SafeAdd(Gained, value);
            //Debug.Log("AddGained: " + value + " , " + Gained);
            return Gained;
        }

        public static bool UsePP(int value)
        {
            if (Hold < value)
                return false;
            AddHold(value * -1);
            return true;
        }
    }
}
