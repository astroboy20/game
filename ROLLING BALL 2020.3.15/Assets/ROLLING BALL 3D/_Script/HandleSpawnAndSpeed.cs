using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSpawnAndSpeed : MonoBehaviour, IListener {
    public static HandleSpawnAndSpeed Instance;
    [Header("Spawn Path")]
    public GameObject pathStart;
    public GameObject building;
    public Transform beginPointSpawn;
    public GameObject[] testPath;
    public bool useTestPath = false;
    public GameObject nextLevelPath;
    public WeightedObject[] pathGroup1;
    public WeightedObject[] pathGroup2;
    public WeightedObject[] pathGroup3;

    [Tooltip(" = 0 mean use path weight")]
    float path1Weight, path2Weight, path3Weight;

    List<GameObject> paths = new List<GameObject>();
    List<float> weights = new List<float>();

    int pathSpawned = 0;
    int pathCounter = 0;

    public int maxAllowPaths = 12;

    [Header("UP LEVEL")]
    public UpdateWave[] updateWaves;

    GameObject endPointObj;
    int currentLevel = 1;

    //public void SetEndObject(GameObject gameobject)
    //{
    //    endPointObj = gameobject;
    //}

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start() {
        if(useTestPath && testPath.Length > 0)
        {
            Debug.LogError("TESTING PATH ");
        }
        endPointObj = beginPointSpawn.gameObject;
        UpdatePathAndWeight();


        //Instantiate(pathStart, Vector3.zero, Quaternion.identity);
        GameObject _path = Instantiate(pathStart, Vector3.zero, Quaternion.identity) as GameObject;
        //endPointObj = _path.transform.FindChild("End").gameObject;

        Instantiate(building, Vector3.zero, Quaternion.Euler(45, 0, 0));
        Instantiate(building, new Vector3(0, -240, 210), Quaternion.Euler(45, 0, 0));

        
    }

    void UpdatePathAndWeight()
    {
        paths.Clear();
        weights.Clear();

        if (pathGroup1.Length > 0)
        {
            for (int i = 0; i < pathGroup1.Length; i++)
            {
                paths.Add(pathGroup1[i].gameObject);

                if (path1Weight != 0)
                    weights.Add(path1Weight);
                else
                    weights.Add(pathGroup1[i].weight);
            }
        }

        if (pathGroup2.Length > 0)
        {
            for (int i = 0; i < pathGroup2.Length; i++)
            {
                paths.Add(pathGroup2[i].gameObject);

                if (path2Weight != 0)
                    weights.Add(path2Weight);
                else
                    weights.Add(pathGroup2[i].weight);
            }
        }

        if (pathGroup3.Length > 0)
        {
            for (int i = 0; i < pathGroup3.Length; i++)
            {
                paths.Add(pathGroup3[i].gameObject);

                if (path3Weight != 0)
                    weights.Add(path3Weight);
                else
                    weights.Add(pathGroup3[i].weight);
            }
        }
    }

    IEnumerator CreatePathCo()
    {
        while (true)
        {
            while(GameManager.Instance.state != GameManager.State.Playing) { yield return null; }

            yield return new WaitForSeconds(0.5f);

            int rand = RandomWeightedObject();
            if (rand != -1)
            {
                GameObject _path;

                if (pathCounter == 0)
                {
                    _path = Instantiate(nextLevelPath, endPointObj.transform.position, Quaternion.Euler(-45, 0, 0)) as GameObject;
                    _path.GetComponent<NextLevelPath>().Init(currentLevel);
                }
                else if (useTestPath && testPath.Length > 0)
                {
                    _path = Instantiate(testPath[Random.Range(0, testPath.Length)], endPointObj.transform.position, Quaternion.Euler(-45, 0, 0)) as GameObject;
                }
                else
                {
                    _path = Instantiate(paths[rand], endPointObj.transform.position, Quaternion.Euler(-45, 0, 0)) as GameObject;
                }

                _path.transform.SetParent(transform);
                endPointObj = _path.transform.Find("End").gameObject;
                pathSpawned = transform.childCount;

                pathCounter++;
            }

            while (pathSpawned >= maxAllowPaths)
            {
                pathSpawned = transform.childCount;
                yield return null;
            }

        }

    }

    private int RandomWeightedObject()
    {
        if (paths == null) return -1;
        if (paths.Count == 0) return -1;
        
        int randomIndex = UltiHepler.GetRandomWeightedIndex(weights);
        //Debug.LogError("randomIndex" + randomIndex);

        return randomIndex;
    }

    
    IEnumerator SetVelocityCo()
    {
        for (int i = 0; i < updateWaves.Length; i++)
        {
            while (GameManager.Instance.state != GameManager.State.Playing) { yield return null; }
            //yield return new WaitForSeconds(updateWaves[i].delay);

            GameManager.Instance.vecBallHitPath = updateWaves[i].velocityContactPath;
            GameManager.Instance.rateRotate = updateWaves[i].rateRotate;
            GameManager.Instance.speedBallAllow = updateWaves[i].speedBallAllow;

            path1Weight = updateWaves[i].path1Weight;
            path2Weight = updateWaves[i].path2Weight;
            path3Weight = updateWaves[i].path3Weight;

            UpdatePathAndWeight();

            while (pathCounter <= updateWaves[i].spawnPaths) { yield return null; }

            currentLevel++;
            if (currentLevel <= updateWaves.Length)
            {
                pathCounter = 0;
            }
        }
    }

    public void IPlay()
    {
        StartCoroutine(CreatePathCo());
        StartCoroutine(SetVelocityCo());
    }

    public void ISuccess()
    {
        //throw new System.NotImplementedException();
    }

    public void IPause()
    {
        //throw new System.NotImplementedException();
    }

    public void IUnPause()
    {
        //throw new System.NotImplementedException();
    }

    public void IGameOver()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnRespawn()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOn()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOff()
    {
        //throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class WeightedObject
{
    public GameObject gameObject;
    [Range(0, 1f)]
    public float weight = 0.5f;
}

[System.Serializable]
public class UpdateWave
{
    public Vector3 velocityContactPath;
    public float rateRotate = 15;
    public float speedBallAllow = 15;
    [Range(0,1)]
    public float path1Weight = 1;
    [Range(0, 1)]
    public float path2Weight = 1;
    [Range(0, 1)]
    public float path3Weight = 1;
    public int spawnPaths = 30;
}

//xxx
