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
    public static class UEventDispatcher
    {

        private static IList<UEventListener> eventListenerList = new List<UEventListener>();

        public class UEventListener
        {

            public SCEvent eventType;

            public UEventListener(SCEvent eventType)
            {
                this.eventType = eventType;
            }

            public delegate void EventListenerDelegate(UEvent evt);
            public event EventListenerDelegate OnEvent;

            public void Excute(UEvent evt)
            {
                if (OnEvent != null)
                {
                    this.OnEvent(evt);
                }
            }
        }

        public static void addEventListener(SCEvent eventType, UEventListener.EventListenerDelegate callback)
        {
            UEventListener eventListener = getListener(eventType);
            if (eventListener == null)
            {
                eventListener = new UEventListener(eventType);
                eventListenerList.Add(eventListener);
            }

            eventListener.OnEvent += callback;
        }

        public static void removeEventListener(SCEvent eventType, UEventListener.EventListenerDelegate callback)
        {
            UEventListener eventListener = getListener(eventType);
            if (eventListener != null)
            {
                eventListener.OnEvent -= callback;
            }
        }

        public static bool hasListener(SCEvent eventType)
        {
            return getListenerList(eventType).Count > 0;
        }

        public static void dispatchEvent(UEvent evt, object gameObject)
        {
            IList<UEventListener> resultList = getListenerList(evt.eventType);

            foreach (UEventListener eventListener in resultList)
            {
                evt.target = gameObject;

                eventListener.Excute(evt);
            }
        }

        public static void dispatchEvent(SCEvent eventType, object gameObject, object eventParams = null)
        {
            UEvent evt = new UEvent(eventType, eventParams);
            IList<UEventListener> resultList = getListenerList(evt.eventType);

            foreach (UEventListener eventListener in resultList)
            {
                evt.target = gameObject;

                eventListener.Excute(evt);
            }
        }

        private static IList<UEventListener> getListenerList(SCEvent eventType)
        {
            IList<UEventListener> resultList = new List<UEventListener>();
            foreach (UEventListener eventListener in eventListenerList)
            {
                if (eventListener.eventType == eventType) resultList.Add(eventListener);
            }
            return resultList;
        }

        private static UEventListener getListener(SCEvent eventType)
        {
            foreach (UEventListener eventListener in eventListenerList)
            {
                if (eventListener.eventType == eventType) return eventListener;
            }
            return null;
        }
    }
}