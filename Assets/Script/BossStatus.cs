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
                //“oê‰‰o‚Ìˆ—‚ğ‘‚­
                break;
            case BossState.Battle:
                //í“¬’†‚Ìˆ—‚ğ‘‚­
                break;
            case BossState.ClearEnsyutu:
                //ƒNƒŠƒA‚Ìˆ—‚ğ‘‚­
                break;
        }
    }
}