using UnityEngine;

namespace RigMan.UserInput
{
    [CreateAssetMenu(menuName = "RigMan/UserInput/InputData")]
    public class InputData : ScriptableObject
    {
        [SerializeField] private float zDepth;
        public float ZDepth => zDepth;

        [SerializeField] private LayerMask draggableLayerMask;
        public LayerMask DraggableLayerMask => draggableLayerMask;
    }
}
