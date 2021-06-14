using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGun : MonoBehaviour
{
    bool m_isFirstClick = true;
    Board m_board;

    private void Start()
    {
        m_board = FindObjectOfType<Board>();
    }
    private void Update()
    {
        if (Board.m_IsGameOver) return;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) // 左クリック OR　右クリック
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "Cover")
                    {
                        Cover cover = hit.collider.gameObject.GetComponent<Cover>();
                        if (m_isFirstClick)
                        {
                            m_isFirstClick = false;
                            Board board = FindObjectOfType<Board>();
                            board.m_firstClickPointX = cover.GetCell().indexOfArrayX;
                            board.m_firstClickPointY = cover.GetCell().indexOfArrayY;
                            board.MineAdd();
                        }
                        cover.PushUpCover();
                    }
                }
                else
                {
                    Debug.Log("左クリックが押された");
                    Debug.Log(hit.collider.name);
                    if (hit.collider.tag == "Cover")
                    {
                        hit.collider.gameObject.transform.GetComponentInParent<Cell>()?.SetOrRemoveFlag();
                    }
                }
            }
        }
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = pos;
    }
}



