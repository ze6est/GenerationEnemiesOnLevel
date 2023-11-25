using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase
{
    public class Spawner : MonoBehaviour
    {
        private const string EnemiePath = "Prefabs/Enemies";

        [SerializeField] private SpawnPoint[] _spawnPoints;        
        [SerializeField] private float _spawnDuration;

        private List<Enemie> _enemies = new List<Enemie>();
        private Coroutine _spawnEnemieJob;
        private bool _isGameOn;

        private void Awake()
        {
            _isGameOn = true;
            _spawnPoints = GetComponentsInChildren<SpawnPoint>();
            LoadEnemies();
        }

        private void Start() =>
            _spawnEnemieJob = StartCoroutine(SpawnEnemie());

        private void OnDestroy() =>
            StopCoroutine(_spawnEnemieJob);

        private void LoadEnemies() => 
            _enemies = Resources.LoadAll<Enemie>(EnemiePath).ToList();

        private IEnumerator SpawnEnemie()
        {
            WaitForSeconds waitTime = new WaitForSeconds(_spawnDuration);

            while (_isGameOn)
            {
                yield return waitTime;

                {
                    int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
                    SpawnPoint spawnPoint = _spawnPoints[spawnPointIndex];

                    Enemie enemie = _enemies.FirstOrDefault<Enemie>(x => x.EnemieType == spawnPoint.EnemieType);
                    
                    if (enemie != null )
                    {
                        enemie.Init(spawnPoint.Target);

                        Instantiate(enemie, spawnPoint.transform.position, Quaternion.identity);
                    }                    
                }
            }
        }
    }
}