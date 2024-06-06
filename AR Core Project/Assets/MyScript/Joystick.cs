using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // �� UI�� ������
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    // UI�� �̵� ���� ����
    [SerializeField, Range(10f, 150f)]
    private float leverRange;


    private Vector2 inputVector; 
    private bool isInput;

    // �÷��̾� �̵� ��ũ��Ʈ ��������
    [SerializeField]
    private PlayerMovement playerMovement;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // �巡�� ���� �̺�Ʈ
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData); 
        isInput = true;  
    }

    // �巡�� �ϴ� �̺�Ʈ  
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);  
        isInput = true;  
    }

 
    // �̵� ��� �Լ�
    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    // �巡�� ������ �̵� ���߱�
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        playerMovement.move(new Vector2(0f, 0f));
        isInput = false;    // �߰�
    }

    // �巡�� Ȱ��ȭ �� InputControlVector����
    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }

    // �÷��̾� �̵� �Լ��� ������ move�� ���� ����
    private void InputControlVector()
    {
        playerMovement.move(inputVector);
        // ĳ���Ϳ��� �Էº��͸� ����
    }
}
