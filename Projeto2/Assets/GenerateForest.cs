using UnityEngine;

public class GenerateForest: MonoBehaviour
{
    public GameObject tree;
    public GameObject rock;

    public float minTreeSize;
    public float maxTreeSize;
    public float minRockSize;
    public float maxRockSize;
    public Texture2D noiseImage;
    public float forestSize;
    public float treeDensity;
    GameObject newGameObj;

    private float baseDensity = 5.0f;


    void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        for (int y = -250; y < forestSize; y++)
        {
            for (int x = -250; x < forestSize; x++)
            {
                float probability = noiseImage.GetPixel(x, y).r / (baseDensity / treeDensity);

                if (CanPlace(probability))
                {
                    float treeSize = Random.Range(minTreeSize, maxTreeSize);

                    float rockSize = Random.Range(minRockSize, maxRockSize);

                    RandomObject(Random.Range(1, 3));
                    if (newGameObj.tag != "Stone")
                    {
                        newGameObj.transform.localScale = Vector3.one * treeSize;
                        newGameObj.transform.position = new Vector3(x, 0, y);
                        newGameObj.transform.parent = transform;
                    }
                    else
                    {
                        newGameObj.transform.localScale = Vector3.one * rockSize;
                        newGameObj.transform.position = new Vector3(x, 0.55f, y);
                        newGameObj.transform.rotation = Random.rotation;
                        newGameObj.transform.parent = transform;
                    }
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
            newGameObj = Instantiate(tree);
        }

        if (random == 2)
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
