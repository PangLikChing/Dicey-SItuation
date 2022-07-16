using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceSides : MonoBehaviour
{
    public List<GameObject> sides = new List<GameObject>();

    private void Start()
    {
        if(sides.Count == 0)
        {
            foreach (Transform child in transform)
            {
                sides.Add(child.gameObject);
            }
        }
    }

    public GameObject GetHighestSide()
    {
        return sides.OrderByDescending(item => item.transform.position.y).First();
    }
}
