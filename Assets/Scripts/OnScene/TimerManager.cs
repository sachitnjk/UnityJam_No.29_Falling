using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float elaspedTime;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     StartTimer();
    // }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isTimerActive){
            elaspedTime += Time.deltaTime;
            GameManager.Instance.elaspedTime = elaspedTime;
        }
    }

    public void ResetTimer(){
        elaspedTime = 0;
    }
}
