using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    public static RespawnController instance;

    private Vector3 respawnPoint;
    [SerializeField] private float waitToRespawn;

    [SerializeField] GameObject player;

    [SerializeField] GameObject deathVFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        player = PlayerHealthController.instance.gameObject;

        respawnPoint = player.transform.position;
    }

    
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        player.SetActive(false);

        Instantiate(deathVFX, player.transform.position, player.transform.rotation);

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        player.transform.position = respawnPoint;
        player.SetActive(true);

        PlayerHealthController.instance.FillHealth();
    }


}
