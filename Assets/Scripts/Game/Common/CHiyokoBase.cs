using UnityEngine;
using System.Collections;

public class CHiyokoBase : MonoBehaviour, ITapBehaviour
{
	// 手動フラグ
	private bool _manual = false;
	// 状態
	private int _state = 0;
	// アニメーション状態
	private int _animationState = 0;
	// Playアニメーション名
	private string _animetionName = "";

	// アニメーター管理
	private Animator _animator;

	void Start ()
	{
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( _manual )
		{
			// 手動操作
			manualControl();
		}
		else
		{
			// 自動操作
			autoControl();
		}

		// タップ時の挙動監視
		if( _animetionName.Length > 0 )
		{
			if( _animator.GetBool("is_"+_animetionName) )
			{
				if( _animator.GetCurrentAnimatorStateInfo(0).IsName( _animetionName ) == true )
				{
					_animationState = 1;
				}
				else if( _animator.GetCurrentAnimatorStateInfo(0).IsName( _animetionName ) == false )
				{
					if( _animationState == 1 )
					{
						_animator.SetBool("is_"+_animetionName, false);
						_state = 0;
						_animetionName = "";
					}
				}
			}
		}
	}
	
	/**
	 * 初期化
	 * @param 手動フラグ
	 */
	public void init( bool manual )
	{
		_manual = manual;
	}

	/**
	 * 手動操作
	 */
	private void manualControl()
	{
		// 前進
		if (Input.GetKey("up")) 
		{
			transform.position += transform.forward * 0.15f;
			_animator.SetBool("is_run", true);
		}
		else
		{
			_animator.SetBool("is_run", false);
		}
		// 方向転換
		if (Input.GetKey("right"))
		{
			transform.Rotate(0, 3, 0);
		}
		if (Input.GetKey ("left"))
		{
			transform.Rotate(0, -3, 0);
		}
	}

	/**
	 * 自動操作
	 */
	private void autoControl()
	{
		if( _state == 0 )
		{
			// まっすぐ進みのみ
			transform.position += transform.forward * 0.15f;
			_animator.SetBool("is_run", true);
		}
	}


	// タップされたときの処理
	public void TapDown (ref RaycastHit hit)
	{
		Debug.Log( "ひよこタップ開始" );

		// 走るアニメーションを止める
		// TODO
		// 現状ではデフォルトで走る状態のため可能
		// 様々な状態がある場合は割り込み判定が必要
		_animationState = 0;

		// ランダムでアニメーション出し分け
		if( Random.Range( 0, 2 ) == 0 )
		{
			// タップアニメーション
			_animator.SetBool("is_tap", true);
			_animetionName = "tap";
			_state = 1;
		}
		else
		{
			_animator.SetBool("is_run", false);
			// ハッピーアニメーション
			_animator.SetBool("is_happy", true);
			_animetionName = "happy";
			_state = 2;
		}
	}
	
	// タップを離したときの処理
	public void TapUp (ref RaycastHit hit)
	{
		Debug.Log( "ひよこタップ終了" );
	}
}
