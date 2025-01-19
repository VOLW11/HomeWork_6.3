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
        private ReactiveVariable<float> _energy;     
        private float _time;
        private float _tempEnergy;

        private ICondition _condition;
        private IReadOnlyVariable<float> _maxEnergy;
        private IReadOnlyVariable<float> _recoveryTime;

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

                if (_time >= _recoveryTime.Value)
                {
                    _time = 0;
                    _tempEnergy = _energy.Value + (_maxEnergy.Value * 0.1f);
                    _energy.Value = Math.Min(_tempEnergy, _maxEnergy.Value);
                    Debug.Log("Запас энергии: " + _energy.Value);
                }
            }
        }
    }
}