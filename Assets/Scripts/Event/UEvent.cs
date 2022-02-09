using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Create Time: 2021/10/13
 * Creator:     YANG ZHIZHUANG
 * Modified:    2021/10/13  by  YANG ZHIZHUANG
 * Modified:
 */
namespace SubmarineConcierge.Event
{

    public class UEvent
    {
        public SCEvent eventType;

        public object eventParams;

        public object target;

        public UEvent(SCEvent eventType, object eventParams = null)
        {
            this.eventType = eventType;
            this.eventParams = eventParams;
        }
    }
}