using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigMan.Target
{
    [CreateAssetMenu(menuName = "RigMan/Target/TargetSettings")]
    public class TargetSettings : ScriptableObject
    {
        [SerializeField] private List<float> damageList;
        public List<float> DamageList => damageList;

        [SerializeField] private LayerMask bodyLayerMask;
        public LayerMask BodyLayerMask => bodyLayerMask;
    }
}
