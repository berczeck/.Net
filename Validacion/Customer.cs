using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Validacion
{
    public sealed class Customer
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required(ErrorMessage = "The FirstName is a mandatory Field")]
        [StringLength(10, ErrorMessage =
        "The FirstName should be greater than 10 characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required(ErrorMessage = "The LastName is a mandatory Field")]
        [StringLength(10, ErrorMessage =
        "The LastName should be greater than 10 characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required(ErrorMessage = "The Title is a mandatory Field")]
        public string Title { get; set; }
    }
}
