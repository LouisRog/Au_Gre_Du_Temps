using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    // Start is called before the first frame update

    List<String> state = DialogueManager.stateOfTheGame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state = DialogueManager.stateOfTheGame;
    }
}
