using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CModeTest : CGameBase
{
	// スコア
	private int _testScore = 0;

	/**
	 * 初期化処理
	 */
	public override void init ()
	{
		// カメラ角度設定スライダー
		GameObject sObj = GameObject.Find("Canvas/Slider"); 
		if( sObj != null )
		{
			Slider slider = sObj.GetComponent <Slider> ();
			slider.onValueChanged.AddListener((value) => {
				_testScore = Mathf.RoundToInt(value);
				GameObject.Find("Canvas/Score").GetComponent<Text>().text = _testScore.ToString();
			});
		}
	}
	
	/**
	 * 実行処理
	 */
	public override void main ()
	{
	}
		
	public void clearButton()
	{
		// 結果ウィンドウ表示
		setResult( _testScore );
	}
}
