using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102.WebApp.ViewModel
{
    public class OkViewModel:NotifyViewModelBase<String>
    {
        public OkViewModel()
        {
            Title = "Islem Basarili";
        }
    }
}