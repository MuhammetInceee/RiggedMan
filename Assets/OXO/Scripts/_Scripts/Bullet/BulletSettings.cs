using DG.Tweening;
using UnityEngine;

namespace RigMan.Bullet
{
    [CreateAssetMenu(menuName = "RigMan/Bullet/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [SerializeField] private GameObject bulletPrefab;
        public GameObject BulletPrefab => bulletPrefab;
        
        [SerializeField] private float shootForce;
        public float ShootForce => shootForce;

        [SerializeField] private float bulletSpeed;
        public float BulletSpeed => bulletSpeed;

        [SerializeField] private float bulletBackDelay;
        public float BulletBackDelay => bulletBackDelay;

        public Ease shootEase;
    }
}
