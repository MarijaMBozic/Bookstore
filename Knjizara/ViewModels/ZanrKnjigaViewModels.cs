using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.ViewModels
{
    public class ZanrKnjigaViewModels
    {
        public Book Book { get; set; }

        public List<Genre> Zanrovi { get; set; }

        public int SelectedGenreId { get; set; }
    }
}