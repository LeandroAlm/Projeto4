using UnityEngine;

public class GenerateForest: MonoBehaviour
{

    public GameObject tree;
    public GameObject tree2;
    public GameObject rock;

    public float minTreeSize;
    public float maxTreeSize;
    public Texture2D noiseImage;
    public float forestSize;
    public float treeDensity;
    GameObject newGameObj;

    private float baseDensity = 5.0f;


    void Start()
    {
        Generate();
    }

    public void Generate()
    {

        for (int y = 0; y < forestSize; y++)
        {
            for (int x = 0; x < forestSize; x++)
            {
                float probability = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (CanPlace(probability))
                {
                    float size = Random.Range(minTreeSize, maxTreeSize);

                    RandomObject(Random.Range(1, 4));
                    newGameObj.transform.localScale = Vector3.one * size;
                    newGameObj.transform.position = new Vector3(x, 0, y);
                    newGameObj.transform.parent = transform;
                }
            }
        }
    }
    public void RandomObject(int random)
    {
        if (random == 1)
        {
            newGameObj = Instantiate(tree);
        }

        if (random == 2)
        {
            newGameObj = Instantiate(tree2);
        }

        if (random == 3)
        {
            newGameObj = Instantiate(rock);
        }
    }

    public bool CanPlace(float chance)
    {
        if (Random.Range(0.0f, 1.0f) <= chance)
        {
            return true;
        }
        return false;
    }
}
