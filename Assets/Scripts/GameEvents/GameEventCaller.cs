using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventCaller<T> : MonoBehaviour
{
    [SerializeField] private T _value;
    [SerializeField] private GameEvent<T> _event;

    private void Start()
    {
        _event.Invoke(_value);
    }
}