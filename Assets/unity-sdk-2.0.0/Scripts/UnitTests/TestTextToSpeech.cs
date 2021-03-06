/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using FullSerializer;
using System;
using System.IO;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;

namespace IBM.Watson.DeveloperCloud.UnitTests
{
    public class TestTextToSpeech : UnitTest
    {
        //private string _username = null;
        //private string _password = null;
        //private fsSerializer _serializer = new fsSerializer();
        //private string _token = "<authentication-token>";

        TextToSpeech _textToSpeech;
        public static string text = "Oi";
        public TestTextToSpeech()
        {

        }
        public TestTextToSpeech(string textExternal)
        {
            text = textExternal;
        }
        //public TestTextToSpeech teste = new TestTextToSpeech("Bom dia");
        //string _testString = "<speak version=\"1.0\"><express-as type=\"GoodNews\">Hi, my name is Adam, nice to meet you.</express-as></speak>";
        string _testString = "<speak version=\"1.0\">"+ text + "</speak>"; //"< speak version=\"1.0\"><say-as interpret-as=\"letters\">I'm sorry</say-as>. <prosody pitch=\"150Hz\">This is Text to Speech!</prosody><express-as type=\"GoodNews\">I'm sorry. This is Text to Speech!</express-as></speak>";
        //string _testConversationString = "<phoneme alphabet=\\\"ipa\\\" ph=\\\"\\u02c8\\u025b\\u0259\\u02c8\\u0279\\u0259n\\\">Arin</phoneme>";
        /*
        string _createdCustomizationId;
        CustomVoiceUpdate _customVoiceUpdate;
        string _customizationName = "unity-example-customization";
        string _customizationLanguage = "en-US";
        string _customizationDescription = "A text to speech voice customization created within Unity.";
        string _testWord = "Watson";
        */
        private bool _synthesizeTested = false;
        /*
        private bool _synthesizeConversationTested = false;
        private bool _getVoicesTested = false;
        private bool _getVoiceTested = false;
        private bool _getPronuciationTested = false;
        private bool _getCustomizationsTested = false;
        private bool _createCustomizationTested = false;
        private bool _deleteCustomizationTested = false;
        private bool _getCustomizationTested = false;
        private bool _updateCustomizationTested = false;
        private bool _getCustomizationWordsTested = false;
        private bool _addCustomizationWordsTested = false;
        private bool _deleteCustomizationWordTested = false;
        private bool _getCustomizationWordTested = false;
        */
        public override IEnumerator RunTest()
        {
            //LogSystem.InstallDefaultReactors();
            /*
            try
            {
                VcapCredentials vcapCredentials = new VcapCredentials();
                fsData data = null;

                //  Get credentials from a credential file defined in environmental variables in the VCAP_SERVICES format. 
                //  See https://www.ibm.com/watson/developercloud/doc/common/getting-started-variables.html.
                var environmentalVariable = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                var fileContent = File.ReadAllText(environmentalVariable);

                //  Add in a parent object because Unity does not like to deserialize root level collection types.
                fileContent = Utility.AddTopLevelObjectToJson(fileContent, "VCAP_SERVICES");

                //  Convert json to fsResult
                fsResult r = fsJsonParser.Parse(fileContent, out data);
                if (!r.Succeeded)
                    throw new WatsonException(r.FormattedMessages);

                //  Convert fsResult to VcapCredentials
                object obj = vcapCredentials;
                r = _serializer.TryDeserialize(data, obj.GetType(), ref obj);
                if (!r.Succeeded)
                    throw new WatsonException(r.FormattedMessages);

                //  Set credentials from imported credntials
                Credential credential = vcapCredentials.VCAP_SERVICES["text_to_speech"][TestCredentialIndex].Credentials;
                _username = credential.Username.ToString();
                _password = credential.Password.ToString();
                _url = credential.Url.ToString();
            }
            catch
            {
                Log.Debug("TestTextToSpeech.RunTest()", "Failed to get credentials from VCAP_SERVICES file. Please configure credentials to run this test. For more information, see: https://github.com/watson-developer-cloud/unity-sdk/#authentication");
            }
            */
            //  Create credential and instantiate service
            //Credentials credentials = new Credentials(_username, _password, _url);
            Credentials credentials = new Credentials("b68f8093-13ad-494d-8c5b-0e5515a45d01", "jBN4EQuBVOeQ", "https://stream.watsonplatform.net/text-to-speech/api");
            //Credentials credentials = new Credentials("220b59fe-28ac-480f-a974-8d6b08d94b2e", "vXMbqAW7CbnT", "https://stream.watsonplatform.net/text-to-speech/api");
            //Credentials credentials = new Credentials("3ba42e96-671b-45b9-a7aa-bebccda83d9b", "3R4h51YPPNxa", "https://stream.watsonplatform.net/text-to-speech/api");

            //  Or authenticate using token
            //Credentials credentials = new Credentials(_url)
            //{
            //    AuthenticationToken = _token
            //};

            //  Credencial
            _textToSpeech = new TextToSpeech(credentials);

            //Synthesize
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting synthesize.");
            //  Idioma e voz
            _textToSpeech.Voice = VoiceType.pt_BR_Isabela;
            _textToSpeech.ToSpeech(HandleToSpeechCallback, OnFail, _testString, true);

            while (!_synthesizeTested)
                yield return null;
            /*
            //  Synthesize Conversation string
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting synthesize a string as returned by Watson Conversation.");
            _textToSpeech.Voice = VoiceType.en_US_Michael;
            _textToSpeech.ToSpeech(HandleConversationToSpeechCallback, OnFail, _testConversationString, true);
            while (!_synthesizeConversationTested)
                yield return null;

            //	Get Voices
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get voices.");
            _textToSpeech.GetVoices(OnGetVoices, OnFail);
            while (!_getVoicesTested)
                yield return null;

            //	Get Voice
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get voice {0}.", VoiceType.en_US_Allison);
            _textToSpeech.GetVoice(OnGetVoice, OnFail, VoiceType.en_US_Allison);
            while (!_getVoiceTested)
                yield return null;

            //	Get Pronunciation
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get pronunciation of {0}", _testWord);
            _textToSpeech.GetPronunciation(OnGetPronunciation, OnFail, _testWord, VoiceType.en_US_Allison);
            while (!_getPronuciationTested)
                yield return null;

            //  Get Customizations
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get a list of customizations");
            _textToSpeech.GetCustomizations(OnGetCustomizations, OnFail);
            while (!_getCustomizationsTested)
                yield return null;

            //  Create Customization
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to create a customization");
            _textToSpeech.CreateCustomization(OnCreateCustomization, OnFail, _customizationName, _customizationLanguage, _customizationDescription);
            while (!_createCustomizationTested)
                yield return null;

            //  Get Customization
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get a customization");
            if (!_textToSpeech.GetCustomization(OnGetCustomization, OnFail, _createdCustomizationId))
                Log.Debug("TestTextToSpeech.RunTest()", "Failed to get custom voice model!");
            while (!_getCustomizationTested)
                yield return null;

            //  Update Customization
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to update a customization");
            Word[] wordsToUpdateCustomization =
            {
                new Word()
                {
                    word = "hello",
                    translation = "hullo"
                },
                new Word()
                {
                    word = "goodbye",
                    translation = "gbye"
                },
                new Word()
                {
                    word = "hi",
                    translation = "ohioooo"
                }
            };

            _customVoiceUpdate = new CustomVoiceUpdate()
            {
                words = wordsToUpdateCustomization,
                description = "My updated description",
                name = "My updated name"
            };

            if (!_textToSpeech.UpdateCustomization(OnUpdateCustomization, OnFail, _createdCustomizationId, _customVoiceUpdate))
                Log.Debug("TestTextToSpeech.UpdateCustomization()", "Failed to update customization!");
            while (!_updateCustomizationTested)
                yield return null;

            //  Get Customization Words
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get a customization's words");
            string customIdentifierToGetWords = "1476ea80-5355-4911-ac99-ba39162a2d34";
            if (!_textToSpeech.GetCustomizationWords(OnGetCustomizationWords, OnFail, customIdentifierToGetWords))
                Log.Debug("TestTextToSpeech.GetCustomizationWords()", "Failed to get {0} words!", customIdentifierToGetWords);
            while (!_getCustomizationWordsTested)
                yield return null;

            //  Add Customization Words
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to add words to a customization");
            Word[] wordArrayToAddToCustomization =
            {
                new Word()
                {
                    word = "bananna",
                    translation = "arange"
                },
                new Word()
                {
                    word = "orange",
                    translation = "gbye"
                },
                new Word()
                {
                    word = "tomato",
                    translation = "tomahto"
                }
            };

            Words wordsToAddToCustomization = new Words()
            {
                words = wordArrayToAddToCustomization
            };

            if (!_textToSpeech.AddCustomizationWords(OnAddCustomizationWords, OnFail, _createdCustomizationId, wordsToAddToCustomization))
                Log.Debug("TestTextToSpeech.AddCustomizationWords()", "Failed to add words to {0}!", _createdCustomizationId);
            while (!_addCustomizationWordsTested)
                yield return null;

            //  Get Customization Word
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to get the translation of a custom voice model's word.");
            string customIdentifierWord = wordsToUpdateCustomization[0].word;
            if (!_textToSpeech.GetCustomizationWord(OnGetCustomizationWord, OnFail, _createdCustomizationId, customIdentifierWord))
                Log.Debug("TestTextToSpeech.GetCustomizationWord()", "Failed to get the translation of {0} from {1}!", customIdentifierWord, _createdCustomizationId);
            while (!_getCustomizationWordTested)
                yield return null;

            //  Delete Customization Word
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to delete customization word from custom voice model.");
            string wordToDelete = "goodbye";
            if (!_textToSpeech.DeleteCustomizationWord(OnDeleteCustomizationWord, OnFail, _createdCustomizationId, wordToDelete))
                Log.Debug("TestTextToSpeech.DeleteCustomizationWord()", "Failed to delete {0} from {1}!", wordToDelete, _createdCustomizationId);
            while (!_deleteCustomizationWordTested)
                yield return null;

            //  Delete Customization
            Log.Debug("TestTextToSpeech.RunTest()", "Attempting to delete a customization");
            if (!_textToSpeech.DeleteCustomization(OnDeleteCustomization, OnFail, _createdCustomizationId))
                Log.Debug("TestTextToSpeech.DeleteCustomization()", "Failed to delete custom voice model!");
            while (!_deleteCustomizationTested)
                yield return null;

            Log.Debug("TestTextToSpeech.RunTest()", "Text to Speech examples complete.");

            yield break;*/
        }

        void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData)
        {
            PlayClip(clip);
        }

        private void PlayClip(AudioClip clip)
        {
            if (Application.isPlaying && clip != null)
            {
                GameObject audioObject = new GameObject("AudioObject");
                AudioSource source = audioObject.AddComponent<AudioSource>();
                source.spatialBlend = 0.0f;
                source.loop = false;
                source.clip = clip;
                source.Play();

                GameObject.Destroy(audioObject, clip.length);

                _synthesizeTested = true;
            }
        }
        /*
        private void HandleConversationToSpeechCallback(AudioClip clip, Dictionary<string, object> customData)
        {
            if(clip != null) {
                _synthesizeConversationTested = true;
            }
        }

        private void OnGetVoices(Voices voices, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetVoices()", "{0}", customData["json"].ToString());
            Test(voices != null);
            _getVoicesTested = true;
        }

        private void OnGetVoice(Voice voice, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetVoice()", "{0}", customData["json"].ToString());
            Test(voice != null);
            _getVoiceTested = true;
        }

        private void OnGetPronunciation(Pronunciation pronunciation, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetPronunciation()", "{0}", customData["json"].ToString());
            Test(pronunciation != null);
            _getPronuciationTested = true;
        }

        private void OnGetCustomizations(Customizations customizations, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetCustomizations()", "{0}", customData["json"].ToString());
            Test(customizations != null);
            _getCustomizationsTested = true;
        }

        private void OnCreateCustomization(CustomizationID customizationID, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnCreateCustomization()", "{0}", customData["json"].ToString());
            _createdCustomizationId = customizationID.customization_id;
            Test(customizationID != null);
            _createCustomizationTested = true;
        }

        private void OnDeleteCustomization(bool success, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnDeleteCustomization()", "{0}", customData["json"].ToString());
            _createdCustomizationId = null;
            Test(success);
            _deleteCustomizationTested = true;
        }

        private void OnGetCustomization(Customization customization, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetCustomization()", "{0}", customData["json"].ToString());
            Test(customization != null);
            _getCustomizationTested = true;
        }

        private void OnUpdateCustomization(bool success, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnUpdateCustomization()", "{0}", customData["json"].ToString());
            Test(success);
            _updateCustomizationTested = true;
        }

        private void OnGetCustomizationWords(Words words, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetCustomizationWords()", "{0}", customData["json"].ToString());
            Test(words != null);
            _getCustomizationWordsTested = true;
        }

        private void OnAddCustomizationWords(bool success, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnAddCustomizationWords()", "{0}", customData["json"].ToString());
            Test(success);
            _addCustomizationWordsTested = true;
        }

        private void OnDeleteCustomizationWord(bool success, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnDeleteCustomizationWord()", "{0}", customData["json"].ToString());
            Test(success);
            _deleteCustomizationWordTested = true;
        }

        private void OnGetCustomizationWord(Translation translation, Dictionary<string, object> customData)
        {
            Log.Debug("TestTextToSpeech.OnGetCustomizationWord()", "{0}", customData["json"].ToString());
            Test(translation != null);
            _getCustomizationWordTested = true;
        }
        */
        private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
        {
            Log.Error("ExampleRetrieveAndRank.OnFail()", "Error received: {0}", error.ToString());
        }
    }
}
