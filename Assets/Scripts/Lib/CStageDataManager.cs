using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

/**
 * ステージ情報管理クラス
 * シングルトンパターンの為 sceneをまたいでも削除されない
 */
public class CStageDataManager : SingletonMonoBehaviour<CStageDataManager>
{
	// XMLファイルリソース
	public TextAsset _stageXml;
	// ステージ情報配列
	private List<CStageData> _stageDataList = null;
	public int Length()
	{
		if( _stageDataList == null ) return -1;
		return _stageDataList.Count;
	}
	public CStageData this[int i]
	{
		set{_stageDataList[i] = value;}
		get{return _stageDataList[i];}
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
	public List<CStageData> loadXml( TextAsset xmlFile )
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

}
