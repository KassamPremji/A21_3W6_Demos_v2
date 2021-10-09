using CrazyBooks_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBooks_Models.ViewModels
{
  public class AuthorVM
  {
    public Author Author { get; set; }
    public Author AuthorDetail { get; set; }

    }
}
