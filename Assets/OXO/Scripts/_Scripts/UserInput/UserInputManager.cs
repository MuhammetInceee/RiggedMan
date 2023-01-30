using RigMan.ShooterMachine;
using RigMan.Target;
using UnityEngine;

namespace RigMan.UserInput
{
    public class UserInputManager : MonoBehaviour
    {
        private RaycastHit _hit;
        private GameObject _selectedGO;
        private Vector3 _tempPos;

        [SerializeField] private InputData inputData;
        [SerializeField] private ShooterMachineManager shooterMachine;
        [SerializeField] private Camera gameCamera;
        
        
        private Ray Ray => gameCamera.ScreenPointToRay(Input.mousePosition);
        private float InputX => Input.mousePosition.x;
        private float InputY => Input.mousePosition.y;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }

            if (Input.GetMouseButton(0))
            {
                OnDrag();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnDragStop();
            }
        }

        private void OnClick()
        {
            if (Physics.Raycast(Ray, out _hit, Mathf.Infinity, inputData.DraggableLayerMask.value))
            {
                _selectedGO = _hit.collider.gameObject;
                if (_selectedGO.TryGetComponent(out TargetManager targetManager))
                {
                    targetManager.isMoving = true;
                }
            }
            else
            {
                shooterMachine.Shoot();
            }
        }

        private void OnDrag()
        {
            if(_selectedGO == null) return;

            _tempPos = _selectedGO.transform.position;

            _tempPos = gameCamera.ScreenToWorldPoint(new Vector3(InputX, InputY, inputData.ZDepth));

            _selectedGO.transform.position = _tempPos;
        }

        private void OnDragStop()
        {
            if (_selectedGO != null && _selectedGO.TryGetComponent(out TargetManager targetManager))
            {
                targetManager.isMoving = false;
                targetManager.CheckNull();
            }
            _selectedGO = null;
        }
    }
}
