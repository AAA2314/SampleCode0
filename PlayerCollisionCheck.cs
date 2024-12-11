using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    // 衝突チェックの距離
    public float rayDistance = 1.0f;

    // レイヤーマスク（必要に応じて衝突を特定のオブジェクトに限定可能）
    public LayerMask collisionMask;

    public bool hitUp;
    public bool hitDown;
    public bool hitRight;
    public bool hitLeft;

    void Update()
    {
        // 上方向の判定
        hitUp = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, collisionMask);
        // 下方向の判定
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, collisionMask);
        // 右方向の判定
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, collisionMask);
        // 左方向の判定
        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, collisionMask);

        // 結果のデバッグ表示
        //Debug.Log($"上: {hitUp}, 下: {hitDown}, 右: {hitRight}, 左: {hitLeft}");
    }
}
