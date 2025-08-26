using System;
using UnityEngine;

public class FollowSocket : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform socket;
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!socket) {return;}
        transform.position = socket.position;
        transform.rotation = socket.rotation;
    }
}
