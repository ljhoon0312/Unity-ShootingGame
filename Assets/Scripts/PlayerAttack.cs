﻿using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private int bulletCount = 1;
    public AudioClip shootSFX;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void IncreaseBulletCount()
    {
        bulletCount = Mathf.Min(bulletCount + 1, 5); // 아이템 먹었을 때 증가용
    }

    public void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("총알 프리팹 또는 발사 위치가 지정되지 않았습니다.");
            return;
        }

        if (shootSFX != null && audioSource != null)
            audioSource.PlayOneShot(shootSFX);

        float angleStep = 10f;
        float startAngle = -angleStep * (bulletCount - 1) / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 spawnPos = firePoint.position;
            spawnPos.z = 0f;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            Instantiate(bulletPrefab, spawnPos, rot);
        }
    }
}
