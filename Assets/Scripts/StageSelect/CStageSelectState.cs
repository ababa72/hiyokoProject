using UnityEngine;
using System.Collections;

public class CStageSelectState : SingletonMonoBehaviour<CStageSelectState>
{
	// 状態一覧
	public enum STATE
	{
		NONE = -1,
		SCROLL = 0,
		DETAIL
	};
	// 現在の状態
	public STATE _state = STATE.NONE;

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
	}
	
	// Use this for initialization
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
