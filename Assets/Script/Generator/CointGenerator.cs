using UnityEngine;

public class CointGenerator : MonoBehaviour
{
    [Header("Coint")]
    [SerializeField] private GameObject CointPrefab;
    [SerializeField] private RangeNumber CointNumber;

    [Space(5)]
    [Header("Limit")]
    [SerializeField] private Transform TopLimit;
    [SerializeField] private Transform BottomLimit;
    [SerializeField] private Transform LeftLimit;
    [SerializeField] private Transform RightLimit;

    private ObjectPool _cointPool;

    private void Awake()
    {
        _cointPool = new ObjectPool(CointPrefab);
    }

    private void Start()
    {
        // Fill the pool
        for (int i = 0; i < (int)Random.Range(CointNumber.start, CointNumber.end); i++)
        {
            _cointPool.GenerateNew(true);
        }
        
        // Randomize position
        RangeNumber HorzLimit = new RangeNumber(LeftLimit.position.x, RightLimit.position.x);
        RangeNumber VertLimit = new RangeNumber(TopLimit.position.y, BottomLimit.position.y);
        foreach (GameObject coint in _cointPool.GetList())
        {
            RandomizePosition(coint.transform, HorzLimit, VertLimit);
        }
    }

    private void RandomizePosition(Transform transform, RangeNumber HorzLimit, RangeNumber VertLimit)
    {
        float x = Random.Range(HorzLimit.start, HorzLimit.end);
        float y = Random.Range(VertLimit.start, VertLimit.end);

        transform.position = new Vector2(x, y);
    }
}