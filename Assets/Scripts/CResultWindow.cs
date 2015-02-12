using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * 結果ウィンドウ
 */
public class CResultWindow : MonoBehaviour
{
	// 成否スプライト配列
	public Sprite[] _issueSpriteArr;
	// メダルスプライト配列
	public Sprite[] _medalsSpriteArr;

	/*
	void Start ()
	{
		init( 100 );
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	*/

	/**
	 * 初期化
	 * @param score 獲得スコア
	 */
	public void init( int score )
	{
		// ステージ情報取得
		CStageData data = CStageDataManager.Instance[ CStageDataManager.Instance.selectStageIndex ];
		// クリアライン
		CStageData.BORDERLINE clearLine = CStageData.BORDERLINE.NONE;
		// ステージ成否判定
		foreach (CStageData.BORDERLINE key in data.level.Keys)
		{
			//Console.WriteLine(string.Format("Key : {0} / Value : {1}", key, sampleDict[key]));
			if( score >= data.level[ key ] )
			{
				clearLine = key;
			}
		}

		// 失敗
		if( clearLine == CStageData.BORDERLINE.NONE )
		{
			// 成否文字設定
			GameObject.Find( "Content/Issue" ).GetComponent<Image>().sprite = _issueSpriteArr[ 1 ];
			// メダル設定
			GameObject.Find( "Content/Medal" ).GetComponent<Image>().sprite = null;//_medalsSpriteArr[ (int)clearLine ];

			// ボタン設定
			// 次へボタンを効かないようにする
			GameObject.Find( "Content/NextButton" ).gameObject.SetActive( false );
		}
		// 成功
		else
		{
			// 成否文字設定
			GameObject.Find( "Content/Issue" ).GetComponent<Image>().sprite = _issueSpriteArr[ 0 ];
			// メダル設定
			GameObject.Find( "Content/Medal" ).GetComponent<Image>().sprite = _medalsSpriteArr[ (int)clearLine ];
		}


		// TODO
		// スコア等保存
		CStageDataManager.Instance.updateData( score );
	}

	/**
	 * 次へボタン
	 */
	public void nextButton()
	{
		int nowSelectIndex = CStageDataManager.Instance.selectStageIndex;
		nowSelectIndex++;
		CStageDataManager.Instance.selectStageIndex = nowSelectIndex;
		// 次のゲームシーンへ遷移
		CStageDataManager.Instance.gotoSelectStageScene();
	}

	/**
	 * リトライボタン
	 */
	public void retryButton()
	{
		// ゲームシーンへ遷移
		CStageDataManager.Instance.gotoSelectStageScene();
	}

	/**
	 * ステージ選択画面へボタン
	 */
	public void selectButton()
	{
		// ステージ選択画面へ遷移
		Application.LoadLevel("stageSelect");
	}

}