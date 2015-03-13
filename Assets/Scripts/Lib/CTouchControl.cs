using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CTouchControl : MonoBehaviour
{
	private CGameMain3 _main;

#if UNITY_EDITOR
	// デバッグ機能
	private List<Ray> _rayList = new List<Ray>();
	private List<float> _timeList = new List<float>();
	// 表示時間
	const float DRAW_TIME = 10f;
#endif

	void Start()
	{
		_main = GameObject.Find( "Main Camera" ).GetComponent< CGameMain3 >();
	}

	void Update ()
	{
		// タッチされたとき
		if(Input.GetMouseButtonDown(0))
		{
			// メインカメラからクリックしたポジションに向かってRayを撃つ。
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			Debug.Log( "GetMouseButtonDown" );


#if UNITY_EDITOR
			// デバッグ処理
			_timeList.Add( Time.time );
			_rayList.Add( ray );
#endif
			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log( "HIT" );
				GameObject selectedGameObject = hit.collider.gameObject;
				ITapBehaviour target = selectedGameObject.GetComponent(typeof(ITapBehaviour)) as ITapBehaviour;
				if(target != null){
					_main._targetTapCount++;
					target.TapDown(ref hit);
				}
			}
			
		}
		// 指を離したとき
		else if(Input.GetMouseButtonUp(0))
		{
			// メインカメラからクリックしたポジションに向かってRayを撃つ。
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit = new RaycastHit();
			
			if (Physics.Raycast(ray, out hit))
			{
				GameObject selectedGameObject = hit.collider.gameObject;
				ITapBehaviour target = selectedGameObject.GetComponent(typeof(ITapBehaviour)) as ITapBehaviour;
				if(target != null){
					target.TapUp(ref hit);
				}
			}
		}

#if UNITY_EDITOR
		// デバッグ機能
		for( int i = _timeList.Count - 1 ; i >= 0 ; i-- )
		{
			float draw_time = _timeList[ i ] + DRAW_TIME;
			if( draw_time < Time.time )
			{
				// 表示時間が過ぎているなら削除
				_timeList.RemoveAt( i );
				_rayList.RemoveAt( i );
			}
			else
			{
				// Ray表示
				// Scene Viewに光線を可視化する。
				// Game Viewには表示されない。
				Debug.DrawRay(_rayList[i].origin, _rayList[i].direction*50.0f, 
				              new Color( 0.0f, 0.0f, 1.0f, (float)(draw_time - Time.time) / draw_time));
			}
		}
#endif
	}
}