using UnityEngine;
using UnityEngine.EventSystems;

// Simple script that resizes the UI on mouseover, mainly used for all of the buttons and the main cookie.
[RequireComponent(typeof(RectTransform))]
public class ResizeUIOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float PointerEnterSize = 1.0f;
    [SerializeField] float PointerExitSize = 1.0f;

    RectTransform _rectTransform;
    bool _pointerEnterFlag;
    public bool PointEnterFlag { get => _pointerEnterFlag; set => _pointerEnterFlag = value; }

    public void OnPointerEnter(PointerEventData eventData) => _pointerEnterFlag = true;
    public void OnPointerExit(PointerEventData eventData) => _pointerEnterFlag = false;

    void Start() => _rectTransform = this.GetComponent<RectTransform>();
    void Update()
    {
        if (Time.timeSinceLevelLoad < 1.0f) { return; }

        if (_pointerEnterFlag)
        {
            if (_rectTransform.localScale.x < PointerEnterSize)
            {
                float newScale = _rectTransform.localScale.x + Time.unscaledDeltaTime;
                _rectTransform.localScale = new Vector2(newScale, newScale);
            }
        }
        else
        {
            if (_rectTransform.localScale.x > PointerExitSize)
            {
                float newScale = _rectTransform.localScale.x - Time.unscaledDeltaTime;
                _rectTransform.localScale = new Vector2(newScale, newScale);
            }
        }
    }
}
