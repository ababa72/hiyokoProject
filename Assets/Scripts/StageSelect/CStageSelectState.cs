using UnityEngine;
using System.Collections;

public class CStageSelectState : CSingletonMonoBehaviour<CStageSelectState>
{
	// 状態一覧
	public enum STATE
	{
		NONE = -1,
		SCROLL = 0,
		DETAIL
	};
	// 現在の状態
	private STATE _state = STATE.NONE;
	public STATE state
	{
		get{ return _state; }
	}
	// 次の状態
	public STATE _nextState = STATE.NONE;


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
		//DontDestroyOnLoad( this.gameObject );
	}
	
	// Use this for initialization
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( _state != _nextState )
		{
			_state = _nextState;
		}
	}
}
