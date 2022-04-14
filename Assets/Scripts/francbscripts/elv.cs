using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elv : MonoBehaviour
{
    public GameObject[] pos; 
    public GameObject t;
    public float speed = 1,cur_pro=0;
  //  public int max = 0;
    int reqsted_pos, curr_pos;
     public string start_pos;
    bool go=false;
    public string end_pos_nm;
    // Start is called before the first frame update
    void Start()


    {
        get_started();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs   ( Vector3.Magnitude(t.transform.position - pos[reqsted_pos].transform.position) )> 0.01f  && go == true)
        {
            cur_pro += speed;
            t.transform.position = new Vector3(Mathf.Lerp(pos[curr_pos].transform.position.x, pos[reqsted_pos].transform.position.x, cur_pro),
                Mathf.Lerp(pos[curr_pos].transform.position.y, pos[reqsted_pos].transform.position.y, cur_pro),
               Mathf.Lerp(pos[curr_pos].transform.position.z, pos[reqsted_pos].transform.position.z, cur_pro));
        }
        else 
        { 
        //t.transform.position=pos[reqsted_pos].transform.position;
            //go = false;
           // curr_pos = reqsted_pos;
            cur_pro = 0;

        }
    }
    public void get_started()
    {
       // nm[pos.Length - 1] = "sds";
       
      //  for (int i = 0; i < pos.Length-1; i++)
       // {
       //     nm[i] = pos[i].name;
       // }

        for (int i = 0; i < pos.Length - 1; i++)
        {
            if (start_pos == pos[i].gameObject.name)
            {
                curr_pos = i;
            }
            else { }
        }

        go = false;


        t.transform.position = pos[curr_pos].transform.position;
        find_pos(end_pos_nm);
    }
    public void find_pos(string k)
    {
        for (int i = 0; i < pos.Length - 1; i++)
        {
            if (k == pos[i].gameObject.name)
            {
                reqsted_pos = i;
            }
            else { }
        }
    }
    public void gotopos()
    {
      //  find_pos(end_pos_nm);   
        go = true;
        

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            gotopos();
        }

    }
}
