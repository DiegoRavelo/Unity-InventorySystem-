using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLevel : MonoBehaviour, IExperenciable
{
    // Start is called before the first frame update

      [SerializeField] private Image slider;

    public LevelSystem levelSystem ;
    void Awake()
    {

       levelSystem = new LevelSystem();
        
        //Debug.Log(levelSystem.ReturnLevel());

        slider = GameObject.Find("Bar").GetComponent<Image>();

        slider.fillAmount = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        //slider.fillAmount += Time.deltaTime;
        levelSystem.AddExperience(10);        
        slider.fillAmount = levelSystem.Experience/levelSystem.ExperienceToNextLevel;
        

    }

    public void NormalizedExperience()
    {
        slider.fillAmount = levelSystem.Experience/levelSystem.ExperienceToNextLevel;
    }

    public void EventManagerOnExpCollected(int amount)
    {
        print("eyrr");
        levelSystem.AddExperience(amount);
        NormalizedExperience();

    }

    public void OnEnable()
    {
        EventManager.ExpCollected += EventManagerOnExpCollected;

    }

    public void OnDisable()
    {
        EventManager.ExpCollected -= EventManagerOnExpCollected;

    }
}

[System.Serializable] public class LevelSystem
{
    // Start is called before the first frame update
    [SerializeField] private float level;
    [SerializeField] private int experience;

    public int Experience { get { return experience; } set { experience = value; }}
    [SerializeField] private float experienceToNextLevel;

    public float ExperienceToNextLevel { get { return experienceToNextLevel; } set { experienceToNextLevel = value; }}

    
    [SerializeField] private float levelExponent;
    [SerializeField] private float normalizedExperience;

    public LevelSystem()
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
        levelExponent = 1.2f;
        normalizedExperience = 0f;
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        if(experience >= experienceToNextLevel)
        {
            NextLevel();
        }



    }

    public void NextLevel()
    {
        level++;

        experienceToNextLevel = 100*(Mathf.Pow(level, levelExponent));

        experience = 0;
         
    }

    public float ReturnLevel()
    {
        return level;
    }

    public float ReturnNormalizedExperience()
    {
        return experience/experienceToNextLevel;
    }
}
