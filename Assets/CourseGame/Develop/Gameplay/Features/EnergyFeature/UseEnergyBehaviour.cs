using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
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
        private ReactiveEvent _isTeleport;

        private IReadOnlyVariable<float> _useEnergy;
        private ICondition _condition;
        private IDisposable _disposableUseEnergy;

        public void OnInit(Entity entity)
        {
            _energy = entity.GetEnergy();
            _useEnergy = entity.GetAmountEnergyForTeleport();
            _isTeleport = entity.GetIsTeleportEvent();
            _condition = entity.GetTeleportCondition();
            _disposableUseEnergy = _isTeleport.Subscribe(OnUseEnergy);
        }

        private void OnUseEnergy()
        {
            if (_condition.Evaluate() == false)
                return;

            float tempEnergy = _energy.Value - _useEnergy.Value;
            _energy.Value = Math.Max(tempEnergy, 0);

            Debug.Log("������������ �������: " + _useEnergy.Value);
        }

        public void OnDispose()
        {
            _disposableUseEnergy.Dispose();
        }
    }
}