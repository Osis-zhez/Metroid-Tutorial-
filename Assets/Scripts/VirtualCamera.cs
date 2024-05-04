using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    private PlayerHealthController player;

    private CinemachineVirtualCamera cam;

    void Start()
    {
        player = PlayerHealthController.instance;
        cam = this.GetComponent<CinemachineVirtualCamera>();
        cam.Follow = player.transform;

        AudioManager.Instance.PlayLevelMusic();
    }

    
    void Update()
    {
        
    }
}
