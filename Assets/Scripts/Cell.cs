using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Cell : MonoBehaviour
{
    [SerializeField] GameObject m_lampParent = null;
    List<Light> m_lights = new List<Light>();
    [SerializeField] GameObject m_mine = null;
    [SerializeField] float m_mineOffset = 0.5f;
    [SerializeField] Cover m_cover = null;
    [SerializeField] CellState m_cellState = CellState.None;
    [SerializeField] GameObject m_mineFlag = null;
    [SerializeField] ParticleSystem m_particle = null;
    bool m_isAddedFlag = false;
    public bool m_IsOpened { get; set; }
    public CellState m_CellState
    {
        get => m_cellState;
        set
        {
            m_cellState = value; OnCellStateChanged();
        }
    }

    public int indexOfArrayX;
    public int indexOfArrayY;

    private void Awake()
    {
        if (m_mineFlag) m_mineFlag.SetActive(false);
        m_isAddedFlag = false;
        m_IsOpened = false;
        m_lights = GetComponentsInChildren<Light>().ToList();
        m_lights.ForEach(light => light.enabled = false);
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    public void OpenCell()
    {
        if (m_isAddedFlag && m_IsOpened) return;
        m_IsOpened = true;
        m_cover.PushUpCover();

        if (m_cellState == CellState.None) FindObjectOfType<Board>().OpenAroundCells(indexOfArrayX, indexOfArrayY);
        if (m_cellState == CellState.Mine)
        {
            Instantiate(m_mine, this.transform.position + new Vector3(0, m_mineOffset, 0), Quaternion.Euler(0, 0, 90));
            Board.m_IsGameOver = true;
            Debug.Log("GAME OVER");
        }
        if (m_particle && !Board.m_IsGameOver)
        {
            Instantiate(m_particle.gameObject, this.gameObject.transform.position + new Vector3(0, m_mineOffset, 0), Quaternion.identity);
        }
    }

    public void SetOrRemoveFlag()
    {
        if (m_IsOpened) return;
        if (!m_isAddedFlag)
        {
            Debug.Log("setFlag");
            m_mineFlag.gameObject.SetActive(true);
            m_isAddedFlag = true;
        }
        else
        {
            Debug.Log("removeFlag");
            m_mineFlag.gameObject.SetActive(false);
            m_isAddedFlag = false;
        }
    }

    private void OnCellStateChanged()
    {
        if (m_lights == null) return;
        switch (m_cellState)
        {
            case CellState.None:

                break;
            case CellState.One:
                m_lights.FirstOrDefault().enabled = true;
                break;
            case CellState.Two:
                for (int i = 0; i < 2; i++)
                {
                    m_lights[i].enabled = true;
                }
                break;
            case CellState.Three:
                for (int i = 0; i < 3; i++)
                {
                    m_lights[i].enabled = true;
                }
                break;
            case CellState.Four:
                for (int i = 0; i < 4; i++)
                {
                    m_lights[i].enabled = true;
                }
                break;
            case CellState.Five:
                for (int i = 0; i < 5; i++)
                {
                    m_lights[i].enabled = true;
                }
                break;
            case CellState.Six:
                for (int i = 0; i < 6; i++)
                {
                    m_lights[i].enabled = true;
                }
                break;
            case CellState.Seven:
                for (int i = 0; i < 7; i++)
                {
                    m_lights[i].enabled = true;
                }
                break;
            case CellState.Eight:
                for (int i = 0; i < 8; i++)
                {
                    m_lights[i].enabled = true;
                }
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
