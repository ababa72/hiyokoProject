using UnityEngine;
using UnityEngine.UI;

public class CStageSelectMain : MonoBehaviour
{


	void Awake ()
	{
		// スクロール状態へ変更
		CStageSelectState.Instance._nextState = CStageSelectState.STATE.SCROLL;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// 状態が変更されたなら
		if( CStageSelectState.Instance.state != CStageSelectState.Instance._nextState )
		{
			GameObject icons = GameObject.Find( "Canvas/ScrollView/Icons" ).gameObject;
			switch( CStageSelectState.Instance._nextState )
			{
			case CStageSelectState.STATE.DETAIL:
				// スクロールが効かないようにする
				GameObject.Find( "Canvas/ScrollView" ).GetComponent<ScrollRect>().vertical = false;
				// 全てのアイコンのボタンを効かないようにする
				for( int i = 0 ; i < icons.transform.childCount ; i++ )
				{
					icons.transform.GetChild( i ).GetComponent<Button>().enabled = false;
				}
				break;
			case CStageSelectState.STATE.SCROLL:
				// スクロールが効くようにする
				GameObject.Find( "Canvas/ScrollView" ).GetComponent<ScrollRect>().vertical = true;
				// 全てのアイコンのボタンを効かないようにする
				for( int i = 0 ; i < icons.transform.childCount ; i++ )
				{
					icons.transform.GetChild( i ).GetComponent<Button>().enabled = true;
				}
				break;
			}
		}
	}

	/**
	 * 詳細ウィンドウOKボタン
	 */
	public void detailOkButton()
	{
		// ゲームシーンへ遷移
		//CStageDataManager.Instance.selectStageIndex
		CStageDataManager.Instance.gotoSelectStageScene();
	}

	/**
	 * 詳細ウィンドウキャンセルボタン
	 */
	public void detailCanselButton()
	{
		// window非表示アニメーション
		GameObject.Find("Canvas/DetailWindow").GetComponent<Animation>().Play( "WindowClose" );
		// スクロール状態へ
		CStageSelectState.Instance._nextState = CStageSelectState.STATE.SCROLL;
	}
}
