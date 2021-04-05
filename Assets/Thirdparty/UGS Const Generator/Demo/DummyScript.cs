/*
 * Unbound Game Studio
 * https://unboundgamestudio.ir/
 * 
 * M.R.Arashiyan
 * m.r.arashiyan@gmail.com
 */

using UnityEngine;
using UGS.Const;
public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("======= Const value of SaveLoad ===========");
        print($@"SaveLoad.Player_Name : {SaveLoad.Player_Name}");
        print($@"SaveLoad.IsInit : {SaveLoad.IsInit}");
        print($@"SaveLoad.Score : {SaveLoad.Score}");
        
        print("======= Const value of Items ===========");
        print($@"Items.PlayerTag : {Items.PlayerTag}");
        print($@"Items.BackwardSpeed : {Items.BackwardSpeed}");
        print($@"Items.isEnabled : {Items.isEnabled}");
        print($@"Items.Scale : {Items.Scale}");

        print("======= Const value of ServerKey ===========");
        print($@"ServerKey.Username : {ServerKey.Username}");
        print($@"ServerKey.Username : {ServerKey.Password}");
    }

}
