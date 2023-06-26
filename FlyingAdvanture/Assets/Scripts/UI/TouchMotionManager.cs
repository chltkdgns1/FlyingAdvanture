using UnityEngine;

public class TouchMotionManager : MonoSingleTon<TouchMotionManager>
{
    GameObject touchMotionPrefabs;
    GameObject[] touchMotionPool;

    int poolIndex = 0;
    int poolSize = 10;

    int index;

    protected override void Init()
    {
        touchMotionPrefabs = Resources.Load("Prefabs/UI/Touch") as GameObject;
    }

    private void Awake()
    {
        CreateObjectPool();
    }

    public void Start()
    {
        index = TouchScreen.Instance.AddEvent(OnTCEvent);
    }

    public void OnTCEvent(Vector3 pos)
    {
        CreateObjectPool();
        StartTouchMotion(pos);
    }

    public void StartTouchMotion(Vector3 position)
    {
        if (touchMotionPool[poolIndex] == null)
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            if (canvasObject == null)
            {
                Debug.LogError("Canvas does not found");
                return;
            }

            touchMotionPool[poolIndex] = Instantiate(touchMotionPrefabs, canvasObject.transform);
            touchMotionPool[poolIndex].SetActive(false);
        }

        touchMotionPool[poolIndex].transform.position = position;
        touchMotionPool[poolIndex].SetActive(true);
        touchMotionPool[poolIndex].transform.SetAsLastSibling();
        poolIndex++;
        poolIndex %= poolSize;
    }

    void CreateObjectPool()
    {
        if (touchMotionPrefabs == null)
        {
            Debug.LogError("touchMotionPrefabs is null");
            return;
        }

        GameObject canvasObject = GameObject.Find("Canvas");
        if (canvasObject == null)
        {
            Debug.LogError("Canvas does not found");
            return;
        }

        if (touchMotionPool.Length > 0)
            return;

        if (touchMotionPool == null)
            touchMotionPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            touchMotionPool[i] = Instantiate(touchMotionPrefabs, canvasObject.transform);
            touchMotionPool[i].SetActive(false);
        }
    }

    public void OnStart() { }
}
