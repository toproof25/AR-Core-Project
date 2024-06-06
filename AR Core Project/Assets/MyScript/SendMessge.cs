using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendMessge : MonoBehaviour
{
    //싱글톤 변수
    private static SendMessge m_instance = null;
    public static SendMessge Instance { 
        get {
            if (m_instance == null) return null;
            else return m_instance; 
        } 
    }

    public TextMeshProUGUI textProGUI;

    // 5초간 메시지 뜨고 사라짐
    private const float time = 5f;
    private float timer;
    private bool isMessge = false;

    private void Awake()
    {
        // 싱글톤 변수 설정
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // 메시지가 오면 time초 동안 서서히 사라짐(투명도 조절)
    void Update()
    {
        if (isMessge)
        {
            // 투명도 조절
            timer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / time); 
            textProGUI.alpha = alpha;

            if (timer <= 0)
            {
                isMessge = false;
                textProGUI.alpha = 0;
            }
        }   
    }

    // 다른 컴포넌트에서 싱글톤으로 해당 함수 피라미터로 메시지를 보내어 실행
    public void ShowMessage(string messege)
    {
        textProGUI.text = messege;
        isMessge = true;
        timer = time;
        textProGUI.alpha = 1;
    }
}
