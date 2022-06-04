using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AndroidButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    public UnityEvent buttonFunction;

    void Awake()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld) { this.gameObject.SetActive(false); }
    }

    void Update()
    {
        if (buttonPressed) { buttonFunction.Invoke(); }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
