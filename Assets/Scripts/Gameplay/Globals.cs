using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{

    [Header("Sky Data")]
    public GameObject sky; // sky object
    public float skySpeed; // scrolling speed
    public float skyStartingPosition; // starting position

    [Header("Floor Data")]
    public GameObject floor; // floor object
    public Sprite desert; // sprite of a desert
    bool _desertMode; // is in desert mode

    public static Globals instance;

    void Awake()
    {
        instance = this;
        Random.InitState(System.DateTime.Now.Millisecond); // sets randomizer seed
    }

    void Update()
    {
        // Scrolling the sky
        sky.transform.position = new Vector2(sky.transform.position.x - skySpeed, sky.transform.position.y);
        // if sky position is equal or less than the opposite of sky starting position
        // resets sky to default position
        if (sky.transform.position.x <= skyStartingPosition * -1)
            sky.transform.position = new Vector2(skyStartingPosition, sky.transform.position.y);

        // If the player reaches score of 50, speeds up carrots and changes the floor sprite
        if (PlayerStatistics.instance.Score >= 50 && !_desertMode)
        {
            ObjectiveSpawner.instance.fallSpeed *= 1.75f;
            floor.GetComponent<SpriteRenderer>().sprite = desert;
            _desertMode = true;
        }
    }

    public void GameOver()
    {
        EndValues.score = PlayerStatistics.instance.Score;
        SceneManager.LoadScene("ResultScreen");
    }
}
