/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月21日
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

namespace SubmarineConcierge.Plankton
{
	/// <summary>
	/// ルートデータのインターフェース
	/// </summary>
	public interface IRoute
	{
		/// <summary>
		/// 位相を基に座標を算出
		/// </summary>
		/// <param name="value">位相</param>
		/// <returns>ルート上の座標</returns>
		public Vector2 Lerp(float value);

		/// <summary>
		/// 親の偏位とスケールを考慮したLerp
		/// </summary>
		/// <param name="value">位相</param>
		/// <param name="localToWorldMatrix">変換Matrix</param>
		/// <returns>ルート上の座標</returns>
		public Vector2 Lerp(float value, Matrix4x4 localToWorldMatrix);
	}
}
