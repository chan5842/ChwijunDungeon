using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnSet : MonoBehaviour
{

    bool esc;
    public GameObject menuPanel;
    public GameObject gamePanel;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        esc = Input.GetButtonDown("Cancel");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void MenuLoad()
    {
        if (esc)
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void MenuClosed()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
