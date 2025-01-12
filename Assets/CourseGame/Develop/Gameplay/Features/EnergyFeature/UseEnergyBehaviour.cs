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
        private ReactiveEvent _useEnergyEvent;
        private ReactiveVariable<float> _energy;
        private ReactiveVariable<float> _useEnergy;

        private IDisposable _disposableUseEnergy;

        public void OnInit(Entity entity)
        {
            _useEnergyEvent = entity.GetUseEnergyEvent();//
            _energy = entity.GetEnergy();
            _useEnergy = entity.GetAmountEnergyForTeleport();

            _disposableUseEnergy = _useEnergyEvent.Subscribe(OnUseEnergy);
        }

        private void OnUseEnergy()
        {
           /* if (_useEnergy.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(_useEnergy));*/

            float tempEnergy = _energy.Value - _useEnergy.Value;
            _energy.Value = Math.Max(tempEnergy, 0);
        }

        public void OnDispose()
        {
            _disposableUseEnergy.Dispose();
        }
    }
}