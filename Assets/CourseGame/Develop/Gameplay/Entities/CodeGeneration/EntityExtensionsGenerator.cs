﻿using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Entities.CodeGeneration
{
    public static class EntityExtensionsGenerator
    {
        private static Dictionary<EntityValues, Type> _entityValuesToType = new Dictionary<EntityValues, Type>()
        {
            {EntityValues.MoveDirection, typeof(ReactiveVariable<Vector3>) },
            {EntityValues.MoveSpeed, typeof(ReactiveVariable<float>) },
            {EntityValues.MoveCondition, typeof(ICompositeCondition) },
            {EntityValues.IsMoving, typeof(ReactiveVariable<bool>) },

            {EntityValues.RotationDirection, typeof(ReactiveVariable<Vector3>) },
            {EntityValues.RotationSpeed, typeof(ReactiveVariable<float>) },
            {EntityValues.RotationCondition, typeof(ICompositeCondition) },

            {EntityValues.TeleportCondition, typeof(ICompositeCondition) }, 

            {EntityValues.SelfTriggerDamage, typeof(ReactiveVariable<float>) },
            {EntityValues.SelfTriggerReciever, typeof(TriggerReciever) },

            {EntityValues.CharacterController, typeof(CharacterController)},
            {EntityValues.Transform, typeof(Transform)},

            {EntityValues.Health, typeof(ReactiveVariable<float>) },
            {EntityValues.MaxHealth, typeof(ReactiveVariable<float>) },

            {EntityValues.Energy, typeof(ReactiveVariable<float>) }, 
            {EntityValues.MaxEnergy, typeof(ReactiveVariable<float>) }, 
            {EntityValues.AmountEnergyForTeleport, typeof(ReactiveVariable<float>) }, 
            {EntityValues.TimeRecoveryUnitEnergy, typeof(ReactiveVariable<float>) },
            {EntityValues.RecoveryEnergyCondition, typeof(ICompositeCondition) },

            {EntityValues.IsTeleportEvent, typeof(ReactiveEvent) },
            {EntityValues.EffectTeleportEvent, typeof(ReactiveEvent<Transform>) },
            {EntityValues.IsTeleport, typeof(ReactiveVariable<bool>) }, 
            {EntityValues.IsTeleportProcess, typeof(ReactiveVariable<bool>) }, 

            {EntityValues.SphereColliderDamage, typeof(Collider) }, 

            {EntityValues.TakeDamageRequest, typeof(ReactiveEvent<float>) },
            {EntityValues.TakeDamageEvent, typeof(ReactiveEvent<float>) },
            {EntityValues.TakeDamageCondition, typeof(ICompositeCondition) },

            {EntityValues.IsDead, typeof(ReactiveVariable<bool>) },
            {EntityValues.IsDeathProcess, typeof(ReactiveVariable<bool>) },
            {EntityValues.DeathCondition, typeof(ICompositeCondition) },
            {EntityValues.SelfDestroyCondition, typeof(ICompositeCondition) },
        };

        [InitializeOnLoadMethod]
        [MenuItem("Tools/GenerateEntityExtensions")]
        private static void Generate()
        {
            string path = GetPathToExtensionsFile();

            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine(GetClassHeader());
            writer.WriteLine("{");

            foreach (KeyValuePair<EntityValues, Type> entityValueToTypePair in _entityValuesToType)
            {
                string type = entityValueToTypePair.Value.FullName;

                if (entityValueToTypePair.Value.IsGenericType)
                {
                    type = type.Substring(0, type.IndexOf('`'));

                    type += "<";

                    for(int i = 0; i < entityValueToTypePair.Value.GenericTypeArguments.Length; i++)
                    {
                        type += entityValueToTypePair.Value.GenericTypeArguments[i].FullName;

                        if (i != entityValueToTypePair.Value.GenericTypeArguments.Length - 1)
                            type += ",";
                    }

                    type += ">";
                }

                if (HasEmptyConstructor(entityValueToTypePair.Value))
                    writer.WriteLine($"public static {typeof(Entity)} Add{entityValueToTypePair.Key}(this {typeof(Entity)} entity) => entity.AddValue({typeof(EntityValues)}.{entityValueToTypePair.Key}, new {type}());");

                writer.WriteLine($"public static {typeof(Entity)} Add{entityValueToTypePair.Key}(this {typeof(Entity)} entity, {type} value) => entity.AddValue({typeof(EntityValues)}.{entityValueToTypePair.Key}, value);");
                writer.WriteLine($"public static {type} Get{entityValueToTypePair.Key}(this {typeof(Entity)} entity) => entity.GetValue<{type}>({typeof(EntityValues)}.{entityValueToTypePair.Key});");
                writer.WriteLine($"public static {typeof(bool)} TryGet{entityValueToTypePair.Key}(this {typeof(Entity)} entity, out {type} value) => entity.TryGetValue<{type}>({typeof(EntityValues)}.{entityValueToTypePair.Key}, out value);");
            }

            writer.WriteLine("}");

            writer.Close();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string GetClassHeader() => "public static class EntityExtensionGenerated";

        private static string GetPathToExtensionsFile() => $"{Application.dataPath}/CourseGame/Develop/Gameplay/Entities/CodeGeneration/EntityExtensionGenerated.cs";

        private static bool HasEmptyConstructor(Type type) =>
            type.IsAbstract == false
            && type.IsSubclassOf(typeof(UnityEngine.Object)) == false
            && type.IsInterface == false
            && type.GetConstructors().Any(constructor => constructor.GetParameters().Count() == 0);
    }
}
