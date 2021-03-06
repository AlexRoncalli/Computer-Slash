using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float damageModifier;
    //public GameObject damageParticles;
    public bool drops;
    public GameObject drop;
    public AudioClip deathSound;
    //public bool canBurn;
    //public float burnDamage;
    //public GameObject burnEffects;

    //bool onfire;
    //float nextBurn;
    //float bunrInterval = 1f;
    //float endBurn;

    float currentHealth;

    //public Slider enemyHealthIndicator;

    AudioSource enemyAS;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
        //enemyHealthIndicator.maxValue = enemyMaxHealth;
        //enemyHealthIndicator.value = currentHealth;
        enemyAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(onFire && Time.time > nextBurn)
        {
            addDamage(burnDamage);
            nextBurn += burnInterval;
        }
        if(onFire && Time.time > endBurn)
        {
            onFire = false;
            burnEffects.SetActive(false);
        }*/
        
    }

    public void addDamage(float damage)
    {
        //enemyHealthIndicator.gameObject.SetActive(true);
        damage = damage * damageModifier;
        if (damage <= 0f) return;
        currentHealth -= damage;
        //enemyHealthIndicator.value = currentHealth;
        enemyAS.Play();
        if (currentHealth <= 0) makeDead();
    }

    /*public void damageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }*/

    /*
    public void addFire()
    {
        if (!canBurn) return;
        onFire = true;
        burnEffect.SetActive(true);
        endBurn = Time.time + burnTime;
        nextBurn = Time.time + burnInterval;
    }*/

    void makeDead()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.15f);

        Destroy(gameObject.transform.root.gameObject);
        if (drops) Instantiate(drop, transform.position, transform.rotation);
    }
}
