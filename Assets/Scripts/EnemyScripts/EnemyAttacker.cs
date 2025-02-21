using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private EnemyBullet _bullet;

    public void SpawnBullet()
    {
        Instantiate(_bullet, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
    }
}