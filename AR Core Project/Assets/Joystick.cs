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

    private Vector2 inputVector;    // 추가
    private bool isInput;    // 추가

    [SerializeField]
    private PlayerMovement playerMovement;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);  // 추가
        isInput = true;    // 추가
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("드래그 시작");
        
        ControlJoystickLever(eventData);    // 추가
        isInput = true;    // 추가
    }

    // 추가
    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("드래그 종료");
        lever.anchoredPosition = Vector2.zero;
        playerMovement.move(new Vector2(0f, 0f));
        isInput = false;    // 추가
    }

    private void InputControlVector()
    {
        playerMovement.move(inputVector);
        // 캐릭터에게 입력벡터를 전달
    }

    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }
}
