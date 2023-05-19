using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject[] EndingImg = new GameObject[5];
    //Image EndingBG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.stageLevel > 1)
        {
            if (GameManager.score < 60000)
            {
                EndingImg[0].SetActive(false);
                EndingImg[1].SetActive(true);
                EndingImg[2].SetActive(false);
                EndingImg[3].SetActive(false);
                EndingImg[4].SetActive(false);
                // "알바생 엔딩...";
            }
            else if (GameManager.score < 90000)
            {
                EndingImg[0].SetActive(false);
                EndingImg[1].SetActive(false);
                EndingImg[2].SetActive(true);
                EndingImg[3].SetActive(false);
                EndingImg[4].SetActive(false);
                // "중소기업 취업 성공";
            }
            else if (GameManager.score < 100000)
            {
                EndingImg[0].SetActive(false);
                EndingImg[1].SetActive(false);
                EndingImg[2].SetActive(false);
                EndingImg[3].SetActive(true);
                EndingImg[4].SetActive(false);
                //"대기업 취업 성공";
            }
            else
            {
                EndingImg[0].SetActive(false);
                EndingImg[1].SetActive(false);
                EndingImg[2].SetActive(false);
                EndingImg[3].SetActive(false);
                EndingImg[4].SetActive(true);
                // "억만장자!!!!";
            }
            //AudioManager.Instance.BgmStop();
        }
        else
        {
            EndingImg[0].SetActive(true);
            EndingImg[1].SetActive(false);
            EndingImg[2].SetActive(false);
            EndingImg[3].SetActive(false);
            EndingImg[4].SetActive(false);
            // 백수 엔딩
        }
    }
}
