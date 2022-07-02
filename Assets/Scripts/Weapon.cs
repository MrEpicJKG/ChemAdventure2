using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform barrelTipObj;
    public float shotDamagePerBullet = 5;
    public float shotAcc = 2; //lower = better acc
    public float shotRange = 20;
    public float shotNoise = 10; //lower = quieter
    public float numBulletsShot = 1;
    public int bulletsPerFullMag = 20;
    public int magsLeft = 2;
    public int bulletsLeftInCurrMag = 20;



    public WeaponType weaponType = WeaponType.SemiPistol;
    public enum WeaponType { MeleeLight, MeleeHeavy, SemiPistol, SMG, Shotgun, Rifle, BullpupRifle, LMG, RPG, Minigun };

    public Transform bulletFarEndObj;

    private Animator myAnim;

	private void Start()
	{
        myAnim = GameObject.Find("UpperBody").GetComponent<Animator>();
	}

	[HideInInspector] public void Attack()
    {
        if(weaponType == WeaponType.MeleeLight || weaponType == WeaponType.MeleeHeavy)
        {
            print("Melee Attack Triggered");
		}
        else //ranged (gun)
        {
            if(bulletsLeftInCurrMag > 0)
            {
                bulletsLeftInCurrMag--;
                for (int i = 0; i < numBulletsShot; i++)
                {
                    bulletFarEndObj.localPosition = Vector3.zero;
                    bulletFarEndObj.gameObject.SetActive(true);
                    bulletFarEndObj.Translate(Random.Range(-shotAcc / 10, shotAcc / 10), shotRange, 0, Space.Self);

                    RaycastHit2D hitInfo = Physics2D.Linecast(barrelTipObj.position, bulletFarEndObj.position, LayerMask.NameToLayer("Monsters"), -100, 100);
                    if (hitInfo.collider.CompareTag("Monster") == true)
                    {
                        hitInfo.transform.gameObject.GetComponent<Enemy>().RecieveDamage(shotDamagePerBullet);
                    }

                    bulletFarEndObj.gameObject.SetActive(false);
                }
			}

            
		}
	}
}
