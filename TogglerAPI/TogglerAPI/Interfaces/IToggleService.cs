using System.Collections.Generic;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Interfaces
{
    public interface IToggleService
    {
        int CreateToggle(ToggleRequestModel toggleRequestModel);
        bool DeleteToggle(int id);
        ToggleResponseModel GetToggle(int id);
        List<ToggleResponseModel> GetToggleList();
        bool UpdateToggle(ToggleRequestModel toggleRequestModel);
    }
}
