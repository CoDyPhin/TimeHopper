using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    public StarterAssets.ThirdPersonController player;

    public WaveSpawner waveSpawner;

    public SceneSwitch sceneSwitch;

    private bool triggerActive;

    public void performAction(bool death = false)
    {
        bool reduceCurrencies = false;
        string ExtractionStatus = "";
        if(death){
            if(waveSpawner != null){
                reduceCurrencies = true;
                ExtractionStatus = "Extraction sequence completed with errors. Half of the currencies have been lost.";
            }
            else ExtractionStatus = "Successfully completed extraction sequence. All currencies have been extracted.";
        }
    	else{
            ExtractionStatus = "Successfully completed extraction sequence. All currencies have been extracted.";
        }
        ExtractionStatus += "\nColloseum Currency: ";
        if(player.colCurrency >= 0){
            if(reduceCurrencies) player.colCurrency = Mathf.RoundToInt(player.colCurrency * 0.5f);
            ExtractionStatus += player.colCurrency;
            PlayerPrefs.SetInt("ColliseumCurrency", PlayerPrefs.GetInt("ColliseumCurrency") + player.colCurrency);
        }
        else ExtractionStatus += "0";
        ExtractionStatus += "\nForest Currency: ";
        if(player.forCurrency >= 0){
            if(reduceCurrencies) player.forCurrency = Mathf.RoundToInt(player.forCurrency * 0.5f);
            ExtractionStatus += player.forCurrency;
            PlayerPrefs.SetInt("ForestCurrency", PlayerPrefs.GetInt("ForestCurrency") + player.forCurrency);
        }
        else ExtractionStatus += "0";
        ExtractionStatus += "\nFactory Currency: ";
        if(player.facCurrency >= 0){
            if(reduceCurrencies) player.facCurrency = Mathf.RoundToInt(player.facCurrency * 0.5f);
            ExtractionStatus += player.facCurrency;
            PlayerPrefs.SetInt("FactoryCurrency", PlayerPrefs.GetInt("FactoryCurrency") + player.facCurrency);
        }
        else ExtractionStatus += "0";
        ExtractionStatus += "\nRumble Currency: ";
        if(player.rumCurrency >= 0){
            ExtractionStatus += player.rumCurrency;
            PlayerPrefs.SetInt("RumbleCurrency", PlayerPrefs.GetInt("RumbleCurrency") + player.rumCurrency);
        }
        if(waveSpawner == null){
            RumbleSpawner rumbleSpawner = GameObject.Find("RumbleSpawner").GetComponent<RumbleSpawner>();
            if (rumbleSpawner.roundNr != 0){
            string arenaRounds = sceneSwitch.ArenaName+"Rounds";
            int currentRounds = PlayerPrefs.GetInt(arenaRounds);
            PlayerPrefs.SetInt(arenaRounds, Mathf.Max(currentRounds, rumbleSpawner.roundNr));
            }
            if (rumbleSpawner.numKills != 0){
                string arenaKills = sceneSwitch.ArenaName+"Kills";
                int currentKills = PlayerPrefs.GetInt(arenaKills);
                PlayerPrefs.SetInt(arenaKills, Mathf.Max(currentKills, rumbleSpawner.numKills));
            }
            if (rumbleSpawner.timePlayed != 0f){
                string arenaTime = sceneSwitch.ArenaName+"TimePlayed";
                int currentTimePlayed = PlayerPrefs.GetInt(arenaTime);
                PlayerPrefs.SetInt(arenaTime, currentTimePlayed + ((int)rumbleSpawner.timePlayed));
                int currentTimePlayedNew = PlayerPrefs.GetInt("ColliseumTimePlayed");
            }
        }
        else{
            if (waveSpawner.roundNr != 0){
                string arenaRounds = sceneSwitch.ArenaName+"Rounds";
                int currentRounds = PlayerPrefs.GetInt(arenaRounds);
                PlayerPrefs.SetInt(arenaRounds, Mathf.Max(currentRounds, waveSpawner.roundNr));
            }
            if (waveSpawner.numKills != 0){
                string arenaKills = sceneSwitch.ArenaName+"Kills";
                int currentKills = PlayerPrefs.GetInt(arenaKills);
                PlayerPrefs.SetInt(arenaKills, Mathf.Max(currentKills, waveSpawner.numKills));
            }
            if (waveSpawner.timePlayed != 0f){
                string arenaTime = sceneSwitch.ArenaName+"TimePlayed";
                int currentTimePlayed = PlayerPrefs.GetInt(arenaTime);
                PlayerPrefs.SetInt(arenaTime, currentTimePlayed + ((int)waveSpawner.timePlayed));
                int currentTimePlayedNew = PlayerPrefs.GetInt("ColliseumTimePlayed");
            }
        }
        if(ExtractionStatus != ""){
            PlayerPrefs.SetString("ExtractionStatus", ExtractionStatus);
        }
        else{
            PlayerPrefs.SetString("ExtractionStatus", "");
        }
        string arenaAttempts = sceneSwitch.ArenaName+"Attempts";
        PlayerPrefs.SetInt(arenaAttempts, PlayerPrefs.GetInt(arenaAttempts) + 1);
        PlayerPrefs.Save();
        sceneSwitch.setArenaName("Hub");
        sceneSwitch.LoadArenaScene(death);
    }
 
}
