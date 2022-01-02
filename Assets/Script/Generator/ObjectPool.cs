using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly GameObject @object;
    private List<GameObject> _pool;

    public ObjectPool(GameObject @object)
    {
        this.@object = @object;
        _pool = new List<GameObject>();
    }

    private void Pool(GameObject objectToPool)
    {
        _pool.Add(objectToPool);
    }

    public GameObject GenerateNew(bool usedImmediately)
    {
        GameObject newObject = Object.Instantiate(@object);
        newObject.SetActive(usedImmediately);
        Pool(newObject);
        return newObject;
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject @object in _pool)
        {
            if (!@object.activeSelf)
            {
                @object.SetActive(true);
                return @object;
            }
        }

        // if there are no other free object, create new
        return GenerateNew(true);
    }

    public List<GameObject> GetList()
    {
        return _pool;
    }
}
