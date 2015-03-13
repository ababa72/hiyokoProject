using UnityEngine;
using System.Collections;

public class CGameCommonInstance : CSingletonMonoBehaviour<CGameCommonInstance>
{
	// 結果画面Prefab
	public GameObject _resultWindowPrefab;

	/**
     * 起動時処理
     */
	void Awake()
	{
		if( this != Instance )
		{
			Destroy( this );
			Destroy( gameObject );
			return;
		}
		DontDestroyOnLoad( this.gameObject );
	}


	/**
	 * 結果ウィンドウ表示
	 */
	public void initResultWindow( int score )
	{
		// 重複起動チェック
		if ( ( CResultWindow )FindObjectOfType( typeof( CResultWindow ) ) != null )
		{
			return;
		}

		// Canvas取得
		GameObject canvas = GameObject.Find( "Canvas" );
		if( canvas == null )
		{
			throw new System.Exception( "Canvasが設定されていません" );
		}

		// 結果ウィンドウ生成
		GameObject ins = (GameObject)Instantiate( _resultWindowPrefab, new Vector3(), new Quaternion() ) as GameObject;
		ins.transform.localScale = new Vector3( 1, 1, 1 );
		// キャンバス下へ
		ins.transform.SetParent( canvas.transform, false );
		// 初期化
		ins.GetComponent<CResultWindow>().init( score );
	}




}
