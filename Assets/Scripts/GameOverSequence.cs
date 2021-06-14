using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverSequence : EventSubscriber
{
    Sequence m_seq;

    public override void OnGameOver()
    {
        m_seq.Play();
    }

    private void BuildSequence()
    {
       
    }
}
