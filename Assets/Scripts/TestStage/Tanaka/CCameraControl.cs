using UnityEngine;
using System.Collections;

public class CCameraControl : MonoBehaviour
{
	// タッチステータス
	private enum TOUCH_STATUS
	{
		NONE = -1,
		UP = 0,
		TAP,
		PINCH,
	};
	private TOUCH_STATUS _touchStatus = TOUCH_STATUS.UP;
	private TOUCH_STATUS _touchStatusNext = TOUCH_STATUS.NONE;

	// ピンチ開始時の2点間の距離
	private float _pinchLength;
	// ピンチ時の中間ベクトル
	private Vector2 _centerPos;

	// スワイプ座標
	private Vector2 _swipePos;
	
	public GameObject _cube = null;

	void Start()
	{
	}
	
	void Update()
	{
		switch(_touchStatus)
		{
			case TOUCH_STATUS.UP:
			{
				// 2点以上タップ
				if(Input.touchCount >= 2)
				{
					Touch touch0 = Input.GetTouch(0);
					Touch touch1 = Input.GetTouch(1);

					// タップしている2点間の距離を取得
					_pinchLength = Vector2.Distance(touch0.position, touch1.position);
					// 2点間の中間ベクトル取得
					_centerPos = (touch0.position + touch1.position) * 0.5f;
					// 座標系変換
					_centerPos = convertCenter(_centerPos);
					// 中間へオブジェクトを配置
					movePoint(_centerPos);
					
					_touchStatusNext = TOUCH_STATUS.PINCH;
				}
				// 1点以上タップ
				else if( Input.touchCount >= 1 )
				{
					Touch touch0 = Input.GetTouch(0);
					_swipePos = convertCenter( touch0.position );
					_touchStatusNext = TOUCH_STATUS.TAP;
				}
			}
			break;
				
			case TOUCH_STATUS.PINCH:
			{
				if(Input.touchCount < 2)
				{
					_touchStatusNext = TOUCH_STATUS.UP;
				}
			}
			break;

			case TOUCH_STATUS.TAP:
			{
				if(Input.touchCount != 1)
				{
					_touchStatusNext = TOUCH_STATUS.UP;
				}
			}
			break;
		}

		// 状態更新
		while(_touchStatusNext != TOUCH_STATUS.NONE)
		{
			_touchStatus = _touchStatusNext;
			_touchStatusNext = TOUCH_STATUS.NONE;
		}

		// 各状態時処理
		switch(_touchStatus)
		{
			// タップ(1点以上)
			case TOUCH_STATUS.TAP:
			{
				Touch touch0 = Input.GetTouch(0);
				Vector2 newSwipePos = convertCenter( touch0.position );
				Vector2 diff = newSwipePos - _swipePos;
				Vector3 newCameraPos = Camera.main.transform.localPosition - new Vector3(diff.x, 0, diff.y);
				Camera.main.transform.localPosition = newCameraPos;
				_swipePos = newSwipePos;
			}
			break;

			// ピンチ(2点以上)
			case TOUCH_STATUS.PINCH:
			{
				Touch touch0 = Input.GetTouch(0);
				Touch touch1 = Input.GetTouch(1);
				float nowPinchLength = Vector2.Distance(touch0.position, touch1.position);
				float scale = nowPinchLength / _pinchLength;
				// y座標計算（カメラ拡大率）
				float newCameraY = Camera.main.transform.localPosition.y / scale;
				_pinchLength = nowPinchLength;
				// 範囲内の拡大率なら
				if( newCameraY >= 15 && newCameraY <= 25 )
				{
					// XZ平面の為
					Vector2 nowCamPos = new Vector2(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.z);
					Vector2 diff = _centerPos - nowCamPos;
					scale = 1.0f - scale;
					diff = diff * scale;
					// カメラ座標適用
					Vector3 newCameraPos = Camera.main.transform.localPosition - new Vector3(diff.x, 0, diff.y);
					newCameraPos.y = newCameraY;
					Camera.main.transform.localPosition = newCameraPos;
				}
			}
			break;
		}
	}

	/**
	 * ピンチ時中間ベクトルへオブジェクト配置
	 */
	void movePoint(Vector2 po)
	{
		if( _cube != null )
		{
			_cube.transform.localPosition = new Vector3(po.x, 0, po.y);
		}
	}
	
	/**
	 * スクリーン座標系（のxy平面）からワールド座標系（のxz平面）へ変換
	 */
	Vector2 convertCenter(Vector2 po)
	{
		// xz平面へ変換
		Debug.Log( "変換前:" + po.ToString());
		Vector3 po2 = new Vector3( po.x, po.y, Camera.main.transform.position.y );
		Vector3 p = Camera.main.ScreenToWorldPoint(po2) - Camera.main.transform.position;
		Debug.Log( "変換後:" + p.ToString());
		return new Vector2(p.x, p.z);
	}
}
