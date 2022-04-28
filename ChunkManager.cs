using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Object chunkInstance;

    public Noise noise;

    private Dictionary<Vector3Int, Chunk> chunks;

    void Start()
    {
        chunks = new Dictionary<Vector3Int, Chunk>();

        int initSize = 5;

        for (int x = 0; x < initSize; x++)
        {
            for (int y = 0; y < initSize; y++)
            {
                for (int z = 0; z < initSize; z++)
                {
                    GameObject chunk = (GameObject)Object.Instantiate(chunkInstance);
                    chunk.transform.position = new Vector3(x, y, z) * (float)Chunk.SIZE;
                    Chunk chunkComponent = chunk.GetComponent<Chunk>();
                    chunkComponent.key = new Vector3Int(x, y, z);
                    chunkComponent.manager = this;
                    chunkComponent.InitChunkData();
                    chunks.Add(chunkComponent.key, chunkComponent);
                }
            }
        }

        for (int x = 0; x < initSize; x++)
        {
            for (int y = 0; y < initSize; y++)
            {
                for (int z = 0; z < initSize; z++)
                {
                    chunks[new Vector3Int(x, y, z)].UpdateMeshData();
                }
            }
        }
    }

    public void Edit(Vector3Int pos, float r, float v)
    {
        Vector3Int chunkKey = new Vector3Int(pos.x / Chunk.SIZE, pos.y / Chunk.SIZE, pos.z / Chunk.SIZE);

        Vector3Int chunkPos = new Vector3Int(pos.x % Chunk.SIZE, pos.y % Chunk.SIZE, pos.z % Chunk.SIZE);
        int radius = Mathf.CeilToInt(r);

        int overX = chunkPos.x + radius >= Chunk.SIZE ? -1 : (chunkPos.x - radius < 0 ? 1 : 0);
        int overY = chunkPos.y + radius >= Chunk.SIZE ? -1 : (chunkPos.y - radius < 0 ? 1 : 0);
        int overZ = chunkPos.z + radius >= Chunk.SIZE ? -1 : (chunkPos.z - radius < 0 ? 1 : 0);
        List<Vector3Int> updatedChunks = new List<Vector3Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x != 0 && overX == 0 || y != 0 && overY == 0 || z != 0 && overZ == 0)
                    {
                        continue;
                    }

                    Vector3Int ikey = chunkKey + new Vector3Int(x, y, z);
                    if (chunks.ContainsKey(ikey))
                    {
                        chunks[ikey].Edit(pos, r, v);
                        updatedChunks.Add(ikey);
                    }
                }
            }
        }

        foreach (Vector3Int key in updatedChunks)
        {
            chunks[key].UpdateMeshData();
        }
    }

    public float Value(int x, int y, int z)
    {
        Vector3Int pos = new Vector3Int(
            (int)Mathf.Floor((float)x / Chunk.SIZE),
            (int)Mathf.Floor((float)y / Chunk.SIZE),
            (int)Mathf.Floor((float)z / Chunk.SIZE));

        if (chunks.ContainsKey(pos))
        {
            return chunks[pos].Value(x % Chunk.SIZE, y % Chunk.SIZE, z % Chunk.SIZE);
        }

        return 0.0f;
    }

}
