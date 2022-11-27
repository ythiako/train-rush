using System;
using Behaviours;
using Factories;
using Game.Scripts.Enumerations;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class FactoryController : MonoBehaviour
    {
        [SerializeField] private TrainFactory trainFactory;

        private static FactoryController _instance;

        private static FactoryController Instance => _instance ? _instance : _instance = FindObjectOfType<FactoryController>();

        public static TrainFactory TrainFactory => Instance.trainFactory;
    }
}