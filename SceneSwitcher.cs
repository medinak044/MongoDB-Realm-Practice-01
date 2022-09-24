using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public enum SceneEnum
{
    MainMenu = 0, CharacterDialogue = 1
}

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;

    public SceneEnum scene;

    void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(this); } else { Destroy(gameObject); }
    }

    [Button("Change Scene")]
    void Test_ChangeScene()
    {
        SceneManager.LoadScene(scene.ToString()); // Loads Scene according to name
    }
}

