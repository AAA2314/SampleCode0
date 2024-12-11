using System.Collections.Generic;
using UnityEngine;

public class GridNavigator : MonoBehaviour
{
    const int GridSize = 5; // グリッドのサイズ

    // 使用するポリオミノ（タイル）の種類
    static List<(int, int)[]> Tiles = new List<(int, int)[]>
    {
        new (int, int)[] { (0, 0) },                // 1x1
        new (int, int)[] { (0, 0), (0, 1) },         // 1x2
        new (int, int)[] { (0, 0), (1, 0) },         // 2x1
        new (int, int)[] { (0, 0), (0, 1), (1, 0), (1, 1) }, // 2x2
        new (int, int)[] { (0, 0), (0, 1), (1, 1) }, // L型
        new (int, int)[] { (0, 0), (1, 0), (1, 1) }  // 逆L型
    };

    void Start()
    {
        // ランダムなグリッドを作成（数字をランダムに配置）
        int[,] grid = GenerateRandomGrid(GridSize, GridSize);
        PrintGrid(grid);

        // 訪問履歴を保持するリストとセット
        List<string> path = new List<string>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>(); // 訪問済みのセル

        // 開始地点
        int currentRow = 0, currentCol = 0;
        visited.Add((currentRow, currentCol)); // 開始地点を記録

        // 移動処理
        while (true)
        {
            // 次の移動先を決定
            var nextMove = GetNextMove(grid, currentRow, currentCol, visited);

            // 移動先がない場合、終了
            if (!nextMove.HasValue)
                break;

            // 移動を記録
            int nextRow = nextMove.Value.Item1;
            int nextCol = nextMove.Value.Item2;
            if (grid[currentRow, currentCol] != grid[nextRow, nextCol])
                path.Add($"[{currentRow}][{currentCol}]→[{nextRow}][{nextCol}]");

            // 現在地を更新
            currentRow = nextRow;
            currentCol = nextCol;

            // 移動先を訪問済みとして記録
            visited.Add((currentRow, currentCol));
        }

        // 結果の出力
        Debug.Log("\n移動パス:");
        foreach (var step in path)
        {
            Debug.Log(step);
        }
    }

    // ランダムなグリッドを生成
    static int[,] GenerateRandomGrid(int rows, int cols)
    {
        // 5x5の空のグリッドを作成
        int[,] grid = new int[GridSize, GridSize];

        // タイルをランダムに配置
        int tileId = 1; // タイルの識別番号

        for (int i = 0; i < 100; i++) // 最大100回試みる
        {
            // ランダムなタイルを選択
            var tile = Tiles[Random.Range(0, Tiles.Count)];

            // ランダムな位置を選択
            int startRow = Random.Range(0, GridSize);
            int startCol = Random.Range(0, GridSize);

            // タイルが収まるかチェック
            bool fits = true;
            foreach (var (dr, dc) in tile)
            {
                int r = startRow + dr;
                int c = startCol + dc;
                if (r < 0 || r >= GridSize || c < 0 || c >= GridSize || grid[r, c] != 0)
                {
                    fits = false;
                    break;
                }
            }

            // 収まる場合、タイルを配置
            if (fits)
            {
                foreach (var (dr, dc) in tile)
                {
                    grid[startRow + dr, startCol + dc] = tileId;
                }
                tileId++; // 次のタイル番号
            }
        }
        return grid;
    }

    // 次の移動先を決定
    static (int, int)? GetNextMove(int[,] grid, int row, int col, HashSet<(int, int)> visited)
    {
        int currentValue = grid[row, col];
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        // 移動方向（上下左右）を定義
        int[][] directions = new int[][]
        {
            new int[] { -1, 0 }, // 上
            new int[] { 1, 0 },  // 下
            new int[] { 0, -1 }, // 左
            new int[] { 0, 1 }   // 右
        };

        // 隣接セルを収集
        List<(int, int, int)> neighbors = new List<(int, int, int)>(); // (row, col, value)
        foreach (var dir in directions)
        {
            int newRow = row + dir[0];
            int newCol = col + dir[1];
            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && !visited.Contains((newRow, newCol)))
            {
                neighbors.Add((newRow, newCol, grid[newRow, newCol]));
            }
        }

        // 同じ数字のセルがあれば優先して選択
        foreach (var neighbor in neighbors)
        {
            if (neighbor.Item3 == currentValue)
                return (neighbor.Item1, neighbor.Item2);
        }

        // 異なる数字の場合、数字の小さいセルを選択
        (int, int)? smallestNeighbor = null;
        int smallestValue = int.MaxValue;
        foreach (var neighbor in neighbors)
        {
            if (neighbor.Item3 < smallestValue)
            {
                smallestNeighbor = (neighbor.Item1, neighbor.Item2);
                smallestValue = neighbor.Item3;
            }
        }

        return smallestNeighbor;
    }

    // グリッドを出力
    static void PrintGrid(int[,] grid)
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
}
