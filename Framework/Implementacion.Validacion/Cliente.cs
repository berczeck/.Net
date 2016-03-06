using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Implementacion.Validacion
{
    public sealed  class Cliente
    {/// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required(ErrorMessageResourceType = typeof (Mensajes), ErrorMessageResourceName = "Cliente_FirstName_The_FirstName_is_a_mandatory_Field")]
        [StringLength(10, ErrorMessageResourceType = typeof (Mensajes), ErrorMessageResourceName = "Cliente_FirstName_The_FirstName_should_be_greater_than_10_characters_")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required(ErrorMessageResourceType = typeof (Mensajes), ErrorMessageResourceName = "Cliente_LastName_The_LastName_is_a_mandatory_Field")]
        [StringLength(10, ErrorMessageResourceType = typeof (Mensajes), ErrorMessageResourceName = "Cliente_LastName_The_LastName_should_be_greater_than_10_characters_")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required(ErrorMessageResourceType = typeof (Mensajes), ErrorMessageResourceName = "Cliente_Title_The_Title_is_a_mandatory_Field")]
        public string Title { get; set; }

        [Required]
        [Email(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
    }
}
