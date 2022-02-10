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
    public class LeftRightPanelSize : MonoBehaviour
    {
        private RectTransform parent;

        public bool isLeft;

        private const float paddingLeftRight = 50f;
        private const float marginCenter = 25f;


        private void Start()
        {
            parent = transform.parent as RectTransform;
            RectTransform me = transform as RectTransform;
            me.anchoredPosition = new Vector2(isLeft ? paddingLeftRight : -paddingLeftRight, me.anchoredPosition.y);
            me.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (parent.rect.width - paddingLeftRight - paddingLeftRight - marginCenter) / 2f);
            me.offsetMin = new Vector2(me.offsetMin.x, 50f);
            me.offsetMax = new Vector2(me.offsetMax.x, -50f);
        }

    }
}
