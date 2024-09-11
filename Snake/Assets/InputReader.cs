using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReader : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector2 startDrag;
    public void OnPointerDown(PointerEventData eventData)
    {
        startDrag = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 endDrag = eventData.position;
        Vector2 swipeDelta = endDrag - startDrag;

        if (swipeDelta.magnitude < 50)
        {
            return;
        }

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0)
            {
                SnakeMovement.instance.HandleInput(Vector2Int.right);
            }
            else
            {
                SnakeMovement.instance.HandleInput(Vector2Int.left);
            }
        }
        else
        {
            if (swipeDelta.y > 0)
            {
                SnakeMovement.instance.HandleInput(Vector2Int.up);
            }
            else
            {
                SnakeMovement.instance.HandleInput(Vector2Int.down);
            }
        }

    }
}
