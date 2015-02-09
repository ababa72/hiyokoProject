using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[System.Serializable]
public class CIconSetting : MonoBehaviour
{
	public GameObject _content;
	public GameObject _iconPrefab;

	// Use this for initialization
	void Start ()
	{
		float iconHeight = 0;
		// ステージ選択用Iconを動的生成
		GameObject canvas = GameObject.Find("Canvas/ScrollView/Icons");
		for( int i = 0 ; i < CStageDataManager.Instance.Length() ; i++ )
		{
			//プレハブからボタンを生成
			GameObject icon = Instantiate(_iconPrefab) as GameObject;
			// 親子関係設定
			icon.transform.SetParent( canvas.transform, false );
			// 表示位置設定
			//icon.transform.localPosition = new Vector2( -90 + 90*(i%3), -90 * (i/3) + 180 );
			// テキスト設定
			icon.transform.FindChild("Numbers").GetComponent<NumberScript>()._num = CStageDataManager.Instance[i].id + 1;
			// アンロック設定
			if( CStageDataManager.Instance[i].unlock )
			{
				// キーマークを非表示にする
				icon.transform.FindChild("KeyMark").gameObject.SetActive( false );
			}
			// ボタンイベント設定
			//引数に何番目のボタンかを渡す
			int n = i;
			icon.GetComponent<Button>().onClick.AddListener(() => MyOnClick(n));

			// アイコンの高さ取得
			iconHeight = icon.GetComponent<RectTransform>().sizeDelta.y;
		}
		Debug.Log(iconHeight);
		Debug.Log( (float)(iconHeight*Math.Ceiling((double)CStageDataManager.Instance.Length() / 3)) );

		// スクロールサイズ設定
		RectTransform contentRectTrans = _content.GetComponent<RectTransform>();
		contentRectTrans.sizeDelta = new Vector2( contentRectTrans.sizeDelta.x, (float)(iconHeight*Math.Ceiling((double)CStageDataManager.Instance.Length() / 3)));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// ボタンクリックイベント
	void MyOnClick( int index )
	{
		Debug.Log( index );
		// スクロール状態へなら選択可能
		if( CStageSelectState.Instance._state == CStageSelectState.STATE.SCROLL )
		{
			// アンロックされているか
			if( CStageDataManager.Instance[ index ].unlock )
			{
				// OK音再生
				CSoundManager.Instance.PlaySE( 0 );
				// window表示アニメーション
				GameObject.Find("Canvas/DetailWindow").GetComponent<Animation>().Play( "WindowOpen" );
				// 各パラメータを流し込む
				GameObject.Find("Canvas/DetailWindow/Content/StageName").GetComponent<Text>().text = "ステージ" + (CStageDataManager.Instance[ index ].id + 1);
				// 詳細ウィンドウ表示状態へ
				CStageSelectState.Instance._state = CStageSelectState.STATE.DETAIL;
			}
			else
			{
				// NG音再生
				CSoundManager.Instance.PlaySE( 1 );
			}
		}
	}
}
