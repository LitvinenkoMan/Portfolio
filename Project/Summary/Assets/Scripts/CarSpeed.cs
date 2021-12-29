using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class CarSpeed : MonoBehaviour
{
    [SerializeField]
    CarMovment carMovment;

    TextMesh TextMesh;

    Vector3 priviosPosition;
    Vector3 currentPosition;

    [SerializeField, Min(0)]
    float MaxSpeed;
    [SerializeField]
    float updateFrequency = 1f;
    float CurrentSpeed;

    public IEnumerator SpeedCalculater()
    {
        while (true)
        {
            priviosPosition = gameObject.transform.position;
            //
            yield return new WaitForSeconds(updateFrequency);
            //
            currentPosition = gameObject.transform.position;
            CurrentSpeed = CalculateCarSpeed(priviosPosition, currentPosition);
            if (TextMesh)
            {
                TextMesh.text = CurrentSpeed.ToString();
            }
        }
        yield return null;
    }

    private void FixedUpdate() {

        if (carMovment)
        {
            if (CurrentSpeed > MaxSpeed && carMovment.IsCarCanMove())
            {
                carMovment.StopMovment();
            }
            else if (CurrentSpeed < MaxSpeed && !carMovment.IsCarCanMove())
            {
                carMovment.StartMovment();
            }                
        }
    }

    private void Start()
    {
        priviosPosition = gameObject.transform.position;
        currentPosition = gameObject.transform.position;
        StartCoroutine(SpeedCalculater());
    }

    public float CalculateCarSpeed(Vector3 start, Vector3 finish)
    {
        float speed = 0;
        speed = Vector3.Distance(start, finish) * updateFrequency;
        Debug.Log(speed * 3.6f);
        return speed * 3.6f;
    }
    public float GetCurrentSpeed()
    {
        return CurrentSpeed;
    }
    public float GetMaxSpeed()
    {
        return MaxSpeed;
    }
}
