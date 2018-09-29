using System.Collections.Generic;

namespace AspNetVS2017.Capitulo03.Mvc.Portfolio.Models
{
    public class PortfolioViewModel
    {
        //public PortfolioViewModel()
        //{
        //    CaminhosImagens = new List<string>();
        //}

        public List<string> CaminhosImagens { get; set; } = new List<string>();
    }
}