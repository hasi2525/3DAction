using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField]
    private TriggerEvent onTriggerEnter = new TriggerEvent();

    [SerializeField]
    private TriggerEvent onTriggerStay = new TriggerEvent();

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }
    private void OnTriggerStay(Collider collider)
    {
        onTriggerStay.Invoke(collider);
    }

    [Serializable] 
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}