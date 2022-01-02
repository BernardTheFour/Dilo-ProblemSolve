using System.Collections;
using System.Linq;
using UnityEngine;

public class CointGenerator : MonoBehaviour
{
    public static CointGenerator Instance;

    [Header("Coint")]
    [SerializeField] private GameObject CointPrefab = default;
    [SerializeField] private RangeNumber CointRandomRange = default;

    [Space(5)]
    [Header("Limit")]
    [SerializeField] private Transform TopLimit = default;
    [SerializeField] private Transform BottomLimit = default;
    [SerializeField] private Transform LeftLimit = default;
    [SerializeField] private Transform RightLimit = default;

    public static ObjectPool _cointPool;

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
            PlaceCoint(_cointPool.GenerateNew(true).transform);
        }
    }

    private void PlaceCoint(Transform coint)
    {
        // Randomize position
        Vector2 position = RandomizePosition();

        // Check whether it was overlapped with another coint
        foreach (GameObject cointMember in _cointPool.GetList())
        {
            // If overlapped, randomize again
            if (Vector2.Distance(position, _player.position) < 2 ||
                Vector2.Distance(position, cointMember.transform.position) < 1)
            {
                position = RandomizePosition();
            }
        }
        coint.position = new Vector2(position.x, position.y);
    }

    private Vector2 RandomizePosition()
    {
        float x = Random.Range(HorzLimit.start, HorzLimit.end);
        float y = Random.Range(VertLimit.start, VertLimit.end);

        return new Vector2(x, y);
    }

    public void Regenerate(GameObject coint)
    {
        coint.SetActive(false);
        StartCoroutine(RegenerateCooldown(coint));
    }

    private IEnumerator RegenerateCooldown(GameObject coint)
    {
        yield return new WaitForSeconds(3);
        coint.SetActive(true);
        PlaceCoint(coint.transform);
    }
}