using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CellTestScript : MonoBehaviour
{
    [SerializeField] GameObject m_mine = null;
    [SerializeField] float m_mineOffset = 0.5f;
    [SerializeField] Cover m_cover = null;
    [SerializeField] CellStats m_cellState = CellStats.None;
    [SerializeField] GameObject m_mineFlag = null;
    [SerializeField] ParticleSystem m_particle = null;
    [SerializeField] Text m_upperText = default;
    bool m_isAddedFlag = false;
    public bool m_IsOpened { get; set; }
    public CellStats m_CellState
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
        if (!m_upperText) return;
        switch (m_cellState)
        {
            case CellStats.None:

                break;
            case CellStats.One:
                m_upperText.text = "1";
                break;
            default:
                m_upperText.text = "";
                break;
        }
    }
}
public enum CellStats
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
