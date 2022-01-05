using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class CarSpeed : MonoBehaviour
{
    TextMesh TextMesh;

    Vector3 priviosPosition;
    Vector3 currentPosition;

    [SerializeField, Min(0)]
    float MaxSpeed;
    [SerializeField]
    float updateFrequency = 1f;
    float currentSpeed;

    public IEnumerator SpeedCalculater()
    {
        while (true)
        {
            priviosPosition = gameObject.transform.position;
            //
            yield return new WaitForSeconds(updateFrequency);
            //
            currentPosition = gameObject.transform.position;
            currentSpeed = CalculateCarSpeed(priviosPosition, currentPosition);
            if (TextMesh)
            {
                TextMesh.text = currentSpeed.ToString();
            }
        }
        yield return null;
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
        speed = Vector3.Distance(start, finish) / updateFrequency;
        //Debug.Log(speed * 3.6f / gameObject.transform.lossyScale.x);
        return speed * 3.6f / gameObject.transform.lossyScale.x;
    }

    public float CalculateMotorTorque(float motorForce) 
    {
        float motorTorque = MaxSpeed / 100 * currentSpeed;
        Debug.Log($"motorTorque: {motorTorque}, force: {motorForce / motorTorque}");
        return motorForce / motorTorque;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    public float GetMaxSpeed()
    {
        return MaxSpeed;
    }
}
