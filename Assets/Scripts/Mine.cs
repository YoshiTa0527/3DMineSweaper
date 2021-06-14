using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] GameObject m_explosiveEffect = default;
    [SerializeField] float m_radius = 10f;
    float m_timer;
    bool m_isEploded = false;

    private void Update()
    {
        if (this.gameObject.activeSelf && !m_isEploded)
        {
            m_timer += Time.deltaTime;
            if (m_timer > 2f) Explosion();
        }
    }

    /// <summary>
    /// 爆発する
    /// </summary>
    public void Explosion()
    {
        m_isEploded = true;
        Instantiate(m_explosiveEffect, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);
    }
}
