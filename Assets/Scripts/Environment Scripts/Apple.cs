using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private bool _isTaken;

    public Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public bool IsTaken
    {
        get { return _isTaken; }
        set { _isTaken = value; }
    }

}
