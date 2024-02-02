using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    private enum BossState 
    {
        StartEnsyutu,  
        Battle,
        ClearEnsyutu,
    }

    private BossState nowState = BossState.StartEnsyutu;
    void Start()
    {
        
    }

    void Update()
    {
        switch (nowState)  
        {
            case BossState.StartEnsyutu:
                //登場演出時の処理を書く
                break;
            case BossState.Battle:
                //戦闘中の処理を書く
                break;
            case BossState.ClearEnsyutu:
                //クリア時の処理を書く
                break;
        }
    }
}