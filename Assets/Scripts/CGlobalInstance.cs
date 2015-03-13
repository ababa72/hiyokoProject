using UnityEngine;
using System.Collections;

public class CGlobalInstance : CSingletonMonoBehaviour<CGlobalInstance>
{
	public GameObject[] _instances;

	void Awake()
	{
		if( this != Instance )
		{
			Destroy( this );
			Destroy( gameObject );
			return;
		}

		// 各グローバルインスタンス生成
		if( _instances != null )
		{
			for( int i = 0 ; i < _instances.Length ; i++ )
			{
				GameObject obj = (GameObject)Instantiate( _instances[ i ] );
				obj.transform.SetParent( this.transform );
			}
		}
		DontDestroyOnLoad( this.gameObject );
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

}
