using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;

    protected override void Start()
    {
        //MobStatusクラスのStartを継承
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }
    protected override void OnDie()
    {
        //倒された時の消滅コルーチン
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }
    //２秒後に消える
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}