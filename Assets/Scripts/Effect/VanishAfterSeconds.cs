/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年02月10日
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

namespace SubmarineConcierge
{
    public class VanishAfterSeconds : MonoBehaviour
    {
        public float life = 1f;

        private void Start()
        {
            Invoke("Vanish", life);
        }

        private void Vanish()
        {
            Destroy(gameObject);
        }
    }
}
