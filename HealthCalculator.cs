using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class HealthCalculator : MonoBehaviour
{
    //Inspector
    [Header("Your Character, Use all caps for race and class!")]
    public int constitution;//use
    public int level;//use
    public string race;//key
    public string Name;//for printing, class reserved
    public string Class; //key, class reserved

    [Header("Feats")] //use in final calculations
    public bool stout;
    public bool tough;

    [Header("Health")] //use to determine final calculation method
    public bool averaged;
    public bool rolled;
    
    //dictionary of dictionaries
    Dictionary <string,Dictionary<string,int>> Data = new Dictionary < string,Dictionary<string,int>>
    {
        {
            "Races", new Dictionary<string,int>
                {
                    {"DRAGONBORN", 0},
                    {"DWARF",      2},
                    {"ELF",        0},
                    {"GNOME",      0},
                    {"GOLIATH",    1},
                    {"HALFLING",   0},
                    {"HUMAN",      0},
                    {"ORC",        1},
                    {"TIEFLING",   0}
                }
        },
        {
            "Classes", new Dictionary<string,int>
                {
                    {"ARTIFICER",  8},
                    {"BARBARIAN", 12},
                    {"BARD",       8},
                    {"CLERIC",     8},
                    {"DRUID",      8},
                    {"FIGHTER",   10},
                    {"MONK",       8},
                    {"RANGER",    10},
                    {"ROGUE",      8},
                    {"PALADIN",   10},
                    {"SORCERER",   6},
                    {"WIZARD",     6},
                    {"WARLOCK",    8}
                }
        }
    };

 //Data["key1"]["key2"]

    int Calculator(bool Averaged,bool Stout, bool Tough, string Class, string Race, int Level)
    {
        int stoutnum = 0;
        int toughnum = 0;
        int hp;

        if(Tough) //if true
        {
            toughnum = 2;
        }
        if(Stout) //if true
        {
            stoutnum = 1;
        }

        if (Averaged)
        {
            return hp = (Data["Classes"][Class]/2 + 1) + (Data["Races"][Race]*Level) + Level + (toughnum*Level)+ (stoutnum*Level); //averaged and all other stuff is added
        }

        else //rolled
        {
            return hp = Random.Range(1,Data["Classes"][Class]) + (Data["Races"][Race]*Level) + Level + (toughnum*Level)+ (stoutnum*Level); //randomized and added
        }
    }


 
    void Start()
    {
        if(!Data["Races"].ContainsKey(race)||!Data["Classes"].ContainsKey(Class))
        {
            Debug.Log("Please use a valid Class or Race, and use ALL CAPS");
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
        if(averaged && rolled )
        {
            Debug.Log("Please pick averaged OR rolled.");
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
        if(level > 20)
        {
           Debug.Log("Max level is 20!");
            UnityEditor.EditorApplication.isPlaying = false; 
            return;
        }

        int hitpoints = Calculator(averaged,stout,tough,Class,race,level);

        //Stout or Tough logic, could be function
        string Has = "does not have";
        string Stough = "stout or tough";
        if(stout || tough)
        {       
            Has = "has";
            if(stout && tough)
            {
                Has = "has both";
                Stough = "Stout and Tough";
            }

            else if(stout)
            {
                Stough = "stout";
            }
            
            else
            {
                Stough = "tough";
            }
        }
        //using string interpolation to print
       Debug.Log( $"your character,{Name} ,a level {level} {race} {Class} with a CON score of {constitution} and {Has} {Stough} has {hitpoints} hitpoints.");
    }

}
