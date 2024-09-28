using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement Enemy;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawedEnemies;
    [SerializeField] AudioClip spawnedEnemySFX;
    int score;

    void Start()
    {
        StartCoroutine(SpawnEnemiesRepeatedly());
        spawedEnemies.text = score.ToString();
    }

    IEnumerator SpawnEnemiesRepeatedly()
    {
        while (true)
        {
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            var newEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform.transform;
            yield return new WaitForSeconds(secondsBetweenSpawns);

        }
    }

    private void AddScore()
    {
        score++;
        spawedEnemies.text = score.ToString();
    }
}
