using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	private Dictionary<BORDERLINE,int> _borderLine = new Dictionary<BORDERLINE,int>();
	public Dictionary<BORDERLINE,int> level
	{
		get{ return _borderLine; }
	}

	// クリア条件列挙
	public enum BORDERLINE
	{
		NONE = -1,
		LEVEL1 = 0,
		LEVEL2,
		LEVEL3
	};


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
		_borderLine[ BORDERLINE.LEVEL1 ] = levels[ 0 ];
		_borderLine[ BORDERLINE.LEVEL2 ] = levels[ 1 ];
		_borderLine[ BORDERLINE.LEVEL3 ] = levels[ 2 ];
	}

	/**
	 * クリアしているか？
	 */
	public bool isClear()
	{
		// スコアがレベル1以上に達している
		return ( _score >= _borderLine[ BORDERLINE.LEVEL1 ]);
	}
}
