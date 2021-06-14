using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSubscriber : MonoBehaviour
{
    Rigidbody m_rb;
    [SerializeField] float m_pushPower = 5f;

    void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver;
    }

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    public virtual void OnGameOver()
    {
        FreeConstrains();
    }

    public void FreeConstrains()
    {

        if (m_rb)
        {
            m_rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void RandomPush()
    {
        FreeConstrains();
        float randomDirX = UnityEngine.Random.Range(-1f, 1);
        float randomDirZ = UnityEngine.Random.Range(-1f, 1);

        //transform.Rotate(,)で回転する
        for (int i = 0; i < 360; i++)
        {
            this.transform.Rotate(i * randomDirX, i * randomDirX, i * randomDirX);
        }

        m_rb.AddForce(m_pushPower * new Vector3(randomDirX, 1.5f, randomDirZ), ForceMode.Impulse);
    }
}
