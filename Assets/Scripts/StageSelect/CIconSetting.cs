
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
		for( int i = 0 ; i < CStageManager.Instance.Length() ; i++ )
		{
			//プレハブからボタンを生成
			GameObject icon = Instantiate(_iconPrefab) as GameObject;
			// 親子関係設定
			icon.transform.SetParent( canvas.transform, false );
			// 表示位置設定
			//icon.transform.localPosition = new Vector2( -90 + 90*(i%3), -90 * (i/3) + 180 );
			// テキスト設定
			icon.transform.FindChild("Numbers").GetComponent<NumberScript>()._num = CStageManager.Instance[i].id + 1;
			// アンロック設定
			// TODO
			if( i < 10 )
			{
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
		Debug.Log( (float)(iconHeight*Math.Ceiling((double)CStageManager.Instance.Length() / 3)) );

		// スクロールサイズ設定
		RectTransform contentRectTrans = _content.GetComponent<RectTransform>();
		contentRectTrans.sizeDelta = new Vector2( contentRectTrans.sizeDelta.x, (float)(iconHeight*Math.Ceiling((double)CStageManager.Instance.Length() / 3)));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// ボタンクリックイベント
	void MyOnClick( int index )
	{
		Debug.Log( index );
		// シーン遷移
		//Application.LoadLevel(_stageInfo[ index ]._scenePath);
	}
}
