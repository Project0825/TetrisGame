using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public const int NORMAL_ROWS = 20;
    public const int MAX_ROWS = 23;
    public const int MAX_COLUMNS = 10;

    private Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];

    private int score = 0;
    private int highScore = 0;
    private int numberGame = 0;

    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }
    public int NumberGame { get { return numberGame; } }


    public bool IsDataUpdate = false;

    private void Awake()
    {
        loadData();
    }
    /// <summary>
    /// 检查是否可以放置
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsVaildMapPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            if (IsInsideMap(pos) == false) return false;
            if (map[(int)pos.x, (int)pos.y] != null) return false;
        }
        return true;
    }
    public bool IsGameOver()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j, i] != null)
                {
                    numberGame++;
                    saveData();
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }
    public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }
        return checkMap();
    }
    
    private bool checkMap()
    {
        int count = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = checkIsRowFull(i);
            if (isFull)
            {
                count++;
                deleteRow(i);
                moveDownRowsAbove(i + 1);
                i--;
            }
        }
        if (count > 0)
        {
            score += (count * 10);
            if (score > highScore)
            {
                highScore = score;
            }
            IsDataUpdate = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool checkIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] == null) return false;
        }
        return true;
    }
    private void deleteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }
    private void moveDownRowsAbove(int row)
    {

        for (int i = row; i < MAX_ROWS; i++)
        {
            moveDownRow(i);
        }
    }
    private void moveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if(map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }

    }
    private void loadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        numberGame = PlayerPrefs.GetInt("NumberGame", 0);
    }
    private void saveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumberGame", numberGame);
    }
    public void Restart()
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            for (int k = 0; k < MAX_ROWS; k++)
            {
                if (map[i, k] != null)
                {
                    Destroy(map[i, k].gameObject);
                    map[i, k] = null;
                }
            }
        }
        score = 0;
    }
    public void ClearData()
    {
        score = 0;
        highScore = 0;
        numberGame = 0;
        saveData();
    }
}
