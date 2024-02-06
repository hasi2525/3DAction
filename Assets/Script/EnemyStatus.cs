using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;

    protected override void Start()
    {
        //MobStatusƒNƒ‰ƒX‚ÌStart‚ğŒp³
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }
    protected override void OnDie()
    {
        //“|‚³‚ê‚½‚ÌÁ–ÅƒRƒ‹[ƒ`ƒ“
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }
    //‚Q•bŒã‚ÉÁ‚¦‚é
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}