using System.Web.Http;
using Microsoft.Practices.Unity;
using WF.EAI..BO.CMT.AltOffers;
using WF.EAI.Model.DTO.CMT.AltOffers;
using xxxxProjec....SF.App.Host.WebApi.Contract;
using xxxxProjec....SF.App.Host.WebApi.Contract.Interface;

namespace xxxxProjec.....xxxxProjec.....WebApi.Controllers
{
    public class AltOffersController : ApiController
    {
        #region Fields

        private IResponseDto<ViewModel, DataModel> _responseDto;
        private DataModel _dataModel=new DataModel();

        #endregion

        #region Dependency

        /// <summary>
        /// IResponseDto
        /// </summary>
        [Dependency]
        public IResponseDto<ViewModel, DataModel> ResponseDto
        {
            get
            {
                if (_responseDto == null)
                {
                    _responseDto = new ResponseDto<ViewModel, DataModel>();
                }
                return _responseDto;
            }
            set
            {
                _responseDto = value;
            }
        }

        #endregion

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
         [HttpPost]
        public IResponseDto<ViewModel, DataModel> Post(RequestDto<AltOffersRequestHeader> request)
        {
            var bo = new AltOffersBO();
            this.ResponseDto.ViewModel= bo.Invoke(request);
            this.ResponseDto.DataModel = _dataModel;
            return this.ResponseDto;
        }
    }
}
