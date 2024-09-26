using UnityEngine;
using System.Collections;
/*
 * This is SoundManager
 * In other script, you just need to call SoundManager.PlaySfx(AudioClip) to play the sound
*/
public class SoundManager : MonoBehaviour {
	public static SoundManager Instance;
 //   public AudioClip beginSoundInMainMenu;
	//[Tooltip("Play music clip when start")]
	//public AudioClip musicsMenu;
	//[Range(0,1)]
	//public float musicMenuVolume = 0.5f;
	public AudioClip musicsGame;
	[Range(0,1)]
	public float musicsGameVolume = 0.5f;

	[Tooltip("Place the sound in this to call it in another script by: SoundManager.PlaySfx(soundname);")]
    public AudioClip soundBallExplosion;
    public AudioClip soundClick;
	public AudioClip soundRewarded;
    public AudioClip soundUnlockBall;
    public AudioClip soundPickBall;
    public AudioClip soundNoEnoughGem;
    public AudioClip soundBuyItem;
    public AudioClip soundNextLevel;

    public AudioClip soundCollectGem;

    private AudioSource musicAudio;
	private AudioSource soundFx;
    
	public void PauseMusic(bool isPause){
		if (isPause)
			Instance.musicAudio.mute = true;
//			Instance.musicAudio.Pause ();
		else
			Instance.musicAudio.mute = false;
//			Instance.musicAudio.UnPause ();
	}
	//GET and SET
	public static float MusicVolume{
		
		set{ Instance.musicAudio.volume = value; }
		get{ return Instance.musicAudio.volume; }
	}
	public static float SoundVolume{
		set{ Instance.soundFx.volume = value; }
		get{ return Instance.soundFx.volume; }
	}
	// Use this for initialization
	void Awake(){
		Instance = this;
		musicAudio = gameObject.AddComponent<AudioSource> ();
		musicAudio.loop = true;
		musicAudio.volume = 0.5f;
		soundFx = gameObject.AddComponent<AudioSource> ();
	}
	void Start () {
//		//Check auido and sound
//		if (!GlobalValue.isMusic)
//			musicAudio.volume = 0;
//		if (!GlobalValue.isSound)
//			soundFx.volume = 0;
//		PlayMusic(musicsGame,musicsGameVolume);
		//		//Check auido and sound

		PlayMusic (musicsGame, musicsGameVolume);
	}

	public static void Click(){
		PlaySfx (Instance.soundClick);
	}

	public  void ClickBut(){
		PlaySfx (soundClick);
	}

	public static void PlaySfx(AudioClip clip){
		if (Instance != null) {
			Instance.PlaySound (clip, Instance.soundFx);
			Debug.Log (clip);
		}


	}

    public static void PlaySfx(AudioClip[] clips)
    {
        if (Instance != null && clips.Length > 0)
            Instance.PlaySound(clips[Random.Range(0, clips.Length)], Instance.soundFx);
    }

    public static void PlaySfx(AudioClip clip, float volume){
		if(Instance!=null)
		Instance.PlaySound(clip, Instance.soundFx, volume);
	}

	public static void PlayMusic(AudioClip clip){
		Instance.PlaySound (clip, Instance.musicAudio);
	}

	public static void PlayMusic(AudioClip clip, float volume){
		Instance.PlaySound (clip, Instance.musicAudio, volume);
	}

	private void PlaySound(AudioClip clip,AudioSource audioOut){
		if (clip == null) {
			//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (Instance == null)
			return;

		if (audioOut == musicAudio) {
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume);
	}

	private void PlaySound(AudioClip clip,AudioSource audioOut, float volume){
		if (clip == null) {
			//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (audioOut == musicAudio) {
			audioOut.volume = volume;
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume * volume);
	}
}
