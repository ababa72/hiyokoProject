using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CModeTest : MonoBehaviour
{

	// 結果ウィンドウPrefab
	public GameObject _resultWindowPrefab;
	// スコア
	private int _score = 0;

	// Use this for initialization
	void Start () {
		// カメラ角度設定スライダー
		GameObject sObj = GameObject.Find("Canvas/Slider"); 
		if( sObj != null )
		{
			Slider slider = sObj.GetComponent <Slider> ();
			slider.onValueChanged.AddListener((value) => {
				_score = Mathf.RoundToInt(value);
				GameObject.Find("Canvas/Score").GetComponent<Text>().text = _score.ToString();
			});
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clearButton()
	{
		// Canvas取得
		GameObject canvas = GameObject.Find( "Canvas" );
		// 結果ウィンドウ生成
		GameObject ins = (GameObject)Instantiate( _resultWindowPrefab, new Vector3(), new Quaternion() ) as GameObject;
		ins.transform.localScale = new Vector3( 1, 1, 1 );
		// キャンバス下へ
		ins.transform.SetParent( canvas.transform, false );
		// 初期化
		ins.GetComponent<CResultWindow>().init( _score );
	}
}
