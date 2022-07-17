using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceSides : MonoBehaviour
{
    public List<Side> sides = new List<Side>();

    private void Start()
    {
        if(sides.Count == 0)
        {
            foreach (Transform child in transform)
            {
                Side side = child.gameObject.GetComponent<Side>();

                if(side != null)
                    sides.Add(side);
            }
        }
    }

    public Side GetHighestSide()
    {
        return sides.OrderByDescending(item => item.transform.position.y).First();
    }
}
