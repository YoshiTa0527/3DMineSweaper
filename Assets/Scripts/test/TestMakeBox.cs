using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMakeBox : MonoBehaviour
{
    [SerializeField] GameObject m_box = default;
    [SerializeField] TestExplosion m_mine = default;

    private void Start()
    {
        GameObject[,,] boxArray = new GameObject[5, 5, 5];
        for (int i = 0; i < boxArray.GetLength(0); i++)
        {
            for (int j = 0; j < boxArray.GetLength(1); j++)
            {
                for (int k = 0; k < boxArray.GetLength(2); k++)
                {
                    if (i == 2 && j == 0 && k == 2)
                    {
                        Debug.Log("bomb");
                        GameObject mine = Instantiate(m_mine.gameObject, new Vector3(-4 + 1.5f * i, -4f + 1.5f * j, -4f + 1.5f * k), Quaternion.identity);
                        mine.name = $"Mine:i:{i}:j:{j}:k:{k}";
                    }
                    else
                    {
                        GameObject box = Instantiate(m_box, new Vector3(-4 + 1.5f * i, -4f + 1.5f * j, -4f + 1.5f * k), Quaternion.identity);
                        boxArray[i, j, k] = box;
                        box.name = $"cube:i:{i}:j:{j}:k:{k}";
                    }

                }
            }

        }
    }



    public void PlayExploosionCol()
    {
        m_mine.Exploosion();
    }

}
