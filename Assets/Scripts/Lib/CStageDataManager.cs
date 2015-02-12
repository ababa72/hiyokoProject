using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System;

/**
 * ステージ情報管理クラス
 * シングルトンパターンの為 sceneをまたいでも削除されない
 */
public class CStageDataManager : CSingletonMonoBehaviour<CStageDataManager>
{
	// XMLファイルリソース
	public TextAsset _stageXml;
	// ゲームシーンPath一覧
	public string[] _gameScenePath;

	// ステージ情報配列
	private List<CStageData> _stageDataList = null;
	public int Length()
	{
		if( _stageDataList == null ) return -1;
		return _stageDataList.Count;
	}
	public CStageData this[int index]
	{
		set{_stageDataList[index] = value;}
		get{return _stageDataList[index];}
	}

	// 選択中のステージ番号
	private int _selectStageIndex = -1;
	public int selectStageIndex
	{
		set
		{
			if( value < 0 || value >= _stageDataList.Count )
			{
				throw new System.Exception("_stageDataList 範囲外 : " + value);
			}
			_selectStageIndex = value;
		}
		get
		{
			if( _selectStageIndex == -1 || _selectStageIndex < 0 || _selectStageIndex >= _stageDataList.Count )
			{
#if UNITY_EDITOR
				return 0;
#endif
				throw new System.Exception("_selectStageIndex 異常値");
			}
			return _selectStageIndex;
		}
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

		// XML読み込み
		_stageDataList = loadXml( _stageXml );
		// セーブ情報読み込み
		_stageDataList = loadData (_stageDataList);
	}
	
	/**
     * XML読み込み、解析処理
     * @return ステージ情報配列
     */
	private List<CStageData> loadXml( TextAsset xmlFile )
	{
		// 戻り値用
		List<CStageData> ret = null;
		
		// XML読み込み
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xmlFile.text);
		// XML解析処理
		XmlNode root = xmlDoc.FirstChild;
		XmlNodeList stageList = xmlDoc.GetElementsByTagName("data");
		foreach(XmlNode stageInfo in stageList)
		{
			if( ret == null )
			{
				ret = new List<CStageData>();
			}

			int id = 0, mode = 0, unlock = 0;
			int[] levels = new int[3];

			XmlNodeList stagecontent = stageInfo.ChildNodes;

			id = int.Parse (stageInfo.Attributes ["id"].Value);
			foreach (XmlNode child in stagecontent)
			{
				if (child.Name == "mode")
				{
					mode = int.Parse( child.InnerText );
				}
				else if (child.Name == "borderLine")
				{
					//foreach (XmlNode levels in child.CloneNode)
					for( int i = 0 ; i < child.ChildNodes.Count ; i++ )
					{
						levels[ i ] = int.Parse( child.ChildNodes.Item (i).InnerText );
					}
				}
				else if (child.Name == "initial")
				{
					unlock = int.Parse( child.InnerText );
				}
			}

			// パラメータセット
			ret.Add( new CStageData(  id, mode, (unlock == 1), levels ) );
		}
		return ret;
	}

	/**
	 * セーブデータ読み込み
	 * @return ステージ情報配列
	 */
	private List<CStageData> loadData( List<CStageData> list )
	{
		for (int i = 0; i < list.Count; i++)
		{
			list[i].score = CSaveDataManager.Instance.getScore (list [i].id);
			// 初回開放ではないステージ情報のみ読み込む
			if (!list [i].unlock)
			{
				list [i].unlock = (1 == CSaveDataManager.Instance.getUnlock (list [i].id));
			}
		}
		return list;
	}

	/**
	 * セーブデータ保存
	 */
	public void saveData()
	{
		List<CStageData> list = _stageDataList;
		for (int i = 0; i < list.Count; i++)
		{
			// スコア
			CSaveDataManager.Instance.setScore (list [i].id, list[i].score);
			// アンロック
			CSaveDataManager.Instance.setUnlock (list [i].id, Convert.ToInt32(list[i].unlock));
		}
	}

	/**
	 * 選択中のステージ番号に則ってゲームシーンへ遷移させる
	 */
	public void gotoSelectStageScene()
	{
		// selectStageIndex
		string scenePath = _gameScenePath[ _stageDataList[ selectStageIndex ].mode ];
		Application.LoadLevel( scenePath );
	}

	/**
	 * リスト情報更新
	 */
	public void updateData( int score )
	{
		CStageData data = _stageDataList[ selectStageIndex ];

		// スコアを更新しているなら
		if( data.score < score )
		{
			data.score = score;
		}
		
		// アンロック情報更新
		// TODO
		// XMLから次にアンロックされるステージ情報を取得するのも有り
		// 現状は次のステージを強制アンロック
		CStageData nextData = _stageDataList[ selectStageIndex + 1 ];
		nextData.unlock = true;

		// セーブデータ保存
		saveData();
	}

}
