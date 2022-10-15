using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordController : Obstacable
{
    //Sword gelisimleri var.
    //Sword etki alani.
    //Sword savurma hizi.
    internal override void DoAction(EnemyTrigger enemy)
    {
        //GameManager.Instance.EnemyCounter--;
        if(GameManager.Instance.canSwing)
        {
            //UpdateManagers();
            //

                //!!!!!ENEMY ILE ILGILI BIR SEYE ULASAMIYORUM.

            //
            /*if(!enemy.hit)
            {
                enemy.hit=true;
                enemy.GetComponent<MeshRenderer>().material.color=Color.grey;
            }*/
            //enemy.gameObject.SetActive(false);
        }
        
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
