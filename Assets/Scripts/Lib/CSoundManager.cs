using UnityEngine;
using System;
using System.Collections;

/**
 * サウンド管理クラス
 */
public class CSoundManager : CSingletonMonoBehaviour<CSoundManager>
{
	// 音量
	public SoundVolume volume = new SoundVolume();
	
	// === AudioSource ===
	// BGM
	private AudioSource BGMsource;
	// SE
	private AudioSource[] SEsources;// = new AudioSource[16];

	// === AudioClip ===
	// BGM
	public AudioClip[] BGM;
	// SE
	public AudioClip[] SE;
	
	void Awake ()
	{
		if( this != Instance )
		{
			Destroy( this );
			Destroy( gameObject );
			return;
		}
		DontDestroyOnLoad( this.gameObject );


		//volume.load ();
		
		// 全てのAudioSourceコンポーネントを追加する
		
		// BGM AudioSource
		BGMsource = gameObject.AddComponent<AudioSource> ();
		// BGMはループを有効にする
		BGMsource.loop = true;
		
		// SE AudioSource
		SEsources = new AudioSource[SE.Length];
		for (int i = 0; i < SEsources.Length; i++)
		{
			SEsources [i] = gameObject.AddComponent<AudioSource> ();
		}
	}
	
	void Update ()
	{
		settingVolume ();
	} 
	
	
	// ***** BGM *****
	
	/**
	 * BGM再生
	 */
	public void PlayBGM(int index)
	{
		if (0 > index || BGM.Length <= index)
		{
			return;
		}
		// 同じBGMの場合は何もしない
		if (BGMsource.clip == BGM [index])
		{
			return;
		}
		settingVolume ();
		BGMsource.Stop();
		BGMsource.clip = BGM[index];
		BGMsource.Play();
	}
	
	/**
	 * BGM停止
	 */
	public void StopBGM()
	{
		BGMsource.Stop();
		BGMsource.clip = null;
	}
	
	// ***** SE *****
	/**
	 * SE再生
	 */
	public void PlaySE(int index)
	{
		if (0 > index || SE.Length <= index)
		{
			return;
		}
		
		settingVolume ();
		// 再生中で無いAudioSouceで鳴らす
		foreach (AudioSource source in SEsources)
		{
			if (false == source.isPlaying)
			{
				//source.clip = SE [index];
				source.PlayOneShot (SE [index]);
				return;
			}
		}
	}
	
	/**
	 * SE停止
	 */
	public void StopSE()
	{
		// 全てのSE用のAudioSouceを停止する
		foreach (AudioSource source in SEsources)
		{
			source.Stop ();
			source.clip = null;
		}
	}
	
	
	/**
	 * 音量適用
	 */
	private void settingVolume()
	{
		// ミュート設定
		BGMsource.mute = volume.Mute;
		foreach (AudioSource source in SEsources)
		{
			source.mute = volume.Mute;
		}
		
		// ボリューム設定
		BGMsource.volume = volume.BGM;
		foreach (AudioSource source in SEsources)
		{
			source.volume = volume.SE;
		}
	}
}

/**
 * 音量クラス
 */
[Serializable]
public class SoundVolume
{
	public float BGM = 1.0f;
	public float Voice = 1.0f;
	public float SE = 1.0f;
	public bool Mute = false;
	
	public SoundVolume()
	{
		BGM = 1.0f;
		Voice = 1.0f;
		SE = 1.0f;
		Mute = false;
	}
	
	public void load()
	{
		// ローカルから読み込み
		Mute = (PlayerPrefs.GetInt ("Mute", 1) == 1 );
	}
	
	public void save()
	{
		// ローカルへ保存
		PlayerPrefs.SetInt ("Mute", Convert.ToInt32(Mute));
	}
}