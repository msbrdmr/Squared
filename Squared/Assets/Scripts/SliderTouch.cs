using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SliderTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerUpEvent;
    public UnityEvent<float> OnPointerDragEvent;
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        slider.value = (slider.minValue+slider.maxValue)/2;
    }
    public void OnPointerDown(PointerEventData eventdata)
    {
        if (OnPointerDownEvent != null)
            OnPointerDownEvent.Invoke();

        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(slider.value);
        }
    }
    public void OnPointerUp(PointerEventData eventdata)
    {
        if (OnPointerUpEvent != null)
        {
            OnPointerUpEvent.Invoke();
        }
    }
    public void OnSliderValueChanged(float v)
    {
        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(v);
        }
    }
}
