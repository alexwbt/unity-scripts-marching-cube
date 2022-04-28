using UnityEngine;

public class Noise : MonoBehaviour
{
	public int seed;
	public bool randomSeed;

	public float scale;

	public int octaves;
	[Range(0, 1)]
	public float persistance;
	public float lacunarity;

	private System.Random rand;
	private Vector3 offset;

	public void Start()
	{
		if (randomSeed)
		{
			seed = (int)System.DateTime.Now.Ticks;
		}

		rand = new System.Random(seed);
		offset = new Vector3(rand.Next(-100000, 100000), rand.Next(-100000, 100000), rand.Next(-100000, 100000));
	}

	public float Density(float height, Vector3 pos)
	{
		pos /= scale;
		float density = height / scale - pos.y;
		for (int i = 0; i < octaves; i++)
		{
			density += PerlinNoise3D(pos * Mathf.Pow(lacunarity, i)) * Mathf.Pow(persistance, i);
		}
		return density;
	}

	public float PerlinNoise3D(Vector3 pos)
	{
		Vector3 rpos = pos + offset;
		float xy = Mathf.PerlinNoise(rpos.x, rpos.y);
		float xz = Mathf.PerlinNoise(rpos.x, rpos.z);
		float yz = Mathf.PerlinNoise(rpos.y, rpos.z);
		float yx = Mathf.PerlinNoise(rpos.y, rpos.x);
		float zx = Mathf.PerlinNoise(rpos.z, rpos.x);
		float zy = Mathf.PerlinNoise(rpos.z, rpos.y);

		return (xy + xz + yz + yx + zx + zy) / 6;
	}

}
