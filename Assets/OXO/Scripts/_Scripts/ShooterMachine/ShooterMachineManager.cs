using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Redcode.Extensions;
using RigMan.Bullet;
using RigMan.Pooling;
using RigMan.Target;
using UnityEngine;

namespace RigMan.ShooterMachine
{
    public class ShooterMachineManager : MonoBehaviour
    {
        [SerializeField] private BulletSettings bulletSettings;
        [SerializeField] private BulletPoolDataHolder bulletDataHolder;
        [SerializeField] private Transform firePointTransform;

        [SerializeField] private List<GameObject> targetList;


        public void Shoot()
        {
            GameObject bullet = bulletDataHolder.bulletList.FirstOrDefault(m => !m.activeInHierarchy);

            if (bullet == null) return;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            GameObject getRandomTarget = targetList.GetRandomElement();
            TargetManager targetManager = getRandomTarget.GetComponent<TargetManager>();
            

            bulletDataHolder.UseCase(bullet);
            bullet.transform.position = firePointTransform.position;

            bullet.transform.DOJump(targetManager.bodyTarget.transform.position,
                    bulletSettings.ShootForce, 1,
                    1 / bulletSettings.BulletSpeed)
                .SetEase(bulletSettings.shootEase)
                .OnComplete(() =>
                {
                    bulletRb.AddForce(Vector3.forward * 500);
                    StartCoroutine(BulletCallBack(bullet));
                });
        }

        private IEnumerator BulletCallBack(GameObject bullet)
        {
            yield return new WaitForSeconds(bulletSettings.BulletBackDelay);
            bulletDataHolder.BackPoolCase(bullet);
        }
    }
}