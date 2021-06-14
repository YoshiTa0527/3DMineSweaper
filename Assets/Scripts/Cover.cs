using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cover : EventSubscriber
{

    [SerializeField] float m_deleteTime = 1f;
    [SerializeField] Cell m_cell = default;
    public Cell GetCell() { return m_cell; }
    public bool m_IsHit { get; set; }
    Renderer m_rend;

    private void Awake()
    {
        m_IsHit = false;
        m_rend = GetComponent<Renderer>();

    }
    private void Update()
    {
        if (m_IsHit)
        {

        }
    }

    public override void OnGameOver()
    {
        base.OnGameOver();
    }
    /// <summary>
    /// カバー（自分自身）をランダムに吹き飛ばす
    /// </summary>
    public void PushUpCover()
    {
        if (m_cell.m_isAddedFlag) return;
        m_IsHit = false;
        RandomPush();
        StartCoroutine(DestroyCover());
        m_cell.OpenCell();
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
        Destroy(this.gameObject, 1.1f);
    }


}
