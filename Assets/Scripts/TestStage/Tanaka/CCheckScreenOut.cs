using UnityEngine;
using System.Collections;

public class CCheckScreenOut : MonoBehaviour {
	
	//Margin
	float margin = 0.1f; //マージン(画面外に出てどれくらい離れたら消えるか)を指定
	float negativeMargin;
	float positiveMargin;

	// Use this for initialization
	void Start ()
	{
		negativeMargin = 0 - margin;
		positiveMargin = 1 + margin;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// 毎フレーム画面内確認を行う
		if( isOutOfScreen( gameObject.transform ) )
		{
			// 画面外なら削除
			Destroy( gameObject );
		}
	}
	
	/**
	 * カメラ範囲外か調べる
	 */
	private bool isOutOfScreen( Transform trns ) 
	{
		Vector3 positionInScreen = Camera.main.WorldToViewportPoint(trns.position);
		positionInScreen.z = trns.position.z;
		
		if (positionInScreen.x <= negativeMargin ||
		    positionInScreen.x >= positiveMargin ||
		    positionInScreen.y <= negativeMargin ||
		    positionInScreen.y >= positiveMargin)
		{
			//Debug.Log( "外" );
			return true;
		} else {
			//Debug.Log( "内" );
			return false;
		}
	}
}
