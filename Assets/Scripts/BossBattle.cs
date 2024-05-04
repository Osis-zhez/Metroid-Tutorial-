using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossBattle : MonoBehaviour
{
    private Camera mainCamera;
    private VirtualCamera virtualCamera;
    [SerializeField] private Transform bossCameraPoint;
    [SerializeField] private float camSpeed;

    [SerializeField] int threshold1;

    [SerializeField] float activeTime, fadeOutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;

    [SerializeField] Transform[] spawnPoints;
    private Transform targerPoint;
    [SerializeField] float moveSpeed;

    [SerializeField] Animator anim;

    [SerializeField] Transform boss;
    
    [SerializeField] float timeBetweenShot, timeBetweenShot2;
    private float shotCounter;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotPoint;

    [SerializeField] GameObject winObject;
    bool battleEnded = false;


    void Start()
    {
        mainCamera = Camera.main;
        virtualCamera = FindObjectOfType<VirtualCamera>();

        virtualCamera.gameObject.SetActive(false);

        AudioManager.Instance.PlayBossMusic();

        activeCounter = activeTime;

        shotCounter = timeBetweenShot;
    }

    
    void Update()
    {
        BossCameraOn();
        BattleController();
        WinObjectActivator();
    }

    private void BattleController()
    {
        if(battleEnded == true) { return; }
        if (BossHealthController.Instance.currentHealth > threshold1)
        {
            if (activeCounter > 0) // Счетчик активности босса, когда он видим.
            {
                activeCounter -= Time.deltaTime;
                if (activeCounter <= 0)
                {
                    fadeCounter = fadeOutTime; // включается счетчки фазы исчезноваения 
                    anim.SetTrigger("vanish");
                }

                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShot;

                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                }


            }
            else if (fadeCounter > 0)//фаза исчезноваения
            {
                fadeCounter -= Time.deltaTime;
                if (fadeCounter <= 0)
                {
                    boss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter > 0)// фаза невидимости
            {
                inactiveCounter -= Time.deltaTime;
                if (inactiveCounter <= 0)
                {
                    boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    boss.gameObject.SetActive(true);

                    activeCounter = activeTime;
                }
            }
        }

        if (BossHealthController.Instance.currentHealth <= threshold1) // вторая фаза сражения с босом
        {
            if (activeCounter > 0)
            {
                activeCounter -= Time.deltaTime;
                if (activeCounter <= 0)
                {
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");
                }

                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShot2;

                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                }


            }
            else if (fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;
                if (fadeCounter <= 0)
                {
                    boss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter > 0)
            {
                inactiveCounter -= Time.deltaTime;
                if (inactiveCounter <= 0)
                {
                    boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    boss.gameObject.SetActive(true);

                    activeCounter = activeTime;
                }
            }
        }
    }

    private void BossCameraOn()
    {
        if(battleEnded == true) { return; }
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position,
            bossCameraPoint.position, camSpeed * Time.deltaTime);
    }


    public void EndBattle()
    {
        battleEnded = true;

        fadeCounter = fadeOutTime;
        anim.SetTrigger("vanish");
        boss.GetComponent<Collider2D>().enabled = false;
        virtualCamera.gameObject.SetActive(true);
        
    }

    void WinObjectActivator()
    {
        if (battleEnded == true)
        {
            fadeCounter -= Time.deltaTime;
            if(fadeCounter <= 0)
            {
                if(winObject != null)
                {
                    winObject.SetActive(true);
                    winObject.transform.SetParent(null);
                }
                gameObject.SetActive(false);

                AudioManager.Instance.PlayLevelMusic();
            }
            
        }
    }
}


    //Создать отдельный метод босс батл
    //Создать переменную батленд и winGameObject
    //Создать метод EndBattle и запустить из BossHealth
    // Вызвать энбтатл из босхилс
