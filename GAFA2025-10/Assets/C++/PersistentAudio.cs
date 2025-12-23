using UnityEngine;
using UnityEngine.SceneManagement; // 需要这个命名空间

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio _instance;
    public AudioSource audioSource;

    // 定义一个字符串数组，列出你不想播放BGM的场景名字
    // 注意：场景名字必须与你在Build Settings中的名称完全一致
    public string[] scenesWithoutBGM = { "MainMenu", "GameOverScreen", "Credits" };

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 在Start方法中播放BGM，但先检查当前场景
        PlayBGMIfAllowed();
    }

    void OnEnable()
    {
        // 注册场景加载的事件监听
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 注销场景加载的事件监听
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 当场景加载完成后，再次检查是否应该播放BGM
        PlayBGMIfAllowed();
    }

    void PlayBGMIfAllowed()
    {
        // 如果AudioSource为空，说明AudioManager可能还没有准备好，先跳过
        if (audioSource == null) return;

        string currentSceneName = SceneManager.GetActiveScene().name;

        // 检查当前场景是否在不允许播放BGM的列表中
        bool shouldPlayBGM = true;
        foreach (string sceneName in scenesWithoutBGM)
        {
            if (currentSceneName == sceneName)
            {
                shouldPlayBGM = false;
                break;
            }
        }

        if (shouldPlayBGM)
        {
            // 如果允许播放BGM，并且当前没有在播放，则开始播放
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // 如果不允许播放BGM，则停止播放
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    // 可选：提供方法来动态更新BGM（如果需要）
    public void PlayCustomBGM(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            PlayBGMIfAllowed(); // 确保场景允许播放
        }
    }
}
