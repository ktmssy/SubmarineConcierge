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
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SubmarineConcierge.Utilities;

public class SafeAddTest
{
    [Test]
    public void SafeAdd()
    {
        Assert.AreEqual(MathUtility.SafeAdd(1, 2), 3);
        Assert.AreEqual(MathUtility.SafeAdd(-1, 2), 1);
        Assert.AreEqual(MathUtility.SafeAdd(-1, -2), -3);
        Assert.AreEqual(MathUtility.SafeAdd(1, int.MaxValue - 1), int.MaxValue);
        Assert.AreEqual(MathUtility.SafeAdd(int.MaxValue - 1, 2), int.MaxValue);
        Assert.AreEqual(MathUtility.SafeAdd(int.MinValue + 1, -2), int.MinValue);
        Assert.AreEqual(MathUtility.SafeAdd(int.MinValue + 1, -1), int.MinValue);
        Assert.AreEqual(MathUtility.SafeAdd(int.MinValue, int.MaxValue), -1);
    }
}
