using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FireHpBar : MonoBehaviour
{
    [Header("HP Settings")]
    public Image HpBar;
    [Range(0, 1000)]
    public float startHP = 10;
    public float burnMultiplier = 1;
    public string animationID;
    public int totalLifes = 1;
    private bool respawn = false;

    [Header("Animation Settings")]
    public Animator fireMoveAnimator;
    public Image fireImage;
    public UnityEvent onPlayerIsDeadNoMoreLifes;
    public UnityEvent onPlayerRespawn;

    [Header("Destroy Animation")]
    public GameObject character;
    public Animator characterAnimator;
    public string characterAnimationID;
    public float respawnTime;
    float timePassedToRespawn;
    public Vector2 respawnPosition;


    private bool playerIsDead = false;
    private float actualHP = 10;

    public void Start()
    {
        actualHP = startHP;
        if(fireMoveAnimator) fireMoveAnimator.speed = 0f;
    }

    public void Update()
    {
        if (playerIsDead) return;
        Burn();
        Respawn();
    }

    private void Respawn()
    {
        if (!respawn) return;
        timePassedToRespawn -= Time.deltaTime;
        if (timePassedToRespawn <= 0)
        {
            onPlayerRespawn?.Invoke();
            fireImage.gameObject.SetActive(true);
            respawn = false;
            if(character != null) character.transform.position = (Vector3)respawnPosition + new Vector3(0,0, character.transform.position.z);
            actualHP = startHP;
            if(characterAnimator != null) characterAnimator.SetBool(characterAnimationID, false);
        }
    }

    private void Burn()
    {
        if (respawn) return;
        actualHP -= Time.deltaTime * burnMultiplier;
        
        fireMoveAnimator.Play(animationID, 0, 1 - (actualHP/startHP));
        HpBar.fillAmount = (actualHP / startHP);
        if (actualHP <= 0)
        {
            totalLifes -= 1;
            
            if(totalLifes <= 0)
            {
                playerIsDead = true;
                fireMoveAnimator.StopPlayback();
                fireImage.gameObject.SetActive(false);
                onPlayerIsDeadNoMoreLifes?.Invoke();
            }
            else
            {
                fireImage.gameObject.SetActive(false);
                if (characterAnimator != null) characterAnimator.SetBool(characterAnimationID, true);
                timePassedToRespawn = respawnTime;
                respawn = true;
            }
        }
    }

    private void ModifyHealth(float amount)
    {
        actualHP += amount;
        actualHP = actualHP > startHP ? startHP : actualHP;
        actualHP = actualHP < 0 ? 0 : actualHP;
    }
}
