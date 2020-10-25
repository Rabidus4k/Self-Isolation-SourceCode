using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    public GameObject[] CloudsChunks;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnCloudsChunk), 0, 60f);   
    }

    private void SpawnCloudsChunk()
    {
        var randomChunkIndex = Random.Range(0, CloudsChunks.Length);
        Instantiate(CloudsChunks[randomChunkIndex], transform.position, Quaternion.identity);
    }
}
