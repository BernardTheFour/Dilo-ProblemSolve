using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameplayButton : MonoBehaviour
{
    [SerializeField] private string SceneName;

    private Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        myButton.onClick.AddListener(() => OpenScene());
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
 }
