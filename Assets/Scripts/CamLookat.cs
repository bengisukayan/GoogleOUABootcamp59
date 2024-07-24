using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookat : MonoBehaviour
{
    public Transform player, cameraTrans;
	
	void Update(){
		cameraTrans.LookAt(player);
	}
}