using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    // 기존 변수들
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;


    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    // 직접 추가한 변수들
    public Transform playerSpawn;
    public GameObject lives_ui;
    public ItemScript itemScript;

    // 키 아이템 활성화
    private bool m_item = false;

    // 목숨 3개
    private int m_lives = 3;

    // 피격 시 무적 2초
    private bool isInvincible = false;
    private float invincible_timer = -1f;


    // 아이템 컴포넌트에서 플레이어와 아이템이 트리거되면 m_item을 true로 설정
    public void GetItem() => m_item = true;
    void OnTriggerEnter (Collider other)
    {
        // && m_item을 추가하여 아이템이 없으면 탈출하지 못하게끔
        if (other.gameObject == player && m_item)
        {
            m_IsPlayerAtExit = true;
        }
    }


    // 기존에는 한번 잡히면 게임 종료 -> 3번 잡혀야 게임 종료하게끔 설정
    // 무적시간을 추가하여 피격시 invincible_timer가 2초가 되고, update에서 0초 미만이 되면 다시 피격이 가능하게 활성화
    public void CaughtPlayer ()
    {
        // 피격 시 2초 무적
        if (isInvincible)
        {
            return;
        }
        invincible_timer = 2f;

        // 텍스트 알림
        SendMessge.Instance.ShowMessage("lose one life...");

        // 목숨 UI에 하나가 줄어든다
        GameObject live = lives_ui.transform.Find("Lives" + m_lives).gameObject;
        live.SetActive(false);


        // 목숨이 0이하면 게임종료
        if (--m_lives <= 0)
        {
            m_IsPlayerCaught = true;
        }
    }


    void Update ()
    {
        // invincible_timer가 0초 이상이면 Time.deltaTime을 빼준다
        if (invincible_timer >= 0f)
        {
            isInvincible = true;
            invincible_timer -= Time.deltaTime;
        }
        // 0초 미만이면 피격이 가능하게 활성화
        else
        {
            isInvincible = false;
        }

        // 기존 코드로 탈출 혹은 잡히는 경우를 지속적으로 검사
        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            // 기존에는 잡히면 씬을 다시 로드하였음 -> 로드하지 않고 각종 변수, 설정, 위치을 초기화하는방식으로 제작 (다시 로드하면 오류나서 이렇게 구현)
            if (doRestart)
            {
                imageCanvasGroup.alpha = 0f;
                Restart();
            }
            else
            {
                SendMessge.Instance.ShowMessage("Game Clear!!!");
                Application.Quit ();
            }
        }
    }

    private void Restart()
    {
        SendMessge.Instance.ShowMessage("Game Over....");

        // 플레이어 시작 위치로 초기화
        player.transform.localPosition = playerSpawn.localPosition;

        // 게임 상태 초기화
        m_IsPlayerCaught = false;
        m_HasAudioPlayed = false;

        // 목숨, 아이템 획득 상태, 타이머 초기화
        m_Timer = 0f;
        m_lives = 3;
        m_item = false;

        // 피격 상태 초기화
        isInvincible = false;
        invincible_timer = -1f;

        // Key 아이템 초기화
        itemScript.Respawn();

        // 목숨 UI 초기화
        foreach (Transform live in lives_ui.transform)
            live.gameObject.SetActive(true);

    }
}
