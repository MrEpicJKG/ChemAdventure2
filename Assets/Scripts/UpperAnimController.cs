using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpperAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
	        #region Weapon Animations
	/*[HindeInInspector*/ public AnimationClip unarmedIdle;
    /*[HindeInInspector*/ public AnimationClip unarmedPunch;

    //==== Light Melee Anims ====
    /*[HindeInInspector*/ public AnimationClip meleeLightIdleStill;
    /*[HindeInInspector*/ public AnimationClip meleeLightSwingOverhead;
    /*[HindeInInspector*/ public AnimationClip meleeLightSwingUnderhand;
    /*[HindeInInspector*/ public AnimationClip meleeLightIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip meleeLightIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip meleeLightIdleWalk; //extra

    //==== Heavy Melee Anims ====
    /*[HindeInInspector*/ public AnimationClip meleeHeavyIdleStill;
    /*[HindeInInspector*/ public AnimationClip meleeHeavySwingOverhead;
    /*[HindeInInspector*/ public AnimationClip meleeHeavySwingUnderhand;
    /*[HindeInInspector*/ public AnimationClip meleeHeavyIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip meleeHeavyIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip meleeHeavyIdleWalk; //extra

    //==== Pistol Anims ====
    /*[HindeInInspector*/ public AnimationClip pistolIdleStill;
    /*[HindeInInspector*/ public AnimationClip pistolReload;
    /*[HindeInInspector*/ public AnimationClip pistolShot;
    /*[HindeInInspector*/ public AnimationClip pistolIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip pistolIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip pistolIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip pistolIdleWalk; //extra

    //==== SMG Anims ====
    /*[HindeInInspector*/ public AnimationClip smgIdleStill;
    /*[HindeInInspector*/ public AnimationClip smgReload;
    /*[HindeInInspector*/ public AnimationClip smgShot;
    /*[HindeInInspector*/ public AnimationClip smgIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip smgIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip smgIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip smgIdleWalk; //extra

    //==== Rifle Anims ====
    /*[HindeInInspector*/ public AnimationClip rifleIdleStill;
    /*[HindeInInspector*/ public AnimationClip rifleReload;
    /*[HindeInInspector*/ public AnimationClip rifleShot;
    /*[HindeInInspector*/ public AnimationClip rifleIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip rifleIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip rifleIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip rifleIdleWalk; //extra

    //==== Bullpup Anims ====
    /*[HindeInInspector*/ public AnimationClip bullpupIdleStill;
    /*[HindeInInspector*/ public AnimationClip bullpupReload;
    /*[HindeInInspector*/ public AnimationClip bullpupShot;
    /*[HindeInInspector*/ public AnimationClip bullpupIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip bullpupIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip bullpupIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip bullpupIdleWalk; //extra
    //38
    //==== Shotgun Anims ====
    /*[HindeInInspector*/ public AnimationClip shotgunIdleStill;
    /*[HindeInInspector*/ public AnimationClip shotgunReloadShell;
    /*[HindeInInspector*/ public AnimationClip shotgunReloadClip;
    /*[HindeInInspector*/ public AnimationClip shotgunPump;
    /*[HindeInInspector*/ public AnimationClip shotgunShot;
    /*[HindeInInspector*/ public AnimationClip shotgunIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip shotgunIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip shotgunIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip shotgunIdleWalk; //extra

    //==== RPG Anims ====
    /*[HindeInInspector*/ public AnimationClip rpgIdleStill;
    /*[HindeInInspector*/ public AnimationClip rpgReload;
    /*[HindeInInspector*/ public AnimationClip rpgShot;
    /*[HindeInInspector*/ public AnimationClip rpgIdleCrouch; //extra
    /*[HindeInInspector*/ public AnimationClip rpgIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip rpgIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip rpgIdleWalk; //extra

    //==== Minigun Anims ====
    /*[HindeInInspector*/ public AnimationClip minigunIdleStill;
    /*[HindeInInspector*/ public AnimationClip minigunReload;
    /*[HindeInInspector*/ public AnimationClip minigunShot;
    /*[HindeInInspector*/ public AnimationClip minigunIdleGunUp; //extra
    /*[HindeInInspector*/ public AnimationClip minigunIdleRun; //extra
    /*[HindeInInspector*/ public AnimationClip minigunIdleWalk; //extra
    #endregion

    [HideInInspector] public bool isMeleeHeavy = false;
    [HideInInspector] public int ammoLeft = 0;
    [HideInInspector] public int clipsLeft = 0;
    [HideInInspector] public bool isPumpShotgun = false;
    [HideInInspector] public bool canAttk = true;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isCrouched = false;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isAirborne = false;
    [HideInInspector] public float secsSinceLastShot = 0;

    [HideInInspector] public enum WeapType { Unarmed, LightMelee, HeavyMelee, Pistol, SMG, Rifle, Bullpup, Shotgun, RPG, Minigun};
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //Update is called once per frame

    //void Update()
    //{

    //}

    public void PlayAnim(WeapType weapType, bool isAttacking, bool shouldReload = false)//, bool loop = false, float speedMult = 1)
    {
        animator.SetBool("isMeleeHeavy", isMeleeHeavy);
        animator.SetInteger("ammoLeft", ammoLeft);
        animator.SetBool("isPumpShotgun", isPumpShotgun);
        animator.SetBool("shouldReload", shouldReload);
        animator.SetInteger("clipsLeft", clipsLeft);
        animator.SetBool("canAttk", canAttk);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isCrouched", isCrouched);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isAiorborne", isAirborne);
        animator.SetFloat("secsSinceLastShot", secsSinceLastShot);

        if ((weapType == WeapType.LightMelee || weapType == WeapType.HeavyMelee) & isAttacking == true)
        {
            int rand = Random.Range(0, 2); //return either 0 or 1
            if (rand == 0)
            {
                animator.SetBool("shouldMeleeSwingUnderhand", false);
            }
            else
            {
                animator.SetBool("shouldMeleeSwingUnderhand", true);
			}
        }

        if (weapType != WeapType.Unarmed)
        {
            animator.SetBool("hasAnyuWeapEquipped", true);
        }
        else
        {
            animator.SetBool("hasAnyWeapEquipped", false);
        }

        if (weapType == WeapType.Unarmed || weapType == WeapType.LightMelee || weapType == WeapType.HeavyMelee)
		{
            animator.SetInteger("equippedWeaponType", 0);
		}
        else
        {
            animator.SetInteger("equippedWeaponType", 1);
		}

        if (weapType == WeapType.Pistol)
        {
            animator.SetInteger("gunType", 0);
        }
        else if (weapType == WeapType.SMG)
        {
            animator.SetInteger("gunType", 1);
        }
        else if (weapType == WeapType.Shotgun)
        {
            animator.SetInteger("gunType", 2);
        }
        else if(weapType == WeapType.Rifle)
        {
            animator.SetInteger("gunType", 3);
		}
        else if(weapType == WeapType.Bullpup)
        {
            animator.SetInteger("gunType", 4);
		}
        else if(weapType == WeapType.RPG)
        {
            animator.SetInteger("gunType", 5);
		}
        else if(weapType == WeapType.Minigun)
        {
            animator.SetInteger("gunType", 6);
		}
	}
}
