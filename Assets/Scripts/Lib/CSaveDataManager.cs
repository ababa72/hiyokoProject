using UnityEngine;
using System.Collections;

/**
 * セーブデータ管理クラス
 * データを永続化させたいので、PlayerPrefsを使用する
 * TODO
 * ・サーバ等で保持する場合は修正
 * ・大量のデータをPlayerPrefsで管理するのは不向きらしいのでSQLite等の使用を検討
 * http://bribser.co.jp/blog/post-319/
 */
public class CSaveDataManager : SingletonMonoBehaviour<CSaveDataManager>
{
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
	 * データ保存
	 */
	public void save()
	{

	}

	/**
	 * データ読み込み
	 */
	public void load()
	{

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
