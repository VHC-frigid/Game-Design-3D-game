using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;
    
    public float bigdamage = 2f;
    public float smalldamage = 1f;

    public float fireRate = 1;
    private float nextTimeToFire;

    public LayerMask enemyLayerMask;
    public LayerMask raycastLayerMask;
    
    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;

    public Animator fire;
    private bool gunFire;
    
    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
            gunFire = true;
        }
        else
        {
            gunFire = false;
        }
        fire.SetBool("GunShoot", gunFire);
        fire.SetBool("Shoot", gunFire);
    }

    void Fire()
    {

        //simulate gun shot radius
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        //alert any enemy in range
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }
        
        //not final just testing audio but plays test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        
        // damage enemies
        foreach(var enemy in enemyManager.enemiesInTrigger)
        {
            // get direction to enemy
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if(Physics.Raycast(transform.position, dir,out hit, range + 1.5f, raycastLayerMask))
            {
                if(hit.transform == enemy.transform)
                {
                    // range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);
                    
                    if (dist > range * 0.5f)
                    {
                        // damage enemy a small amount
                        enemy.TakeDamage(smalldamage);
                    }
                    else
                    {
                        // damage enemy the full amount
                        enemy.TakeDamage(bigdamage);
                    }
                }
            }
        }

        // reset time to fire
        //nextTimeToFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        //add potential enemy to shoot
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            // add enemy here
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //remove potential enemy to shoot
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            // remove enemy
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
