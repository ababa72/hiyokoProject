using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[System.Serializable]
public class CIconSetting : MonoBehaviour
{
	public GameObject _content;
	public GameObject _iconPrefab;

	void Awake ()
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
			icon.transform.FindChild("Numbers").GetComponent<CNumberScript>()._num = CStageDataManager.Instance[i].id + 1;
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
		if( CStageSelectState.Instance.state == CStageSelectState.STATE.SCROLL )
		{
			// アンロックされているか
			if( CStageDataManager.Instance[ index ].unlock )
			{
				// ステージ詳細ウィンドウ表示
				// OK音再生
				CSoundManager.Instance.PlaySE( 0 );
				// window表示アニメーション
				GameObject.Find("Canvas/DetailWindow").GetComponent<Animation>().Play( "WindowOpen" );
				// 各パラメータを流し込む
				GameObject.Find("Canvas/DetailWindow/Content/Title").GetComponent<Text>().text = "ステージ" + (CStageDataManager.Instance[ index ].id + 1);
				//GameObject.Find("Canvas/DetailWindow/Content/borderLine").GetComponent<Text>().text = "ステージ" + (CStageDataManager.Instance[ index ].id + 1);
				GameObject.Find("Canvas/DetailWindow/Content/borderLine/Level1").GetComponent<Text>().text = "Level1 : " + CStageDataManager.Instance[ index ].level[ CStageData.BORDERLINE.LEVEL1 ];
				GameObject.Find("Canvas/DetailWindow/Content/borderLine/Level2").GetComponent<Text>().text = "Level2 : " + CStageDataManager.Instance[ index ].level[ CStageData.BORDERLINE.LEVEL2 ];
				GameObject.Find("Canvas/DetailWindow/Content/borderLine/Level3").GetComponent<Text>().text = "Level3 : " + CStageDataManager.Instance[ index ].level[ CStageData.BORDERLINE.LEVEL3 ];
				// スコア
				GameObject.Find("Canvas/DetailWindow/Content/Score").GetComponent<Text>().text = "Score : " + CStageDataManager.Instance[ index ].score;
				// クリア済みスタンプ
				GameObject.Find("Canvas/DetailWindow/Content/ClearStamp").GetComponent<Image>().enabled = false;
				if( CStageDataManager.Instance[ index ].isClear() )
				{
					GameObject.Find("Canvas/DetailWindow/Content/ClearStamp").GetComponent<Image>().enabled = true;
				}
				// 詳細ウィンドウ表示状態へ
				CStageSelectState.Instance._nextState = CStageSelectState.STATE.DETAIL;
				// 選択されているステージ番号を保持する
				CStageDataManager.Instance.selectStageIndex = index;
			}
			else
			{
				// NG音再生
				CSoundManager.Instance.PlaySE( 1 );
			}
		}
	}
}
