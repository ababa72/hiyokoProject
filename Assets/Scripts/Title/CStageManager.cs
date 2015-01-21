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
public class CStageManager : SingletonMonoBehaviour<CStageManager>
{
	// XMLファイルリソース
	public TextAsset _stageXml;
	// ステージ情報配列
	public List<CStageData> _stageDataList;
	
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
			CStageData tmp = new CStageData();
			XmlNodeList stagecontent = stageInfo.ChildNodes;
			tmp.id = int.Parse( stageInfo.Attributes[ "id" ].Value );
			tmp.mode = int.Parse( stagecontent[0].InnerText );
			tmp.level1 = int.Parse( stagecontent[1].ChildNodes.Item( 0 ).InnerText );
			tmp.level2 = int.Parse( stagecontent[1].ChildNodes.Item( 1 ).InnerText );
			tmp.level3 = int.Parse( stagecontent[1].ChildNodes.Item( 2 ).InnerText );
			
			if( ret == null )
			{
				ret = new List<CStageData>();
			}
			ret.Add( tmp );
		}
		return ret;
	}
}
