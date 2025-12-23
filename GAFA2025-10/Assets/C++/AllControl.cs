using System.Collections.Generic;
using UnityEngine;

public class AllControl : MonoBehaviour
{
    // AllControl 脚本本身可以用来管理 GameManager 的初始化，
    // 或者让 GameManager 在 Awake() 中处理自己的初始化。
    // 为了简化，这里保留 OnEnable，但实际上 GameManager 的单例
    // 可以在第一次访问 Instance 时自动创建。

    private void OnEnable()
    {
        Debug.Log("AllControl Enabled");
        // 确保 GameManager 在此脚本被启用时被初始化
        // 尽管访问 Instance 也会触发初始化，但显式调用可以确保
        // 即使没有立即访问 Instance，GameManager 也已经准备好。
        var _ = GameManager.Instance;
    }

    public class GameManager
    {
        // 单例模式
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                    // 在 GameManager 实例创建时，初始化所有计数
                    _instance.score = 0; // 初始分数
                    _instance.cherriesCount = 0; // 新增：初始化 cherries 计数
                    Debug.Log("GameManager initialized.");
                }
                return _instance;
            }
        }

        // 计分板数据
        public int score = 0;

        // 新增：cherries 计数
        public int cherriesCount = 0;

        // 方法：重置游戏状态
        public void ResetGame()
        {
            score = 0;
            cherriesCount = 0;
            Debug.Log("Game state reset: score and cherriesCount set to 0.");
        }
    }
}