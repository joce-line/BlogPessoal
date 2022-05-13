using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
    /// <summary>
    /// <para>Resume: Enum responsable to define users types on the system</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        NORMAL,
        ADMINISTRATOR
    }


}
