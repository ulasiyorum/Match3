using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance { get; private set; }
    public LinkedList<Cube> cubes = new LinkedList<Cube>();
    public GameObject[] cubePrefabs;
    private float spawnTimer = 0f;
    private float spawnInterval = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnCube();
        }
    }
    
    void SpawnCube()
    {
        GameObject cube = Instantiate(cubePrefabs[Random.Range(0, cubePrefabs.Length)], 
            (cubes.Last?.Value.transform.position ?? new Vector3()) + Vector3.up * 2, Quaternion.identity);
        cubes.AddLast(cube.GetComponent<Cube>());

        if (cubes.Count >= 8)
        {
            DestroyCube(cubes.First.Value, cubes.First.Next!.Value, cubes.First.Next.Next!.Value);
        }
    }
    
    public void DestroyCube(params Cube[] cube)
    {
        foreach (var cube1 in cube)
        {
            cubes.Remove(cube1);
            Destroy(cube1.gameObject);
        }
    }
}
