using System;
using DG.Tweening;
using RigMan.Interfaces;
using UnityEngine;

namespace RigMan.Character
{
    public class DamageablePart : MonoBehaviour, OnDamageable
    {
        private bool _isWaitToRecover;
        
        [SerializeField] private DamageablePartSettings damageSettings;

        private bool CanDamageable => damageSettings.Health > 0;

        private float MaterialSliderValue
        {
            get => damageSettings.TargetMaterial.GetFloat("_Slider");
            set => damageSettings.TargetMaterial.SetFloat("_Slider", value);
        }
        public void GetDamage(float damage)
        {
            if(_isWaitToRecover) return;
            
            damageSettings.Health -= damage;
            if (CanDamageable)
            {
                DOTween.To(() => MaterialSliderValue, (m) => MaterialSliderValue = m, MaterialSliderValue + damage / 100, 0.1f)
                    .OnComplete(() =>
                    {
                        if (!CanDamageable)
                        {
                            PlasterVisualizer();
                        }
                    });
            }
            else
            {
                PlasterVisualizer();
            }
        }

        private void PlasterVisualizer()
        {
            _isWaitToRecover = true;
            GameObject plaster = Instantiate(damageSettings.PlasterPrefab, transform.position, Quaternion.Euler(90, 0,0));
            plaster.transform.position = new Vector3(plaster.transform.position.x, transform.position.y, -0.5f);
            
            DOTween.To(() => MaterialSliderValue, (m) => MaterialSliderValue = m, 0, 15f)
                .OnComplete(() =>
                {
                    _isWaitToRecover = false;
                    damageSettings.Health = 100;
                    Destroy(plaster);
                });
        }

        private void OnDisable()
        {
            damageSettings.Health = 100;
            MaterialSliderValue = 0;
        }
    }
}
