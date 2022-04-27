using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DealDamage(int _damages)
    {
        //Change 400 with tier 1 damages
        if (_damages < 400)
            return;

        Hit(_damages);
    }
}
