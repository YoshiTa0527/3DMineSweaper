﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Board : MonoBehaviour
{
    [SerializeField] int m_indexX = 5;
    [SerializeField] int m_indexY = 5;
    [SerializeField] Cell m_cell = null;
    [SerializeField] GameObject m_cellParent = null;
    public GameObject m_targetCell;


    public static bool m_IsGameOver { get; set; }
    Cell[,] m_cellArray;

    /// <summary>地雷の個数</summary>
    [SerializeField] int m_mineCount = 5;
    private void Start()
    {
        m_IsGameOver = false;
        MakeBoard();

    }
    /// <summary>
    /// 
    /// </summary>
    private void SetCellState()
    {
        for (int n = 0; n < m_cellArray.GetLength(0); n++)
        {
            for (int k = 0; k < m_cellArray.GetLength(1); k++)
            {
                ChangeCellState(CheckArround(n, k), m_cellArray[n, k]);
            }
        }
    }

    void MakeBoard()
    {
        m_cellArray = new Cell[m_indexX, m_indexY];

        for (int n = 0; n < m_cellArray.GetLength(0); n++)
        {
            for (int k = 0; k < m_cellArray.GetLength(1); k++)
            {
                Cell cell = Instantiate(m_cell, new Vector3(-4 + 1.5f * n, 0, -4f + 1.5f * k), Quaternion.identity, m_cellParent.transform);

                m_cellArray[n, k] = cell;
                cell.indexOfArrayX = n;
                cell.indexOfArrayY = k;
                cell.m_CellState = 0;
                Debug.Log(cell.GetHashCode());
            }
        }
    }

    /// <summary>
    /// ランダムな位置に指定された個数爆弾を設置する
    /// </summary>
    public void MineAdd()
    {
        if (m_mineCount >= m_indexX * m_indexY) return;

        int mineCount = 0;
        while (true)
        {
            if (m_cellArray != null)
            {
                int randomX = UnityEngine.Random.Range(0, m_cellArray.GetLength(0));
                int randomY = UnityEngine.Random.Range(0, m_cellArray.GetLength(1));
                /*ランダムに選ばれた配列の場所に既に地雷がある場合はもう一度ランダムに設定する*/
                if (m_cellArray[randomX, randomY].m_CellState != CellState.Mine)
                {

                    m_cellArray[randomX, randomY].m_CellState = CellState.Mine;
                    mineCount++;

                }

                m_cellArray[randomX, randomY].gameObject.name = "Mine" + (mineCount + 1);
            }
        }




        SetCellState();
        Debug.Log("配列に設定されたMineの数：" + mineCount);
        Debug.Log("ヒエラルキーに設定されたMineの数：" + m_cellParent.transform.GetComponentsInChildren<Cell>()
                                                    .Where(cell => cell.gameObject.name.Contains("Mine"))
                                                    .Count());
    }

    /// <summary>
    /// ランダムに並び替えられた配列を作る
    /// </summary>
    int[] SetRandomNumber(int endNum)
    {
        List<int> list = new List<int>();
        int[] randomArray = new int[endNum];
        for (int i = 0; i < endNum; i++)
        {
            list.Add(i);
        }

        int arrayIndex = 0;
        while (list.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            randomArray[arrayIndex] = list[randomIndex];
            list.RemoveAt(randomIndex);
            arrayIndex++;
        }
        return randomArray;
    }

    /// <summary>
    /// あるセルの周囲を調べ、それを配列として返す
    /// </summary>
    Cell[] CheckArround(int x, int y)
    {
        List<Cell> cellsAround = new List<Cell>();
        bool isTop = x == 0;
        bool isButtom = x == m_cellArray.GetLength(0) - 1;
        bool isLeft = y == 0;
        bool isRight = y == m_cellArray.GetLength(1) - 1;

        if (!isTop && !isLeft) cellsAround.Add(m_cellArray[x - 1, y - 1]);
        if (!isTop) cellsAround.Add(m_cellArray[x - 1, y]);
        if (!isTop && !isRight) cellsAround.Add(m_cellArray[x - 1, y + 1]);
        if (!isRight) cellsAround.Add(m_cellArray[x, y + 1]);
        if (!isButtom && !isRight) cellsAround.Add(m_cellArray[x + 1, y + 1]);
        if (!isButtom) cellsAround.Add(m_cellArray[x + 1, y]);
        if (!isButtom && !isLeft) cellsAround.Add(m_cellArray[x + 1, y - 1]);
        if (!isLeft) cellsAround.Add(m_cellArray[x, y - 1]);

        return cellsAround.ToArray();
    }
    /// <summary>
    /// 周囲の地雷の数に応じて指定されたcellの数を変える
    /// </summary>
    /// <param name="cellsAround"></param>
    /// <param name="cell"></param>
    void ChangeCellState(Cell[] cellsAround, Cell cell)
    {
        if (cell.m_CellState == CellState.Mine) return;
        int mineCount = cellsAround.ToList().Where(i => i.m_CellState == CellState.Mine).Count();
        cell.m_CellState = (CellState)mineCount;
    }

    /// <summary>
    /// 隣接するマスに地雷が置かれていないときはそれらが自動で開けられる
    /// </summary>
    public void OpenAroundCells(int x, int y)
    {
        foreach (var item in CheckArround(x, y))
        {
            if (!item.m_IsOpened) item.OpenCell();
        }
    }
}
