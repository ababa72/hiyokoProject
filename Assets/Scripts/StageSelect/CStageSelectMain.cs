using UnityEngine;

public class CStageSelectMain : MonoBehaviour
{


	// Use this for initialization
	void Start ()
	{
		// スクロール状態へ変更
		CStageSelectState.Instance._state = CStageSelectState.STATE.SCROLL;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * 詳細ウィンドウキャンセルボタン
	 */
	public void detailCanselButton()
	{
		// window非表示アニメーション
		GameObject.Find("Canvas/DetailWindow").GetComponent<Animation>().Play( "WindowClose" );
		// スクロール状態へ
		CStageSelectState.Instance._state = CStageSelectState.STATE.SCROLL;
	}
}
