using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.Shared.Enums
{
    public enum DifficultyLevel
    {
        [Display(Name = "Hard")]
        Hard = 1,
        [Display(Name = "Medium")]
        Medium = 2,
        [Display(Name = "Easy")]
        Easy = 3,
    }
}
