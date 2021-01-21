using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAddOn : MonoBehaviour
{
    [SerializeField]
    Transform Cube;
    float rotateSpeed = 20f;
    bool shouldMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.RotateAround(Cube.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        shouldMove = true; 
    }
}
