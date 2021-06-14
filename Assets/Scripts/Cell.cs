using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Cell : EventSubscriber
{
    [SerializeField] GameObject m_numberParent = default;
    GameObject[] m_numbers;
    [SerializeField] GameObject m_mine = null;
    [SerializeField] float m_mineOffset = 0.5f;
    [SerializeField] Cover m_cover = null;
    [SerializeField] CellState m_cellState = CellState.None;
    [SerializeField] GameObject m_mineFlag = null;
    [SerializeField] ParticleSystem m_particle = null;
    public bool m_isAddedFlag { get; set; }
    Board m_board;
    public bool m_IsOpened { get; set; }
    public CellState m_CellState
    {
        get => m_cellState;
        set
        {
            m_cellState = value;
            OnCellStateChanged();
        }
    }

    public int indexOfArrayX;
    public int indexOfArrayY;

    private void Awake()
    {
        if (m_mineFlag) m_mineFlag.SetActive(false);
        m_isAddedFlag = false;
        m_IsOpened = false;
        if (m_numberParent)
        {
            GetAllChildObject();
            m_numbers.ToList().ForEach(n => n.SetActive(false));
        }
        m_board = FindObjectOfType<Board>();
    }

    private void GetAllChildObject()
    {
        m_numbers = new GameObject[m_numberParent.transform.childCount];

        for (int i = 0; i < m_numberParent.transform.childCount; i++)
        {
            m_numbers[i] = m_numberParent.transform.GetChild(i).gameObject;
        }
    }

    private void OnValidate()
    {
        //OnCellStateChanged();
    }

    public override void OnGameOver()
    {
        base.OnGameOver();
    }

    public void OpenCell()
    {
        if (m_IsOpened) return;
        if (!m_isAddedFlag)
        {
            m_IsOpened = true;
            m_cover.PushUpCover();
            SoundManager sm = FindObjectOfType<SoundManager>();
            if (m_cellState == CellState.None)
            {
                //FindObjectOfType<Board>().OpenAroundCells(indexOfArrayX, indexOfArrayY);
                FindObjectOfType<Board>().OpenAroundCell(indexOfArrayX, indexOfArrayY);
                sm.PlayCoverHit();
            }
            else if (m_cellState == CellState.Mine)
            {
                sm.PlayBakuhatsuCoverHit();
                MineApear();
                EventManager.GameOver();
                Debug.Log("GAME OVER");
            }
            else { sm.PlayCoverHit(); }
            if (m_particle) Instantiate(m_particle.gameObject, this.gameObject.transform.position + new Vector3(0, m_mineOffset, 0), Quaternion.identity);
        }
    }

    public void MineApear()
    {
        FindObjectOfType<BGMManager>()?.StopBgm();
        FindObjectOfType<SoundManager>()?.PlayDoukasen();
        if (m_cellState == CellState.Mine)
            Instantiate(m_mine, this.transform.position + new Vector3(0, m_mineOffset, 0), Quaternion.Euler(0, 0, 90));
    }

    public void SetOrRemoveFlag()
    {
        if (m_IsOpened) return;
        if (!m_isAddedFlag)
        {
            Debug.Log("setFlag");
            m_mineFlag.gameObject.SetActive(true);
            m_board.m_FlagCount++;
            m_isAddedFlag = true;
        }
        else
        {
            Debug.Log("removeFlag");
            m_mineFlag.gameObject.SetActive(false);
            m_board.m_FlagCount--;
            m_isAddedFlag = false;
        }
        if (m_cellState == CellState.Mine && m_isAddedFlag) m_board.m_colectCount++;
        else if (m_cellState == CellState.Mine && !m_isAddedFlag) m_board.m_colectCount--;
        Debug.Log("colectCOunt" + m_board.m_colectCount);
        m_board.RefleshTexts();
    }

    private void OnCellStateChanged()
    {
        if (m_numbers == null) return;
        Debug.Log("cellstate" + m_CellState.ToString());
        this.gameObject.name = m_CellState.ToString();
        switch (m_cellState)
        {
            case CellState.None:
                //m_numbers[0].SetActive(true);
                break;
            case CellState.One:
                m_numbers[1].SetActive(true);
                break;
            case CellState.Two:
                m_numbers[2].SetActive(true);
                break;
            case CellState.Three:
                m_numbers[3].SetActive(true);
                break;
            case CellState.Four:
                m_numbers[4].SetActive(true);
                break;
            case CellState.Five:
                m_numbers[5].SetActive(true);
                break;
            case CellState.Six:
                m_numbers[6].SetActive(true);
                break;
            case CellState.Seven:
                m_numbers[7].SetActive(true);
                break;
            case CellState.Eight:
                m_numbers[8].SetActive(true);
                break;
            case CellState.Mine:
                break;
            default:
                break;
        }
    }
}
public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,
}
