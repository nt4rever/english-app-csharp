using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.Api
{
    public class GoogleTranslate
    {
        public async Task<String[]> TranslateText(string sentence, string lang_in = "auto", string lang_out = "vi")
        {
            var arr = new String[2];
            await Task.Run(() =>
            {

                // Set the language from/to in the url (or pass it into this function)
                string url = String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", lang_in, lang_out, sentence);
                HttpClient httpClient = new();
                string result = httpClient.GetStringAsync(url).Result;

                // Get all json data
                var jsonData = JsonConvert.DeserializeObject<List<dynamic>>(result);

                // Extract just the first array element (This is the only data we are interested in)
                var translationItems = jsonData[0];
                // Translation Data
                string translation = "";

                // Loop through the collection extracting the translated objects
                try
                {
                    foreach (object item in translationItems)
                    {
                        // Convert the item array to IEnumerable
                        IEnumerable translationLineObject = item as IEnumerable;

                        // Convert the IEnumerable translationLineObject to a IEnumerator
                        IEnumerator translationLineString = translationLineObject.GetEnumerator();

                        // Get first object in IEnumerator
                        translationLineString.MoveNext();

                        // Save its value (translated text)
                        translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
                    }

                    // Remove first blank character
                    if (translation.Length > 1) { translation = translation.Substring(1); };

                    arr[0] = translation;
                    arr[1] = jsonData[2];
                }
                catch
                {
                    arr[0] = "";
                    arr[1] = "";
                }
                // Return translation
            });
            return arr;
        }

        public void PlayMp3FromUrl(string url)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }
    }
}
