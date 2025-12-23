using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 【重要】引用 UI 库，否则无法控制按钮

public class LevelMenuController : MonoBehaviour
{
    // 这里需要在这个脚本里拿到 Level 2 的按钮组件
    public Button level2Button;

   

    void Start()
    {
        // === 核心检查逻辑 ===

        // 获取存档状态，如果没有存档（第一次玩），默认返回 0
        int isUnlocked = PlayerPrefs.GetInt("Level2Unlocked", 0);

        // 如果获取到的值是 1，说明解锁了
        if (isUnlocked == 1)
        {
            level2Button.interactable = true; // 按钮可点击
        }
        else
        {
            level2Button.interactable = false; // 按钮禁用（变灰且不可点）
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    // === 开发测试用：重置存档 ===
    // 可以在 Start 页面做一个隐藏按钮或者按键盘 R 键调用这个
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("存档已重置，Level 2 重新上锁。");
        // 重新刷新一下按钮状态
        level2Button.interactable = false;
    }
}