using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float StartPosition;
    [SerializeField] private float DayInSeconds;
    float dayTime;
    
    IEnumerator TimeCounter()
    {
        while (true)
        {
            dayTime += Time.deltaTime;
            if (dayTime > DayInSeconds)
            {
                dayTime = 0.1f;
            }

            yield return null;
        }
    }
    void Start()
    {
        dayTime = StartPosition;
        StartCoroutine(TimeCounter());
    }

    void Update()
    {
        float x = 360.0f / DayInSeconds * dayTime;
        transform.rotation = Quaternion.Euler(x, 0, -90f);
    }
}
