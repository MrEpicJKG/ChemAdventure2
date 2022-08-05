using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(UpperAnimController))]
[CanEditMultipleObjects]
[ExecuteAlways]
public class UpperAnimControllerEditor : Editor
{
    private bool showAnims = true;
    private bool showUnarmed = true;
    private bool showLightMelee = true;
    private bool showHeavyMelee = true;
    private bool showPistol = true;
    private bool showSMG = true;
    private bool showRifle = true;
    private bool showBullpup = true;
    private bool showShotgun = true;
    private bool showRPG = true;
    private bool showMinigun = true;

    private bool showExtraAnims = true;

	#region Weapon Anim Props
	SerializedProperty clip_UnarmedIdle;
    SerializedProperty clip_UnarmedPunch;

    //==== Light Melee Anims ====
    SerializedProperty clip_MeleeLightIdleStill;
    SerializedProperty clip_MeleeLightSwingOverhead;
    SerializedProperty clip_MeleeLightSwingUnderhand;
    SerializedProperty clip_MeleeLightIdleCrouch; //extra
    SerializedProperty clip_MeleeLightIdleRun; //extra
    SerializedProperty clip_MeleeLightIdleWalk; //extra

    //==== Heavy Melee Anims ====
    SerializedProperty clip_MeleeHeavyIdleStill;
    SerializedProperty clip_MeleeHeavySwingOverhead;
    SerializedProperty clip_MeleeHeavySwingUnderhand;
    SerializedProperty clip_MeleeHeavyIdleCrouch; //extra
    SerializedProperty clip_MeleeHeavyIdleRun; //extra
    SerializedProperty clip_MeleeHeavyIdleWalk; //extra

    //==== Pistol Anims ====
    SerializedProperty clip_PistolIdleStill;
    SerializedProperty clip_PistolReload;
    SerializedProperty clip_PistolShot;
    SerializedProperty clip_PistolIdleCrouch; //extra
    SerializedProperty clip_PistolIdleGunUp; //extra
    SerializedProperty clip_PistolIdleRun; //extra
    SerializedProperty clip_PistolIdleWalk; //extra

    //==== SMG Anims ====
    SerializedProperty clip_SMGIdleStill;
    SerializedProperty clip_SMGReload;
    SerializedProperty clip_SMGShot;
    SerializedProperty clip_SMGIdleCrouch; //extra
    SerializedProperty clip_SMGIdleGunUp; //extra
    SerializedProperty clip_SMGIdleRun; //extra
    SerializedProperty clip_SMGIdleWalk; //extra

    //==== Rifle Anims ====
    SerializedProperty clip_RifleIdleStill;
    SerializedProperty clip_RifleReload;
    SerializedProperty clip_RifleShot;
    SerializedProperty clip_RifleIdleCrouch; //extra
    SerializedProperty clip_RifleIdleGunUp; //extra
    SerializedProperty clip_RifleIdleRun; //extra
    SerializedProperty clip_RifleIdleWalk; //extra

    //==== Bullpup Anims ====
    SerializedProperty clip_BullpupIdleStill;
    SerializedProperty clip_BullpupReload;
    SerializedProperty clip_BullpupShot;
    SerializedProperty clip_BullpupIdleCrouch; //extra
    SerializedProperty clip_BullpupIdleGunUp; //extra
    SerializedProperty clip_BullpupIdleRun; //extra
    SerializedProperty clip_BullpupIdleWalk; //extra

    //==== Shotgun Anims ====
    SerializedProperty clip_ShotgunIdleStill;
    SerializedProperty clip_ShotgunReloadShell;
    SerializedProperty clip_ShotgunReloadClip;
    SerializedProperty clip_ShotgunPump;
    SerializedProperty clip_ShotgunShot;
    SerializedProperty clip_ShotgunIdleCrouch; //extra
    SerializedProperty clip_ShotgunIdleGunUp; //extra
    SerializedProperty clip_ShotgunIdleRun; //extra
    SerializedProperty clip_ShotgunIdleWalk; //extra

    //==== RPG Anims ====
    SerializedProperty clip_RPGIdleStill;
    SerializedProperty clip_RPGReload;
    SerializedProperty clip_RPGShot;
    SerializedProperty clip_RPGIdleCrouch; //extra
    SerializedProperty clip_RPGIdleGunUp; //extra
    SerializedProperty clip_RPGIdleRun; //extra
    SerializedProperty clip_RPGIdleWalk; //extra

    //==== Minigun Anims ====
    SerializedProperty clip_MinigunIdleStill;
    SerializedProperty clip_MinigunReload;
    SerializedProperty clip_MinigunShot;
    SerializedProperty clip_MinigunIdleGunUp; //extra
    SerializedProperty clip_MinigunIdleRun; //extra
    SerializedProperty clip_MinigunIdleWalk; //extra
	#endregion

	public void OnEnable()
	{
        //EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth * 0.6f;

        clip_UnarmedIdle = serializedObject.FindProperty("unarmedIdle");
        clip_UnarmedPunch = serializedObject.FindProperty("unarmedPunch");

        clip_MeleeLightIdleStill = serializedObject.FindProperty("meleeLightIdleStill");
        clip_MeleeLightSwingOverhead = serializedObject.FindProperty("meleeLightSwingOverhead");
        clip_MeleeLightSwingUnderhand = serializedObject.FindProperty("meleeLightSwingUnderhand");
        clip_MeleeLightIdleCrouch = serializedObject.FindProperty("meleeLightIdleCrouch");
        clip_MeleeLightIdleRun = serializedObject.FindProperty("meleeLightIdleRun");
        clip_MeleeLightIdleWalk = serializedObject.FindProperty("meleeLightIdleWalk");

        clip_MeleeHeavyIdleStill = serializedObject.FindProperty("meleeHeavyIdleStill");
        clip_MeleeHeavySwingOverhead = serializedObject.FindProperty("meleeHeavySwingOverhead");
        clip_MeleeHeavySwingUnderhand = serializedObject.FindProperty("meleeHeavySwingUnderhand");
        clip_MeleeHeavyIdleCrouch = serializedObject.FindProperty("meleeHeavyIdleCrouch");
        clip_MeleeHeavyIdleRun = serializedObject.FindProperty("meleeHeavyIdleRun");
        clip_MeleeHeavyIdleWalk = serializedObject.FindProperty("meleeHeavyIdleWalk");

        clip_PistolIdleStill = serializedObject.FindProperty("pistolIdleStill");
        clip_PistolReload = serializedObject.FindProperty("pistolReload");
        clip_PistolShot = serializedObject.FindProperty("pistolShot");
        clip_PistolIdleCrouch = serializedObject.FindProperty("pistolIdleCrouch");
        clip_PistolIdleGunUp = serializedObject.FindProperty("pistolIdleGunUp");
        clip_PistolIdleRun = serializedObject.FindProperty("pistolIdleRun");
        clip_PistolIdleWalk = serializedObject.FindProperty("pistolIdleWalk");

        clip_SMGIdleStill = serializedObject.FindProperty("smgIdleStill");
        clip_SMGReload = serializedObject.FindProperty("smgReload");
        clip_SMGShot = serializedObject.FindProperty("smgShot");
        clip_SMGIdleCrouch = serializedObject.FindProperty("smgIdleCrouch");
        clip_SMGIdleGunUp = serializedObject.FindProperty("smgIdleGunUp");
        clip_SMGIdleRun = serializedObject.FindProperty("smgIdleRun");
        clip_SMGIdleWalk = serializedObject.FindProperty("smgIdleWalk");

        clip_RifleIdleStill = serializedObject.FindProperty("rifleIdleStill");
        clip_RifleReload = serializedObject.FindProperty("rifleReload");
        clip_RifleShot = serializedObject.FindProperty("rifleShot");
        clip_RifleIdleCrouch = serializedObject.FindProperty("rifleIdleCrouch");
        clip_RifleIdleGunUp = serializedObject.FindProperty("rifleIdleGunUp");
        clip_RifleIdleRun = serializedObject.FindProperty("rifleIdleRun");
        clip_RifleIdleWalk = serializedObject.FindProperty("rifleIdleWalk");

        clip_BullpupIdleStill = serializedObject.FindProperty("bullpupIdleStill");
        clip_BullpupReload = serializedObject.FindProperty("bullpupReload");
        clip_BullpupShot = serializedObject.FindProperty("bullpupShot");
        clip_BullpupIdleCrouch = serializedObject.FindProperty("bullpupIdleCrouch");
        clip_BullpupIdleGunUp = serializedObject.FindProperty("bullpupIdleGunUp");
        clip_BullpupIdleRun = serializedObject.FindProperty("bullpupIdleRun");
        clip_BullpupIdleWalk = serializedObject.FindProperty("bullpupIdleWalk");

        clip_ShotgunIdleStill = serializedObject.FindProperty("shotgunIdleStill");
        clip_ShotgunReloadShell = serializedObject.FindProperty("shotgunReloadShell");
        clip_ShotgunReloadClip = serializedObject.FindProperty("shotgunReloadClip");
        clip_ShotgunPump = serializedObject.FindProperty("shotgunPump");
        clip_ShotgunShot = serializedObject.FindProperty("shotgunShot");
        clip_ShotgunIdleCrouch = serializedObject.FindProperty("shotgunIdleCrouch");
        clip_ShotgunIdleGunUp = serializedObject.FindProperty("shotgunIdleGunUp");
        clip_ShotgunIdleRun = serializedObject.FindProperty("shotgunIdleRun");
        clip_ShotgunIdleWalk = serializedObject.FindProperty("shotgunIdleWalk");

        clip_RPGIdleStill = serializedObject.FindProperty("rpgIdleStill");
        clip_RPGReload = serializedObject.FindProperty("rpgReload");
        clip_RPGShot = serializedObject.FindProperty("rpgShot");
        clip_RPGIdleCrouch = serializedObject.FindProperty("rpgIdleCrouch");
        clip_RPGIdleGunUp = serializedObject.FindProperty("rpgIdleGunUp");
        clip_RPGIdleRun = serializedObject.FindProperty("rpgIdleRun");
        clip_RPGIdleWalk = serializedObject.FindProperty("rpgIdleWalk");

        clip_MinigunIdleStill = serializedObject.FindProperty("minigunIdleStill");
        clip_MinigunReload = serializedObject.FindProperty("minigunReload");
        clip_MinigunShot = serializedObject.FindProperty("minigunShot");
        clip_MinigunIdleGunUp = serializedObject.FindProperty("minigunIdleGunUp");
        clip_MinigunIdleRun = serializedObject.FindProperty("minigunIdleRun");
        clip_MinigunIdleWalk = serializedObject.FindProperty("minigunIdleWalk");



    }

	public override void OnInspectorGUI()
	{
        serializedObject.Update();
        float mult = 0.6f;
        if(EditorGUIUtility.currentViewWidth > 600)
        {
            mult = 0.4f;
		}
        else if(EditorGUIUtility.currentViewWidth > 400)
        {
            mult = 0.5f;
		}
		EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth * mult;

		EditorGUILayout.LabelField("Animation Clips", EditorStyles.boldLabel);
        showExtraAnims = EditorGUILayout.ToggleLeft("Show Extra Anims", showExtraAnims);
        showAnims = EditorGUILayout.ToggleLeft("Show Anim Fields", showAnims);
        if (showAnims == true)
        {

            showUnarmed = EditorGUILayout.BeginFoldoutHeaderGroup(showUnarmed, "Show Unarmed Anims");
            if (showUnarmed == true)
            {
                EditorGUILayout.PropertyField(clip_UnarmedIdle);
                EditorGUILayout.PropertyField(clip_UnarmedPunch);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showLightMelee = EditorGUILayout.BeginFoldoutHeaderGroup(showLightMelee, "Show Light Melee Anims");
            if (showLightMelee == true)
            {
                EditorGUILayout.PropertyField(clip_MeleeLightIdleStill);
                EditorGUILayout.PropertyField(clip_MeleeLightSwingOverhead);
                EditorGUILayout.PropertyField(clip_MeleeLightSwingUnderhand);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_MeleeLightIdleCrouch);
                    EditorGUILayout.PropertyField(clip_MeleeLightIdleRun);
                    EditorGUILayout.PropertyField(clip_MeleeLightIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showHeavyMelee = EditorGUILayout.BeginFoldoutHeaderGroup(showHeavyMelee, "Show Heavy Melee Anims");
            if (showHeavyMelee == true)
            {
                EditorGUILayout.PropertyField(clip_MeleeHeavyIdleStill);
                EditorGUILayout.PropertyField(clip_MeleeHeavySwingOverhead);
                EditorGUILayout.PropertyField(clip_MeleeHeavySwingUnderhand);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_MeleeHeavyIdleCrouch);
                    EditorGUILayout.PropertyField(clip_MeleeHeavyIdleRun);
                    EditorGUILayout.PropertyField(clip_MeleeHeavyIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showPistol = EditorGUILayout.BeginFoldoutHeaderGroup(showPistol, "Show Pistol Anims");
            if (showPistol == true)
            {
                EditorGUILayout.PropertyField(clip_PistolIdleStill);
                EditorGUILayout.PropertyField(clip_PistolReload);
                EditorGUILayout.PropertyField(clip_PistolShot);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_PistolIdleCrouch);
                    EditorGUILayout.PropertyField(clip_PistolIdleGunUp);
                    EditorGUILayout.PropertyField(clip_PistolIdleRun);
                    EditorGUILayout.PropertyField(clip_PistolIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showSMG = EditorGUILayout.BeginFoldoutHeaderGroup(showSMG, "Show SMG Anims");
            if (showSMG == true)
            {
                EditorGUILayout.PropertyField(clip_SMGIdleStill);
                EditorGUILayout.PropertyField(clip_SMGReload);
                EditorGUILayout.PropertyField(clip_SMGShot);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_SMGIdleCrouch);
                    EditorGUILayout.PropertyField(clip_SMGIdleGunUp);
                    EditorGUILayout.PropertyField(clip_SMGIdleRun);
                    EditorGUILayout.PropertyField(clip_SMGIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showRifle = EditorGUILayout.BeginFoldoutHeaderGroup(showRifle, "Show Rifle Anims");
            if (showRifle == true)
            {
                EditorGUILayout.PropertyField(clip_RifleIdleStill);
                EditorGUILayout.PropertyField(clip_RifleReload);
                EditorGUILayout.PropertyField(clip_RifleShot);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_RifleIdleCrouch);
                    EditorGUILayout.PropertyField(clip_RifleIdleGunUp);
                    EditorGUILayout.PropertyField(clip_RifleIdleRun);
                    EditorGUILayout.PropertyField(clip_RifleIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showBullpup = EditorGUILayout.BeginFoldoutHeaderGroup(showBullpup, "Show Bullpup Anims");
            if (showBullpup == true)
            {
                EditorGUILayout.PropertyField(clip_BullpupIdleStill);
                EditorGUILayout.PropertyField(clip_BullpupReload);
                EditorGUILayout.PropertyField(clip_BullpupShot);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_BullpupIdleCrouch);
                    EditorGUILayout.PropertyField(clip_BullpupIdleGunUp);
                    EditorGUILayout.PropertyField(clip_BullpupIdleRun);
                    EditorGUILayout.PropertyField(clip_BullpupIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showShotgun = EditorGUILayout.BeginFoldoutHeaderGroup(showShotgun, "Show Shotgun Anims");
            if (showShotgun == true)
            {
                EditorGUILayout.PropertyField(clip_ShotgunIdleStill);
                EditorGUILayout.PropertyField(clip_ShotgunReloadShell);
                EditorGUILayout.PropertyField(clip_ShotgunReloadClip);
                EditorGUILayout.PropertyField(clip_ShotgunShot);
                EditorGUILayout.PropertyField(clip_ShotgunPump);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_ShotgunIdleCrouch);
                    EditorGUILayout.PropertyField(clip_ShotgunIdleGunUp);
                    EditorGUILayout.PropertyField(clip_ShotgunIdleRun);
                    EditorGUILayout.PropertyField(clip_ShotgunIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showRPG = EditorGUILayout.BeginFoldoutHeaderGroup(showRPG, "Show RPG Anims");
            if (showRPG == true)
            {
                EditorGUILayout.PropertyField(clip_RPGIdleStill);
                EditorGUILayout.PropertyField(clip_RPGReload);
                EditorGUILayout.PropertyField(clip_RPGShot);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_RPGIdleCrouch);
                    EditorGUILayout.PropertyField(clip_RPGIdleGunUp);
                    EditorGUILayout.PropertyField(clip_RPGIdleRun);
                    EditorGUILayout.PropertyField(clip_RPGIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showMinigun = EditorGUILayout.BeginFoldoutHeaderGroup(showMinigun, "Show Minigun Anims");
            if (showMinigun == true)
            {
                EditorGUILayout.PropertyField(clip_MinigunIdleStill);
                EditorGUILayout.PropertyField(clip_MinigunReload);
                EditorGUILayout.PropertyField(clip_MinigunShot);

                if (showExtraAnims == true)
                {
                    EditorGUILayout.PropertyField(clip_MinigunIdleGunUp);
                    EditorGUILayout.PropertyField(clip_MinigunIdleRun);
                    EditorGUILayout.PropertyField(clip_MinigunIdleWalk);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        base.OnInspectorGUI();
	}

}
