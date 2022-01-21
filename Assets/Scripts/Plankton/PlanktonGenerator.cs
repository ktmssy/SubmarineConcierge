/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
 *　更新日：2022年01月21日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.クラス名をGeneratorからPlanktonGeneratorに変更...楊志庄
 *　2.親の座標も計算に入れる...楊志庄
 *　3.プランクトンの間隔距離を指定できるようにした...楊志庄
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubmarineConcierge.Utilities;

namespace SubmarineConcierge.Plankton
{
    /// <summary>
    /// プランクトン生成マネージャー
    /// </summary>
    public class PlanktonGenerator : MonoBehaviour
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
        /// 生成する数
        /// </summary>
        [Tooltip("生成する数")]
        public int Count;

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

        /// <summary>
        /// プランクトンを生成する
        /// </summary>
        public void Generate()
        {
            //生成する場所を決める位相
            float phase = 0f;


            //二匹のプランクトンの位相差
            float delta = distance;

            //0の場合は等間隔
            if (distance == 0f)
                delta = Data.TotalDistance / Count;

            for (int i = 0; i < Count; ++i)
            {
                //プランクトンを生成する。場所はルートデータのLerp関数で算出
                GameObject go = Instantiate(Prefab, CoordinateUtility.CalcPos(Data.Lerp(phase), transform.localToWorldMatrix), Quaternion.identity);

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

                //mar.localToWorldMatrix = transform.localToWorldMatrix;

                //次のプランクトンの位相を準備
                phase += delta;
            }
        }

        private void Start()
        {
            Generate();
        }
    }
}
