using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigMan.Pooling
{
    [CreateAssetMenu(menuName = "RigMan/Bullet/BulletPoolDataHolder")]
    public class BulletPoolDataHolder : ScriptableObject
    {
        public List<GameObject> bulletList;
        
        
        public void Disable()
        {
            bulletList.Clear();
        }

        public void UseCase(GameObject bullet)
        {
            bullet.SetActive(true);
        }

        public void BackPoolCase(GameObject bullet)
        {
            bullet.SetActive(false);
        }
    }
}
