using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class CTitleMain : MonoBehaviour
{
	public GameObject _buttonPrefab;
	public CStageInfo[] _stageInfo;

	// Use this for initialization
	void Start ()
	{
		// ステージ選択用ボタンを動的生成
		GameObject canvas = GameObject.Find("Canvas");
		for( int i = 0 ; i < _stageInfo.Length ; i++ )
		{
			//プレハブからボタンを生成
			GameObject button = Instantiate(_buttonPrefab) as GameObject;
			// 親子関係設定
			button.transform.SetParent( canvas.transform );
			// 表示位置設定
			button.transform.localPosition = new Vector2( -180f, -80f + 40f * i );
			// テキスト設定
			button.transform.FindChild("Text").GetComponent<Text>().text = _stageInfo[i]._displayName;
			// ボタンイベント設定
			//引数に何番目のボタンかを渡す
			int n = i;
			button.GetComponent<Button>().onClick.AddListener(() => MyOnClick(n));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ボタンクリックイベント
	void MyOnClick( int index )
	{
		Debug.Log( index );
		Debug.Log( _stageInfo[ index ]._scenePath );

		// シーン遷移
		Application.LoadLevel(_stageInfo[ index ]._scenePath);
	}

	// ステージ情報クラス
	[System.Serializable]
	public class CStageInfo
	{
		public string _displayName;
		public string _scenePath;
	}
}
