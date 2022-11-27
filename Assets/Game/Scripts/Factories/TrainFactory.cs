using System;
using System.Collections.Generic;
using Behaviours;
using Game.Scripts.Enumerations;
using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(menuName = "Game/Train Factory")]
    public class TrainFactory : ScriptableObject
    {
        [SerializeField] 
        private SerializableDictionary<TrainType, TrainPrefab> trainPrefabs;
        
        [SerializeField] 
        private SerializableDictionary<TrainType, SerializableDictionary<TrainColor, TrainInfo>> defaultTrainData;
        
        public void CreateTrain(List<TrainPartBehaviour> trainParts, TrainType type, TrainColor color, int count)
        {
            if (!trainPrefabs.TryGetValue(type, out var prefabs))
            {
                return;
            }

            if (!defaultTrainData.TryGetValue(type, out var infos))
            {
                return;
            }

            if (!infos.TryGetValue(color, out var trainInfo))
            {
                return;
            }

            var locomotive = Instantiate(prefabs.locomotive);
            var locomotiveMeshInstance = Instantiate(trainInfo.locomotive);
            
            locomotive.SetMesh(locomotiveMeshInstance);
            
            trainParts.Add(locomotive);

            for (int i = 0; i < count; i++)
            {
                var wagon = Instantiate(prefabs.wagon);
                var wagonMeshInstance = Instantiate(trainInfo.passengerWagon);
            
                wagon.SetMesh(wagonMeshInstance);
                
                trainParts.Add(wagon);
            }
        }

        public LocomotiveBehaviour CreateLocomotive(TrainType type, TrainColor color)
        {
            if (!trainPrefabs.TryGetValue(type, out var prefabs))
            {
                return null;
            }

            if (!defaultTrainData.TryGetValue(type, out var infos))
            {
                return null;
            }

            if (!infos.TryGetValue(color, out var trainInfo))
            {
                return null;
            }

            var locomotive = Instantiate(prefabs.locomotive);
            var meshInstance = Instantiate(trainInfo.locomotive);
            
            locomotive.SetMesh(meshInstance);

            return locomotive;
        }

        public WagonBehaviour CreateWagon(TrainType type, TrainColor color)
        {
            if (!trainPrefabs.TryGetValue(type, out var prefabs))
            {
                return null;
            }

            if (!defaultTrainData.TryGetValue(type, out var infos))
            {
                return null;
            }

            if (!infos.TryGetValue(color, out var trainInfo))
            {
                return null;
            }

            var wagon = Instantiate(prefabs.wagon);
            var meshInstance = Instantiate(trainInfo.passengerWagon);
            
            wagon.SetMesh(meshInstance);

            return wagon;
        }
        
        
        [Serializable]
        private class TrainInfo
        {
            public GameObject locomotive;
            public GameObject passengerWagon;
        }

        [Serializable]
        private class TrainPrefab
        {
            public LocomotiveBehaviour locomotive;
            public WagonBehaviour wagon;
        }
    }
}