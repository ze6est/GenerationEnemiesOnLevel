using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const string EnemiePath = "Prefabs/Enemie";

    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private float _spawnDuration;

    private List<Enemie> _enemies = new List<Enemie>();    
    private Coroutine _spawnEnemieJob;
    private bool _isGameOn;

    private void Awake()
    {
        _isGameOn = true;
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        CreateEnemies();
    }

    private void Start() => 
        _spawnEnemieJob = StartCoroutine(SpawnEnemie());

    private void OnDestroy() => 
        StopCoroutine(_spawnEnemieJob);

    private void CreateEnemies()
    {
        Enemie enemie = Resources.Load<Enemie>(EnemiePath);        

        for (int i = 0; i < _enemiesCount; i++)
        {
            Enemie enemieInstance = Instantiate<Enemie>(enemie);
            _enemies.Add(enemieInstance);
            enemieInstance.gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnEnemie()
    {
        var waitTime = new WaitForSeconds(_spawnDuration);        

        while (_isGameOn)
        {
            yield return waitTime;

            {
                int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
                Enemie enemie = _enemies.FirstOrDefault(x => x.gameObject.activeInHierarchy == false);

                if (enemie != null)
                {
                    SpawnPoint spawnPoint = _spawnPoints[spawnPointIndex];
                    enemie.transform.position = spawnPoint.transform.position;
                    enemie.Init(spawnPoint.EnemieDirection);
                    enemie.gameObject.SetActive(true);
                }
            }
            
        }        
    }
}