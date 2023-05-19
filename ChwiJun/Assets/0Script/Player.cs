using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip hitClip;      // 피격 시 재생할 오디오 클립
    public AudioClip coinClip;     // 코인 획득 시 재생할 오디오 클립
    public AudioClip bigCoinClip;  // 빅 코인 획득 시 재생할 오디오 클립
    public AudioClip jumpClip;     // 점프 시 재생할 오디오 클립

    float moveZ = 1.8f;
    float moveTimeZ = 0.1f;
    public bool isMove = false;

    public GameObject[] lifeSprite;

    public float JumpForce = 250f;  // 점프 할 때 가해지는 힘
    int jumpCount = 0;              // 점프 횟수(2단 점프 구현 관련)
    int maxHP = 3;
    public int currentHP;

    public bool isGrounded;                // 바닥에 있는지 확인
    bool isDead;                    // 생사 확인
    bool isDubleJump;               // 이단 점프 가능 유무
    bool isUnbeatTime;              // 무적시간 활성화 유무(피격 판정 관련)


    Rigidbody rb;                   // 플레이어 리지드 바디
    Animator animator;              // 사용할 애니메이더
    AudioSource audioSource;        // 오디오 소스
    Transform tr;

    public GameObject Hit_Effect;
    public GameObject []Effect = new GameObject[2];

    SkinnedMeshRenderer meshs;
    public Material mat1;
    public Material mat2;


    public bool bHit;

    // Start is called before the first frame update
    void Start()
    {
        // 각 변수값 초기화
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        tr = GetComponent<Transform>();
        meshs = GetComponentInChildren<SkinnedMeshRenderer>();


        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // 낙사판정
        if (gameObject.transform.position.y < -2)
        {
            Die();
        }

        //Move();
        Move2();

        // 사망 시 게임 종료
        if (isDead)
        {
            // 게임 종료 장면으로 이동
            GameManager.instance.isGameOver = true;
        }

        Jump();
    }

    void FixedUpdate()
    {
        //rb.AddForce(Vector3.down * 1f);
        // 시간에 따른 점수 추가
        //GameManager.instance.addScore(1);

    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "Hit":
                audioSource.PlayOneShot(hitClip);
                break;
            case "Jump":
                audioSource.PlayOneShot(jumpClip);
                break;
            case "Coin":
                audioSource.PlayOneShot(coinClip);
                break;
            case "BigCoin":
                audioSource.PlayOneShot(bigCoinClip);
                break;
        }
        //audioSource.PlayOneShot();
    }

    void LifeUpdate()
    {
        switch (currentHP)
        {
            case 0:
                lifeSprite[0].SetActive(false);
                lifeSprite[1].SetActive(false);
                lifeSprite[2].SetActive(false);
                break;
            case 1:
                lifeSprite[0].SetActive(false);
                lifeSprite[1].SetActive(false);
                lifeSprite[2].SetActive(true);
                break;
            case 2:
                lifeSprite[0].SetActive(false);
                lifeSprite[1].SetActive(true);
                lifeSprite[2].SetActive(true);
                break;
            case 3:
                lifeSprite[0].SetActive(true);
                lifeSprite[1].SetActive(true);
                lifeSprite[2].SetActive(true);
                break;
        }
    }

    void LifeUp()
    {
        if(currentHP >= maxHP)
        {
            Debug.Log("더 이상 회복 할 수 없습니다.");
            return;
        }

        currentHP++;
        Debug.Log("회복");
        LifeUpdate();
    }

    void MoveToZ(int z)
    {
        if (isMove)
            return;

        // 왼쪽 or 가운데
        if (z > 0 && tr.position.z < moveZ)
        {
            StartCoroutine(OnMoveToZ(z));
        }
        // 오른쪽 or 가운데
        else if (z < 0 && tr.position.z > -moveZ)
        {
            StartCoroutine(OnMoveToZ(z));
        }
    }

    IEnumerator OnMoveToZ(int dir)
    {
        float current = 0;
        float percent = 0;
        float start = tr.position.z;
        float end = tr.position.z + dir * moveZ;

        isMove = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeZ;

            float z = Mathf.Lerp(start, end, percent);
            tr.position = new Vector3(tr.position.x, tr.position.y, z);

            yield return null;
        }
        isMove = false;
    }

    void Move2()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveToZ((int)Mathf.Sign(-1));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveToZ((int)Mathf.Sign(1));
        }

        // 이동거리 제한
        Vector3 currentPosition = tr.position;

        //currentPosition.x = 0;
        currentPosition.z = Mathf.Clamp(tr.position.z, -1.8f, 1.8f);

        tr.position = currentPosition;
    }

    // 캐릭터의 점프
    void Jump()
    {
        // 점프키(Space바)를 누른 경우
        if (Input.GetButtonDown("Jump"))
        {
            // 일반 점프
            if (jumpCount < 1)
            {
                animator.SetBool("Grounded", false);
                isMove = false;
                //isGrounded = false;
                // 점프 횟수 증가
                jumpCount++;
                rb.velocity = Vector3.zero;
                // Y축으로 jumpForce만큼 힘을 가해줌
                rb.AddForce(new Vector3(0, JumpForce, 0));
                // 사운드 재생
                PlaySound("Jump");
            }
            // 이단 점프
            else if (jumpCount == 1 && isDubleJump)
            {
                //if (isGrounded)
                //{
                //    isDubleJump = false;
                //}
                PlaySound("Jump");
                rb.AddForce(new Vector3(0, JumpForce, 0));
                isDubleJump = false;
            }
        }

        // 애니메이터에서 땅에 있는 상태로 변경
        //animator.SetBool("Grounded", isGrounded);

    }

    // 캐릭터의 사망
    void Die()
    {
        animator.SetTrigger("Die");
        //audioSource.clip = deathClip;
        //audioSource.Play();

        isDead = true;
        //gameObject.SetActive(false);
        Time.timeScale = 0f;
        //Destroy(gameObject);
    }


    public void HitEnd()
    {
        Debug.Log("무적 상태 종료");
        bHit = false;
    }
    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        //bHit = true;
        animator.SetTrigger("hit");

        meshs.material = mat2; //모든 재질의 색상 변경

        while (countTime < 3)
        {


            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        isUnbeatTime = false;
        meshs.material = mat1; //모든 재질의 색상 변경

        yield return null;

    }

    // 트리거 콜라이더를 가진 장애물과 충돌을 감지
    private void OnTriggerEnter(Collider other)
    {
        // Obstacle(장애물)과 충돌했고, 아직 사망하지 않았다면
        if (other.CompareTag("Obstacle") && !isDead && !isUnbeatTime)
        {
            currentHP--;
            Instantiate(Hit_Effect, 
                transform.position + new Vector3(0,1.5f,0), tr.rotation);
            PlaySound("Hit");
            LifeUpdate();

            if (currentHP < 1)
            {
                Die();
            }

            // 장애물에 닿았지만 살아 있다면
            isUnbeatTime = true;
            StartCoroutine(UnBeatTime());
        }


        // 코인과 충돌했다면
        if (other.CompareTag("Coin"))
        {
            //audioSource.clip = coinClip
            //audioSource.Play();
            Debug.Log("코인 획득");

            // 점수 획득 코드
            GameManager.instance.addScore(100);
            PlaySound("Coin");

            other.gameObject.SetActive(false);
            Instantiate(Effect[0], transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            //other.gameObject.SetActive(false);
        }

        if (other.CompareTag("BigCoin"))
        {
            //audioSource.clip = coinClip
            //audioSource.Play();
            Debug.Log("왕코인 획득");

            LifeUp();
            // 점수 획득 코드
            GameManager.instance.addScore(5000);
            PlaySound("BigCoin");

            other.gameObject.SetActive(false);
            Instantiate(Effect[1], transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
        }

        // 에너미와 충돌했을 경우
        if (other.gameObject.tag.CompareTo("Enemy") == 0)
        {
            Debug.Log("적에게 공격 받았습니다.");
            currentHP--;

            if (currentHP < 1)
            {
                Die();
            }

            // 장애물에 닿았지만 살아 있다면
            isUnbeatTime = true;
            StartCoroutine(UnBeatTime());
        }

        // 더블 점프 아이템과 충돌했을 경우
        if (other.CompareTag("DoubleJump"))
        {
            // 더블점프 기능 활성화
            isDubleJump = true;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            GameManager.instance.isGameClear = true;
        }


    }

    // 바닥에 닿았을 때 처리
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 표면이 바닥이라면
        if (collision.gameObject.tag.CompareTo("Ground") == 0)
        {
            animator.SetBool("Grounded", true);
            // 땅 위에 존재하는 상태로 변경
            isGrounded = true;
            // 더블 점프 비활성화(일시적으로만 가능하게 만들기 위함)
            isDubleJump = false;
            // 점프 횟수 초기화
            jumpCount = 0;

        }
    }

    // 바닥에서 벗어낫을 때 처리
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }


}