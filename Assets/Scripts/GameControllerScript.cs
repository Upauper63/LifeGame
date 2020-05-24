using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {
    public int[,] board = new int[15, 20];
    public int[,] board_alias = new int[15, 20];
    GameObject cell;
    public Material blackCellColor;
    public Material whiteCellColor;
    public bool isPlaying;

    // Use this for initialization
    void Start () {
        cell = (GameObject)Resources.Load("Prefabs/Cell");
        initBoard();
        isPlaying = false;
    }

    // Update is called once per frame
    void Update () {
        RenderBoard();
    }

    void initBoard()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                Vector3 position = new Vector3(j, 0, i);
                Instantiate(cell, position, Quaternion.Euler(90, 0, 0));
                board[j, i] = 0;
            }
        }
    }

    void RenderBoard()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                board_alias[j, i] = board[j, i];
            }
        }

        var cells = GameObject.FindGameObjectsWithTag("Cell");

        foreach(var cell in cells)
        {
            Destroy(cell);
        }

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                CheckAdjacent(j, i);
                Vector3 position = new Vector3(j, 0, i);
                GameObject obj = Instantiate(cell, position, Quaternion.Euler(90, 0, 0));
                if (board[j, i] == 0)
                {
                    obj.GetComponent<Renderer>().material.color = blackCellColor.color;
                }
                else if (board[j, i] == 1)
                {
                    obj.GetComponent<Renderer>().material.color = whiteCellColor.color;
                }
            }
        }
    }

    void CheckAdjacent(int s, int t)
    {
        if (isPlaying)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (board_alias[(s + 15 + j) % 15, (t + 20 + i) % 20] == 1)
                    {
                        count++;
                    }
                }
            }
            CheckCellStatus(count, s, t);
        }
    }

    void CheckCellStatus(int count, int s, int t)
    {
        if (board_alias[s, t] == 0 && count == 3)
        {
            board[s, t] = 1;
        }else if (board_alias[s, t] == 1 && (count == 2 || count == 3))
        {
            board[s, t] = 1;
        }else if(board_alias[s, t] == 1 && count <= 1)
        {
            board[s, t] = 0;
        }else if(board_alias[s, t] == 1 && count >= 4)
        {
            board[s, t] = 0;
        }
    }
}
