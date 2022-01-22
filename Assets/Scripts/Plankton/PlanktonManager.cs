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
using SubmarineConcierge.Fire;
using SubmarineConcierge.Utilities;
using System;
using SubmarineConcierge.SaveData;

namespace SubmarineConcierge.Plankton
{
    public class PlanktonManager : MonoBehaviour
    {
        /// <summary>
        /// プランクトンのプレハブ
        /// </summary>
        [Tooltip("プランクトンのプレハブ")]
        public GameObject Prefab;

        /// <summary>
        /// ルートデータ
        /// </summary>
        [Tooltip("ルートデータ")]
        public RouteData Data;

        /// <summary>
        /// 火のレベルデータベース
        /// </summary>
        [Tooltip("火のレベルデータベース")]
        public LevelDatabase FireLevelDatabase;

        /// <summary>
        /// プランクトンたちのスピード
        /// </summary>
        [Tooltip("プランクトンたちのスピード")]
        public float Speed;

        /// <summary>
        /// プランクトンの間隔距離。0の場合は等間隔。
        /// </summary>
        [Tooltip("プランクトンの間隔距離。0の場合は等間隔。")]
        public float distance = 0f;

        private int count = 0;

        private int CalcPP()
        {
            DateTime last = PPTimeSaveData.Time;
            DateTime now = DateTime.Now;
            TimeSpan lastSpan = new TimeSpan(last.Ticks);
            TimeSpan nowSpan = new TimeSpan(now.Ticks);
            TimeSpan deltaSpan = nowSpan.Subtract(lastSpan).Duration();
            double seconds = deltaSpan.TotalSeconds;
            Debug.Log("deltaSeconds: " + seconds);

            LevelData fireLevelData = FireLevelDatabase.GetLevelData(FireLevelSaveData.Level);
            Debug.Log("FireLevel: " + FireLevelSaveData.Level);

            int delta = (int)(fireLevelData.PlanktonAmount * fireLevelData.PlanktonPointPerSecond * seconds);
            Debug.Log("CalcPP: " + delta);
            Debug.Log("GainedPP: " + PPSaveData.Gained);

            PPTimeSaveData.Time = now;
            return PPSaveData.AddGained(delta);
        }

        public void GainPP()
        {
            CalcPP();
            Debug.Log("PlanktonCount: " + count);
            int pp = PPSaveData.Gained / count--;
            PPSaveData.AddHold(pp);
            PPSaveData.AddGained(-pp);
            Debug.Log("GainPP: " + pp);
        }

        public void Generate(float phase)
        {
            //プランクトンを生成する。場所はルートデータのLerp関数で算出
            GameObject go = Instantiate(Prefab, CoordinateUtility.CalcWorldPosFromLocalPos(Data.Lerp(phase), transform.localToWorldMatrix), Quaternion.identity);

            //プランクトンの親を自分にする
            go.transform.parent = transform;

            //プランクトンの制御スクリプトを取得
            var plankton = go.GetComponent<Plankton>();

            //位相の値を付与する
            plankton.Phase = phase;

            //ルートデータを付与する
            plankton.Data = Data;

            //スピードを付与する
            plankton.Speed = Speed;

            plankton.manager = this;

        }

        /// <summary>
        /// プランクトンを生成する
        /// </summary>
        public void GenerateAll()
        {
            //生成する数を算出
            LevelData fireLevelData = FireLevelDatabase.GetLevelData(FireLevelSaveData.Level);
            count = fireLevelData.PlanktonAmount;


            //生成する場所を決める位相
            float phase = 0f;


            //二匹のプランクトンの位相差
            float delta = distance;

            //0の場合は等間隔
            if (distance == 0f)
                delta = Data.TotalDistance / count;

            for (int i = 0; i < count; ++i)
            {
                Generate(phase);

                //次のプランクトンの位相を準備
                phase += delta;
            }
        }

        private void Start()
        {
            GenerateAll();
        }
    }
}
