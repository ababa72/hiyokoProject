using UnityEngine;
using System.Collections;

public class CSpawnHiyoko : MonoBehaviour {

	public GameObject _modelPrefab = null;
	
	// 出現開始時間
	private float _startTime;
	// 出現間隔
	private const float SPAWN_INTERVAL = 1;
	// 最大出現数
	private const float SPAWN_MAX = 10;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( _modelPrefab != null )
		{
			if( isSpawn() )
			{
				// 出現初期化
				initSpawn();

				// カメラの右から左へ流す
				{
					// モデル生成
					GameObject hiyoko = Instantiate( _modelPrefab,
					                                   // XZ平面上の画面内にランダム生成
					                                   Camera.main.ViewportToWorldPoint( new Vector3( 1.0f, Random.value, Camera.main.transform.position.y )),
					                                   new Quaternion() ) as GameObject;
					// オブジェクトルートへ
					hiyoko.transform.SetParent( GameObject.Find( "ObjectRoot" ).transform, false );
					// 左端へむかせる
					hiyoko.transform.Rotate( 0, Random.Range( 225, 315 ), 0 );

					// スクリプト取得
					CHiyokoBase script = (CHiyokoBase)hiyoko.GetComponent( "CHiyokoBase" );
					// 初期化
					script.init( false );
				}
			}
		}
	}
	
	/**
	 * 出現初期化
	 */
	private void initSpawn()
	{
		_startTime = Time.time;
	}
	
	/**
	 * 出現確認
	 */
	private bool isSpawn()
	{
		// 出現間隔確認
		if( _startTime + SPAWN_INTERVAL <= Time.time )
		{
			// 生成済みモデル数確認
			int cnt = 0;
			// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
			{
				// ルートオブジェクトのみ取得
				if( obj.transform.parent == GameObject.Find( "ObjectRoot" ).transform )
				{
					// シーン上に存在するオブジェクトならば処理.
					if (obj.activeInHierarchy)	
					{
						if( obj.name.IndexOf( "hiyoko" ) != -1 )
						{
							cnt++;
						}
					}
				}
			}
			// 最大出現数を超えていないなら
			if( cnt < SPAWN_MAX )
			{
				// 出現可
				return true;
			}
		}
		// 出現不可
		return false;
	}
}
