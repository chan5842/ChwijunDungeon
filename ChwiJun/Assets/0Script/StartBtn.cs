using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HowTo()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void LoadScene()
    {
        //AudioManager.Instance.BgmPlay();
        SceneManager.LoadScene(1);
        
    }
}
