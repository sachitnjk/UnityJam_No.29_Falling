using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    [SerializeField] float score;

    public float GetScore(){
        return score;
    }
}
