using UnityEngine;

public class PlayerPicker2D : MonoBehaviour
{
    [Header("设置")]
    public float pickRange = 5f;       // 拾取半径
    public Transform playerTransform;  // 玩家的位置
    public Transform pickCenter;       // 射线发射点（通常是玩家中心或眼睛位置）
    public LayerMask pickableLayer;    // (可选) 建议设置一个 Layer 专门放物品，避免射线打到墙壁或自己
    [SerializeField] private AudioSource collectSoundEffect;

    void Start()
    {
        if (playerTransform == null) playerTransform = transform;
        if (pickCenter == null) pickCenter = transform;
    }

    void Update()
    {
        // 0是左键，1是右键，2是中键
        if (Input.GetMouseButtonDown(1))
        {
            collectSoundEffect.Play();
            TryPickObject();
        }
    }

    void TryPickObject()
    {
        Vector2 rayOrigin = pickCenter.position;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 计算鼠标和玩家的距离
        float distanceToMouse = Vector2.Distance(rayOrigin, mouseWorldPos);

        // 1. 距离检查：如果鼠标点击的位置超过了拾取范围，直接忽略
        if (distanceToMouse > pickRange)
        {
            Debug.Log("太远了，够不着！");
            return;
        }

        // 2. 射线检测 (Raycast) - 优先检测鼠标指向路径上的物体
        Vector2 direction = (mouseWorldPos - rayOrigin).normalized;
        // 使用 pickableLayer 过滤，如果没有设置 LayerMask (默认 Nothing)，则检测所有层
        // 这里的 distanceToMouse 是为了防止射线穿过鼠标点击点打到更后面的东西
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, distanceToMouse, pickableLayer);

        Debug.DrawLine(rayOrigin, mouseWorldPos, Color.red, 0.5f); // 调试画线

        if (hit.collider != null)
        {
            if (HandlePickup(hit.collider.gameObject)) return; // 如果成功拾取，就结束
        }

        // 3. 重叠检测 (Overlap) - 如果射线没打中（比如点歪了一点点），检测鼠标点击那个点上有没有东西
        // 这种方式让点击体验更流畅
        Collider2D[] hitObjects = Physics2D.OverlapPointAll(mouseWorldPos, pickableLayer);

        foreach (Collider2D col in hitObjects)
        {
            if (HandlePickup(col.gameObject)) return; // 找到一个就退出
        }
    }

    // 处理拾取逻辑，返回 true 表示成功拾取
    bool HandlePickup(GameObject pickedObject)
    {
        // 防止捡到玩家自己 (如果玩家也有 Collider)
        if (pickedObject == gameObject) return false;

        // ====== 检查 Apple ======
        if (pickedObject.CompareTag("Apple"))
        {
            Debug.Log("右键拾取了苹果 (Apple) !");

            // 在这里添加背包逻辑，例如: Inventory.Add("Apple");

            Destroy(pickedObject); // 销毁场景中的物体
            return true;
        }
        // ====== 检查 Cherry ======
        else if (pickedObject.CompareTag("Cherry"))
        {
            Debug.Log("右键拾取了樱桃 (Cherry) !");
            Destroy(pickedObject);
            return true;
        }

        return false;
    }
}