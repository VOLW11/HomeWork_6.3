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
    public class RecoveryEnergyBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ICondition _condition;
        private ReactiveVariable<float> _energy;
        private ReactiveVariable<float> _maxEnergy;
        private float _time;
        private float _tempEnergy;

        IReadOnlyVariable<float> _recoveryTime;

        public void OnInit(Entity entity)
        {
            _energy = entity.GetEnergy();
            _maxEnergy = entity.GetMaxEnergy();
            _condition = entity.GetRecoveryEnergyCondition();
            _recoveryTime = entity.GetTimeRecoveryUnitEnergy();
        }

        public void OnUpdate(float deltaTime)
        {     
            if (_condition.Evaluate())
            {
                _time += deltaTime;

                if(_time >= _recoveryTime.Value)
                {
                    _tempEnergy = _energy.Value + (_maxEnergy.Value * 0.1f); 
                    _energy.Value = Math.Min(_tempEnergy, _maxEnergy.Value);
                    _time = 0;
                    Debug.Log("Запас энергии: " + _energy.Value);
                }
            }
        }
    }
}