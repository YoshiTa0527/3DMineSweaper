using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string m_loadSceneName = null;

    public void LoadScene()
    {
        SceneManager.LoadScene(m_loadSceneName);
    }

}
