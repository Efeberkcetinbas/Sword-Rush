using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : Obstacable
{
    //Sword gelisimleri var.
    //Sword etki alani.
    //Sword savurma hizi.

    internal override void DoAction(EnemyTrigger enemy)
    {
        //GameManager.Instance.EnemyCounter--;
        UpdateManagers();
        enemy.gameObject.SetActive(false);
        Debug.Log("sdasdsadsad");
        //Sword enemy'i kesiyor.
        //Score Update Ediyor.
        //Progress arttiriyor.
    }

    private void UpdateManagers()
    {
        GameManager.Instance.ProgressValue+=1/(float)GameManager.Instance.EnemyCounter;
        UIManager.Instance.SetProgressbar(GameManager.Instance.ProgressValue);
    }
}
