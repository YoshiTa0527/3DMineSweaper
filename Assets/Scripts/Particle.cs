using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] ParticleSystem m_particle = null;
    [SerializeField] float m_deleteTime = 0.5f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject go = Instantiate(m_particle.gameObject);
            Destroy(go, m_deleteTime);
        }
    }
}
