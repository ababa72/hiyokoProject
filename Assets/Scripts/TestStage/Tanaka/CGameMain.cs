using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * ゲームメイン
 */
public class CGameMain : MonoBehaviour
{
	// 選択中のステージ情報
	public CStageData _stageData;

	// ターゲットタップ回数
	public int _targetTapCount = 0;
	// ターゲットタップ目標回数
	//public int _targetTapMissionCount = 0;
	// 制限時間開始
	public float _startTime;


	// Use this for initialization
	void Start ()
	{
		// Stage情報取得
		_stageData = CStageDataManager.Instance.selectStage;
		// 制限時間を設定する
		_startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

		// ミッション完了チェック
		if( _targetTapCount >= _stageData.parameters[ 1 ] )
		{
			// 結果ウィンドウ表示
			CGameCommonInstance.Instance.initResultWindow( Mathf.FloorToInt(  _startTime + _stageData.parameters[ 0 ] - Time.time ) );
		}
	
		// 制限時間を超えていないか調べる
		if( Time.time >= _startTime + _stageData.parameters[ 0 ]  )
		{
			// 結果ウィンドウ表示
			CGameCommonInstance.Instance.initResultWindow( 0 );
		}

		// タップ回数表示
		GameObject.Find( "Canvas/Target/Text" ).GetComponent< Text >().text = _targetTapCount.ToString();
		// 制限時間表示
		GameObject.Find( "Canvas/Time/Text" ).GetComponent< Text >().text = Mathf.Floor(  _startTime + _stageData.parameters[ 0 ] - Time.time ).ToString();
	}
}
