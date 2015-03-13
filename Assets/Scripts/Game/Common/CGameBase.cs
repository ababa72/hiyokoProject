using UnityEngine;
using System.Collections;

/**
 * ゲームメイン抽象クラス
 */
abstract public class CGameBase : MonoBehaviour
{
	// 終了フラグ
	private bool _isFinish;
	// スコア
	private int _score;

	void Awake()
	{
		_isFinish = false;
		_score = 0;

		init();
		// TODO
		// GameCommnInstancesがあるかチェック

	}

	void Update ()
	{
		if( !isFinish() )
		{
			main();
		}
	}

	/**
	 * ゲーム結果設定
	 */
	protected void setResult( int score )
	{
		_isFinish = true;
		_score = score;
		// スコア等保存
		CStageDataManager.Instance.updateData( score );
		// 結果ウィンドウ表示
		CGameCommonInstance.Instance.initResultWindow( score );

		// TODO
		// ゲーム停止処理
	}

	/**
	 * ゲーム終了チェック
	 */
	protected bool isFinish()
	{
		return _isFinish;
	}



	/**
	 * 初期化処理
	 */
	public abstract void init();

	/**
	 * 実行処理
	 */
	public abstract void main();
	

}
