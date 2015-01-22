using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class NumberScript : MonoBehaviour {

	// 
	public int _num = -1;
	public int _oldNum = -1;
	// 数字Sprite
	public Sprite[] _numberInsArr;
	// 
	public GameObject _numberBase;
	// 左揃え
	public bool _leftAlignment = false;

	// TODO スタイリッシュじゃない方法
	// 縮尺
	public float _rate = 1.0f;

	// Use this for initialization
	void Start ()
	{
		setNumbers (_num);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_num != _oldNum)
		{
			setNumbers (_num);
			_oldNum = _num;
		}	
	}

	/**
	 * 数字設定
	 */
	private void setNumbers( int num )
	{
		// 初期化
		clearNumber ();

		// 負数の場合は全て非表示にする
		if (num < 0)
			return;

		// 桁数取得
		int number_digit = unchecked((int)Math.Log10(num)) + 1;
		// インスタンス動的追加
		for( int i = 0 ; i < number_digit ; i++ )
		{
			//プレハブからボタンを生成
			GameObject base_ins = Instantiate(_numberBase) as GameObject;
			// 親子関係設定
			base_ins.transform.SetParent( this.transform, false );
			// 表示位置設定
			base_ins.transform.localPosition = new Vector2( (80*_rate) * i - ((80*_rate)*(number_digit-1)/2), 0 );
			base_ins.transform.localScale = new Vector2( _rate, _rate );

			// 数値設定
			int tmp_num = (int)(num / Mathf.Pow (10, (number_digit-1)-i));
			int index = tmp_num % 10;
			if (tmp_num > 0 || i == 0)
			{
				base_ins.GetComponent<Image> ().sprite = _numberInsArr [index];
			}
		}
	}

	/**
	 * 数字初期化
	 */
	private void clearNumber()
	{
		for( int i = 0 ; i < transform.childCount ; i++ )
		{
			Destroy( transform.GetChild (i).gameObject );
		}
	}

	/*
	private void setNumbers( int num )
	{
		int start = 0;
		if (_leftAlignment)
		{
			// 桁あぶれを考慮していない
			start = transform.childCount - num.ToString ().Length;
		}

		int cnt = 0;
		for( int i = start ; i < transform.childCount ; i++ )
		{
			if (transform.GetChild (i).name.IndexOf ("num") == -1)
				continue;

			int tmp = (int)(num / Mathf.Pow (10, cnt));
			int index = tmp % 10;
			Image img = null;
			img = transform.GetChild (i).gameObject.GetComponent<Image> ();

			img.enabled = false;
			if (tmp > 0 || i == 0)
			{
				img.sprite = _numberInsArr [index];
				img.enabled = true;
			}
			cnt++;
		}
	}
	*/

}
