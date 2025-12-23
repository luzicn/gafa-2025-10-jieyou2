using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AllControl; // 引入 AllControl 的静态类

public class playerlife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            Die();
        }
        if (collision.gameObject.CompareTag("fallground"))
        {
            deathSoundEffect.Play();
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    // 修改此方法
    private void RestartLevel()
    {
        // 在重新加载场景之前，重置 GameManager 的状态
        // 确保 GameManager.Instance 已经被创建
        if (GameManager.Instance != null) // 检查是否已初始化
        {
            GameManager.Instance.ResetGame(); // 调用重置方法
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}