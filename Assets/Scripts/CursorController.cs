using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] Texture2D m_coursorTexture;
    [SerializeField] CursorMode m_cursorMode = CursorMode.Auto;
    [SerializeField] Vector2 m_hotSpot = Vector2.zero;
    private void OnMouseEnter()
    {
        Cursor.SetCursor(m_coursorTexture, m_hotSpot, m_cursorMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, m_hotSpot, m_cursorMode);
        Rigidbody m_rb = GetComponent<Rigidbody>();
    }
}
