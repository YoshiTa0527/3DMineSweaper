using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cover : MonoBehaviour
{
    [SerializeField] float m_pushPower = 5f;
    [SerializeField] float m_deleteTime = 1f;
    [SerializeField] Cell m_cell = null;
    public bool m_IsHit { get; set; }
    Renderer m_rend;
    Rigidbody m_rb;
    private void Awake()
    {
        m_IsHit = false;
        m_rend = GetComponent<Renderer>();
        m_rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (m_IsHit)
        {
            m_cell.OpenCell();
        }
    }

    public void PushUpCover()
    {
        Debug.Log("openCoverCalled");
        m_IsHit = false;
        FreeConstrains();
        this.gameObject.tag = "Untagged";

        float randomDirX = Random.Range(-1f, 1);
        float randomDirZ = Random.Range(-1f, 1);

        //transform.Rotate(,)で回転する
        for (int i = 0; i < 360; i++)
        {
            this.transform.Rotate(i * randomDirX, i * randomDirX, i * randomDirX);
        }

        m_rb.AddForce(m_pushPower * new Vector3(randomDirX, 1.5f, randomDirZ), ForceMode.Impulse);
        StartCoroutine(DestroyCover());
    }

    Sequence m_seq;
    /// <summary>
    /// 一秒かけて透明にする
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyCover()
    {
        m_seq.Append(DOTween.ToAlpha(() => m_rend.material.color, c => m_rend.material.color = c, 0f, m_deleteTime)).OnComplete(() => Destroy(this.gameObject));
        yield return new WaitForSeconds(1);
        m_seq.Play();
    }

    void FreeConstrains()
    {
        m_rb.constraints = RigidbodyConstraints.None;
    }



}
