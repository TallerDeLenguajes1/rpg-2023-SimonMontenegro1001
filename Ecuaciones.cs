// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace EspacioApi
{
    public class Ecuaciones
    {
        public string operation { get; set; }
        public string expression { get; set; }
        public string result { get; set; }
    }

}