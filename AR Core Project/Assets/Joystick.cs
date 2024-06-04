using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 inputVector;    // �߰�
    private bool isInput;    // �߰�

    [SerializeField]
    private PlayerMovement playerMovement;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);  // �߰�
        isInput = true;    // �߰�
    }

    // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ
    // ������ Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("�巡�� ����");
        
        ControlJoystickLever(eventData);    // �߰�
        isInput = true;    // �߰�
    }

    // �߰�
    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("�巡�� ����");
        lever.anchoredPosition = Vector2.zero;
        playerMovement.move(new Vector2(0f, 0f));
        isInput = false;    // �߰�
    }

    private void InputControlVector()
    {
        playerMovement.move(inputVector);
        // ĳ���Ϳ��� �Էº��͸� ����
    }

    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }
}
