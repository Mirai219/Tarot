using TMPro;
using UnityEngine;

public class ShowSkill : MonoBehaviour
{
    public TMP_Text skillText;
    public GameObject skill;
    public GameObject square;

    void Start()
    {
        
    }

    int x = 0;
    void Update()
    {   
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero);

            if (ray.collider != null)
            {
                if (ray.collider.tag == "skill")
                {
                    switch (ray.collider.name) 
                    {
                        case "skill1":                            x = 0;                            break;
                        case "skill2":                            x = 1;                            break;
                        case "skill3":                            x = 2;                            break;
                        case "skill4":                            x = 3;                            break;
                        case "skill5":                            x = 4;                            break;
                        case "skill6":                            x = 5;                            break;

                    }
                   if(Input.GetMouseButton(0))
                {
                    if(FightProgress.skillsChosenByPlayer[x].tempPower ==FightProgress.skillsChosenByPlayer[x].Maxpower)
                    {
                        FightProgress.skillsChosenByPlayer[x].startSkill=true;
                    }
                }
                }
            }
        square.transform.localScale = new Vector3((float)FightProgress.skillsChosenByPlayer[x].tempPower / (float)FightProgress.skillsChosenByPlayer[x].Maxpower, 1, 1);
        skillText.text = FightProgress.skillsChosenByPlayer[x].name +": "+ FightProgress.skillsChosenByPlayer[x].description;
    }
}
