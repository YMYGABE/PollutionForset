using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform Player;
    public float smooth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != Player.position)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position, smooth);
        }
    }
}
