using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendMessge : MonoBehaviour
{
    //�̱��� ����
    private static SendMessge m_instance = null;
    public static SendMessge Instance { 
        get {
            if (m_instance == null) return null;
            else return m_instance; 
        } 
    }

    public TextMeshProUGUI textProGUI;

    // 5�ʰ� �޽��� �߰� �����
    private const float time = 5f;
    private float timer;
    private bool isMessge = false;

    private void Awake()
    {
        // �̱��� ���� ����
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // �޽����� ���� time�� ���� ������ �����(���� ����)
    void Update()
    {
        if (isMessge)
        {
            // ���� ����
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

    // �ٸ� ������Ʈ���� �̱������� �ش� �Լ� �Ƕ���ͷ� �޽����� ������ ����
    public void ShowMessage(string messege)
    {
        textProGUI.text = messege;
        isMessge = true;
        timer = time;
        textProGUI.alpha = 1;
    }
}
