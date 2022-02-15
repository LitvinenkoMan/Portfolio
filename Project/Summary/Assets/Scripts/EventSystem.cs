using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour, IPublisher
{
    private List<ISubscriber> Subscribers;
    public void Notify()
    {
        
    }

    public void Subscribe(ISubscriber subscriber)
    {
        Subscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
        bool SubscriberExist = false;
        foreach (var item in Subscribers)
        {
            if (item == subscriber)
            {
                SubscriberExist = true;
            }
        }
        if (SubscriberExist)
        {
            Subscribers.Remove(subscriber);
        }
    }
}
