using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PossiblePositionDirections
{
    
    public GameObject position;
    public GameObject[] nextPosition;

    public PossiblePositionDirections(GameObject position, params GameObject[] nextPosition)
    {
        this.position = position;
        this.nextPosition = nextPosition;
    }
}
