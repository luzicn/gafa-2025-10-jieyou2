using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AllControl; // 引入 AllControl 的静态类

public class item_collector : MonoBehaviour
{
    // 移除私有的 cherries 变量，直接从 GameManager 获取
    // int cherries = GameManager.Instance.score;

    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectSoundEffect;

    void Start()
    {
        // 在 Start 方法中更新一次 UI，确保初始显示正确
        UpdateCherriesUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);

            // 直接增加 GameManager 中的 cherriesCount
            GameManager.Instance.cherriesCount++;

            // 更新 UI
            UpdateCherriesUI();

            // 如果您还有独立的 score，可以在这里更新：
            // GameManager.Instance.score = GameManager.Instance.cherriesCount; // 或者其他逻辑
        }
    }

    // 更新 UI 的方法
    private void UpdateCherriesUI()
    {
        if (cherriesText != null)
        {
            cherriesText.text = "Cherries:" + GameManager.Instance.cherriesCount;
        }
    }
}