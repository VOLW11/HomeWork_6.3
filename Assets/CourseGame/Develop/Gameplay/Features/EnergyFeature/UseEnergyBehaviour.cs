using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.EnergyFeature
{
    public class UseEnergyBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<float> _energy;
        private ReactiveVariable<float> _useEnergy;
        private ReactiveEvent<bool> _isTeleport;

        private IDisposable _disposableUseEnergy;

        public void OnInit(Entity entity)
        {
            _energy = entity.GetEnergy();
            _useEnergy = entity.GetAmountEnergyForTeleport();
            _isTeleport = entity.GetIsTeleportEvent();

            _disposableUseEnergy = _isTeleport.Subscribe(OnUseEnergy);
        }

        private void OnUseEnergy(bool isTeleport)
        {
            if (_useEnergy.Value > _energy.Value)
                isTeleport = false;

            if (isTeleport == false)
                return;

            float tempEnergy = _energy.Value - _useEnergy.Value;
            _energy.Value = Math.Max(tempEnergy, 0);

            Debug.Log("Использовано энергии: " + _useEnergy.Value);
        }

        public void OnDispose()
        {
            _disposableUseEnergy.Dispose();
        }
    }
}