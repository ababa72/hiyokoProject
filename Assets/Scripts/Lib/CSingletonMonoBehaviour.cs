using UnityEngine;

/**
 * シングルトンMonoBehaviour
 * @see http://naichilab.blogspot.jp/2013/11/unitymanager.html
 */
public class CSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	
	/**
     * シングルトン用インスタンスキャッシュ
     * @var T
     */
	private static T _instance;
	
	/**
     * インスタンスゲッタ
     * @return T
     */
	public static T Instance
	{
		get
		{
			if ( _instance == null )
			{
				_instance = ( T )FindObjectOfType( typeof( T ) );
				if ( _instance == null )
				{
					Debug.LogError( typeof( T ) + "is nothing" );
				}
			}
			return _instance;
		}
	}
	
}
