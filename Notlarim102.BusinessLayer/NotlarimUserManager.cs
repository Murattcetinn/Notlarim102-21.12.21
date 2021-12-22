using Notlarim102.Common.Helper;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using Notlarim102.Entity.Messages;
using Notlarim102.Entity.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class NotlarimUserManager
    {
        Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();
        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = ruser.Find(s => s.Username == data.Username || s.Email == data.Email);
            BusinessLayerResult<NotlarimUser> layerResult = new BusinessLayerResult<NotlarimUser>();



            if (user!= null)
            {
                if (user.Username==data.Username)
                {
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExist,"Kullanici adi daha once kaydedilmis.");
                }
                if (user.Email==data.Email)
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExist,"Email daha once kullanilmis.");
                }
                //throw new Exception("Bu bilgiler daha önce kullanilmis.");
            }
            else
            {
                DateTime now = DateTime.Now;
                int dbResult = ruser.Insert(new NotlarimUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive=false,
                    IsAdmin=false,
                    //Kapatilanlar repositoryde otomatik eklenecek sekilde duzenlenecektir.
                    //ModifiedOn=now,
                    //CreatedOn=now,
                    //ModifiedUsername="system"
                });
                if (dbResult>0)
                {
                    layerResult.Result = ruser.Find(s => s.Email == data.Email && s.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("siteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body = $"Merhaba{layerResult.Result.Username};<br><br> Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'> Tiklayin </a>.";
                    MailHelper.SendMail(body, layerResult.Result.Email, "Notlarim102 hesap aktiflestirme");
                }
                
            }
            return layerResult;
        }
        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giris kontrolunu
            //hesap aktif mi kontrolü

            //yonlendirme
            //sessiona kullanici bilgilerini gonderme
            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(s => s.Username == data.Username && s.Password == data.Password);
            if (res.Result!=null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive,"Kullanici aktiflestirilmemis.");
                    res.AddError(ErrorMessageCode.CheckYourEmail,"Lutfen Mailinizi kontrol edin");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong,"kullanici adi yada sifre yanlis");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> ActivateUser(Guid id)
        {
            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(x => x.ActivateGuid == id);
            if (res.Result!=null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "bu hesap daha once aktif edilmistir.");
                    return res;
                }
                res.Result.IsActive = true;
                ruser.Update(res.Result);

            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExist, "Ali Osman siteyi bi rahat birak.");
            }
            return res;
        }
    }
}
