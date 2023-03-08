using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingShipGunController : MonoBehaviour
{
    ParticleSystem[] particlesInChildren;
    List<GunSystem> gunSystems = new List<GunSystem>();

    int side;
    int enumLength = Enum.GetNames(typeof(Side)).Length;

    // Start is called before the first frame update
    void Start()
    {
        side = 0;

        particlesInChildren = GetComponentsInChildren<ParticleSystem>();
        SetUpGunsystems();
        Debug.Log("Just a random comment");

    }


    //Populate the gunSystem List with the guns
    void SetUpGunsystems()
    {
        for (int i = 0; i < particlesInChildren.Length; i++)
        {
            Side side;

            if (particlesInChildren[i].gameObject.transform.position.x < transform.position.x)
            {
                side = Side.left;
            }
            else
            {
                side = Side.right;
            }

            gunSystems.Add(new GunSystem(particlesInChildren[i], side));
        }
    }


    //Iterate through the GunSystem and determine if the side we triggered through the buttons equals the determinded side 
    public void ToggleShooting()
    {

        side = (side + 1);
        side %= enumLength;


        foreach (GunSystem gunSys in gunSystems)
        {

            if ((int)gunSys.side == side || side == enumLength - 1)
            {
                gunSys.partSys.Play();
            }

            else
            {
                gunSys.partSys.Stop(true);
                gunSys.partSys.Clear();
            }

        }
    }
}

public struct GunSystem
{
    public ParticleSystem partSys;
    public Side side;

    public GunSystem(ParticleSystem _partSys, Side _side)
    {
        partSys = _partSys;
        side = _side;
    }
}

public enum Side
{
    none,
    left,
    right,
    both
}