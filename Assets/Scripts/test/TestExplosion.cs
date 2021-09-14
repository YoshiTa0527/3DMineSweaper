using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExplosion : MonoBehaviour
{
    [SerializeField] float m_radius = 5f;
    [SerializeField] float m_explosionForce = 5f;
    [SerializeField] float m_upwords = 5f;

    public void Exploosion()
    {
        Debug.Log("bomb");
        Collider[] cols = Physics.OverlapSphere(this.transform.position, m_radius);
        var rbs = new List<Rigidbody>();

        foreach (var col in cols)
        {

            if (col.attachedRigidbody != null && !rbs.Contains(col.attachedRigidbody))
            {
                Debug.Log($"{col.name}");
                rbs.Add(col.attachedRigidbody);
            }
        }


        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(m_explosionForce, this.transform.position, m_radius, m_upwords, ForceMode.Impulse);
        }

    }
}
