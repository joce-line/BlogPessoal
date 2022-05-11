using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for create a new theme</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class AddThemeDTO
    {
        
        [Required, StringLength(20)]
        public string Description { get; set; }

        public AddThemeDTO(string description)
        {
            Description = description;
        }
    }

    /// <summary>
    /// <para>Resume: Mirror class responsible for update a new theme</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class UpdateThemeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }
        public UpdateThemeDTO(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
