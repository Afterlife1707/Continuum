using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzle;
    public AudioSource shootSound;
    public static Shooting instance;
    public float bulletForce = 30f;
    public float fireRate;
    float nextFire;
    public bool closeUIFire;


    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isDialogueDisplayed || GameManager.instance.levelComplete || PauseMenu.IsPaused)
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            if(!closeUIFire)
            {
                closeUIFire = true; //will run once
                return;
            }
            Shoot();
        }
    }

    void Shoot()
    {
        if (GetComponent<PlayerMovement>().isRolling)
            return;
        if(Time.time>nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            StartCoroutine(ShowMuzzle());
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            shootSound.Play();
            Destroy(bullet, 3f);
        }
    }
    IEnumerator ShowMuzzle()
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzle.SetActive(false);
    }
}
