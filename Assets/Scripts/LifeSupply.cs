using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LifeSupply : MonoBehaviour
{
    public int lifes;
    public List<Image> lifesIcon;
    public void RemoveLife()
    {
        lifesIcon[lifes - 1].GetComponent<Image>().enabled = false;
        lifes--;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
