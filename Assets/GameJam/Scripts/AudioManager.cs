using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    [Header("环境声音")]
    public AudioClip musicClip;  // 背景音乐
    public AudioClip winClip;  // 胜利音乐

    [Header("主角音效")]
    public AudioClip pullClip;  // 角色拉时的音效
    public AudioClip[] walkStepClips;  //角色行走脚步声
    public AudioClip laughClip;  //角色吃到球的笑声
    public AudioClip deathClip;  //角色死亡时音效

    [Header("球的音效")]
    public AudioClip ballAddClip;  //球增加时的音效
    public AudioClip ballReboundClip; //球撞击障碍物反弹时的音效
    public AudioClip ballLostAudio; //球消失时的音效

    AudioSource musicSource;  //播放背景音乐的音响
    AudioSource playerSource; //播放角色相关的音响
    AudioSource ballSource;  //播放球相关的音响


    private void Awake()
    {
        if (current == null)
		{
			current = this;
			DontDestroyOnLoad(gameObject); //切换场景重新加载时不删除

			musicSource = gameObject.AddComponent<AudioSource>();  // 初始化音响
			playerSource = gameObject.AddComponent<AudioSource>();
			ballSource = gameObject.AddComponent<AudioSource>();

			StartLevelMusic();
		}
		else
		{
			Destroy(this.gameObject);
		}
    }

    public static void StartLevelMusic()  // 播放场景音乐 
    {
        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }

    public static void PlayWinAudio() // 播放胜利音效
    {
        current.musicSource.clip = current.winClip;
        current.musicSource.volume = 0.5f;
        current.musicSource.loop = false;
        current.musicSource.Play();
    }

    public static void PlayPullAudio()  // 播放 拉 音效
    {
        current.playerSource.clip = current.pullClip;
        current.playerSource.Play();
    }

    public static void PlayFootstepAudio() // 播放移动脚步 音效
    {
        int index = Random.Range(0, current.walkStepClips.Length); // 在脚步声数组中随机选择脚步声进行播放
        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.volume = 0.3f;
        current.playerSource.Play();
    }

    public static void PlayLaughAudio()  // 吃到球时 播放笑声音效
    {
        current.playerSource.clip = current.laughClip;
        current.playerSource.Play();
    }

    public static void PlayDeathAudio()  // 角色死亡时播放音效
    {
        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();
    }

    public static void PlayBallReboundAudio()   // 播放球反弹时的音效
    {
        current.ballSource.clip = current.ballReboundClip;
        current.ballSource.Play();
    }

    public static void PlayBallAddAudio()  //播放球增加时的音效
    {
        current.ballSource.clip = current.ballAddClip;
        current.ballSource.Play();
    }

    public static void PlayBallLostAudio()  //播放球减少 消失时的音效
    {
        current.ballSource.clip = current.ballLostAudio;
        current.ballSource.Play();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
