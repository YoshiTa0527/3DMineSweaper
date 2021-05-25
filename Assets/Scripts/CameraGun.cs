using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGun : MonoBehaviour
{
    [SerializeField] ParticleSystem m_particle = null;
    bool m_isFirstClick = true;
    private void Update()
    {
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
                        if (m_isFirstClick)
                        {
                            m_isFirstClick = false;
                            FindObjectOfType<Board>().MineAdd();
                        }
                        hit.collider.gameObject.GetComponent<Cover>().m_IsHit = true;


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
}
