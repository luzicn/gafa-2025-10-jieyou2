using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour // 1. 重命名为 CameraFollow 以避免与 Unity 的 Camera 类冲突
{
    [SerializeField] private Transform player;

    // 2. 移除空的 Start() 方法，因为它在此脚本中没有必要

    // Update is called once per frame
    void Update()
    {
        // 3. 检查 player 是否已赋值，防止空引用异常
        if (player != null)
        {
            // 4. 保持摄像机的 Z 轴位置，只跟随玩家的 X 和 Y 轴
            //    这通常是2D摄像机跟随玩家的需求
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("CameraFollow: Player transform is not assigned!"); // 5. 在未赋值时发出警告
        }
    }
}