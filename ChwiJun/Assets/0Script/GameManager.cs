using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // 싱글턴을 할당할 변수
    public static GameManager instance;

    [SerializeField]
    static int stageLevel = 1;

    public GameObject menuPanel; // esc로 불러올 메뉴
    public GameObject gamePanel; // 게임 인터페이스
    public GameObject resultPanel;  // 게임 결과 UI
    public GameObject gameOverPanel;  // 게임 오버 UI
    public RectTransform uiGroup; // esc 메뉴 트렌스폼

    public bool isGameOver = false;
    public bool isGameClear = false;

    // Score의 Text
    public Text Score_text;
    // 결과 점수 Text
    public Text ResultScore_Text;
    // 최고 점수 Text
    public Text BestScore_Text;

    // 결과 점수 Text
    public Text ResultScore_Text2;
    // 최고 점수 Text
    public Text BestScore_Text2;

    public Text GameOverText;
    public Text GameClearText;

    // 현재 게임 점수
    public static int score = 0;

    // 최고 5위까지 점수 배열에 저장
    int[] bestScore = new int[3];

    private void Awake()
    {
        // 싱글턴 변수가 비어있다면 자기 자신을 할당
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("두개의 매니저가 존재합니다.");
            Destroy(gameObject);
        }

        AudioManager.Instance.BgmPlay();
        
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Score_text.text = score.ToString();
        //AudioManager.Instance.BgmPlay();
        
        //BestScore_Text.text = PlayerPrefs.GetInt(0 + "BestScore").ToString();
        //Debug.Log(PlayerPrefs.GetInt("BestScore"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameClear)
        {
            resultPanel.SetActive(true);
            //Time.timeScale = 0;

            ResultScore_Text.text = score.ToString();

            ScoreSave(score);

            //if(score > PlayerPrefs.GetInt("BestScore"))
            //{
            //    PlayerPrefs.SetInt("BestScore", score);
            //}
            BestScore_Text.text = PlayerPrefs.GetInt(0 + "BestScore").ToString();

            if (stageLevel > 1)
            {
                if (score < 60000)
                {
                    GameClearText.text = "알바생 엔딩...";
                }
                else if (score < 90000)
                {
                    GameClearText.text = "중소기업 취업 성공";
                }
                else if (score < 100000)
                {
                    GameClearText.text = "대기업 취업 성공";
                }
                else
                {
                    GameClearText.text = "억만장자!!!!";
                }
                AudioManager.Instance.BgmStop();
            }
        }
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            if(stageLevel > 1)
            { 
                if(score < 60000)
                {
                    GameOverText.text = "알바생 엔딩...";
                }
                else if(score < 90000)
                {
                    GameOverText.text = "중소기업 취업 성공";
                }
                else if(score < 100000)
                {
                    GameOverText.text = "대기업 취업 성공";
                }
                else
                {
                    GameOverText.text = "억만장자!!!!";
                }
            }
           
            ResultScore_Text2.text = score.ToString();
            ScoreSave(score);
            BestScore_Text2.text = PlayerPrefs.GetInt(0 + "BestScore").ToString();
            AudioManager.Instance.BgmStop();

        }

        if (Input.GetButtonDown("Cancel")) // esc 메뉴
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0;
            AudioManager.Instance.BgmPause();
            
        }
    }

    // 스코어 증가 함수
    public void addScore(int newScore)
    {
        score += newScore;
        Score_text.text = score.ToString();
    }

    // 타이틀 화면으로
    public void LoadScene()
    {
        Time.timeScale = 1;
        stageLevel = 1;
        score = 0;
        SceneManager.LoadScene("StartScene");
        AudioManager.Instance.BgmStop();
    }

    // 게임 화면으로
    public void MenuClosed()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.BgmPlay();
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        stageLevel++;
        SceneManager.LoadScene(2);
    }

    public void ClearScene()
    {
        Time.timeScale = 1;
        stageLevel = 1;
        score = 0;
        SceneManager.LoadScene(0);
    }

    public void Retry()

    {
        Time.timeScale = 1;
        stageLevel = 1;
        score = 0;
        
        SceneManager.LoadScene(1);
        AudioManager.Instance.BgmPlay();
    }

    // 점수 저장
    void ScoreSave(int currentScore)
    {
        // 임시로 점수를 저장할 변수
        int tempScore = 0;
        // 임시로 이름을 저장할 변수

        for (int i = 0; i < 3; i++)
        {
            // 저장된 최고 점수와 이름 가져오기
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");

            // 현재 점수가 3위 안에 드는 경우
            while (bestScore[i] < currentScore)
            {
                // 자리 바꾸기
                tempScore = bestScore[i];
                bestScore[i] = currentScore;

                // 랭킹에 저장
                PlayerPrefs.SetInt(i + "BestScore", currentScore);

                // 다음 반복 준비
                currentScore = tempScore;
            }
        }

        // 랭킹에 재 저장
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
        }
    }

    void ScoreLoad()
    {

    }
}