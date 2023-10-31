using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : Explosion
{
    public CubeColor color;
    // Start is called before the first frame update
    protected override void Start()
    {
        color = Random.Range(0, 2) == 0 ? CubeColor.Blue : CubeColor.Yellow;
        GetComponent<Renderer>().material = AssetsHandler.Instance.cubeMaterials[(int)color];
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;

        var value = CubeSpawner.Instance.cubes.Find(this);
        if (value?.Previous?.Previous is null)
            return;

        if (value.Previous.Value.color == color && value.Previous.Previous.Value.color == color)
        {
            CubeSpawner.Instance.DestroyCube(value.Value, value.Previous.Value, value.Previous.Previous.Value);
            Explode();
        }


        
    }
}
