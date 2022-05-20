using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Status")]
        public List<GameObject> enemies;

        [Header("Settings")]
        public int spawnLimit;
        public float spawnInterval;
        public float spawnMinDistance;
        public Transform[] spawnPoints;
        public GameObject enemyPrefab;

        private float _interval;

#if UNITY_EDITOR
        private void OnValidate()
        {
            spawnPoints = new Transform[transform.childCount];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i] = transform.GetChild(i);
            }
        }
#endif

        private void Update()
        {
            if (enemies.Count > spawnLimit)
            {
                _interval = spawnInterval;
                return;
            }

            _interval += Time.deltaTime;
            if (_interval > spawnInterval)
            {
                _interval = 0;
                Spawn();
            }
        }

        private async void Spawn()
        {
            var spawnIndex = Random.Range(0, spawnPoints.Length);
            while (spawnMinDistance > Vector3.Distance(GamePlayer.instance.transform.position, spawnPoints[spawnIndex].transform.position))
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
                await System.Threading.Tasks.Task.Yield();
            }
            var enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPoints[spawnIndex].position;
            enemies.Add(enemy);
        }
    }
}
