using Newtonsoft.Json;

namespace KenshiRandomizer.Models
{
    public class DataToRandomize
    {
        public List<string> Starts { get; set; }
        public string PlusStartsLink { get; set; }
        public List<string> PlusStarts { get; set; }
        public List<string> BaseLocations { get; set; }
        public Dictionary<string, List<string>> Goals { get; set; }
        public Dictionary<string, List<string>> Restrictions { get; set; }

        public static string GetJsonData()
        {
            using (StreamReader r = new StreamReader("Models/DataToRandomize.json"))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }

        public static DataToRandomize ReadJson(string json)
        {
            DataToRandomize data = JsonConvert.DeserializeObject<DataToRandomize>(json);
            return data;
        }

        public List<string> RandomizeSingle(string name)
        {
            var rnd = new Random();
            var list = GetList(name);
            var dictionary = GetDictionary(name);

            if (list != null)
            {
                var randomFromList = new List<string> { list[rnd.Next(0, list.Count)] };
                return randomFromList;
            }

            var randomFromDict = dictionary.ElementAt(rnd.Next(0, dictionary.Count));
            if (randomFromDict.Value.Count > 1)
            {
                var randomModifier = randomFromDict.Value.ElementAt(rnd.Next(1, randomFromDict.Value.Count));
                var text = randomFromDict.Value.FirstOrDefault().Replace("0", randomModifier);

                return new List<string> { randomFromDict.Key, text };
            }

            var randomValue = randomFromDict.Value.ElementAt(rnd.Next(0, randomFromDict.Value.Count));  

            return new List<string> { randomFromDict.Key, randomValue };
        }

        public List<string> GetList(string name)
        {
            switch (name)
            {
                case "Starts":
                    return Starts;

                case "PlusStarts":
                    return PlusStarts;

                case "BaseLocations":
                    return BaseLocations;
            }

            return null;
        }

        public Dictionary<string, List<string>> GetDictionary(string name)
        {
            if (name == "Goals")
                return Goals;

            if (name == "Restrictions")
                return Restrictions;

            return null;
        }
    }
}
