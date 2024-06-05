using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
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

    private bool m_item = false;
    private int m_lives = 3;

    private bool isInvincible = false;
    private float invincible_timer = -1f;


    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player && m_item)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void GetItem() => m_item = true;
 

    public void CaughtPlayer ()
    {
        // 피격 시 2초 무적
        if (isInvincible)
        {
            return;
        }

        invincible_timer = 2f;
        SendMessge.Instance.ShowMessage("lose one life...");

        // 목숨 UI에 하나가 줄어든다
        GameObject live = lives_ui.transform.Find("Lives" + m_lives).gameObject;
        live.SetActive(false);

        //Debug.Log("아야아야ㅏ양");

        m_lives -= 1;

        // 목숨이 0이하면 게임종료
        if (m_lives <= 0)
        {
            m_IsPlayerCaught = true;
        }
    }

    void Update ()
    {
        if (invincible_timer >= 0f)
        {
            isInvincible = true;
            invincible_timer -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }

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
