using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveBox : MonoBehaviour
{
    [SerializeField]
    private InteractiveBox next;    // Цель

    private Transform myTransform;
    private LineRenderer myLineRenderer;
    private const float rayDistance = 100;  // Дистанция

    private void Start()
    {
        myTransform = GetComponent<Transform>();
        myLineRenderer = GetComponent<LineRenderer>();
    }

    public void AddNext(InteractiveBox box)
    {
        // Назначаем цель
        next = box;
    }

    public void ActivateModule()
    {
        /*GetComponentInChildren<UsingParentScript>().Use();*/
    }

    private void FixedUpdate()
    {
        if (next)
        {
            // Отправляем луч
            if (Physics.Raycast(myTransform.position, next.transform.position - myTransform.position, out RaycastHit hit, rayDistance))
            {
                // Рисуем луч
                Debug.DrawLine(myTransform.position, hit.point, Color.magenta, 0.3f);
                DrawInGameMode(hit);

                // Даем по щам
                if (hit.collider.tag == "NEXT")
                {
                    hit.transform.GetComponent<NewBehaviourScript>().onDestroyObstacle.AddListener(() => ActivateModule());
                    hit.transform.GetComponent<NewBehaviourScript>().GetDamage(Time.deltaTime);
                }
            }
        }
        else
        {
            // Стираем луч
            myLineRenderer.positionCount = 0;
        }
    }

    private void DrawInGameMode(RaycastHit hit)
    {
        // Если несколько столкновений
        if (myLineRenderer.positionCount != 2)
        {
            myLineRenderer.positionCount = 2;
        }
        
        myLineRenderer.SetPosition(0, myTransform.position);
        myLineRenderer.SetPosition(1, hit.point);
    }
}
