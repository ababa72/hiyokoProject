using UnityEngine;
using System.Collections;

/**
 * セーブデータ管理クラス
 * データを永続化させたいので、PlayerPrefsを使用する
 * TODO
 * ・サーバ等で保持する場合は修正
 * ・大量のデータをPlayerPrefsで管理するのは不向きらしいのでSQLite等の使用を検討
 *  http://bribser.co.jp/blog/post-319/
 */
public class CSaveDataManager : SingletonMonoBehaviour<CSaveDataManager>
{


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

	/**
	 * 初回起動チェック
	 */
	private bool isInitial()
	{
		return (PlayerPrefs.GetInt ("INIT", 0) == 0);
	}

	private void initial()
	{

	}

	/**
	 * ステージスコア情報保存
	 */
	public void setScore( int stage_id, int score )
	{
		PlayerPrefs.SetInt ("StageScore" + stage_id, score); 
	}

	/**
	 * ステージスコア情報読み込み
	 */
	public int getScore( int stage_id )
	{
		return PlayerPrefs.GetInt ("StageScore" + stage_id, 0);
	}

	/**
	 * ステージアンロック情報保存
	 */
	public void setUnlock( int stage_id, int unlock_f )
	{
		PlayerPrefs.SetInt ("StageUnlock" + stage_id, unlock_f); 
	}

	/*	*
	 * ステージアンロック情報読み込み
	 */
	public int getUnlock( int stage_id )
	{
		return PlayerPrefs.GetInt ("StageUnlock" + stage_id, 0);
	}

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

		// セーブデータの有無を確認する
		// 無い場合は初期設定

	}

	// Use this for initialization
	void Start (){
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
