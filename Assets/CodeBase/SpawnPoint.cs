using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _enemieDirection;

    public Vector3 EnemieDirection => _enemieDirection;

    private void OnValidate() => 
        transform.position = _position;
}