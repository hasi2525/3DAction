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
                //�o�ꉉ�o���̏���������
                break;
            case BossState.Battle:
                //�퓬���̏���������
                break;
            case BossState.ClearEnsyutu:
                //�N���A���̏���������
                break;
        }
    }
}