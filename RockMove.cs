using UnityEngine;

public class RockMove : MonoBehaviour
{
    public PlayerCollisionCheck collisionCheck;

    // RockのID（複数のRockを区別するための識別子）
    public int rockID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (collisionCheck == null)
        {
            collisionCheck = GetComponent<PlayerCollisionCheck>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // RockMoveを呼び出す
        Rock();
    }

    private void Rock()
    {
        // このRockが選択されている場合のみ移動を許可する
        if (Input.GetKey(KeyCode.Alpha1) && rockID == 1 ||
            Input.GetKey(KeyCode.Alpha2) && rockID == 2)
        {
            if (collisionCheck.hitRight && Input.GetKeyDown(KeyCode.D))
            {
                this.transform.position += transform.right;
            }
            if (collisionCheck.hitLeft && Input.GetKeyDown(KeyCode.A))
            {
                this.transform.position -= transform.right;
            }
            if (collisionCheck.hitUp && Input.GetKeyDown(KeyCode.W))
            {
                this.transform.position += transform.up;
            }
            if (collisionCheck.hitDown && Input.GetKeyDown(KeyCode.S))
            {
                this.transform.position -= transform.up;
            }
        }
    }
}
