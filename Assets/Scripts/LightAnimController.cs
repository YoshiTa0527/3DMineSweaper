using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimController : EventSubscriber
{
    Animator m_anim;
    float m_animSpd = 0.5f;
    [SerializeField] float m_waitTime = 1f;
    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_anim.SetFloat("AnimSpeed", m_animSpd);
    }

    public override void OnGameOver()
    {
        m_anim.SetFloat("AnimSpeed", 1.2f);
    }


}
