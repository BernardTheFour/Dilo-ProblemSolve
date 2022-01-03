using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator Instance;

    [Header("Coint")]
    [SerializeField] private GameObject CointPrefab = default;
    [SerializeField] private RangeNumber CointRandomRange = default;
    [SerializeField] private int CointCooldown = 3;

    [Header("Telepoint")]
    [SerializeField] private GameObject TelepointPrefab = default;
    [SerializeField] private RangeNumber TelepointRandomRange = default;
    [SerializeField] private int TelepointCooldown = 10;

    [Space(5)]
    [Header("Limit")]
    [SerializeField] private Transform TopLimit = default;
    [SerializeField] private Transform BottomLimit = default;
    [SerializeField] private Transform LeftLimit = default;
    [SerializeField] private Transform RightLimit = default;

    public static ObjectPool _cointPool;
    public static ObjectPool _telepointPool;

    private RangeNumber HorzLimit, VertLimit;
    private Transform _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _cointPool = new ObjectPool(CointPrefab);
        _telepointPool = new ObjectPool(TelepointPrefab);

        // 0.3f is an offset
        HorzLimit = new RangeNumber(LeftLimit.position.x + 0.3f, RightLimit.position.x - 0.3f);
        VertLimit = new RangeNumber(BottomLimit.position.y + 0.3f, TopLimit.position.y - 0.3f); 

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        // Fill the pool
        for (int i = 0; i < (int)Random.Range(CointRandomRange.start, CointRandomRange.end); i++)
        {
            PlaceObject(_cointPool.GenerateNew(true).transform, _cointPool.GetList());
        }

        for (int i = 0; i < (int)Random.Range(TelepointRandomRange.start, TelepointRandomRange.end); i++)
        {
            PlaceObject(_telepointPool.GenerateNew(true).transform, _telepointPool.GetList());
        }
    }

    private void PlaceObject(Transform @object, List<GameObject> poolList)
    {
        // Randomize position
        Vector2 position = RandomizePosition();

        // Check whether it was overlapped with another coint
        foreach (GameObject member in poolList)
        {
            // If overlapped, randomize again
            if (Vector2.Distance(position, _player.position) < 2 ||
                Vector2.Distance(position, member.transform.position) < 1)
            {
                position = RandomizePosition();
            }
        }
        @object.position = new Vector2(position.x, position.y);
    }

    private Vector2 RandomizePosition()
    {
        float x = Random.Range(HorzLimit.start, HorzLimit.end);
        float y = Random.Range(VertLimit.start, VertLimit.end);

        return new Vector2(x, y);
    }

    public void Regenerate(GameObject @object)
    {
        @object.SetActive(false);

        if (_cointPool.GetList().Contains(@object))
        {
            StartCoroutine(RegenerateCooldown(@object, _cointPool.GetList(), CointCooldown));
        }
        else if (_telepointPool.GetList().Contains(@object))
        {
            StartCoroutine(RegenerateCooldown(@object, _cointPool.GetList(), TelepointCooldown));
        }
    }

    private IEnumerator RegenerateCooldown(GameObject @object, List<GameObject> poolList, int cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        @object.SetActive(true);
        PlaceObject(@object.transform, poolList);
    }
}