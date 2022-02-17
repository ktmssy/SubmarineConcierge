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

using SubmarineConcierge.Event;
using SubmarineConcierge.Fire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.SaveData
{
    [System.Serializable]
    public class FireLevelSaveData : SaveData
    {
        [System.NonSerialized] private int _MAX_LEVEL = 0;

        public int MAX_LEVEL
        {
            get
            {
                if (_MAX_LEVEL == 0)
                    _MAX_LEVEL = levelDatabase.levelDatas.Length;
                return _MAX_LEVEL;
            }
        }

        private int _level = 1;
        private int _exp = 0;

        public int level { get { return _level; } }
        public int exp { get { return _exp; } }

        [System.NonSerialized] LevelDatabase _levelDatabase;
        public LevelDatabase levelDatabase
        {
            get
            {
                if (_levelDatabase == null)
                    _levelDatabase = Resources.Load<LevelDatabase>("LevelDesign/FireData/FireLevelDatabase");
                return _levelDatabase;
            }
        }

        public LevelData currentLevelData
        {
            get
            {
                return levelDatabase.GetLevelData(_level);
            }
        }


        public override void Save()
        {
            SaveDataManager.Save(SaveDataManager.fireLevelSaveData);
        }

        public void AddExp(int e)
        {
            _exp += e;
            UEventDispatcher.dispatchEvent(SCEvent.OnFireExpChanged, null);

            while (LevelUp()) ;
            Save();
        }

        public void SetLevel(int l)
        {
            _level = l;
            Save();
        }

        public void SetExp(int e)
        {
            _exp = e;
            UEventDispatcher.dispatchEvent(SCEvent.OnFireExpChanged, null);

            while (LevelUp()) ;
            Save();
        }

        private bool LevelUp()
        {
            if (_level >= MAX_LEVEL)
                return false;

            LevelData currentLevel = levelDatabase.GetLevelData(_level);
            if (_exp < currentLevel.expToNextLevel)
                return false;

            _exp -= currentLevel.expToNextLevel;
            _level++;
            UEventDispatcher.dispatchEvent(SCEvent.OnFireLevelUp, null);
            return true;
        }

    }
}
