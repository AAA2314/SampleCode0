using System.Collections.Generic;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    const int GridSize = 5; // グリッドのサイズ
    int[,] grid; // グリッドデータ
    int playerRow = 0; // プレイヤーの現在行
    int playerCol = 0; // プレイヤーの現在列
    [SerializeField] GameObject Wall; // 壁オブジェクト
    [SerializeField] GameObject Soil; // 土オブジェクト
    [SerializeField] Transform startPoint; // 基準となる位置
    public bool movecheck = false;

    void Start()
    {
        // グリッドを初期化
        grid = GenerateRandomGrid(GridSize, GridSize);
        PrintGrid(grid);
        Debug.Log($"プレイヤー初期位置: [{playerRow}][{playerCol}]");
        WallCreate();
        SoilCreate();
    }

    void Update()
    {
        HandlePlayerInput();
    }

    // プレイヤーの入力処理
    void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Wキーで上移動
        {
            MovePlayer(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Sキーで下移動
        {
            MovePlayer(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Aキーで左移動
        {
            MovePlayer(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Dキーで右移動
        {
            MovePlayer(0, 1);
        }
    }

    // プレイヤーを移動させる
    void MovePlayer(int rowChange, int colChange)
    {
        int newRow = playerRow + rowChange;
        int newCol = playerCol + colChange;

        // グリッドの範囲外チェック
        if (newRow < 0 || newRow >= GridSize || newCol < 0 || newCol >= GridSize)
        {
            movecheck = false;
            Debug.Log("移動不可: グリッド外");
            return;
        }

        // 壁がない場所でのみ移動
        if (grid[newRow, newCol] == 1) // 1が壁を表す場合
        {
            movecheck = false;
            Debug.Log("移動不可: 壁の上");
            return;
        }

        // 移動可能な場合のみ更新
        Debug.Log($"移動: [{playerRow}][{playerCol}] → [{newRow}][{newCol}]");
        playerRow = newRow;
        playerCol = newCol;
        movecheck = true;
    }

    // ランダムなグリッドを生成
    int[,] GenerateRandomGrid(int rows, int cols)
    {
        int[,] grid = new int[rows, cols];

        // UnityのRandomを使用
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                grid[r, c] = UnityEngine.Random.Range(0, 2); // 0は土、1は壁
            }
        }
        return grid;
    }

    // グリッドを出力
    void PrintGrid(int[,] grid)
    {
        Debug.Log("グリッド:");
        for (int r = 0; r < grid.GetLength(0); r++)
        {
            string row = "";
            for (int c = 0; c < grid.GetLength(1); c++)
            {
                row += grid[r, c] + "\t";
            }
            Debug.Log(row);
        }
    }

    // 壁を基準位置から生成（外周のみ）
    void WallCreate()
    {
        // 基準位置（startPoint）の座標を取得
        Vector2 basePosition = startPoint.position;

        // 外周の壁を生成
        for (int i = 0; i < GridSize; i++) // 行
        {
            for (int j = 0; j < GridSize; j++) // 列
            {
                // 上端と下端の壁
                if (i == 0 || i == GridSize - 1 || j == 0 || j == GridSize - 1)
                {
                    Vector2 position = basePosition + new Vector2(j, i);
                    Instantiate(Wall, position, Quaternion.identity);
                    grid[i, j] = 1; // 壁を1としてマーク
                }
            }
        }
    }

    // 土を基準位置から生成（外周を除く）
    void SoilCreate()
    {
        // 基準位置（startPoint）の座標を取得
        Vector2 basePosition = startPoint.position;

        // 内部の土を生成
        for (int i = 1; i < GridSize - 1; i++) // 外周を避けて生成
        {
            for (int j = 1; j < GridSize - 1; j++) // 外周を避けて生成
            {
                Vector2 position = basePosition + new Vector2(j, i);
                Instantiate(Soil, position, Quaternion.identity);
                grid[i, j] = 0; // 土を0としてマーク
            }
        }
    }
}
