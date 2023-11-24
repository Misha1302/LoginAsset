using System.Linq;
using Tags;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class UiManager : SingletonMb<UiManager>
{
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        // PlayerPrefs.DeleteAll();    
        
        var instance = Instantiate(canvas);

        var input = instance.GetComponentInChildren<InputTag>().GetComponent<TMP_InputField>();
        var submit = instance.GetComponentInChildren<SubmitKeyTag>().GetComponent<Button>();
        var personName = instance.GetComponentInChildren<PersonNameTag>().GetComponent<TMP_Text>();

        if (EnterManager.Entered)
            personName.text = EnterManager.PersonName;

        submit.onClick.AddListener(() =>
        {
            var key = input.text;

            var readOnlySecretKey = KeysManager.Instance.Keys.FirstOrDefault(x => x.Key == key);
            if (readOnlySecretKey != default)
            {
                if (!readOnlySecretKey.Active)
                {
                    personName.text = "Key doesn't active";
                    return;
                }

                EnterManager.Entered = true;
                EnterManager.PersonName = readOnlySecretKey.User;
                personName.text = EnterManager.PersonName;
            }
            else
            {
                personName.text = "Invalid key";
            }
        });
    }
}