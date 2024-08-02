using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleOyun : MonoBehaviour
{

    Camera kamera;
    Vector2 baslangic_pozisyonu;


    GameObject[] kutu_dizisi;



    private void OnMouseDrag()
    {

        Vector3 pozisyon = kamera.ScreenToWorldPoint(Input.mousePosition);
        pozisyon.z = 0;
        transform.position = pozisyon;


    }

    void Start()
    {
        kamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        baslangic_pozisyonu = transform.position;

        kutu_dizisi = GameObject.FindGameObjectsWithTag("kutu");
    }

    
    void Update()
    {
       
        if(Input.GetMouseButtonUp(0))
        {

            foreach(GameObject kutu in kutu_dizisi)
            {
                if (kutu.name== gameObject.name)
                {

                    float mesafe = Vector3.Distance(kutu.transform.position, transform.position);

                    if (mesafe <= 1)
                    {

                        transform.position = kutu.transform.position;


                    }

                    else
                    {
                        transform.position = baslangic_pozisyonu;
                    }


                }



            }




        }



    }
}
