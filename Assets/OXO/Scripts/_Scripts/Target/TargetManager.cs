using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RigMan.Target
{
    public class TargetManager : MonoBehaviour
    {
        private RaycastHit _hit;
        [HideInInspector] public bool isMoving;
        public GameObject bodyTarget;
        
        [SerializeField] private TargetSettings settings;
        
        [SerializeField] private int targetLevel;
        
        public float damage;

        private void Awake()
        {
            bodyTarget = gameObject;
            damage = settings.DamageList[targetLevel];
        }

        [Button]
        public void UpgradeTarget()
        {
            if(targetLevel == settings.DamageList.Count - 1) return;
            
            targetLevel++;
            damage = settings.DamageList[targetLevel];
        }

        private void Update()
        {
            if(!isMoving) return;
            
            if (Physics.Raycast(transform.position, Vector3.forward, out _hit, Mathf.Infinity,
                    settings.BodyLayerMask.value))
            {
                bodyTarget = _hit.collider.gameObject;
            }
            else
            {
                bodyTarget = null;
            }

        }

        public void CheckNull()
        {
            if (bodyTarget == null)
            {
                bodyTarget = gameObject;
            }
        }
    }
}
