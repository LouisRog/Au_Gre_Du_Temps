using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controls : MonoBehaviour
{
    float HorizontalMove;
    [SerializeField] public float speed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        HorizontalMove = Input.GetAxis("Horizontal");
        transform.Rotate(0,-HorizontalMove*speed*Time.deltaTime,0);
    }
}
