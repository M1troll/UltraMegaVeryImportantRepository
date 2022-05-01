using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]    
    private float currentValue;

    [SerializeField] private UnityEvent onDestroyObstacle;

    void GetDamage(float value)
    {
        currentValue -= value;
    }
    void Destroy()
    {
        Destroy(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentValue = 1;
        onDestroyObstacle.AddListener(Destroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.white, currentValue);
        if (currentValue == 0)
        {
            onDestroyObstacle.Invoke();
        }
    }
}
