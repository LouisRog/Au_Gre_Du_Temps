using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;


public class NpcScript : MonoBehaviour
{   
    private Vector3[] pathval = {new Vector3(0.4f,0.1f,0.1f),new Vector3(0.1f,0.1f,-0.6f)};
    DialogueTrigger dialogue;
    // Start is called before the first frame update
    void Start()
    {   
        transform.DOPath(pathval, 10);
    }

    // Update is called once per frame
    void OnMouseDown(){
        dialogue.ClickOnDoor();
    }
}
