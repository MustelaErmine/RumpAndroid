using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    //Переменные для определения свайпа
    public bool tap, leftSwip, rightSwipe, backSwipe, upSwipe;
    //Переменная для определения был ли совершон свойп по экрану
    public bool isDraging;
    private Vector2 startTap, swipeDelta;

    private void Update()
    {

        //Определение какой свайп был
        
    }
    //Сброс переменных для свайпа
    private void Reset()
    {
        startTap = swipeDelta = Vector2.zero;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startTap = eventData.position;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("end");
        upSwipe = false;
        backSwipe = false;
        tap = false;
        swipeDelta = eventData.position - startTap;
        if (swipeDelta.magnitude > 125)
        {
            if (swipeDelta.y > 0)
                upSwipe = true;
            else
                backSwipe = true;
        }
        else
            tap = true;
    }
}
