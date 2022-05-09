using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject SquarePrefab;
    
    public float XMinPos;
    public float XMaxPos;
    public float YMinPos;
    public float YMaxPos;

    public float SpawnTime;

    public void Start()
    {
        StartCoroutine(Spawner());
    }
    
    public void Spawn()
    {
        var posx = Random.Range(XMinPos, XMaxPos);
        var posy = Random.Range(YMinPos, YMaxPos);

        var pos = new Vector3(posx, posy, 0);
        
        var newSquare = Instantiate(SquarePrefab, pos, Quaternion.identity);
    }

    public IEnumerator Spawner()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(SpawnTime);
        }
    }
}
