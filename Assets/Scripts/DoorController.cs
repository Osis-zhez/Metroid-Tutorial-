using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private float distanceToOpen;

    private PlayerController thePlayer;

    [SerializeField] string levelToLoad;

    [SerializeField] Transform DoorEntrance;
    [SerializeField] Transform DoorExit;
    
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();

        StartPlayerOnNewLevel();
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            WhatTheDoor();
            StartCoroutine(UseDoorCo());
        }    
    }

    void WhatTheDoor()
    {
        if (gameObject.tag == "Entrance Door")
        {
            PlayerHealthController.instance.isEntranceDoor = true;
            PlayerHealthController.instance.isExitDoor = false;
        }
        else if (gameObject.tag == "Exit Door")
        {
            PlayerHealthController.instance.isEntranceDoor = false;
            PlayerHealthController.instance.isExitDoor = true;
        }
    }

    void StartPlayerOnNewLevel()
    {
        if(PlayerHealthController.instance.isExitDoor == true)
        {
            PlayerHealthController.instance.transform.position = new Vector2(DoorEntrance.position.x + 5f, DoorEntrance.position.y);
        }
        else if(PlayerHealthController.instance.isEntranceDoor == true)
        {
            PlayerHealthController.instance.transform.position = new Vector2(DoorExit.position.x - 5f, DoorExit.position.y);
        }
    }



    IEnumerator UseDoorCo()
    {

        
        yield return new WaitForSeconds(1f);


        PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        
        SceneManager.LoadScene(levelToLoad);
    }







    // изменить бокс коллайдеры, заполнить скрипты
    // изменить теги


}
    

