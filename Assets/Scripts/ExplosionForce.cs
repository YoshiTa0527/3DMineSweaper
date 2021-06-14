using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExplosionForce : MonoBehaviour
{
    public float explosionForce = 4;
    [SerializeField] float m_multiplier = 2f;
    [SerializeField] GameObject m_duststorm = default;
    [SerializeField] GameObject m_gameOverText = default;
    GameObject m_canvas;
    SoundManager m_sm;
    private void Awake()
    {
        m_canvas = GameObject.FindGameObjectWithTag("Canvas");
        m_sm = FindObjectOfType<SoundManager>();
    }

    private IEnumerator Start()
    {
        // wait one frame because some explosions instantiate debris which should then
        // be pushed by physics force
        yield return null;

        m_sm.PlayBakuhatsu();
        float r = 10 * m_multiplier;
        var cols = Physics.OverlapSphere(transform.position, r);
        var rigidbodies = new List<Rigidbody>();
        foreach (var col in cols)
        {
            if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
            {
                rigidbodies.Add(col.attachedRigidbody);
            }
        }
        foreach (var rb in rigidbodies)
        {
            rb.AddExplosionForce(explosionForce * m_multiplier, transform.position, r, 1 * m_multiplier, ForceMode.Impulse);
        }

        Instantiate(m_duststorm);
        FindObjectOfType<CameraGun>().Shake(1f, 2f);
        Instantiate(m_gameOverText, m_canvas.transform);
    }
}

