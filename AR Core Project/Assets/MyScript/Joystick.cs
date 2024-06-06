using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // 각 UI를 가져옴
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    // UI의 이동 범위 설정
    [SerializeField, Range(10f, 150f)]
    private float leverRange;


    private Vector2 inputVector; 
    private bool isInput;

    // 플레이어 이동 스크립트 가져오기
    [SerializeField]
    private PlayerMovement playerMovement;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // 드래그 시작 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData); 
        isInput = true;  
    }

    // 드래그 하는 이벤트  
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);  
        isInput = true;  
    }

 
    // 이동 계산 함수
    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    // 드래그 끝나면 이동 멈추기
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        playerMovement.move(new Vector2(0f, 0f));
        isInput = false;    // 추가
    }

    // 드래그 활성화 시 InputControlVector실행
    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }

    // 플레이어 이동 함수에 제작한 move에 백터 전달
    private void InputControlVector()
    {
        playerMovement.move(inputVector);
        // 캐릭터에게 입력벡터를 전달
    }
}
