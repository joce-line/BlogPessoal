using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for create a new theme</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class NewPostDTO
    {
        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; } 

        public string Photo { get; set; }

        [Required, StringLength(30)]
        public string EmailCreator { get; set; } 

        [Required]
        public string DescriptionTheme { get; set; } 

        public NewPostDTO(string title, string description, string photo, string emailCreator, string descriptionTheme)
        {
            Title = title;
            Description = description;
            Photo = photo;
            EmailCreator = emailCreator;
            DescriptionTheme = descriptionTheme;
        }
    }

    /// <summary>
    /// <para>Resume: Mirror class responsible for update a new post</para>
    /// <para>Criate by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>

    public class UpdatePostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public UpdatePostDTO(int id, string title, string description, string photo, string descriptionTheme)
        {
            Id = id;
            Title = title;
            Description = description;
            Photo = photo;
            DescriptionTheme = descriptionTheme;
        }
    }
}
