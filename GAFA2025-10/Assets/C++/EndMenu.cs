using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AllControl; // 引入 AllControl 的静态类

public class EndMenu : MonoBehaviour
{
    public void ReloadGame()
    {
        Debug.Log("Reloading game from EndMenu...");

        // --- 关键部分：重置 GameManager 的状态 ---
        // 确保 GameManager 实例存在
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame(); // 重置 score 和 cherriesCount
        }
        else
        {
            Debug.LogError("GameManager instance not found! Cannot reset game state.");
        }
        // ---------------------------------------------

        // 加载您希望的起始场景。
        // SceneManager.GetActiveScene().buildIndex - 3  表示加载倒数第三个场景。
        // 请确保这个场景索引对应的是您的第一个游戏关卡或主菜单。
        // 如果您不确定，建议使用场景名称来加载，例如：
        // SceneManager.LoadScene("YourStartingSceneName");
        // 这里的 SceneManager.GetActiveScene().buildIndex - 3 保持不变，
        // 但请您在 Unity Editor 中确认这个索引是否正确。
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);

        Debug.Log("Game state reset and scene reloaded.");
    }
}