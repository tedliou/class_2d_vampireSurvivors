using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;

    private IEnumerator Start()
    {
        while (true)
        {
            var enemy = Instantiate(Enemy);
            enemy.transform.position = transform.position + new Vector3(12, 0) * (Random.Range(0, 2) < 1 ? 1 : -1);
            enemy.transform.position = enemy.transform.position + new Vector3(0, 12) * (Random.Range(0, 2) < 1 ? 1 : -1);
            yield return new WaitForSeconds(.05f);
        }
    }
}
