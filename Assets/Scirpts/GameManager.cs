using Scirpts.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scirpts
{
    public class GameManager : Singleton<GameManager>
    {


        public void RestarBtn()
        {
            SceneManager.LoadScene(0);
        }
        
        
    }
}
