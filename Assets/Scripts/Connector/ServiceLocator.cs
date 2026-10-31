using System;
using System.Collections.Generic;
using UnityEngine;

namespace GDD.Connector
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }

        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // 서비스를 등록하는 메서드
        public static void Register<T>(T service)
        {
            Type type = typeof(T);

            if (Instance._services.ContainsKey(type))
                Instance._services[type] = service;
            else
                Instance._services.Add(type, service);
        }

        // 등록 해제 메서드 (오브젝트 파괴 시 호출)
        public static void Unregister<T>()
        {
            Type type = typeof(T);
            if (Instance != null && Instance._services.ContainsKey(type))
                Instance._services.Remove(type);
        }

        // 서비스 가져오는 메서드
        public static T Get<T>()
        {
            Type type = typeof(T);

            if (Instance._services.TryGetValue(type, out object service))
                return (T)service;

            Debug.LogError($"[ServiceLocator] {type.Name} 서비스가 등록되지 않았습니다.");
            return default;
        }
    }
}