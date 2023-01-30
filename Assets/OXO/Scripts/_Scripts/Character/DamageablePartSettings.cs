using UnityEngine;

namespace RigMan.Character
{
    [CreateAssetMenu(menuName = "RigMan/Character/DamageablePart")]
    public class DamageablePartSettings : ScriptableObject
    {
        public float Health;

        public GameObject PlasterPrefab;

        [SerializeField] private Material targetMaterial;
        public Material TargetMaterial => targetMaterial;
    }
}
