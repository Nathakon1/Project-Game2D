using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsPanel: MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
