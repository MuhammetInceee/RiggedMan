using System;
using RigMan.Bullet;
using UnityEngine;

namespace RigMan.Pooling
{
    public class BulletPooling : MonoBehaviour
    {
        private Transform _tr;
        
        [SerializeField] private BulletSettings bulletSettings;
        [SerializeField] private BulletPoolDataHolder bulletDataHolder;
        
        [SerializeField] private int bulletPoolSize;

        private void Awake()
        {
            GetReference();
            BulletPoolInitialize();
        }

        private void BulletPoolInitialize()
        {
            for (int i = 0; i < bulletPoolSize; i++)
            {
                GameObject bullet = Instantiate(bulletSettings.BulletPrefab, _tr.position, Quaternion.identity, transform);
                bulletDataHolder.bulletList.Add(bullet);
                bullet.SetActive(false);
            }
        }

        private void OnDisable()
        {
            bulletDataHolder.Disable();
        }

        private void GetReference()
        {
            _tr = GetComponent<Transform>();
        }
    }
}
