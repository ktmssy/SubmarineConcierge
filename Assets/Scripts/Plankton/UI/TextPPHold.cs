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

using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineConcierge
{

    [AddComponentMenu("_SubmarineConcierge/UI/Text/PPHold")]
    public class TextPPHold : MonoBehaviour
    {
        Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }
    
        private void Update()
        {
            text.text = PPSaveData.Hold.ToString();
        }
    }
}
