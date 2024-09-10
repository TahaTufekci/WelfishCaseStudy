using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    public float damage = 20;
    AimCameraController aim;
    [SerializeField] AudioClip gunShot;

    AudioSource audioSource;

    private void Start()
    {
        aim = GetComponentInParent<AimCameraController>();
        audioSource = GetComponent<AudioSource>();
        fireRateTimer = fireRate;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Lose || GameManager.Instance.CurrentGameState == GameState.Win)
        {
            return;
        }
        if (ShouldFire()) Fire();
    }

    private bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }


    private void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        for (int i = 0; i < bulletPerShot; i++)
        {
            var currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            var rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
