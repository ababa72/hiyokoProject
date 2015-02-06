using UnityEngine;
using System.Collections;

/**
 * ステージ情報クラス
 */
public class CStageData
{
	// 骨組み部分

	// ID
	private int _id;
	public int id
	{
		get{ return _id; }
	}
	
	// ゲームモード
	private int _mode;
	public int mode
	{
		get{ return _mode; }
	}
	
	// クリア条件
	private int[] _borderLine = new int[BORDERLINE_MAX];
	public int level1
	{
		get{ return _borderLine[BORDERLINE_LEVEL1]; }
	}
	public int level2
	{
		get{ return _borderLine[BORDERLINE_LEVEL2]; }
	}
	public int level3
	{
		get{ return _borderLine[BORDERLINE_LEVEL3]; }
	}
	
	// クリア条件列挙
	public const int BORDERLINE_LEVEL1 = 0;
	public const int BORDERLINE_LEVEL2 = 1;
	public const int BORDERLINE_LEVEL3 = 2;
	public const int BORDERLINE_MAX = 3;



	// データ部分

	// ハイスコア
	private int _score;
	public int score {
		get{ return _score; }
		set{ _score = value; }
	}

	// アンロック状況
	private bool _unlock;
	public bool unlock {
		get{ return _unlock; }
		set{ _unlock = value; }
	}

	/**
	 * コンストラクタ
	 */
	public CStageData( int id, int mode, bool unlock, int[] levels )
	{
		_id = id;
		_mode = mode;
		_unlock = unlock;
		_borderLine[ BORDERLINE_LEVEL1 ] = levels[ 0 ];
		_borderLine[ BORDERLINE_LEVEL2 ] = levels[ 1 ];
		_borderLine[ BORDERLINE_LEVEL3 ] = levels[ 2 ];
	}

	/**
	 * クリアしているか？
	 */
	public bool isClear()
	{
		// スコアがレベル1以上に達している
		return ( _score >= _borderLine[ BORDERLINE_LEVEL1 ]);
	}
}
