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

namespace SubmarineConcierge.Utilities
{
    public class MathUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int SafeAdd(int a, int b)
        {
            bool flag1 = a > 0;
            bool flag2 = b > 0;

            if (flag1 ^ flag2)
                return a + b;

            if (flag1)
            {
                if (int.MaxValue - a - b > 0)
                    return a + b;
                else
                    return int.MaxValue;
            }
            else
            {
                if (int.MinValue - a - b < 0)
                    return a + b;
                else
                    return int.MinValue;
            }
        }
    }
}
