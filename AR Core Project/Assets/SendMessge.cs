using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendMessge : MonoBehaviour
{
    //싱글톤 변수, Instance를 통해 외부 컴포넌트에서 접근 가능
    private static SendMessge m_instance = null;
    public static SendMessge Instance { 
        get {
            if (m_instance == null) return null;
            else return m_instance; 
        } 
    }

    public TextMeshProUGUI textProGUI;

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


    void Update()
    {
        if (isMessge)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / 2.0f); // 2초 동안 투명도 조절
            textProGUI.alpha = alpha;

            if (timer <= 0)
            {
                isMessge = false;
                textProGUI.alpha = 0; // 2초 후 완전히 투명하게 설정
            }
        }   
    }

    public void ShowMessage(string messege)
    {
        textProGUI.text = messege;
        isMessge = true;
        timer = time;
        textProGUI.alpha = 1;
    }
}
