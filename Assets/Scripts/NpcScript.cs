using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Threading;
using System.Threading.Tasks;


public class NpcScript : MonoBehaviour
{   
    private Vector3[] comingpathval = {new Vector3(0.4f,0.1f,0.1f),new Vector3(0.1f,0.1f,-0.6f)};
    private Vector3[] leavingpathval = {new Vector3(0.4f,0.1f,0.1f),new Vector3(-2,0.1f,0.1f)};
    private bool hasLeft = false;
    [SerializeField] GameObject dialogueManager;
    
    // Start is called before the first frame update
    void Start()
    {   
        transform.DOPath(comingpathval, 6);
    }

    async void Update(){
        if (DialogueManager.dialogueEnded){
            DialogueManager.dialogueEnded = false;
            LeavePlace();
        }
        else if(hasLeft && !DialogueTrigger.noMoreConversation){
            hasLeft = false;
            await Task.Delay(6000);
            ResetPlace();
        }
    }

    // Update is called once per frame
    void OnMouseDown(){
        dialogueManager.GetComponent<DialogueTrigger>().ClickOnDoor();
    }

    void LeavePlace(){
        transform.DOPath(leavingpathval, 6);
        hasLeft = true;
    }

    void ResetPlace(){
        transform.position = new Vector3(2,0.1f,0.1f);
        transform.DOPath(comingpathval, 6);
    }
}
