
using UnityEngine;
using GameData.Environment.Entity;

public class CharacterAppearance : MonoBehaviour
{

    private Transform _nosePosition;
    private Transform _mouthPosition;



    // Use this for initialization
    void Start()
    {
        _nosePosition = transform.Find("Head/NosePosition");
        _mouthPosition = transform.Find("Head/MouthPosition");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeAyes(int id)
    {

    }

    public void ChangeNose(int id)
    {
        // Clear old
        foreach (Transform t in _nosePosition)
        {
            if (t != _nosePosition)
                Destroy(t.gameObject);
        }
        // Instantiate new

        GameObject go = (GameObject)Resources.Load("Prefabs/Character/" + SexType.Male + "/Nose/Nose" + id);
        GameObject instance = Instantiate(go, _nosePosition, false);
    }

    public void ChangeMouth(int id)
    {
        // Clear old
        foreach (Transform t in _mouthPosition)
        {
            if (t != _mouthPosition)
                Destroy(t.gameObject);
        }
        // Instantiate new

        GameObject go = (GameObject)Resources.Load("Prefabs/Character/" + SexType.Male + "/Mouth/Mouth" + id);
        GameObject instance = Instantiate(go, _mouthPosition, false);
    }
}
