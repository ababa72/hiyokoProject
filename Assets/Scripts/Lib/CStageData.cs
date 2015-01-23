using UnityEngine;
using System.Collections;

/**
 * ステージ情報クラス
 */
public class CStageData
{
	// ID
	private int _id;
	public int id
	{
		set{ _id = value; }
		get{ return _id; }
	}
	
	// ゲームモード
	private int _mode;
	public int mode
	{
		set{ _mode = value; }
		get{ return _mode; }
	}
	
	// クリア条件
	private int[] _borderLine = new int[BORDERLINE_MAX];
	public int level1
	{
		get{ return _borderLine[BORDERLINE_LEVEL1]; }
		set{ _borderLine[BORDERLINE_LEVEL1] = value; }
	}
	public int level2
	{
		get{ return _borderLine[BORDERLINE_LEVEL2]; }
		set{ _borderLine[BORDERLINE_LEVEL2] = value; }
	}
	public int level3
	{
		get{ return _borderLine[BORDERLINE_LEVEL3]; }
		set{ _borderLine[BORDERLINE_LEVEL3] = value; }
	}
	
	// クリア条件列挙
	public const int BORDERLINE_LEVEL1 = 0;
	public const int BORDERLINE_LEVEL2 = 1;
	public const int BORDERLINE_LEVEL3 = 2;
	public const int BORDERLINE_MAX = 3;
}
