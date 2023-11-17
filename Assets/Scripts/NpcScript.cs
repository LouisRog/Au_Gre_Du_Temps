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
    private bool isArrived = false;

    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] GameObject dialogueManager;
    [SerializeField] GameObject interrogationPoint;

    // Start is called before the first frame update
    void Start()
    {   
        transform.DOPath(comingpathval, 6).OnComplete(IsArrived);
        interrogationPoint.SetActive(false);
        interrogationPoint.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
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
        interrogationPoint.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void IsArrived()
    {
        isArrived = true;
        interrogationPoint.SetActive(true);

        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip foundClip = Resources.Load<AudioClip>("Audio/sonette");
        if (foundClip != null)
        {
            audioSource.clip = foundClip;
            audioSource.Play();
        }
    }
    // Update is called once per frame
    void OnMouseDown(){
        if (isArrived)
        {
            interrogationPoint.SetActive(false);
            dialogueManager.GetComponent<DialogueTrigger>().ClickOnDoor();
        }
    }

    void LeavePlace(){
        isArrived = false;
        transform.DOPath(leavingpathval, 6);
        hasLeft = true;
    }

    void ResetPlace(){
        transform.position = new Vector3(2,0.1f,0.1f);
        transform.DOPath(comingpathval, 6).OnComplete(IsArrived);
    }
}
