using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectDisappears : MonoBehaviour
{
    [SerializeField, Min(0)]
    float Delay = 0;
    [SerializeField]
    bool DisappearOnStart = true;
    [SerializeField]
    bool DisappearOnCollision = true;
    [SerializeField]
    bool AcceptCollisionWithParentChildrens = false;
    [SerializeField]
    bool IsCanDisappear = true;
    [SerializeField]
    bool DestroyObjectOnDisappearing = true;

    bool isDisappears = false;
    float timer = 0;
    Collider GameObjectCollider;

    void Start()
    {
        GameObjectCollider = gameObject.GetComponent<Collider>();
        if (DisappearOnStart)
        {
            Disappears();
            
        }
    }


    private void Update()
    {
        if (isDisappears)
        {
            timer += Time.deltaTime;
            if (timer + 2 > Delay)
                GameObjectCollider.enabled = false;
            if (timer > Delay && !DestroyObjectOnDisappearing)
                gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (DisappearOnCollision && IsCanDisappear)
        {
            if (AcceptCollisionWithParentChildrens && collision.gameObject.transform.IsChildOf(gameObject.transform.parent))
            {
                Disappears();
            }

            if (!AcceptCollisionWithParentChildrens && !collision.gameObject.transform.IsChildOf(gameObject.transform.parent))
            {
                Disappears();
            }
        }
    }

    public void Disappears() 
    {
        if (IsCanDisappear)
        {
            if (DestroyObjectOnDisappearing)
            {
                Destroy(gameObject, Delay);
            }
            isDisappears = true;
        }
    }
}
