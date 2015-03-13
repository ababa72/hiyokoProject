using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/**
 * ゲーム初期化クラス
 * CGameBaseを継承した各ゲームクラスを動的生成する
 */
public class CGameInitialize : MonoBehaviour
{
	// 
	public Dictionary<int, string> _gamemode = new Dictionary<int, string>()
	{
		{0, "CModeTest"},
		{1, "CModeTest"},
		{2, "CModeTest"},
		{3, "CGameMain3"}
	}; 

	void Awake ()
	{
		// ゲームモードID取得
		int gamemodeID = CStageDataManager.Instance.selectStage.gamemodeID;

		// クラス名取得
		Type type = Type.GetType( _gamemode[ gamemodeID ] );
		if (type == null)
		{
			// 例外
			throw new System.Exception( "GameModeID : " + gamemodeID );
		}
		// ゲームクラススクリプト追加
		gameObject.AddComponent( type );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
